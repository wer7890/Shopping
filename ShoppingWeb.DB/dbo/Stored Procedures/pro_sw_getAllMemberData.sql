CREATE PROCEDURE [dbo].[pro_sw_getAllMemberData]
	@pageNumber INT,
    @pageSize INT,
	@beforePagesTotal INT,
	@totalCount INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

	SELECT @totalCount = COUNT(f_id) FROM t_member WITH(NOLOCK);

	DECLARE @offset INT

	IF (@beforePagesTotal != CEILING(CAST(@totalCount as float) / @pageSize))
    BEGIN
        SET @offset = 0
    END
    ELSE
    BEGIN
        SET @offset = (@pageNumber - 1) * @pageSize
    END

	SELECT m.f_id, m.f_account, m.f_pwd, m.f_name, m.f_level, m.f_phoneNumber, m.f_email, m.f_accountStatus, w.f_amount, ts.f_totalSpent
	FROM t_member m WITH (NOLOCK)
	LEFT JOIN t_wallet w WITH (NOLOCK) ON w.f_memberId = m.f_id
	LEFT JOIN t_totalSpent ts WITH (NOLOCK) ON ts.f_memberId = m.f_id
	ORDER BY m.f_id
	OFFSET @offset ROWS
	FETCH NEXT @pageSize ROWS ONLY; 
END