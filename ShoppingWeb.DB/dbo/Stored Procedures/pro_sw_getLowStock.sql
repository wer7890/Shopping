CREATE PROCEDURE [dbo].[pro_sw_getLowStock]
AS
BEGIN
    SET NOCOUNT ON;

	SELECT f_id, f_nameTW, f_nameEN, f_stock, f_isOpen, f_warningValue
	FROM t_product WITH(NOLOCK)
	WHERE f_stock <= f_warningValue AND f_isOpen = 1 AND f_warningValue >0;
END