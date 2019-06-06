CREATE TABLE [RepUsuario] ( 
  [Id] INTEGER PRIMARY KEY NOT NULL 
, [Nome] text NOT NULL 
, [Login] text NOT NULL 
, [Senha] text NOT NULL 
, [TipoModulo] text NULL 
, [Endereco] text NULL 
, [Complemento] text NULL 
, [Bairro] text NULL 
, [Cidade] text NULL 
, [UF] text NULL 
, [Cep] text NULL 
, [TipoPessoa] text NULL 
, [CpfCnpj] text NULL 
, [RGInscricao] text NULL 
, [Telefone] text NULL 
, [TelefoneComercial] text NULL 
, [Celular] text NULL 
, [Email] text NULL 
, [DataInicial] date NULL 
, [DataFinal] date NULL 
, [Comissao] numeric(53,0) NULL 
, [Observacao] text NULL 
);


CREATE VIEW RepRepresentante AS SELECT Id, Nome FROM "RepUsuario" WHERE TipoModulo = 'RP';


CREATE TABLE [RepPraca] ( 
  [Id] INTEGER PRIMARY KEY NOT NULL 
, [Descricao] text NOT NULL 
);


CREATE TABLE [RepCarga] ( 
  [Id] INTEGER PRIMARY KEY NOT NULL 
, [PracaId] bigint NULL 
, [RepresentanteId] bigint NULL 
, [Ano] bigint NULL 
, [Mes] bigint NULL 
, [DataAbertura] date NULL 
, [DataExportacao] date NULL 
, [DataRetorno] date NULL 
, [DataConferencia] date NULL 
, [DataFinalizacao] date NULL 
, [Status] text NULL 
);


CREATE TABLE [RepCategoria] ( 
  [Id] INTEGER PRIMARY KEY NOT NULL 
, [Descricao] text NULL 
);


CREATE TABLE [RepFornecedor] ( 
  [Id] INTEGER PRIMARY KEY NOT NULL 
, [NomeFantasia] text NULL 
, [RazaoSocial] text NULL 
, [Endereco] text NULL 
, [Complemento] text NULL 
, [Bairro] text NULL 
, [Cidade] text NULL 
, [UF] text NULL 
, [Cep] text NULL 
, [Telefone] text NULL 
, [TelefoneComercial] text NULL 
, [Celular] text NULL 
, [Email] text NULL 
, [Website] text NULL 
, [TipoPessoa] text NULL 
, [CpfCnpj] text NULL 
, [RGInscricao] text NULL 
, [DataCadastro] date NULL 
, [Status] text NULL 
, [Observacao] text NULL 
);

CREATE TABLE [RepProduto] ( 
  [Id] INTEGER PRIMARY KEY NOT NULL 
, [Descricao] text NOT NULL 
, [CategoriaId] bigint NULL 
, [FornecedorId] bigint NULL 
, [Unidade] text NULL 
, [CodigoBarras] text NULL 
, [Digito] text NULL 
, [Quantidade] numeric(53,0) NULL 
, [Status] text NULL 
);


CREATE TABLE [RepProdutoGrade] ( 
  [Id] INTEGER PRIMARY KEY NOT NULL 
, [ProdutoId] bigint NULL 
, [Tamanho] text NULL 
, [Cor] text NULL 
, [ValorSaida] numeric(53,0) NULL 
, [DataInicial] date NULL 
, [DataFinal] date NULL 
, [PesoLiquido] numeric(53,0) NULL 
, [PesoBruto] numeric(53,0) NULL 
, [CodigoBarras] text NULL 
, [Digito] text NULL 
, [ValorCusto] numeric(53,0) NULL 
, [Quantidade] numeric(53,0) NULL 
, [Status] text NULL 
);



CREATE TABLE [RepCargaProduto] ( 
  [Id] INTEGER PRIMARY KEY NOT NULL 
, [CargaId] bigint NULL 
, [ProdutoGradeId] bigint NOT NULL 
, [Quantidade] numeric(53,0) NULL 
, [QuantidadeRetorno] numeric(53,0) NULL 
);


