CREATE VIEW [dbo].[OrderView]
	AS 
	
	SELECT	c.company_id		as company_id,
			c.name				as company_name, 
			o.order_id			as order_id,
			o.description		as description,			
			op.price			as unit_price,
			op.product_id		as product_id,
			op.quantity			as quantity,
			p.name				as product_name, 
			p.price				as product_price
	FROM company c 
	INNER JOIN [order] o on c.company_id=o.company_id
	INNER JOIN orderproduct op on op.order_id = o.order_id
	INNER JOIN product p on op.product_id=p.product_id;