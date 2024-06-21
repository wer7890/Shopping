CREATE PROCEDURE [dbo].[pro_sw_getSearchProductData]
	@category INT,
	@name NVARCHAR(100),
	@allMinorCategories BIT,
	@allBrand BIT,
	@pageNumber INT,
    @pageSize INT,
	@beforePagesTotal INT,
	@totalCount INT OUTPUT,
	@languageNum INT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @offset INT

	IF(@languageNum = 0)
    BEGIN
        IF (@allMinorCategories = 0)
		BEGIN
			SELECT @totalCount = COUNT(f_id)
			FROM t_product WITH(NOLOCK)
			WHERE (
					(@allBrand = 0 AND f_category = @category)
					OR 
					(@allBrand = 1 AND f_category BETWEEN @category AND (@category / 100) * 100 + 99)
				  )
				  AND f_nameTW LIKE '%' + @name + '%';	
			
			IF (@beforePagesTotal != CEILING(CAST(@totalCount as float) / @pageSize))
			BEGIN
				SET @offset = 0
			END
			ELSE
			BEGIN
				SET @offset = (@pageNumber - 1) * @pageSize
			END

			SELECT f_id, f_nameTW AS f_name, f_category, f_price, f_stock, f_isOpen, f_introduceTW AS f_introduce, f_img, f_warningValue
			FROM t_product WITH(NOLOCK)
			WHERE (
					(@allBrand = 0 AND f_category = @category)
					OR 
					(@allBrand = 1 AND f_category BETWEEN @category AND (@category / 100) * 100 + 99)
				  )
				  AND f_nameTW LIKE '%' + @name + '%'
			ORDER BY f_id
			OFFSET @offset ROWS
			FETCH NEXT @pageSize ROWS ONLY; 
		END
		ELSE
		BEGIN
			SELECT @totalCount = COUNT(f_id)
			FROM t_product WITH(NOLOCK)
			WHERE (
					(@allBrand = 0 AND f_category BETWEEN @category AND (@category / 10000) * 10000 + 9999 AND f_category % 100 = @category % 100)
					OR 
					(@allBrand = 1 AND f_category BETWEEN @category AND (@category / 10000) * 10000 + 9999)
				  )
				  AND f_nameTW LIKE '%' + @name + '%';	

			IF (@beforePagesTotal != CEILING(CAST(@totalCount as float) / @pageSize))
			BEGIN
				SET @offset = 0
			END
			ELSE
			BEGIN
				SET @offset = (@pageNumber - 1) * @pageSize
			END			

			SELECT f_id, f_nameTW AS f_name, f_category, f_price, f_stock, f_isOpen, f_introduceTW AS f_introduce, f_img, f_warningValue
			FROM t_product WITH(NOLOCK)
			WHERE (
					(@allBrand = 0 AND f_category BETWEEN @category AND (@category / 10000) * 10000 + 9999 AND f_category % 100 = @category % 100)
					OR
					(@allBrand = 1 AND f_category BETWEEN @category AND (@category / 10000) * 10000 + 9999)
				  )
				  AND f_nameTW LIKE '%' + @name + '%'
			ORDER BY f_id
			OFFSET @offset ROWS
			FETCH NEXT @pageSize ROWS ONLY; 
		END
    END
    ELSE IF(@languageNum = 1)
    BEGIN
        IF (@allMinorCategories = 0)
		BEGIN
			SELECT @totalCount = COUNT(f_id)
			FROM t_product WITH(NOLOCK)
			WHERE (
					(@allBrand = 0 AND f_category = @category)
					OR 
					(@allBrand = 1 AND f_category BETWEEN @category AND (@category / 100) * 100 + 99)
				  )
				  AND f_nameEN LIKE '%' + @name + '%';	
			
			IF (@beforePagesTotal != CEILING(CAST(@totalCount as float) / @pageSize))
			BEGIN
				SET @offset = 0
			END
			ELSE
			BEGIN
				SET @offset = (@pageNumber - 1) * @pageSize
			END

			SELECT f_id, f_nameEN AS f_name, f_category, f_price, f_stock, f_isOpen, f_introduceEN AS f_introduce, f_img, f_warningValue
			FROM t_product WITH(NOLOCK)
			WHERE (
					(@allBrand = 0 AND f_category = @category)
					OR 
					(@allBrand = 1 AND f_category BETWEEN @category AND (@category / 100) * 100 + 99)
				  )
				  AND f_nameEN LIKE '%' + @name + '%'
			ORDER BY f_id
			OFFSET @offset ROWS
			FETCH NEXT @pageSize ROWS ONLY; 
		END
		ELSE
		BEGIN
			SELECT @totalCount = COUNT(f_id)
			FROM t_product WITH(NOLOCK)
			WHERE (
					(@allBrand = 0 AND f_category BETWEEN @category AND (@category / 10000) * 10000 + 9999 AND f_category % 100 = @category % 100)
					OR 
					(@allBrand = 1 AND f_category BETWEEN @category AND (@category / 10000) * 10000 + 9999)
				  )
				  AND f_nameEN LIKE '%' + @name + '%';	
			
			IF (@beforePagesTotal != CEILING(CAST(@totalCount as float) / @pageSize))
			BEGIN
				SET @offset = 0
			END
			ELSE
			BEGIN
				SET @offset = (@pageNumber - 1) * @pageSize
			END

			SELECT f_id, f_nameEN AS f_name, f_category, f_price, f_stock, f_isOpen, f_introduceEN AS f_introduce, f_img, f_warningValue
			FROM t_product WITH(NOLOCK)
			WHERE (
					(@allBrand = 0 AND f_category BETWEEN @category AND (@category / 10000) * 10000 + 9999 AND f_category % 100 = @category % 100)
					OR
					(@allBrand = 1 AND f_category BETWEEN @category AND (@category / 10000) * 10000 + 9999)
				  )
				  AND f_nameEN LIKE '%' + @name + '%'
			ORDER BY f_id
			OFFSET @offset ROWS
			FETCH NEXT @pageSize ROWS ONLY; 
		END
    END
END