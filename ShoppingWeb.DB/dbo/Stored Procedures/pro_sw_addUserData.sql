CREATE PROCEDURE pro_sw_addUserData
    @account VARCHAR(16),
    @pwd VARCHAR(64),
    @roles TINYINT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @userCount INT;
    SELECT @userCount = COUNT(1) FROM t_userInfo WITH(NOLOCK) WHERE f_account = @account COLLATE SQL_Latin1_General_CP1_CS_AS;

    IF (@userCount = 0)
    BEGIN
        INSERT INTO t_userInfo(f_account, f_pwd, f_roles, f_sessionId)
        VALUES(@account, @pwd, @roles, NULL);

        SELECT 1;
    END
    ELSE
    BEGIN
        SELECT 0;
    END
END