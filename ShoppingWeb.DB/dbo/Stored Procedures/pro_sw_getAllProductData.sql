CREATE PROCEDURE [dbo].[pro_sw_getAllProductData]
	@pageNumber INT,
    @pageSize INT,
	@beforePagesTotal INT,
	@totalCount INT OUTPUT,
	@languageNum INT
AS
BEGIN
    SET NOCOUNT ON;

	SELECT @totalCount = COUNT(f_id) FROM t_product WITH(NOLOCK);

    DECLARE @offset INT

	IF (@beforePagesTotal != CEILING(CAST(@totalCount as float) / @pageSize))
    BEGIN
        SET @offset = 0
    END
    ELSE
    BEGIN
        SET @offset = (@pageNumber - 1) * @pageSize
    END

	IF(@languageNum = 0)
	BEGIN
		SELECT f_id, f_nameTW AS f_name, f_category, f_price, f_stock, f_isOpen, f_introduceTW AS f_introduce, f_img, f_warningValue
		FROM t_product WITH(NOLOCK)
		ORDER BY f_id
		OFFSET @offset ROWS
		FETCH NEXT @pageSize ROWS ONLY;
	END
	ELSE IF(@languageNum = 1)
	BEGIN
		SELECT f_id, f_nameEN AS f_name, f_category, f_price, f_stock, f_isOpen, f_introduceEN AS f_introduce, f_img, f_warningValue
		FROM t_product WITH(NOLOCK)
		ORDER BY f_id
		OFFSET @offset ROWS
		FETCH NEXT @pageSize ROWS ONLY;
	END
END