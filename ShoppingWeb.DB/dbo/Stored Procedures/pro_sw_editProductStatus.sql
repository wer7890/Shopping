CREATE PROCEDURE [dbo].[pro_sw_editProductStatus]
	@productId INT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @dbStock INT;
	SELECT @dbStock = f_stock FROM t_product WITH(NOLOCK) WHERE f_id = @productId;

	IF (@dbStock <= 0)
    BEGIN
		SELECT 0;
    END
    ELSE
    BEGIN
        UPDATE t_product WITH(ROWLOCK)
		SET f_isOpen = CASE WHEN f_isOpen = 1 THEN 0 ELSE 1 END
		WHERE f_Id = @productId
		SELECT @@ROWCOUNT;
    END
END