CREATE PROCEDURE [dbo].[pro_sw_getProductData]
	@productId INT
AS
BEGIN

	SET NOCOUNT ON;
	SELECT f_id, f_nameTW, f_nameEN, f_category, f_price, f_stock, f_createdUser, CONVERT(VARCHAR(16), f_createdTime, 20) as f_createdTime, f_introduceTW, f_introduceEN, f_img, f_warningValue
	FROM t_product WITH(NOLOCK) 
	WHERE f_id = @productId;
END