CREATE VIEW RepProdutosCarga AS 
SELECT RepProduto.CodigoBarras || RepProdutoGrade.Digito as CodigoBarras,
	RepProduto.Descricao,
	Cor,
	Tamanho,
	sum(RepCargaProduto.Quantidade) as Quantidade,
	sum(QuantidadeRetorno) QuantidadeRetorno,
	ValorSaida,
	ValorCusto,
	RepProduto.Id as ProdutoId,
	RepCargaProduto.Id as CargaProdutoId,
	RepCarga.Id as CargaId,
	RepProdutoGrade.Id as ProdutoGradeId,
	PracaId,
	RepresentanteId,
	Ano,
	Mes
	from RepCarga 
INNER JOIN RepCargaProduto ON RepCarga.Id = RepCargaProduto.CargaId
INNER JOIN RepProdutoGrade ON RepCargaProduto.ProdutoGradeId = RepProdutoGrade.Id
INNER JOIN RepProduto ON RepProdutoGrade.ProdutoId = RepProduto.Id

GROUP BY RepProduto.CodigoBarras || RepProdutoGrade.Digito,
	RepProduto.Descricao,
	Cor,
	Tamanho,
	ValorSaida,
	ValorCusto,
	RepProduto.Id,
	RepCargaProduto.Id,
	RepCarga.Id,
	RepProdutoGrade.Id,
	PracaId,
	RepresentanteId,
	Ano,
	Mes;
	
	
	
	CREATE VIEW RepProdutosConferencia as
SELECT RepProduto.CodigoBarras || RepProdutoGrade.Digito as CodigoBarras,
	RepProduto.Descricao,
	RepCargaProduto.Quantidade as QuantidadeCarga,
	RepCargaConferencia.Quantidade as QuantidadeInformada,
	RepCargaProduto.Quantidade - IFNULL(RepCargaConferencia.Quantidade, 0) as Diferenca,
	RepProduto.Id as ProdutoId,
	RepCargaProduto.Id as CargaProdutoId,
	RepProdutoGrade.Id as ProdutoGradeId,
	RepCarga.Id as CargaId
	FROM RepCarga 
INNER JOIN RepCargaProduto ON RepCarga.Id = RepCargaProduto.CargaId
INNER JOIN RepProdutoGrade ON RepCargaProduto.ProdutoGradeId = RepProdutoGrade.Id
INNER JOIN RepProduto ON RepProdutoGrade.ProdutoId = RepProduto.Id
LEFT JOIN RepCargaConferencia ON RepCargaConferencia.CargaId = RepCarga.Id AND RepCargaConferencia.ProdutoGradeId = RepProdutoGrade.Id;





CREATE TABLE [RepPedido] (
    [Id]                 INTEGER PRIMARY KEY NOT NULL,
    [VendedorId]          bigint        NOT NULL,
    [CargaId]             bigint        NOT NULL,
    [CodigoPedido]        text NULL,
    [DataLancamento]      date       NULL,
    [DataPrevisaoRetorno] date       NULL,
    [DataRetorno]         date       NULL,
    [ValorTotal]          numeric(53,0) NULL,
    [ValorCompra]         numeric(53,0) NULL,
    [PercentualComissao]  numeric(53,0) NULL,
    [ValorComissao]       numeric(53,0) NULL,
    [ValorLiquido]        numeric(53,0) NULL,
    [ReceitaAnterior]     numeric(53,0) NULL,
    [TotalAPagar]         numeric(53,0) NULL,
    [ValorAcerto]         numeric(53,0) NULL,
    [Status]              text NULL
);


CREATE TABLE [RepPedidoItem] (
    [Id]             INTEGER PRIMARY KEY NOT NULL,
    [PedidoId]       bigint        NOT NULL,
    [ProdutoGradeId] bigint        NOT NULL,
    [Quantidade]     numeric(53,0) NULL,
    [Retorno]        numeric(53,0) NULL,
    [Preco]          numeric(53,0) NULL
);