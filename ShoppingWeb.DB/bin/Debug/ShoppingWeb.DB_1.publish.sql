/*
ShoppingWeb 的部署脚本

此代码由工具生成。
如果重新生成此代码，则对此文件的更改可能导致
不正确的行为并将丢失。
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "ShoppingWeb"
:setvar DefaultFilePrefix "ShoppingWeb"
:setvar DefaultDataPath "D:\MSSQL-DATA\DATA\"
:setvar DefaultLogPath "D:\MSSQL-DATA\DATA\"

GO
:on error exit
GO
/*
请检测 SQLCMD 模式，如果不支持 SQLCMD 模式，请禁用脚本执行。
要在启用 SQLCMD 模式后重新启用脚本，请执行:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'要成功执行此脚本，必须启用 SQLCMD 模式。';
        SET NOEXEC ON;
    END


GO
USE [master];


GO

IF (DB_ID(N'$(DatabaseName)') IS NOT NULL) 
BEGIN
    ALTER DATABASE [$(DatabaseName)]
    SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [$(DatabaseName)];
END

GO
PRINT N'正在创建数据库 $(DatabaseName)...'
GO
CREATE DATABASE [$(DatabaseName)]
    ON 
    PRIMARY(NAME = [$(DatabaseName)], FILENAME = N'$(DefaultDataPath)$(DefaultFilePrefix)_Primary.mdf')
    LOG ON (NAME = [$(DatabaseName)_log], FILENAME = N'$(DefaultLogPath)$(DefaultFilePrefix)_Primary.ldf') COLLATE Chinese_PRC_CI_AS
GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CLOSE OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
USE [$(DatabaseName)];


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                NUMERIC_ROUNDABORT OFF,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL,
                RECOVERY FULL,
                CURSOR_CLOSE_ON_COMMIT OFF,
                AUTO_CREATE_STATISTICS ON,
                AUTO_SHRINK OFF,
                AUTO_UPDATE_STATISTICS ON,
                RECURSIVE_TRIGGERS OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ALLOW_SNAPSHOT_ISOLATION OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET READ_COMMITTED_SNAPSHOT OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_UPDATE_STATISTICS_ASYNC OFF,
                PAGE_VERIFY NONE,
                DATE_CORRELATION_OPTIMIZATION OFF,
                DISABLE_BROKER,
                PARAMETERIZATION SIMPLE,
                SUPPLEMENTAL_LOGGING OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET TRUSTWORTHY OFF,
        DB_CHAINING OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'无法修改数据库设置。您必须是 SysAdmin 才能应用这些设置。';
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET HONOR_BROKER_PRIORITY OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'无法修改数据库设置。您必须是 SysAdmin 才能应用这些设置。';
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET TARGET_RECOVERY_TIME = 0 SECONDS 
    WITH ROLLBACK IMMEDIATE;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET FILESTREAM(NON_TRANSACTED_ACCESS = OFF),
                CONTAINMENT = NONE 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CREATE_STATISTICS ON(INCREMENTAL = OFF),
                MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT = OFF,
                DELAYED_DURABILITY = DISABLED 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE (QUERY_CAPTURE_MODE = ALL, DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_PLANS_PER_QUERY = 200, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 367), MAX_STORAGE_SIZE_MB = 100) 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE = OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET TEMPORAL_HISTORY_RETENTION ON 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF fulltextserviceproperty(N'IsFulltextInstalled') = 1
    EXECUTE sp_fulltext_database 'enable';


GO
PRINT N'正在创建 用户 [SH_USER]...';


GO
CREATE USER [SH_USER] FOR LOGIN [SH_USER];


GO
REVOKE CONNECT TO [SH_USER];


GO
PRINT N'正在创建 角色成员资格 <未命名>...';


GO
EXECUTE sp_addrolemember @rolename = N'db_datareader', @membername = N'SH_USER';


GO
PRINT N'正在创建 角色成员资格 <未命名>...';


GO
EXECUTE sp_addrolemember @rolename = N'db_datawriter', @membername = N'SH_USER';


GO
PRINT N'正在创建 角色成员资格 <未命名>...';


GO
EXECUTE sp_addrolemember @rolename = N'db_owner', @membername = N'SH_USER';


GO
PRINT N'正在创建 表 [dbo].[t_member]...';


GO
CREATE TABLE [dbo].[t_member] (
    [f_id]            INT           IDENTITY (1, 1) NOT NULL,
    [f_account]       VARCHAR (16)  NOT NULL,
    [f_pwd]           VARCHAR (16)  NOT NULL,
    [f_name]          NVARCHAR (15) NOT NULL,
    [f_birthday]      DATE          NOT NULL,
    [f_level]         TINYINT       NOT NULL,
    [f_phoneNumber]   VARCHAR (10)  NOT NULL,
    [f_email]         VARCHAR (50)  NOT NULL,
    [f_address]       NVARCHAR (50) NOT NULL,
    [f_accountStatus] BIT           NOT NULL,
    [f_createdTime]   DATETIME      NOT NULL,
    CONSTRAINT [PK_t_member] PRIMARY KEY CLUSTERED ([f_id] ASC)
);


GO
PRINT N'正在创建 表 [dbo].[t_order]...';


GO
CREATE TABLE [dbo].[t_order] (
    [f_id]             INT           IDENTITY (1, 1) NOT NULL,
    [f_serialNumber]   VARCHAR (24)  NOT NULL,
    [f_memberId]       INT           NOT NULL,
    [f_total]          INT           NOT NULL,
    [f_orderStatus]    TINYINT       NOT NULL,
    [f_deliveryStatus] TINYINT       NOT NULL,
    [f_deliveryMethod] TINYINT       NOT NULL,
    [f_destination]    NVARCHAR (30) NOT NULL,
    [f_arrivalDate]    DATE          NOT NULL,
    [f_createdTime]    DATETIME      NOT NULL,
    CONSTRAINT [PK_t_order] PRIMARY KEY CLUSTERED ([f_id] ASC)
);


GO
PRINT N'正在创建 表 [dbo].[t_orderDetails]...';


GO
CREATE TABLE [dbo].[t_orderDetails] (
    [f_id]              INT            IDENTITY (1, 1) NOT NULL,
    [f_orderId]         INT            NOT NULL,
    [f_productId]       INT            NOT NULL,
    [f_productNameTW]   NVARCHAR (100) NOT NULL,
    [f_productNameEN]   NVARCHAR (100) NOT NULL,
    [f_productPrice]    INT            NOT NULL,
    [f_productCategory] INT            NOT NULL,
    [f_quantity]        INT            NOT NULL,
    [f_subtotal]        INT            NOT NULL,
    CONSTRAINT [PK_t_orderDetails] PRIMARY KEY CLUSTERED ([f_id] ASC)
);


GO
PRINT N'正在创建 表 [dbo].[t_product]...';


GO
CREATE TABLE [dbo].[t_product] (
    [f_id]           INT            IDENTITY (1, 1) NOT NULL,
    [f_nameTW]       NVARCHAR (100) NOT NULL,
    [f_nameEN]       NVARCHAR (100) NOT NULL,
    [f_category]     INT            NOT NULL,
    [f_img]          NVARCHAR (40)  NOT NULL,
    [f_price]        INT            NOT NULL,
    [f_stock]        INT            NOT NULL,
    [f_isOpen]       BIT            NOT NULL,
    [f_introduceTW]  NVARCHAR (500) NOT NULL,
    [f_introduceEN]  NVARCHAR (500) NOT NULL,
    [f_warningValue] INT            NOT NULL,
    [f_createdTime]  DATETIME       NOT NULL,
    [f_createdUser]  INT            NOT NULL,
    CONSTRAINT [PK_t_product] PRIMARY KEY CLUSTERED ([f_id] ASC)
);


GO
PRINT N'正在创建 表 [dbo].[t_totalSpent]...';


GO
CREATE TABLE [dbo].[t_totalSpent] (
    [f_memberId]   INT NOT NULL,
    [f_totalSpent] INT NOT NULL,
    CONSTRAINT [PK_t_totalSpent] PRIMARY KEY CLUSTERED ([f_memberId] ASC)
);


GO
PRINT N'正在创建 表 [dbo].[t_userInfo]...';


GO
CREATE TABLE [dbo].[t_userInfo] (
    [f_id]        INT          IDENTITY (1, 1) NOT NULL,
    [f_account]   VARCHAR (16) NOT NULL,
    [f_pwd]       VARCHAR (64) NOT NULL,
    [f_roles]     TINYINT      NOT NULL,
    [f_sessionId] VARCHAR (24) NULL,
    CONSTRAINT [PK_t_userInfo] PRIMARY KEY CLUSTERED ([f_id] ASC)
);


GO
PRINT N'正在创建 表 [dbo].[t_wallet]...';


GO
CREATE TABLE [dbo].[t_wallet] (
    [f_memberId] INT NOT NULL,
    [f_amount]   INT NOT NULL,
    CONSTRAINT [PK_t_wallet] PRIMARY KEY CLUSTERED ([f_memberId] ASC)
);


GO
PRINT N'正在创建 表 [dbo].[t_walletChanges]...';


GO
CREATE TABLE [dbo].[t_walletChanges] (
    [f_id]             INT      IDENTITY (1, 1) NOT NULL,
    [f_memberId]       INT      NOT NULL,
    [f_previousAmount] INT      NOT NULL,
    [f_finalAmount]    INT      NOT NULL,
    [f_changeAmount]   INT      NOT NULL,
    [f_createdTime]    DATETIME NOT NULL,
    CONSTRAINT [PK_t_walletChanges] PRIMARY KEY CLUSTERED ([f_id] ASC)
);


GO
PRINT N'正在创建 默认约束 [dbo].[DF_t_member_f_level]...';


GO
ALTER TABLE [dbo].[t_member]
    ADD CONSTRAINT [DF_t_member_f_level] DEFAULT ((0)) FOR [f_level];


GO
PRINT N'正在创建 默认约束 [dbo].[DF_t_member_f_accountStatus]...';


GO
ALTER TABLE [dbo].[t_member]
    ADD CONSTRAINT [DF_t_member_f_accountStatus] DEFAULT ((1)) FOR [f_accountStatus];


GO
PRINT N'正在创建 默认约束 [dbo].[DF_t_member_f_createdTime]...';


GO
ALTER TABLE [dbo].[t_member]
    ADD CONSTRAINT [DF_t_member_f_createdTime] DEFAULT (getdate()) FOR [f_createdTime];


GO
PRINT N'正在创建 默认约束 [dbo].[DF_t_order_f_orderStatus]...';


GO
ALTER TABLE [dbo].[t_order]
    ADD CONSTRAINT [DF_t_order_f_orderStatus] DEFAULT ((1)) FOR [f_orderStatus];


GO
PRINT N'正在创建 默认约束 [dbo].[DF_t_order_f_deliveryStatus]...';


GO
ALTER TABLE [dbo].[t_order]
    ADD CONSTRAINT [DF_t_order_f_deliveryStatus] DEFAULT ((1)) FOR [f_deliveryStatus];


GO
PRINT N'正在创建 默认约束 [dbo].[DF_t_order_f_createdTime]...';


GO
ALTER TABLE [dbo].[t_order]
    ADD CONSTRAINT [DF_t_order_f_createdTime] DEFAULT (getdate()) FOR [f_createdTime];


GO
PRINT N'正在创建 默认约束 [dbo].[DF_t_product_f_createdTime]...';


GO
ALTER TABLE [dbo].[t_product]
    ADD CONSTRAINT [DF_t_product_f_createdTime] DEFAULT (getdate()) FOR [f_createdTime];


GO
PRINT N'正在创建 默认约束 [dbo].[DF_t_totalSpent_f_totalSpent]...';


GO
ALTER TABLE [dbo].[t_totalSpent]
    ADD CONSTRAINT [DF_t_totalSpent_f_totalSpent] DEFAULT ((0)) FOR [f_totalSpent];


GO
PRINT N'正在创建 默认约束 [dbo].[DF_t_wallet_f_amount]...';


GO
ALTER TABLE [dbo].[t_wallet]
    ADD CONSTRAINT [DF_t_wallet_f_amount] DEFAULT ((0)) FOR [f_amount];


GO
PRINT N'正在创建 默认约束 [dbo].[DF_t_walletChanges_f_createdTime]...';


GO
ALTER TABLE [dbo].[t_walletChanges]
    ADD CONSTRAINT [DF_t_walletChanges_f_createdTime] DEFAULT (getdate()) FOR [f_createdTime];


GO
PRINT N'正在创建 过程 [dbo].[pro_sw_addMemberData]...';


GO
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
GO
PRINT N'正在创建 过程 [dbo].[pro_sw_addProductData]...';


GO
CREATE PROCEDURE [dbo].[pro_sw_addProductData]
    @name NVARCHAR(100),
	@nameEN NVARCHAR(100),
    @category INT,
    @img NVARCHAR(40),
    @price INT,
    @stock INT,
    @isOpen BIT,
    @introduce NVARCHAR(500),
	@introduceEN NVARCHAR(500),
	@warningValue INT,
    @owner INT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM dbo.t_product WITH(NOLOCK) WHERE f_img = @img)
    BEGIN
        INSERT INTO dbo.t_product (f_nameTW, f_nameEN, f_category, f_img, f_price, f_stock, f_isOpen, f_introduceTW, f_introduceEN, f_warningValue, f_createdUser)
        VALUES (@name, @nameEN, @category, @img, @price, @stock, @isOpen, @introduce, @introduceEN, @warningValue, @owner);
		SELECT 1;
    END
    ELSE
    BEGIN
        SELECT 0;
    END
END
GO
PRINT N'正在创建 过程 [dbo].[pro_sw_addUserData]...';


GO
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
GO
PRINT N'正在创建 过程 [dbo].[pro_sw_delProductData]...';


GO
CREATE PROCEDURE pro_sw_delProductData
    @productId INT,
    @deletedProductImg VARCHAR(40) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
   
    DECLARE @productImg VARCHAR(40);

    SELECT @productImg = f_img
    FROM t_product WITH(NOLOCK)
    WHERE f_id = @productId;

    -- 刪除商品
    DELETE FROM t_product WHERE f_id = @productId;

    -- 賦值
    SET @deletedProductImg = @productImg;

    SELECT @@ROWCOUNT;
END
GO
PRINT N'正在创建 过程 [dbo].[pro_sw_delUserData]...';


GO
CREATE PROCEDURE pro_sw_delUserData
	@userId INT
AS
BEGIN

	SET NOCOUNT ON;
	DELETE FROM t_userInfo WHERE f_id = @userId
	SELECT @@ROWCOUNT 
END
GO
PRINT N'正在创建 过程 [dbo].[pro_sw_editMemberLevel]...';


GO
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
GO
PRINT N'正在创建 过程 [dbo].[pro_sw_editMemberStatus]...';


GO
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
GO
PRINT N'正在创建 过程 [dbo].[pro_sw_editOrderData]...';


GO
CREATE PROCEDURE [dbo].[pro_sw_editOrderData]
	@orderId INT,
	@orderStatus INT,
	@deliveryStatus INT,
	@deliveryMethod INT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @newStock INT
	BEGIN TRANSACTION; -- 開始交易

    BEGIN TRY
		
		--訂單狀態為已付款，配送狀態為發貨中
        IF (@orderStatus = 1 AND @deliveryStatus = 2)
        BEGIN
            -- 檢查庫存是否足夠
            IF EXISTS (
                SELECT 1
                FROM t_product p WITH(NOLOCK)
                INNER JOIN t_orderDetails od ON p.f_id = od.f_productId
                WHERE od.f_orderId = @orderId
                GROUP BY p.f_id
                HAVING SUM(p.f_stock) - SUM(od.f_quantity) < 0
            )
            BEGIN
                -- 库存不足，配送狀態為發貨中，回傳0
                UPDATE t_order WITH(ROWLOCK)
                SET f_orderStatus = @orderStatus, f_deliveryStatus = 1, f_deliveryMethod = @deliveryMethod
                WHERE f_Id = @orderId;

                COMMIT TRANSACTION; -- 提交交易
                SELECT 0; -- 操作成功，回傳0
                RETURN;
            END;

            -- 更新庫存
            UPDATE p WITH(ROWLOCK)
            SET p.f_stock = p.f_stock - od.f_quantity
            FROM t_product p
            INNER JOIN t_orderDetails od ON p.f_id = od.f_productId
            WHERE od.f_orderId = @orderId;

            -- 更新訂單資料
            UPDATE t_order WITH(ROWLOCK)
            SET f_orderStatus = @orderStatus, f_deliveryStatus = 2, f_deliveryMethod = @deliveryMethod
            WHERE f_Id = @orderId;

			-- 如果庫存小於等於0，就關閉商品
			SELECT @newStock = p.f_stock
			FROM t_product p WITH(NOLOCK)
            INNER JOIN t_orderDetails od ON p.f_id = od.f_productId
            WHERE od.f_orderId = @orderId;

			IF (@newStock <= 0)
			BEGIN
				UPDATE p WITH(ROWLOCK)
				SET p.f_isOpen = 0
				FROM t_product p
				INNER JOIN t_orderDetails od ON p.f_id = od.f_productId
				WHERE od.f_orderId = @orderId;
			END

        END

		--訂單狀態為已退款，配送狀態為已退貨
		ELSE IF (@orderStatus = 4 AND @deliveryStatus = 6)
		BEGIN
			DECLARE @totalAmount INT;
			DECLARE @memberId INT;
			DECLARE @walletAmount INT;

			-- 鎖定 t_order 和 t_wallet 表
			SELECT @totalAmount = f_total, @memberId = f_memberId
			FROM t_order WITH(ROWLOCK)
			WHERE f_id = @orderId;

			SELECT @walletAmount = f_amount
			FROM t_wallet WITH(ROWLOCK)
			WHERE f_memberId = @memberId;

			-- 更新會員總花費
			UPDATE t_totalSpent WITH(ROWLOCK)
			SET f_totalSpent = f_totalSpent - @totalAmount
			WHERE f_memberId = @memberId;

			-- 退款給會員錢包中的金額
			UPDATE t_wallet WITH(ROWLOCK)
			SET f_amount = f_amount + @totalAmount
			WHERE f_memberId = @memberId;

			-- 記錄會員錢包異動
			INSERT INTO t_walletChanges (f_memberId, f_previousAmount, f_finalAmount, f_changeAmount)
			VALUES (@memberId, @walletAmount, @walletAmount + @totalAmount, @totalAmount);

			-- 更新訂單資料
			UPDATE t_order WITH(ROWLOCK)
			SET f_orderStatus = 4, f_deliveryStatus = 6
			WHERE f_Id = @orderId;
		END

        ELSE
        BEGIN
            -- 更新訂單資料
            UPDATE t_order WITH(ROWLOCK)
            SET f_orderStatus = @orderStatus, f_deliveryStatus = @deliveryStatus, f_deliveryMethod = @deliveryMethod
            WHERE f_Id = @orderId;
        END

        COMMIT TRANSACTION; -- 提交交易
        SELECT 1; -- 操作成功，回傳1

    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION; -- 回滾交易
        -- 在這裡處理錯誤
        SELECT 0; -- 錯誤，回傳0
    END CATCH

END
GO
PRINT N'正在创建 过程 [dbo].[pro_sw_editProductData]...';


GO
CREATE PROCEDURE [dbo].[pro_sw_editProductData]
	@productId INT,
	@price INT,
	@stock INT,
	@introduce NVARCHAR(500),
	@introduceEN NVARCHAR(500),
	@warningValue INT,
	@checkStoct BIT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @dbStock INT;
	SELECT @dbStock = f_stock FROM t_product WITH(NOLOCK) WHERE f_id = @productId;

	IF(@checkStoct = 1)
	BEGIN
        UPDATE t_product WITH(ROWLOCK) SET f_price = @price, f_stock = @dbStock + @stock, f_introduceTW = @introduce, f_introduceEN = @introduceEN, f_warningValue = @warningValue WHERE f_id = @productId
		SELECT @@ROWCOUNT
    END
    ELSE
    BEGIN

		IF (@dbStock - @stock = 0)
		BEGIN
			UPDATE t_product WITH(ROWLOCK) SET f_isOpen = 0 WHERE f_id = @productId;
		END

		UPDATE t_product WITH(ROWLOCK) SET f_price = @price, f_stock = @dbStock - @stock, f_introduceTW = @introduce, f_introduceEN = @introduceEN, f_warningValue = @warningValue WHERE f_id = @productId AND @stock <= @dbStock
			
		SELECT @@ROWCOUNT
    END
END
GO
PRINT N'正在创建 过程 [dbo].[pro_sw_editProductStatus]...';


GO
CREATE PROCEDURE [dbo].[pro_sw_editProductStatus]
	@productId INT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @dbStock INT;
	SELECT @dbStock = f_stock FROM t_product WITH(NOLOCK) WHERE f_id = @productId;

	IF (@dbStock <= 0)
    BEGIN
		SELECT 0;
    END
    ELSE
    BEGIN
        UPDATE t_product WITH(ROWLOCK)
		SET f_isOpen = CASE WHEN f_isOpen = 1 THEN 0 ELSE 1 END
		WHERE f_Id = @productId
		SELECT @@ROWCOUNT;
    END
END
GO
PRINT N'正在创建 过程 [dbo].[pro_sw_editPwd]...';


GO
CREATE PROCEDURE [dbo].[pro_sw_editPwd]
    @userId INT,
    @pwd VARCHAR(64)
AS
BEGIN
    SET NOCOUNT ON;

	UPDATE t_userInfo WITH(ROWLOCK) SET f_pwd=@pwd, f_sessionId = NULL WHERE f_id = @userId
	SELECT @@ROWCOUNT 
END
GO
PRINT N'正在创建 过程 [dbo].[pro_sw_editReturnOrder]...';


GO
CREATE PROCEDURE [dbo].[pro_sw_editReturnOrder]
	@orderId INT,
	@boolReturn BIT
AS
BEGIN
	SET NOCOUNT ON;
	IF (@boolReturn = 1)
	BEGIN
		-- 更新訂單資料
		UPDATE t_order WITH(ROWLOCK)
		SET f_orderStatus = 3, f_deliveryStatus = 5
		WHERE f_Id = @orderId;

		SELECT @@ROWCOUNT 
	END
	ELSE
	BEGIN 
		-- 更新訂單資料
		UPDATE t_order WITH(ROWLOCK)
		SET f_orderStatus = 1, f_deliveryStatus = 4
		WHERE f_Id = @orderId;

		SELECT @@ROWCOUNT 
	END
END
GO
PRINT N'正在创建 过程 [dbo].[pro_sw_editRoles]...';


GO
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
GO
PRINT N'正在创建 过程 [dbo].[pro_sw_getAllMemberData]...';


GO
CREATE PROCEDURE [dbo].[pro_sw_getAllMemberData]
	@pageNumber INT,
    @pageSize INT,
	@beforePagesTotal INT,
	@totalCount INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

	SELECT @totalCount = COUNT(f_id) FROM t_member WITH(NOLOCK);

	DECLARE @offset INT

	IF (@beforePagesTotal != CEILING(CAST(@totalCount as float) / @pageSize))
    BEGIN
        SET @offset = 0
    END
    ELSE
    BEGIN
        SET @offset = (@pageNumber - 1) * @pageSize
    END

	SELECT m.f_id, m.f_account, m.f_pwd, m.f_name, m.f_level, m.f_phoneNumber, m.f_email, m.f_accountStatus, w.f_amount, ts.f_totalSpent
	FROM t_member m WITH (NOLOCK)
	LEFT JOIN t_wallet w WITH (NOLOCK) ON w.f_memberId = m.f_id
	LEFT JOIN t_totalSpent ts WITH (NOLOCK) ON ts.f_memberId = m.f_id
	ORDER BY m.f_id
	OFFSET @offset ROWS
	FETCH NEXT @pageSize ROWS ONLY; 
END
GO
PRINT N'正在创建 过程 [dbo].[pro_sw_getAllOrderData]...';


GO
CREATE PROCEDURE [dbo].[pro_sw_getAllOrderData]
	@pageNumber INT,
    @pageSize INT,
	@beforePagesTotal INT,
	@totalCount INT OUTPUT
AS
BEGIN
    	SET NOCOUNT ON;

		SELECT @totalCount = COUNT(f_id) FROM t_order WITH(NOLOCK);

		DECLARE @offset INT

		IF (@beforePagesTotal != CEILING(CAST(@totalCount as float) / @pageSize))
		BEGIN
			SET @offset = 0
		END
		ELSE
		BEGIN
			SET @offset = (@pageNumber - 1) * @pageSize
		END

		SELECT o.f_id, m.f_account, CONVERT(VARCHAR(16), o.f_createdTime, 20) as f_createdTime, o.f_orderStatus, o.f_deliveryStatus, o.f_deliveryMethod, o.f_total
		FROM t_order o WITH (NOLOCK)
		LEFT JOIN t_member m WITH (NOLOCK) ON m.f_id = o.f_memberId
		ORDER BY o.f_id
		OFFSET @offset ROWS
		FETCH NEXT @pageSize ROWS ONLY;  

		SELECT
		COUNT(f_deliveryStatus) AS 'statusAll',
        SUM(CASE WHEN f_deliveryStatus = 1 THEN 1 ELSE 0 END) AS 'status1',
        SUM(CASE WHEN f_deliveryStatus = 2 THEN 1 ELSE 0 END) AS 'status2',
        SUM(CASE WHEN f_deliveryStatus = 3 THEN 1 ELSE 0 END) AS 'status3',
        SUM(CASE WHEN f_deliveryStatus = 4 THEN 1 ELSE 0 END) AS 'status4',
        SUM(CASE WHEN f_deliveryStatus = 5 THEN 1 ELSE 0 END) AS 'status5',
        SUM(CASE WHEN f_deliveryStatus = 6 THEN 1 ELSE 0 END) AS 'status6',
		SUM(CASE WHEN f_orderStatus = 2 THEN 1 ELSE 0 END) AS 'orderStatus2'
		FROM t_order WITH (NOLOCK);
END
GO
PRINT N'正在创建 过程 [dbo].[pro_sw_getAllProductData]...';


GO
CREATE PROCEDURE [dbo].[pro_sw_getAllProductData]
	@pageNumber INT,
    @pageSize INT,
	@beforePagesTotal INT,
	@totalCount INT OUTPUT,
	@languageNum INT
AS
BEGIN
    SET NOCOUNT ON;

	SELECT @totalCount = COUNT(f_id) FROM t_product WITH(NOLOCK);

    DECLARE @offset INT

	IF (@beforePagesTotal != CEILING(CAST(@totalCount as float) / @pageSize))
    BEGIN
        SET @offset = 0
    END
    ELSE
    BEGIN
        SET @offset = (@pageNumber - 1) * @pageSize
    END

	IF(@languageNum = 0)
	BEGIN
		SELECT f_id, f_nameTW AS f_name, f_category, f_price, f_stock, f_isOpen, f_introduceTW AS f_introduce, f_img, f_warningValue
		FROM t_product WITH(NOLOCK)
		ORDER BY f_id
		OFFSET @offset ROWS
		FETCH NEXT @pageSize ROWS ONLY;
	END
	ELSE IF(@languageNum = 1)
	BEGIN
		SELECT f_id, f_nameEN AS f_name, f_category, f_price, f_stock, f_isOpen, f_introduceEN AS f_introduce, f_img, f_warningValue
		FROM t_product WITH(NOLOCK)
		ORDER BY f_id
		OFFSET @offset ROWS
		FETCH NEXT @pageSize ROWS ONLY;
	END
END
GO
PRINT N'正在创建 过程 [dbo].[pro_sw_getAllUserData]...';


GO
CREATE PROCEDURE [dbo].[pro_sw_getAllUserData]
    @pageNumber INT,
    @pageSize INT,  
	@beforePagesTotal INT,
	@totalCount INT OUTPUT  
AS
BEGIN
    SET NOCOUNT ON;

	SELECT @totalCount = COUNT(f_id) FROM t_userInfo WITH(NOLOCK);

    DECLARE @offset INT

	IF (@beforePagesTotal != CEILING(CAST(@totalCount as float) / @pageSize))
    BEGIN
        SET @offset = 0
    END
    ELSE
    BEGIN
        SET @offset = (@pageNumber - 1) * @pageSize
    END

	-- 使用 OFFSET FETCH 方法來實現分頁
	SELECT f_id, f_account, f_roles
	FROM t_userInfo WITH(NOLOCK)
	ORDER BY f_id
	OFFSET @offset ROWS
	FETCH NEXT @pageSize ROWS ONLY;  --offset 10 rows ，将前10条记录舍去，fetch next 10 rows only ，向后再读取10条数据。
END
GO
PRINT N'正在创建 过程 [dbo].[pro_sw_getLowStock]...';


GO
CREATE PROCEDURE [dbo].[pro_sw_getLowStock]
AS
BEGIN
    SET NOCOUNT ON;

	SELECT f_id, f_nameTW, f_nameEN, f_stock, f_isOpen, f_warningValue
	FROM t_product WITH(NOLOCK)
	WHERE f_stock <= f_warningValue AND f_isOpen = 1 AND f_warningValue >0;
END
GO
PRINT N'正在创建 过程 [dbo].[pro_sw_getOrderData]...';


GO
CREATE PROCEDURE [dbo].[pro_sw_getOrderData]
	@deliveryStatusNum INT,
	@pageNumber INT,
    @pageSize INT,
	@beforePagesTotal INT,
	@totalCount INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

	SELECT @totalCount = COUNT(f_id) 
	FROM t_order WITH(NOLOCK)
	WHERE f_deliveryStatus = @deliveryStatusNum;

    DECLARE @offset INT

	IF (@beforePagesTotal != CEILING(CAST(@totalCount as float) / @pageSize))
    BEGIN
        SET @offset = 0
    END
    ELSE
    BEGIN
        SET @offset = (@pageNumber - 1) * @pageSize
    END

    SELECT o.f_id, m.f_account, CONVERT(VARCHAR(16), o.f_createdTime, 20) as f_createdTime, o.f_orderStatus, o.f_deliveryStatus, o.f_deliveryMethod, o.f_total
	FROM t_order o WITH (NOLOCK)
	LEFT JOIN t_member m WITH (NOLOCK) ON m.f_id = o.f_memberId
	WHERE o.f_deliveryStatus = @deliveryStatusNum
    ORDER BY o.f_id
    OFFSET @offset ROWS
    FETCH NEXT @pageSize ROWS ONLY;  

	SELECT
	COUNT(f_deliveryStatus) AS 'statusAll',
    SUM(CASE WHEN f_deliveryStatus = 1 THEN 1 ELSE 0 END) AS 'status1',
    SUM(CASE WHEN f_deliveryStatus = 2 THEN 1 ELSE 0 END) AS 'status2',
	SUM(CASE WHEN f_deliveryStatus = 3 THEN 1 ELSE 0 END) AS 'status3',
	SUM(CASE WHEN f_deliveryStatus = 4 THEN 1 ELSE 0 END) AS 'status4',
	SUM(CASE WHEN f_deliveryStatus = 5 THEN 1 ELSE 0 END) AS 'status5',
	SUM(CASE WHEN f_deliveryStatus = 6 THEN 1 ELSE 0 END) AS 'status6',
	SUM(CASE WHEN f_orderStatus = 2 THEN 1 ELSE 0 END) AS 'orderStatus2'
	FROM t_order WITH (NOLOCK);
END
GO
PRINT N'正在创建 过程 [dbo].[pro_sw_getOrderDetailsData]...';


GO
CREATE PROCEDURE [dbo].[pro_sw_getOrderDetailsData]
	@orderId INT,
	@languageNum INT
AS
BEGIN
    SET NOCOUNT ON;

	IF(@languageNum = 0)
    BEGIN
        SELECT f_productNameTW AS f_productName, f_productPrice, f_productCategory, f_quantity, f_subtotal
		FROM t_orderDetails WITH (NOLOCK)
		WHERE f_orderId = @orderId
    END
    ELSE IF(@languageNum = 1)
    BEGIN
        SELECT f_productNameEN AS f_productName, f_productPrice, f_productCategory, f_quantity, f_subtotal
		FROM t_orderDetails WITH (NOLOCK)
		WHERE f_orderId = @orderId
    END
END
GO
PRINT N'正在创建 过程 [dbo].[pro_sw_getProductData]...';


GO
CREATE PROCEDURE [dbo].[pro_sw_getProductData]
	@productId INT
AS
BEGIN

	SET NOCOUNT ON;
	SELECT f_id, f_nameTW, f_nameEN, f_category, f_price, f_stock, f_createdUser, CONVERT(VARCHAR(16), f_createdTime, 20) as f_createdTime, f_introduceTW, f_introduceEN, f_img, f_warningValue
	FROM t_product WITH(NOLOCK) 
	WHERE f_id = @productId;
END
GO
PRINT N'正在创建 过程 [dbo].[pro_sw_getPwdAndEditSessionId]...';


GO
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
GO
PRINT N'正在创建 过程 [dbo].[pro_sw_getReturnOrderData]...';


GO
CREATE PROCEDURE [dbo].[pro_sw_getReturnOrderData]
	@pageNumber INT,
    @pageSize INT,
	@beforePagesTotal INT,
	@totalCount INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT @totalCount = COUNT(f_id) 
	FROM t_order WITH(NOLOCK)
	WHERE f_orderStatus = 2;

    DECLARE @offset INT

	IF (@beforePagesTotal != CEILING(CAST(@totalCount as float) / @pageSize))
    BEGIN
        SET @offset = 0
    END
    ELSE
    BEGIN
        SET @offset = (@pageNumber - 1) * @pageSize
    END

    -- 使用 OFFSET FETCH 方法來實現分頁
    SELECT o.f_id, m.f_account, CONVERT(VARCHAR(16), o.f_createdTime, 20) as f_createdTime, o.f_orderStatus, o.f_deliveryStatus, o.f_deliveryMethod, o.f_total
	FROM t_order o WITH (NOLOCK)
	LEFT JOIN t_member m WITH (NOLOCK) ON m.f_id = o.f_memberId
	WHERE o.f_orderStatus = 2
    ORDER BY o.f_id
    OFFSET @offset ROWS
    FETCH NEXT @pageSize ROWS ONLY;  

	SELECT
	COUNT(f_deliveryStatus) AS 'statusAll',
    SUM(CASE WHEN f_deliveryStatus = 1 THEN 1 ELSE 0 END) AS 'status1',
    SUM(CASE WHEN f_deliveryStatus = 2 THEN 1 ELSE 0 END) AS 'status2',
	SUM(CASE WHEN f_deliveryStatus = 3 THEN 1 ELSE 0 END) AS 'status3',
	SUM(CASE WHEN f_deliveryStatus = 4 THEN 1 ELSE 0 END) AS 'status4',
	SUM(CASE WHEN f_deliveryStatus = 5 THEN 1 ELSE 0 END) AS 'status5',
	SUM(CASE WHEN f_deliveryStatus = 6 THEN 1 ELSE 0 END) AS 'status6',
	SUM(CASE WHEN f_orderStatus = 2 THEN 1 ELSE 0 END) AS 'orderStatus2'
	FROM t_order WITH (NOLOCK);
END
GO
PRINT N'正在创建 过程 [dbo].[pro_sw_getSearchProductData]...';


GO
CREATE PROCEDURE [dbo].[pro_sw_getSearchProductData]
	@category INT,
	@name NVARCHAR(100),
	@allMinorCategories BIT,
	@allBrand BIT,
	@pageNumber INT,
    @pageSize INT,
	@beforePagesTotal INT,
	@totalCount INT OUTPUT,
	@languageNum INT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @offset INT

	IF(@languageNum = 0)
    BEGIN
        IF (@allMinorCategories = 0)
		BEGIN
			SELECT @totalCount = COUNT(f_id)
			FROM t_product WITH(NOLOCK)
			WHERE (
					(@allBrand = 0 AND f_category = @category)
					OR 
					(@allBrand = 1 AND f_category BETWEEN @category AND (@category / 100) * 100 + 99)
				  )
				  AND f_nameTW LIKE '%' + @name + '%';	
			
			IF (@beforePagesTotal != CEILING(CAST(@totalCount as float) / @pageSize))
			BEGIN
				SET @offset = 0
			END
			ELSE
			BEGIN
				SET @offset = (@pageNumber - 1) * @pageSize
			END

			SELECT f_id, f_nameTW AS f_name, f_category, f_price, f_stock, f_isOpen, f_introduceTW AS f_introduce, f_img, f_warningValue
			FROM t_product WITH(NOLOCK)
			WHERE (
					(@allBrand = 0 AND f_category = @category)
					OR 
					(@allBrand = 1 AND f_category BETWEEN @category AND (@category / 100) * 100 + 99)
				  )
				  AND f_nameTW LIKE '%' + @name + '%'
			ORDER BY f_id
			OFFSET @offset ROWS
			FETCH NEXT @pageSize ROWS ONLY; 
		END
		ELSE
		BEGIN
			SELECT @totalCount = COUNT(f_id)
			FROM t_product WITH(NOLOCK)
			WHERE (
					(@allBrand = 0 AND f_category BETWEEN @category AND (@category / 10000) * 10000 + 9999 AND f_category % 100 = @category % 100)
					OR 
					(@allBrand = 1 AND f_category BETWEEN @category AND (@category / 10000) * 10000 + 9999)
				  )
				  AND f_nameTW LIKE '%' + @name + '%';	

			IF (@beforePagesTotal != CEILING(CAST(@totalCount as float) / @pageSize))
			BEGIN
				SET @offset = 0
			END
			ELSE
			BEGIN
				SET @offset = (@pageNumber - 1) * @pageSize
			END			

			SELECT f_id, f_nameTW AS f_name, f_category, f_price, f_stock, f_isOpen, f_introduceTW AS f_introduce, f_img, f_warningValue
			FROM t_product WITH(NOLOCK)
			WHERE (
					(@allBrand = 0 AND f_category BETWEEN @category AND (@category / 10000) * 10000 + 9999 AND f_category % 100 = @category % 100)
					OR
					(@allBrand = 1 AND f_category BETWEEN @category AND (@category / 10000) * 10000 + 9999)
				  )
				  AND f_nameTW LIKE '%' + @name + '%'
			ORDER BY f_id
			OFFSET @offset ROWS
			FETCH NEXT @pageSize ROWS ONLY; 
		END
    END
    ELSE IF(@languageNum = 1)
    BEGIN
        IF (@allMinorCategories = 0)
		BEGIN
			SELECT @totalCount = COUNT(f_id)
			FROM t_product WITH(NOLOCK)
			WHERE (
					(@allBrand = 0 AND f_category = @category)
					OR 
					(@allBrand = 1 AND f_category BETWEEN @category AND (@category / 100) * 100 + 99)
				  )
				  AND f_nameEN LIKE '%' + @name + '%';	
			
			IF (@beforePagesTotal != CEILING(CAST(@totalCount as float) / @pageSize))
			BEGIN
				SET @offset = 0
			END
			ELSE
			BEGIN
				SET @offset = (@pageNumber - 1) * @pageSize
			END

			SELECT f_id, f_nameEN AS f_name, f_category, f_price, f_stock, f_isOpen, f_introduceEN AS f_introduce, f_img, f_warningValue
			FROM t_product WITH(NOLOCK)
			WHERE (
					(@allBrand = 0 AND f_category = @category)
					OR 
					(@allBrand = 1 AND f_category BETWEEN @category AND (@category / 100) * 100 + 99)
				  )
				  AND f_nameEN LIKE '%' + @name + '%'
			ORDER BY f_id
			OFFSET @offset ROWS
			FETCH NEXT @pageSize ROWS ONLY; 
		END
		ELSE
		BEGIN
			SELECT @totalCount = COUNT(f_id)
			FROM t_product WITH(NOLOCK)
			WHERE (
					(@allBrand = 0 AND f_category BETWEEN @category AND (@category / 10000) * 10000 + 9999 AND f_category % 100 = @category % 100)
					OR 
					(@allBrand = 1 AND f_category BETWEEN @category AND (@category / 10000) * 10000 + 9999)
				  )
				  AND f_nameEN LIKE '%' + @name + '%';	
			
			IF (@beforePagesTotal != CEILING(CAST(@totalCount as float) / @pageSize))
			BEGIN
				SET @offset = 0
			END
			ELSE
			BEGIN
				SET @offset = (@pageNumber - 1) * @pageSize
			END

			SELECT f_id, f_nameEN AS f_name, f_category, f_price, f_stock, f_isOpen, f_introduceEN AS f_introduce, f_img, f_warningValue
			FROM t_product WITH(NOLOCK)
			WHERE (
					(@allBrand = 0 AND f_category BETWEEN @category AND (@category / 10000) * 10000 + 9999 AND f_category % 100 = @category % 100)
					OR
					(@allBrand = 1 AND f_category BETWEEN @category AND (@category / 10000) * 10000 + 9999)
				  )
				  AND f_nameEN LIKE '%' + @name + '%'
			ORDER BY f_id
			OFFSET @offset ROWS
			FETCH NEXT @pageSize ROWS ONLY; 
		END
    END
END
GO
PRINT N'正在创建 过程 [dbo].[pro_sw_getSessionId]...';


GO
CREATE PROCEDURE [dbo].[pro_sw_getSessionId]
	@userId INT,
	@sessionId VARCHAR(24)
AS
BEGIN
	DECLARE @dbSessionId VARCHAR(24);

    SET NOCOUNT ON;
    SELECT @dbSessionId = f_sessionId 
	FROM t_userInfo WITH(NOLOCK) 
	WHERE f_id = @userId

	IF (@dbSessionId IS NOT NULL AND @sessionId != @dbSessionId)
    BEGIN		
        SELECT 0; -- 返回登入失敗
    END
    ELSE
    BEGIN
		SELECT 1; -- 返回登入成功
    END
END
GO
PRINT N'正在创建 过程 [dbo].[pro_sw_getUserData]...';


GO
CREATE PROCEDURE pro_sw_getUserData
	@userId INT
AS
BEGIN

	SET NOCOUNT ON;
	SELECT f_id, f_account, f_roles FROM t_userInfo WITH(NOLOCK) WHERE f_id = @userId
END
GO
/*
后期部署脚本模板							
--------------------------------------------------------------------------------------
 此文件包含将附加到生成脚本中的 SQL 语句。		
 使用 SQLCMD 语法将文件包含到后期部署脚本中。			
 示例:      :r .\myfile.sql								
 使用 SQLCMD 语法引用后期部署脚本中的变量。		
 示例:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
USE ShoppingWeb
GO
INSERT INTO t_userInfo(f_account, f_pwd, f_roles, f_sessionId)
        VALUES('adminstrator', '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 1, NULL);
GO

GO
DECLARE @VarDecimalSupported AS BIT;

SELECT @VarDecimalSupported = 0;

IF ((ServerProperty(N'EngineEdition') = 3)
    AND (((@@microsoftversion / power(2, 24) = 9)
          AND (@@microsoftversion & 0xffff >= 3024))
         OR ((@@microsoftversion / power(2, 24) = 10)
             AND (@@microsoftversion & 0xffff >= 1600))))
    SELECT @VarDecimalSupported = 1;

IF (@VarDecimalSupported > 0)
    BEGIN
        EXECUTE sp_db_vardecimal_storage_format N'$(DatabaseName)', 'ON';
    END


GO
PRINT N'更新完成。';


GO
