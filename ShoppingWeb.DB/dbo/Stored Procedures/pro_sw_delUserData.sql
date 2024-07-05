CREATE PROCEDURE [dbo].[pro_sw_delUserData]
	@userId INT,
	@delUserId INT
AS
BEGIN

	SET NOCOUNT ON;
	IF (@delUserId != 1 AND @userId != @delUserId)
    BEGIN		
        DELETE FROM t_userInfo WHERE f_id = @delUserId
		SELECT @@ROWCOUNT 
    END
    ELSE
    BEGIN
		SELECT 0;
    END
END