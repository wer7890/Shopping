CREATE PROCEDURE [dbo].[pro_sw_getAllOrderData]
	@pageNumber INT,
    @pageSize INT,
	@beforePagesTotal INT,
	@totalCount INT OUTPUT
AS
BEGIN
    	SET NOCOUNT ON;

		SELECT @totalCount = COUNT(f_id) FROM t_order WITH(NOLOCK);

		DECLARE @offset INT

		IF (@beforePagesTotal != CEILING(CAST(@totalCount as float) / @pageSize))
		BEGIN
			SET @offset = 0
		END
		ELSE
		BEGIN
			SET @offset = (@pageNumber - 1) * @pageSize
		END

		SELECT o.f_id, m.f_account, CONVERT(VARCHAR(16), o.f_createdTime, 20) as f_createdTime, o.f_orderStatus, o.f_deliveryStatus, o.f_deliveryMethod, o.f_total
		FROM t_order o WITH (NOLOCK)
		LEFT JOIN t_member m WITH (NOLOCK) ON m.f_id = o.f_memberId
		ORDER BY o.f_id
		OFFSET @offset ROWS
		FETCH NEXT @pageSize ROWS ONLY;  

		SELECT
		COUNT(f_deliveryStatus) AS 'statusAll',
        SUM(CASE WHEN f_deliveryStatus = 1 THEN 1 ELSE 0 END) AS 'status1',
        SUM(CASE WHEN f_deliveryStatus = 2 THEN 1 ELSE 0 END) AS 'status2',
        SUM(CASE WHEN f_deliveryStatus = 3 THEN 1 ELSE 0 END) AS 'status3',
        SUM(CASE WHEN f_deliveryStatus = 4 THEN 1 ELSE 0 END) AS 'status4',
        SUM(CASE WHEN f_deliveryStatus = 5 THEN 1 ELSE 0 END) AS 'status5',
        SUM(CASE WHEN f_deliveryStatus = 6 THEN 1 ELSE 0 END) AS 'status6',
		SUM(CASE WHEN f_orderStatus = 2 THEN 1 ELSE 0 END) AS 'orderStatus2'
		FROM t_order WITH (NOLOCK);
END