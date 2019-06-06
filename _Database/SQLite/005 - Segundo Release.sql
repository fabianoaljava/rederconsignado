CREATE TABLE [Receber](
	[Id] INTEGER PRIMARY KEY NOT NULL ,
	[VendedorId] bigint NULL,
	[CargaId] bigint NULL,
	[Documento] bigint NULL,
	[Serie] text NULL,
	[ValorNF] numeric(53,0) NULL,
	[ValorDuplicata] numeric(53,0) NULL,
	[ValorAReceber] numeric(53,0) NULL,
	[ValorJuros] numeric(53,0) NULL,
	[ValorDesconto] numeric(53,0) NULL,
	[DataEmissao] date NULL,
	[DataLancamento] date NULL,
	[DataVencimento] date NULL,
	[DataPagamento] date NULL,
	[Observacoes] text NULL,
	[Status] text NULL
);


CREATE TABLE [ReceberBaixa](
	[Id] INTEGER PRIMARY KEY NOT NULL ,
	[ReceberId] bigint NULL,
	[CargaId] bigint NULL,
	[Valor] numeric(53,0) NULL,
	[DataPagamento] date NULL,
	[DataBaixa] date NULL,
	[Juros] numeric(53,0) NULL,
	[Desconto] numeric(53,0) NULL
);