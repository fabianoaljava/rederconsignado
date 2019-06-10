DROP VIEW RepProdutosConferencia;
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
LEFT JOIN RepCargaConferencia ON RepCargaConferencia.CargaId = RepCarga.Id AND RepCargaConferencia.ProdutoGradeId = RepProdutoGrade.Id