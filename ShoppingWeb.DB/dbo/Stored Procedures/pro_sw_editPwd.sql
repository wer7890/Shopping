CREATE PROCEDURE [dbo].[pro_sw_editPwd]
    @userId INT,
    @pwd VARCHAR(64)
AS
BEGIN
    SET NOCOUNT ON;

	UPDATE t_userInfo WITH(ROWLOCK) SET f_pwd=@pwd, f_sessionId = NULL WHERE f_id = @userId
	SELECT @@ROWCOUNT 
END