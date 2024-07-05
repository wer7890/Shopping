CREATE PROCEDURE [dbo].[pro_sw_getAllUserData]
    @pageNumber INT,
    @pageSize INT,  
	@beforePagesTotal INT,
	@totalCount INT OUTPUT  
AS
BEGIN
    SET NOCOUNT ON;

	SELECT @totalCount = COUNT(f_id) FROM t_userInfo WITH(NOLOCK);

    DECLARE @offset INT

	IF (@beforePagesTotal != CEILING(CAST(@totalCount as float) / @pageSize))
    BEGIN
        SET @offset = 0
    END
    ELSE
    BEGIN
        SET @offset = (@pageNumber - 1) * @pageSize
    END

	-- 使用 OFFSET FETCH 方法來實現分頁
	SELECT f_id, f_account, f_roles
	FROM t_userInfo WITH(NOLOCK)
	WHERE f_roles > 0
	ORDER BY f_id
	OFFSET @offset ROWS
	FETCH NEXT @pageSize ROWS ONLY;  --offset 10 rows ，将前10条记录舍去，fetch next 10 rows only ，向后再读取10条数据。
END