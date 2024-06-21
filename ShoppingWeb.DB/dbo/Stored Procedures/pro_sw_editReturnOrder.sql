CREATE PROCEDURE [dbo].[pro_sw_editReturnOrder]
	@orderId INT,
	@boolReturn BIT
AS
BEGIN
	SET NOCOUNT ON;
	IF (@boolReturn = 1)
	BEGIN
		-- 更新訂單資料
		UPDATE t_order WITH(ROWLOCK)
		SET f_orderStatus = 3, f_deliveryStatus = 5
		WHERE f_Id = @orderId;

		SELECT @@ROWCOUNT 
	END
	ELSE
	BEGIN 
		-- 更新訂單資料
		UPDATE t_order WITH(ROWLOCK)
		SET f_orderStatus = 1, f_deliveryStatus = 4
		WHERE f_Id = @orderId;

		SELECT @@ROWCOUNT 
	END
END