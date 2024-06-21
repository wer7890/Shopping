CREATE PROCEDURE [dbo].[pro_sw_addProductData]
    @name NVARCHAR(100),
	@nameEN NVARCHAR(100),
    @category INT,
    @img NVARCHAR(40),
    @price INT,
    @stock INT,
    @isOpen BIT,
    @introduce NVARCHAR(500),
	@introduceEN NVARCHAR(500),
	@warningValue INT,
    @owner INT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM dbo.t_product WITH(NOLOCK) WHERE f_img = @img)
    BEGIN
        INSERT INTO dbo.t_product (f_nameTW, f_nameEN, f_category, f_img, f_price, f_stock, f_isOpen, f_introduceTW, f_introduceEN, f_warningValue, f_createdUser)
        VALUES (@name, @nameEN, @category, @img, @price, @stock, @isOpen, @introduce, @introduceEN, @warningValue, @owner);
		SELECT 1;
    END
    ELSE
    BEGIN
        SELECT 0;
    END
END