CREATE PROCEDURE pro_sw_delProductData
    @productId INT,
    @deletedProductImg VARCHAR(40) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
   
    DECLARE @productImg VARCHAR(40);

    SELECT @productImg = f_img
    FROM t_product WITH(NOLOCK)
    WHERE f_id = @productId;

    -- 刪除商品
    DELETE FROM t_product WHERE f_id = @productId;

    -- 賦值
    SET @deletedProductImg = @productImg;

    SELECT @@ROWCOUNT;
END