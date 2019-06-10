USE [RederConsignado]
GO


DROP TABLE CargaProduto;
DROP TABLE ProdutoGrade;
DROP TABLE Produto;
DROP TABLE Fornecedor;
DROP TABLE Categoria;
DROP TABLE Carga;
DROP TABLE Usuario;
DROP TABLE Praca;
DROP VIEW Representante;
DROP VIEW ProdutosCarga;

GO

CREATE TABLE [dbo].[Usuario] (
    [Id]                INT            NOT NULL IDENTITY (1,1),
    [Nome]              NCHAR (55)     NULL,
    [Login]             NCHAR (20)     NOT NULL,
    [Senha]             NVARCHAR (MAX) NOT NULL,
    [TipoModulo]        NCHAR (2)      NULL,
    [Endereco]          NCHAR (155)    NULL,
    [Complemento]       NCHAR (30)     NULL,
    [Bairro]            NCHAR (45)     NULL,
    [Cidade]            NCHAR (65)     NULL,
    [UF]                NCHAR (2)      NULL,
    [Cep]               NCHAR (9)      NULL,
    [TipoPessoa]        NCHAR (2)      NULL,
    [CpfCnpj]           NCHAR (18)     NULL,
    [RGInscricao]       NCHAR (45)     NULL,
    [Telefone]          NCHAR (15)     NULL,
    [TelefoneComercial] NCHAR (15)     NULL,
    [Celular]           NCHAR (15)     NULL,
    [Email]             NCHAR (60)     NULL,
    [DataInicial]       DATE           NULL,
    [DataFinal]         DATE           NULL,
    [Comissao]          FLOAT (53)     NULL,
    [Observacao]        TEXT           NULL,
	[temp_old_id]		INT,	
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE VIEW Representante AS SELECT * FROM Usuario WHERE TipoModulo = 'RP';


GO
UPDATE BancoCon.dbo.vendedor SET ven_nome = 'VANDERLEI SILVA' WHERE ven_codi = 1;


GO
INSERT INTO Usuario 
	(Nome, Login, Senha, TipoModulo, Endereco, Bairro, Cep, Cidade, 
	Telefone, Celular, TelefoneComercial, TipoPessoa, CpfCnpj, RGInscricao, 
	DataInicial, Observacao, Email, Comissao, temp_old_id)
SELECT ven_nome, lower(trim(LEFT(dbo.RemoveExtraChars(ven_nome), CHARINDEX(' ', ven_nome)-1)) + '.' + trim(RIGHT(dbo.RemoveExtraChars(ven_nome), CHARINDEX(' ', REVERSE(ven_nome))-1))) ven_login, 'padrao123' ven_senha,
	'RP' tipomodulo, ven_ende, ven_bair, ven_cepp, 'Guararapes' ven_cidade, ven_fone, ven_celu, ven_faxx,
	'PJ' tipopessoa, ven_docu, ven_inrg, ven_dtin, ven_obse, ven_mail, ven_pcom, ven_codi
FROM BancoCon.dbo.vendedor;

GO
INSERT INTO Usuario 
	(Nome, Login, Senha, TipoModulo, DataInicial, temp_old_id)
SELECT usu_nome, usu_logi, usu_senh, 'DP', GETDATE(), usu_codi FROM BancoCon.dbo.Usuario;

GO
CREATE TABLE [dbo].[Praca] (
    [Id]        INT        NOT NULL IDENTITY(1,1),
    [Descricao] NCHAR (50) NULL,
	[temp_old_id] INT,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
INSERT INTO Praca (Descricao, temp_old_id)
select reg_nome, reg_codi from BancoCon.dbo.regiao;


GO
CREATE TABLE [dbo].[Carga] (
    [Id]              INT       NOT NULL IDENTITY (1,1),
    [PracaId]         INT       NULL,
    [RepresentanteId] INT       NULL,
    [Ano]             INT       NULL,
    [Mes]             INT       NULL,
    [DataAbertura]    DATE      NULL,
    [DataExportacao]  DATE      NULL,
    [DataRetorno]     DATE      NULL,
    [DataConferencia] DATE      NULL,
    [DataFinalizacao] DATE      NULL,
    [Status]          NCHAR (1) NULL,
	[temp_old_id]	  INT,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Carga_Praca] FOREIGN KEY ([PracaId]) REFERENCES [dbo].[Praca] ([Id]),
    CONSTRAINT [FK_Carga_Representante] FOREIGN KEY ([RepresentanteId]) REFERENCES [dbo].[Usuario] ([Id])
);


GO
INSERT INTO Carga (PracaId, RepresentanteId, Ano, Mes, DataAbertura, 
	DataExportacao, DataRetorno, DataConferencia, DataFinalizacao, Status, temp_old_id)
SELECT Praca.Id, Usuario.Id, via_anoo, via_mess, via_dtab, via_dtex, via_dtre, via_dtcr, via_dtfi, via_situ, via_codi FROM BancoCon.DBO.viagem 
	INNER JOIN Praca ON reg_codi = Praca.temp_old_id
	INNER JOIN Usuario ON ven_codi = Usuario.temp_old_id AND TipoModulo = 'RP'

GO
CREATE TABLE [dbo].[Categoria] (
    [Id]        INT        NOT NULL IDENTITY(1,1),
    [Descricao] NCHAR (20) NULL,
	[temp_old_id] INT,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
INSERT INTO Categoria (temp_old_id, Descricao)
	SELECT grp_codi, grp_nome FROM BancoCon.dbo.grupoproduto


GO	
CREATE TABLE [dbo].[Fornecedor] (
    [Id]                INT         NOT NULL IDENTITY(1,1),
    [NomeFantasia]      NCHAR (155) NULL,
    [RazaoSocial]       NCHAR (155) NULL,
    [Endereco]          NCHAR (155) NULL,
    [Complemento]       NCHAR (20)  NULL,
    [Bairro]            NCHAR (45)  NULL,
    [Cidade]            NCHAR (65)  NULL,
    [UF]                NCHAR (2)   NULL,
    [Cep]               NCHAR (9)   NULL,
    [Telefone]          NCHAR (15)  NULL,
    [TelefoneComercial] NCHAR (15)  NULL,
    [Celular]           NCHAR (15)  NULL,
    [Email]             NCHAR (60)  NULL,
    [Website]           NCHAR (60)  NULL,
    [TipoPessoa]        NCHAR (2)   NULL,
    [CpfCnpj]           NCHAR (18)  NULL,
    [RGInscricao]       NCHAR (45)  NULL,
    [DataCadastro]      DATE        NULL,
    [Status]            NCHAR (1)   NULL,
    [Observacao]        NTEXT       NULL,
	[temp_old_id]		INT,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
INSERT INTO Fornecedor (NomeFantasia, temp_old_id) VALUES ('NÃO ESPECIFICADO', 0);


GO
INSERT INTO Fornecedor (NomeFantasia, RazaoSocial, Endereco, 
	Complemento, Bairro, Cidade, UF, Cep, Telefone, TelefoneComercial, 
	Celular, Email, Website, TipoPessoa, CpfCnpj, RGInscricao, DataCadastro, Status, temp_old_id)
SELECT for_fant, for_razs, for_tlog + for_ende + for_nume endereco, 
		for_comp, for_bair, 'Guararapes', 'SP', for_cepp, for_fone, for_faxx,
		for_celu, for_emai, for_wwww, CASE WHEN for_tdoc = 1 THEN 'PJ' ELSE 'PF' END tipopessoa ,
		for_doct, for_ierg, for_dtca, for_situ, for_codi
	FROM BancoCon.dbo.fornecedor;

GO
CREATE TABLE [dbo].[Produto] (
    [Id]           INT        NOT NULL IDENTITY(1,1),
    [Descricao]    NCHAR (50) NOT NULL,
    [CategoriaId]  INT        NULL,
    [FornecedorId] INT        NULL,	
    [Unidade]      NCHAR (10) NULL,
    [CodigoBarras] NCHAR (12) NULL,
    [Digito]       NCHAR (1)  NULL,
    [Quantidade]   FLOAT (53) NULL,
    [Status]       NCHAR (10) NULL,
	[temp_old_id]  INT, 
	CONSTRAINT [FK_Produto_Categoria] FOREIGN KEY ([CategoriaId]) REFERENCES [dbo].[Categoria] ([Id]),
	CONSTRAINT [FK_Produto_Fornecedor] FOREIGN KEY ([FornecedorId]) REFERENCES [dbo].[Fornecedor] ([Id]),
    PRIMARY KEY CLUSTERED ([Id] ASC),
);


GO
INSERT INTO Produto (Descricao, CategoriaId, FornecedorId, Unidade, CodigoBarras, Digito, temp_old_id)
SELECT pro_nome, Categoria.Id, Fornecedor.Id, pro_unid, pro_barr, pro_digi, pro_codi FROM 
	BancoCon.dbo.produto
	INNER JOIN Categoria ON grp_codi = Categoria.temp_old_id
	INNER JOIN Fornecedor ON for_codi = Fornecedor.temp_old_id;


GO
INSERT INTO Produto (Descricao, CategoriaId, Unidade, CodigoBarras, Digito, temp_old_id)
SELECT pro_nome, Categoria.Id, pro_unid, pro_barr, pro_digi, pro_codi FROM 
	BancoCon.dbo.produto
	INNER JOIN Categoria ON grp_codi = Categoria.temp_old_id
WHERE for_codi IS NULL;

GO
CREATE TABLE [dbo].[ProdutoGrade] (
    [Id]           INT        NOT NULL IDENTITY (1,1),
    [ProdutoId]    INT        NULL,	
    [Tamanho]      NCHAR (3)  NULL,
    [Cor]          NCHAR (3)  NULL,
    [ValorSaida]   FLOAT (53) NULL,
    [DataInicial]  DATE       NULL,
    [DataFinal]    DATE       NULL,
    [PesoLiquido]  FLOAT (53) NULL,
    [PesoBruto]    FLOAT (53) NULL,
    [CodigoBarras] NCHAR (12) NULL,
    [Digito]       NCHAR (1)  NULL,
    [ValorCusto]   FLOAT (53) NULL,
    [Quantidade]   FLOAT (53) NULL,
    [Status]       NCHAR (1)  NULL,
	[temp_old_id]  INT, 
	[temp_old_proid] INT,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProdutoGrade_Produto] FOREIGN KEY ([ProdutoId]) REFERENCES [dbo].[Produto] ([Id])
);


GO
--4967 rows
UPDATE ProdutoGrade SET ProdutoGrade.CodigoBarras = P.CodigoBarras
FROM Produto P, ProdutoGrade PG WHERE PG.ProdutoId = P.Id


GO
INSERT INTO ProdutoGrade (ProdutoId, Tamanho, Cor, ValorSaida, DataInicial, 
	DataFinal, PesoLiquido, PesoBruto, CodigoBarras, Digito, ValorCusto, temp_old_id, temp_old_proid) 
SELECT Produto.Id,pgr_tama, cor_codi, pgr_vsai, pgr_dtin, 
		pgr_dtfi, pgr_pliq, pgr_pliq, pgr_codi, pgr_digi, pgr_vcus, pgr_codi, pro_codi FROM BancoCon.dbo.produtograde
INNER JOIN Produto ON pro_codi = Produto.temp_old_id;


GO
CREATE TABLE [dbo].[CargaProduto] (
    [Id]                INT        NOT NULL IDENTITY (1,1),
    [CargaId]           INT        NULL,
    [ProdutoGradeId]    INT        NOT NULL,
    [Quantidade]        FLOAT (53) NULL,
    [QuantidadeRetorno] FLOAT (53) NULL,
	[temp_old_id]  INT, 
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CargaProduto_ProdutoGrade] FOREIGN KEY ([ProdutoGradeId]) REFERENCES [dbo].[ProdutoGrade] ([Id]),
    CONSTRAINT [FK_CargaProduto_Carga] FOREIGN KEY ([CargaId]) REFERENCES [dbo].[Carga] ([Id])
);

GO
---457925 rows
INSERT INTO CargaProduto (CargaId, ProdutoGradeId, Quantidade, QuantidadeRetorno, temp_old_id)
SELECT Carga.Id, ProdutoGrade.Id, vit_qtde, vit_qtrt, vit_codi FROM BancoCon.dbo.viagempro
	INNER JOIN Carga ON via_codi = Carga.temp_old_id
	INNER JOIN ProdutoGrade ON pgr_codi = ProdutoGrade.temp_old_id AND pro_codi = ProdutoGrade.temp_old_proid;


GO
CREATE VIEW ProdutosCarga AS 
SELECT 
	Produto.CodigoBarras + ProdutoGrade.Digito CodigoBarras,
	Produto.Descricao,
	Cor,
	Tamanho,
	CargaProduto.Quantidade,
	QuantidadeRetorno,
	ValorSaida,
	ValorCusto,
	Produto.Id ProdutoId,
	CargaProduto.Id CargaProdutoId,
	Carga.Id CargaId,
	ProdutoGrade.Id ProdutoGradeId,
	PracaId,
	RepresentanteId,
	Ano,
	Mes
FROM Carga 
	INNER JOIN CargaProduto ON CargaProduto.CargaId = Carga.Id
	INNER JOIN ProdutoGrade ON ProdutoGrade.Id = ProdutoGradeId
	INNER JOIN Produto ON Produto.Id = ProdutoGrade.ProdutoId;


GO	
CREATE VIEW Totalizadores
AS SELECT CargaId, sum(CargaProduto.Quantidade) QtdProdutos, sum(ValorSaida) TotalProdutos  FROM CargaProduto INNER JOIN ProdutoGrade ON ProdutoGradeId = ProdutoGrade.Id 
GROUP BY CargaId;

