USE [RederConsignado]
GO



IF OBJECT_ID('dbo.Suplemento', 'U') IS NOT NULL DROP TABLE Suplemento;
IF OBJECT_ID('dbo.SuplementoProduto', 'U') IS NOT NULL DROP TABLE SuplementoProduto;
IF OBJECT_ID('dbo.Receber', 'U') IS NOT NULL DROP TABLE Receber;
IF OBJECT_ID('dbo.ReceberBaixa', 'U') IS NOT NULL DROP TABLE ReceberBaixa;
IF OBJECT_ID('dbo.Estoque', 'U') IS NOT NULL DROP TABLE Estoque;


CREATE TABLE [dbo].[Suplemento] (
    [Id]           INT        NOT NULL IDENTITY(1,1),
    [Data]       DATE      NULL,
    [Situacao]   NCHAR (1) NULL,
    [Observacao] NTEXT     NULL,
	[temp_old_id]		  INT, 
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[SuplementoProduto] (
    [Id]           INT        NOT NULL IDENTITY(1,1),
    [SuplementoId]   INT        NULL,
    [ProdutoGradeId] INT        NULL,
    [Quantidade]     FLOAT (53) NULL,
	[temp_old_id]		  INT, 
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SuplementoProduto_ProdutoGrade] FOREIGN KEY ([ProdutoGradeId]) REFERENCES [dbo].[ProdutoGrade] ([Id]),
    CONSTRAINT [FK_SuplementoProduto_Suplemento] FOREIGN KEY ([SuplementoId]) REFERENCES [dbo].[Suplemento] ([Id])
);



CREATE TABLE [dbo].[Receber] (
    [Id]           INT        NOT NULL IDENTITY(1,1),
    [VendedorId]     INT        NULL,
    [CargaId]        INT        NULL,
    [Documento]      INT        NULL,
    [Serie]          NCHAR (5)  NULL,
    [ValorNF]        FLOAT (53) NULL,
    [ValorDuplicata] FLOAT (53) NULL,
    [ValorAReceber]  FLOAT (53) NULL,
    [ValorJuros]     FLOAT (53) NULL,
    [ValorDesconto]  FLOAT (53) NULL,
    [DataEmissao]    DATE       NULL,
    [DataLancamento] DATE       NULL,
    [DataVencimento] DATE       NULL,
    [DataPagamento]  DATE       NULL,
    [Observacoes]    NTEXT      NULL,
    [Status]         NCHAR (1)  NULL,
	[temp_old_id]		  INT, 
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Receber_Vendedor] FOREIGN KEY ([VendedorId]) REFERENCES [dbo].[Vendedor] ([Id]),
    CONSTRAINT [FK_Receber_Carga] FOREIGN KEY ([CargaId]) REFERENCES [dbo].[Carga] ([Id])
);


INSERT INTO Receber (VendedorID, CargaId, Documento, Serie,
ValorNF, ValorDuplicata, ValorAReceber, ValorJuros, ValorDesconto,
DataEmissao, DataLancamento, DataVencimento, DataPagamento,
Observacoes, Status, temp_old_id)
SELECT RVD.Id, RCG.Id, rec_docu, rec_seri, 
rec_vrnf, rec_vrdp, rec_vrrc, rec_juro, rec_desc,
rec_emis, rec_lcto, rec_vcto, rec_pgto,
rec_obse, rec_stat, rec_codi FROM BancoCon.dbo.receber BRC
INNER JOIN Vendedor RVD ON BRC.cli_codi = RVD.temp_old_id
INNER JOIN Carga RCG ON BRC.via_codi = RCG.temp_old_id


CREATE TABLE [dbo].[ReceberBaixa] (
    [Id]           INT        NOT NULL IDENTITY(1,1),
    [ReceberId]     INT        NULL,
    [CargaId]       INT        NULL,
    [Valor]         FLOAT (53) NULL,
    [DataPagamento] DATE       NULL,
    [DataBaixa]     DATE       NULL,
    [Juros]         FLOAT (53) NULL,
    [Desconto]      FLOAT (53) NULL,
	[temp_old_id]		  INT, 
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ReceberBaixa_Receber] FOREIGN KEY ([ReceberId]) REFERENCES [dbo].[Receber] ([Id]),
    CONSTRAINT [FK_ReceberBaixa_Carga] FOREIGN KEY ([CargaId]) REFERENCES [dbo].[Carga] ([Id])
);

INSERT INTO ReceberBaixa(ReceberId, CargaId, Valor, DataPagamento, DataBaixa, Juros, Desconto, temp_old_id) 
SELECT RRC.Id, RCG.Id, rpa_valo, rpa_pgto, rpa_dtbx, rpa_juro, rpa_desc, rpa_regi  FROM BancoCon.dbo.receberparcial BRP
INNER JOIN Receber RRC ON BRP.rec_codi = RRC.temp_old_id
INNER JOIN Carga RCG ON BRP.via_codi = RCG.temp_old_id


CREATE TABLE [dbo].[Estoque] (
    [Id]               INT        NOT NULL,
    [ProdutoId]        INT        NULL,
    [TipoMovimentacao] NCHAR (1)  NULL,
    [Quantidade]       FLOAT (53) NULL,
    [Observacao]       NTEXT      NULL,
	[temp_old_id]		  INT, 
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Estoque_Produto] FOREIGN KEY ([ProdutoId]) REFERENCES [dbo].[Produto] ([Id])
);

