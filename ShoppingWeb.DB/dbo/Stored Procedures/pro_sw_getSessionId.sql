CREATE PROCEDURE [dbo].[pro_sw_getSessionId]
	@userId INT,
	@sessionId VARCHAR(24)
AS
BEGIN
	DECLARE @dbSessionId VARCHAR(24);

    SET NOCOUNT ON;
    SELECT @dbSessionId = f_sessionId 
	FROM t_userInfo WITH(NOLOCK) 
	WHERE f_id = @userId

	IF (@dbSessionId IS NOT NULL AND @sessionId = @dbSessionId)
    BEGIN		
		SELECT 1; -- 返回登入成功
    END
    ELSE
    BEGIN
		SELECT 0; -- 返回登入失敗
    END
END