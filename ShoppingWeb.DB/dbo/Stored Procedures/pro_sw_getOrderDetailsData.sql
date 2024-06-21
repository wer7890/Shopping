CREATE PROCEDURE [dbo].[pro_sw_getOrderDetailsData]
	@orderId INT,
	@languageNum INT
AS
BEGIN
    SET NOCOUNT ON;

	IF(@languageNum = 0)
    BEGIN
        SELECT f_productNameTW AS f_productName, f_productPrice, f_productCategory, f_quantity, f_subtotal
		FROM t_orderDetails WITH (NOLOCK)
		WHERE f_orderId = @orderId
    END
    ELSE IF(@languageNum = 1)
    BEGIN
        SELECT f_productNameEN AS f_productName, f_productPrice, f_productCategory, f_quantity, f_subtotal
		FROM t_orderDetails WITH (NOLOCK)
		WHERE f_orderId = @orderId
    END
END