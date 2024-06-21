CREATE PROCEDURE [dbo].[pro_sw_getPwdAndEditSessionId]
    @account VARCHAR(16),
    @pwd VARCHAR(64),
    @sessionId VARCHAR(24),
	@userId INT OUTPUT,
	@roles TINYINT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @dbPwd NVARCHAR(64);
    SELECT @dbPwd = f_pwd FROM t_userInfo WITH(NOLOCK) WHERE f_account = @account COLLATE SQL_Latin1_General_CP1_CS_AS;

    IF (@dbPwd IS NOT NULL AND @dbPwd = @pwd COLLATE SQL_Latin1_General_CP1_CS_AS)
    BEGIN
		
        UPDATE t_userInfo WITH(ROWLOCK) SET f_sessionId = @sessionId WHERE f_account = @account;
		
		SELECT @userId = f_id, @roles = f_roles FROM t_userInfo WITH(NOLOCK) WHERE f_account = @account

        SELECT 1; -- 返回登入成功
    END
    ELSE
    BEGIN
        SELECT 0; -- 返回登入失敗
    END
END