DROP VIEW RepPosicaoFinanceira ;
CREATE VIEW RepPosicaoFinanceira AS
SELECT RepReceber.rowid Id, VendedorId, SUM(ValorNF) AS ValorAReceber, Sum(Valor) AS ValorRecebido, Sum(ValorNF-Valor) AS ValorAberto 
FROM RepReceber 
LEFT JOIN RepReceberBaixa ON RepReceber.Id = RepReceberBaixa.ReceberId
GROUP BY VendedorId