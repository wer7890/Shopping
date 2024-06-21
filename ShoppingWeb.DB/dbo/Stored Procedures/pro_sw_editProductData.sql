CREATE PROCEDURE [dbo].[pro_sw_editProductData]
	@productId INT,
	@price INT,
	@stock INT,
	@introduce NVARCHAR(500),
	@introduceEN NVARCHAR(500),
	@warningValue INT,
	@checkStoct BIT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @dbStock INT;
	SELECT @dbStock = f_stock FROM t_product WITH(NOLOCK) WHERE f_id = @productId;

	IF(@checkStoct = 1)
	BEGIN
        UPDATE t_product WITH(ROWLOCK) SET f_price = @price, f_stock = @dbStock + @stock, f_introduceTW = @introduce, f_introduceEN = @introduceEN, f_warningValue = @warningValue WHERE f_id = @productId
		SELECT @@ROWCOUNT
    END
    ELSE
    BEGIN

		IF (@dbStock - @stock = 0)
		BEGIN
			UPDATE t_product WITH(ROWLOCK) SET f_isOpen = 0 WHERE f_id = @productId;
		END

		UPDATE t_product WITH(ROWLOCK) SET f_price = @price, f_stock = @dbStock - @stock, f_introduceTW = @introduce, f_introduceEN = @introduceEN, f_warningValue = @warningValue WHERE f_id = @productId AND @stock <= @dbStock
			
		SELECT @@ROWCOUNT
    END
END