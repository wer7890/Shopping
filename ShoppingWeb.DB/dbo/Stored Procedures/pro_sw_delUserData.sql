﻿CREATE PROCEDURE pro_sw_delUserData
	@userId INT
AS
BEGIN

	SET NOCOUNT ON;
	DELETE FROM t_userInfo WHERE f_id = @userId
	SELECT @@ROWCOUNT 
END