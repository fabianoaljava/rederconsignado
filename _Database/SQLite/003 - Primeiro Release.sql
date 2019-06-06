CREATE TABLE [RepCargaAnterior] ( 
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


CREATE TABLE [RepCargaConferencia] ( 
  [Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL 
, [CargaId] bigint NULL 
, [ProdutoGradeId] bigint NOT NULL 
, [Quantidade] numeric(53,0) NULL 
);




CREATE TABLE [RepVendedorBase] (
    [Id]                 INTEGER PRIMARY KEY NOT NULL, 
    [Nome]              text  NULL,
    [RazaoSocial]       text  NULL,
    [Endereco]          text NULL,
    [Complemento]       text NULL,
    [Bairro]            text NULL,
    [Cidade]            text NULL,
    [UF]                text NULL,
    [Cep]               text NULL,
    [TipoPessoa]        text NULL,
    [CpfCnpj]           text NULL,
    [RGInscricao]       text NULL,
    [DataNascimento]    date        NULL,
    [Telefone]          text  NULL,
    [TelefoneComercial] text  NULL,
    [Celular]           text  NULL,
    [Email]             text  NULL,
    [DataInicial]       date        NULL,
    [DataFinal]         date        NULL,
    [LimitePedido]      numeric(53,0)  NULL,
    [LimiteCredito]     numeric(53,0)  NULL,
    [Status]            text   NULL,
    [Observacao]        text        NULL
)