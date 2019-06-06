--1432
SELECT RepVendedor.Id, Nome, CpfCnpj, Endereco, Complemento, Bairro, Cidade, UF, Telefone, Celular, 
CASE WHEN PedidoAnterior.VendedorId IS NOT NULL
       THEN 'Sim'
       ELSE 'Não'
       END AS PedidoAnterior,
CASE 
	WHEN ValorNF-Valor  = 0 THEN 'Total'
	WHEN ValorNF-Valor >0 THEN 'Parcial'
	ELSE NULL
	END AS Recebido,	
CASE WHEN PedidoAtual.VendedorId IS NOT NULL
       THEN 'Sim'
       ELSE 'Não'
       END AS PedidoAtual, CodigoPedido,
CASE 
	WHEN ValorNF-Valor  = 0 THEN 'Não'
	WHEN ValorNF-Valor >0 THEN 'Sim'
	ELSE 'Sim'
	END AS Receber 
FROM RepVendedor
LEFT JOIN (SELECT VendedorId, CodigoPedido  FROM RepPedido WHERE CargaId = 1432) AS PedidoAtual ON RepVendedor.Id = PedidoAtual.VendedorId
LEFT JOIN (SELECT VendedorId FROM RepPedido WHERE CargaId < 1432) AS PedidoAnterior ON RepVendedor.Id = PedidoAnterior.VendedorId
LEFT JOIN RepReceber ON RepVendedor.Id = RepReceber.VendedorId
LEFT JOIN RepReceberBaixa ON RepReceber.Id = RepReceberBaixa.ReceberId



SELECT * FROM RepPedido WHERE CargaId < 1432

SELECT * FROM RepCarga --PracaId, RepresentanteId, Ano, Mes
SELECT * FROM RepPedido
SELECT * FROM RepReceber
SELECT * FROM RepReceberBaixa
SELECT * FROM RepCargaAnterior
SELECT * FROM RepCargaConferencia
--Carga Atual SELECT * FROM RepCarga --PracaId, RepresentanteId, Ano, Mes
--Carga Anterior SELECT * FROM RepCarga -- PracaId (Mesma Praça)




603-924-6397

lev

