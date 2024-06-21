CREATE PROCEDURE pro_sw_getUserData
	@userId INT
AS
BEGIN

	SET NOCOUNT ON;
	SELECT f_id, f_account, f_roles FROM t_userInfo WITH(NOLOCK) WHERE f_id = @userId
END