USE [RederConsignado]
GO


IF OBJECT_ID('dbo.Vendedor', 'U') IS NOT NULL DROP TABLE Vendedor;
IF OBJECT_ID('dbo.Pedido', 'U') IS NOT NULL DROP TABLE Pedido;
IF OBJECT_ID('dbo.PedidoItem', 'U') IS NOT NULL DROP TABLE PedidoItem;



GO

CREATE TABLE [dbo].[Vendedor] (
    [Id]           INT        NOT NULL IDENTITY(1,1),
    [Nome]              NCHAR (55)  NULL,
    [RazaoSocial]       NCHAR (55)  NULL,
    [Endereco]          NCHAR (155) NULL,
    [Complemento]       NCHAR (50)  NULL,
    [Bairro]            NCHAR (45)  NULL,
    [Cidade]            NCHAR (65)  NULL,
    [UF]                NCHAR (2)   NULL,
    [Cep]               NCHAR (9)   NULL,
    [TipoPessoa]        NCHAR (2)   NULL,
    [CpfCnpj]           NCHAR (18)  NULL,
    [RGInscricao]       NCHAR (45)  NULL,
    [DataNascimento]    DATE        NULL,
    [Telefone]          NCHAR (15)  NULL,
    [TelefoneComercial] NCHAR (15)  NULL,
    [Celular]           NCHAR (15)  NULL,
    [Email]             NCHAR (60)  NULL,
    [DataInicial]       DATE        NULL,
    [DataFinal]         DATE        NULL,
    [LimitePedido]      FLOAT (53)  NULL,
    [LimiteCredito]     FLOAT (53)  NULL,
    [Status]            NCHAR (1)   NULL,
    [Observacao]        TEXT        NULL,
	[temp_old_id]		  INT, 
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO

INSERT INTO Vendedor (Nome, RazaoSocial, Endereco,
Complemento, Bairro, Cidade, UF, Cep, TipoPessoa, CpfCnpj,
RGInscricao, DataNascimento, Telefone, TelefoneComercial,
Celular, Email, DataInicial, DataFinal, LimitePedido, LimiteCredito,
Status, Observacao, temp_old_id) 
SELECT cli_nome, cli_rzso, cli_ende, 
cli_comp, cli_bair, CID.cid_nome, CID.cid_esta, cli_cepp, CASE WHEN cli_fiju = 1 THEN 'PF' ELSE 'PJ' END tipopessoa, cli_cjcf,
cli_inrg, cli_dtna, cli_fone, cli_faxx, cli_celu, cli_mail, cli_dtin, cli_dtfi, cli_lped, cli_limi,
cli_situ, cli_obs1, cli_codi
FROM BancoCon.DBO.cliente CLI
LEFT JOIN BancoCon.dbo.cidade CID on CLI.cid_codi = CID.cid_codi;


GO


CREATE TABLE [dbo].[Pedido] (
    [Id]           INT        NOT NULL IDENTITY(1,1),
    [VendedorId]          INT        NOT NULL,
    [CargaId]             INT        NOT NULL,
    [CodigoPedido]        NCHAR (12) NULL,
    [DataLancamento]      DATE       NULL,
    [DataPrevisaoRetorno] DATE       NULL,
    [DataRetorno]         DATE       NULL,
    [ValorPedido]         FLOAT (53) NULL,
    [ValorCompra]         FLOAT (53) NULL,
    [PercentualCompra]    FLOAT (53) NULL,
	[FaixaComissao]		  FLOAT (53) NULL,
	[PercentualFaixa]	  FLOAT (53) NULL,
    [ValorComissao]       FLOAT (53) NULL,	
    [ValorLiquido]        FLOAT (53) NULL,
    [RecebidoAnterior]    FLOAT (53) NULL,    
    [ValorAcerto]         FLOAT (53) NULL,
	[QuantidadeRetorno]	  INT NULL,
	[Remarcado]			  INT NULL,
    [Status]              NCHAR (10) NULL,
	[temp_old_id]		  INT, 
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Pedido_Vendedor] FOREIGN KEY ([VendedorId]) REFERENCES [dbo].[Vendedor] ([Id]),
    CONSTRAINT [FK_Pedido_Carga] FOREIGN KEY ([CargaId]) REFERENCES [dbo].[Carga] ([Id])
);

---////38883 rows
INSERT INTO Pedido (VendedorId, CargaId, CodigoPedido,
DataLancamento, DataPrevisaoRetorno, DataRetorno,
ValorPedido,ValorCompra,PercentualCompra,FaixaComissao, PercentualFaixa, ValorComissao,
ValorLiquido,RecebidoAnterior,ValorAcerto, QuantidadeRetorno, Remarcado,
Status,temp_old_id)
SELECT Vendedor.Id, Carga.Id, ped_codp,
ped_dtlc, ped_dtpr, ped_dtrt,
ped_tota, ped_comp, ped_pcom,
ped_fcom, ped_comi, ped_vcom,
ped_vliq, ped_rcan, ped_acer,
ped_qtre, ped_prem,
ped_situ, ped_codi FROM BancoCon.dbo.pedido
INNER JOIN Vendedor ON cli_codi = Vendedor.temp_old_id
INNER JOIN Carga ON via_codi = Carga.temp_old_id


GO


CREATE TABLE [dbo].[PedidoItem] (
    [Id]           INT        NOT NULL IDENTITY(1,1),
    [PedidoId]       INT        NOT NULL,
    [ProdutoGradeId] INT        NOT NULL,
    [Quantidade]     FLOAT (53) NULL,
    [Retorno]        FLOAT (53) NULL,
    [Preco]          FLOAT (53) NULL,
	[temp_old_id]		  INT, 
    PRIMARY KEY CLUSTERED ([Id] ASC),	
    CONSTRAINT [FK_PedidoItem_Pedido] FOREIGN KEY ([PedidoId]) REFERENCES [dbo].[Pedido] ([Id]),
    CONSTRAINT [FK_PedidoItem_ProdutoGrade] FOREIGN KEY ([ProdutoGradeId]) REFERENCES [dbo].[ProdutoGrade] ([Id])
);


--DROP TABLE #TEMP_PRODUTO
SELECT PG.temp_old_id PGID, PD.temp_old_id PDID, PG.Id ProdutoGradeId INTO #TEMP_PRODUTO FROM ProdutoGrade PG INNER JOIN Produto PD ON PG.ProdutoId = PD.Id

---/// 3326179 rows
INSERT INTO PedidoItem (PedidoId, ProdutoGradeId, Quantidade, Retorno, Preco, temp_old_id)
SELECT Pedido.Id, #TEMP_PRODUTO.ProdutoGradeId, ite_qtde, ite_qtdr, ite_prev, ite_codi  FROM BancoCon.dbo.pedidoitem
INNER JOIN #TEMP_PRODUTO ON pro_codi = PDID AND pgr_codi = PGID
INNER JOIN Pedido ON Pedido.temp_old_id = ped_codi


