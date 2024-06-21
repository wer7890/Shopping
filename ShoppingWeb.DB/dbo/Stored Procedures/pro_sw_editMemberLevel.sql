CREATE PROCEDURE pro_sw_editMemberLevel
	@memberId INT,
	@level TINYINT
AS
BEGIN

	SET NOCOUNT ON;
	UPDATE t_member WITH(ROWLOCK)
	SET f_level = @level
	WHERE f_Id = @memberId
	SELECT @@ROWCOUNT 
END