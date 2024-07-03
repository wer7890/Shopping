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
