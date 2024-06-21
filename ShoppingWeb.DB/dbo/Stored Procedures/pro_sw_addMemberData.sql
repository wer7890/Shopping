CREATE PROCEDURE [dbo].[pro_sw_addMemberData]
    @account VARCHAR(16),
    @pwd VARCHAR(16),
    @name NVARCHAR(15),
    @birthday date,
    @phone VARCHAR(10),
    @email VARCHAR(40),
    @address NVARCHAR(25)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @dbId INT;

	BEGIN TRANSACTION; -- 開始交易

    BEGIN TRY
		
		INSERT INTO t_member(f_account, f_pwd, f_name, f_birthday, f_phoneNumber, f_email, f_address)
		VALUES (@account, @pwd, @name, @birthday, @phone, @email, @address);

		SELECT @dbId = f_id FROM t_member WHERE f_account = @account;
		INSERT INTO t_wallet(f_memberId, f_amount)
		VALUES (@dbId, 0);

		INSERT INTO t_totalSpent(f_memberId, f_totalSpent)
		VALUES (@dbId, 0);

        COMMIT TRANSACTION; -- 提交交易
        SELECT 1; -- 操作成功，回傳1

    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION; -- 回滾交易
        SELECT 0; -- 錯誤，回傳0
    END CATCH
END