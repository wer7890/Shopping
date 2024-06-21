CREATE PROCEDURE pro_sw_editMemberStatus
	@memberId INT
AS
BEGIN

	SET NOCOUNT ON;
	UPDATE t_member WITH(ROWLOCK)
	SET f_accountStatus = CASE WHEN f_accountStatus = 1 THEN 0 ELSE 1 END
	WHERE f_Id = @memberId
	SELECT @@ROWCOUNT 
END