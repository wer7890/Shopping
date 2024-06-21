CREATE PROCEDURE [dbo].[pro_sw_editRoles]
	@userId INT,
	@roles TINYINT
AS
BEGIN

	SET NOCOUNT ON;
	UPDATE t_userInfo WITH(ROWLOCK)
	SET f_roles = @roles, f_sessionId = NULL
	WHERE f_Id = @userId
	SELECT @@ROWCOUNT 
END