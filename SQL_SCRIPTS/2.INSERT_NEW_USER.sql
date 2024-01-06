USE [UsersManagement]
GO

/****** Object:  StoredProcedure [dbo].[INSERT_NEW_USER]    Script Date: 06.1.2024 19:17:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		DIMCHE IVANOSKI
-- Create date: 
-- Description:	CREATES NEW USER INTO DATABASE
-- =============================================
CREATE PROCEDURE [dbo].[INSERT_NEW_USER]
(
	@ID UNIQUEIDENTIFIER,
	@USER_NAME NVARCHAR(50),
	@FULL_NAME NVARCHAR(100),
	@EMAIL NVARCHAR(100),
	@MOBILE_NUMBER NVARCHAR(20) = NULL,
	@LANGUAGE NVARCHAR(10),
	@CULTURE NVARCHAR(10),
	@PASSWORD NVARCHAR(255)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [dbo].[Users] ([Id], [UserName], [FullName], [Email], [MobileNumber], [Language], [Culture], [Password], [DateCreated])
    VALUES (@ID, @USER_NAME, @FULL_NAME, @EMAIL, @MOBILE_NUMBER, @LANGUAGE, @CULTURE, @PASSWORD, GETUTCDATE())

END
GO