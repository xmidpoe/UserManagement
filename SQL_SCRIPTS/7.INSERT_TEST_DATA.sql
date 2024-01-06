USE [UsersManagement]
GO

INSERT INTO [dbo].[Users]([Id],[UserName],[FullName] ,[Email],[MobileNumber],[Language],[Culture],[Password],[DateCreated],[IsDeleted])
VALUES (NEWID(),'User 1','Full Name 1','email1@email.com','123123','en-US','en-US','password1',GETUTCDATE(),0)
GO

INSERT INTO [dbo].[Users]([Id],[UserName],[FullName] ,[Email],[MobileNumber],[Language],[Culture],[Password],[DateCreated],[IsDeleted])
VALUES (NEWID(),'User 2','Full Name 2','email2@email.com','123123','en-US','en-US','password2',GETUTCDATE(),0)
GO