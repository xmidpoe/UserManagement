USE [UsersManagement]
GO

/****** Object:  StoredProcedure [dbo].[UPDATE_USER]    Script Date: 06.1.2024 19:19:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Dimche Ivanoski
-- Create date: <Create Date,,>
-- Description:	Updates current user
-- =============================================
CREATE PROCEDURE [dbo].[UPDATE_USER]
(
	@ID UNIQUEIDENTIFIER,
	@USER_NAME NVARCHAR(50) = NULL,
	@FULL_NAME NVARCHAR(100) = NULL,
	@EMAIL NVARCHAR(100) = NULL,
	@MOBILE_NUMBER NVARCHAR(20) = NULL,
	@LANGUAGE NVARCHAR(10) = NULL,
	@CULTURE NVARCHAR(10) = NULL,
	@PASSWORD NVARCHAR(255) = NULL,
	@IS_DELETED BIT = NULL
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [dbo].[Users]
	   SET [UserName] =	ISNULL(@USER_NAME, [UserName])
		  ,[FullName] = ISNULL(@FULL_NAME, [FullName])
		  ,[Email] = ISNULL(@EMAIL, [Email])
		  ,[MobileNumber] = ISNULL(@MOBILE_NUMBER, [MobileNumber])
		  ,[Language] = ISNULL(@LANGUAGE, [Language])
		  ,[Culture] = ISNULL(@CULTURE, [Culture])
		  ,[Password] = ISNULL(@PASSWORD, [Password])
		  ,[DateUpdated] = GETUTCDATE()
		  ,[IsDeleted] = ISNULL(@IS_DELETED, [IsDeleted])
		WHERE [Id] = @ID

END
GO


