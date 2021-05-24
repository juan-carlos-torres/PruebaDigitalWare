

--Obtener la lista de precios de todos los productos.
SELECT 
	pro_nombre nombre, pro_precio precio
FROM producto




--Obtener la lista de productos cuya existencia en el inventario haya llegado al mínimo permitido (5 unidades).
SELECT 
	pro_nombre nombre
FROM inventario
INNER JOIN producto ON inventario.inv_id_producto = producto.pro_id_producto
WHERE inv_cantidad = 5




--Obtener una lista de clientes no mayores de 35 años que hayan realizado compras entre el 1 de febrero de 2000 y el 25 de mayo de 2000.
SELECT 
	(cli_nombres + ' ' +  cli_apellidos) nombre_completo 
FROM cliente
INNER JOIN factura ON cliente.cli_id_cliente = factura.fac_id_cliente
WHERE DATEDIFF(YEAR,cli_fecha_nacimiento,GETDATE()) < 35 
		AND fac_fecha_registro BETWEEN '2000-02-01' AND '2000-05-25'




--Obtener el valor total vendido por cada producto en el año 2000.
SELECT 
	producto.pro_nombre nombre, 
	ISNULL(SUM(fpr_valor),0) valor 
FROM factura_producto
INNER JOIN factura ON factura_producto.fpr_id_compra = factura.fac_id_factura AND  YEAR(factura.fac_fecha_registro) = 2000
RIGHT JOIN producto ON producto.pro_id_producto = factura_producto.fpr_id_producto  
GROUP BY producto.pro_nombre




--Obtener la última fecha de compra de un cliente y según su frecuencia de compra estimar en qué fecha podría volver a comprar.
DECLARE @ID_CLIENTE UNIQUEIDENTIFIER = '0FFBB9F3-2188-4D85-B9B3-5103771A5FD2'

SELECT 
	MAX(fac_fecha_registro) fecha_ultima_compra, 
	DATEADD(
		DAY, 
		DATEDIFF(
			DAY,
			MIN(fac_fecha_registro),
			MAX(fac_fecha_registro)
		) / COUNT(*), 
		MAX(fac_fecha_registro)
	) fecha_proxima_compra
FROM factura
WHERE fac_id_cliente = @ID_CLIENTE 
