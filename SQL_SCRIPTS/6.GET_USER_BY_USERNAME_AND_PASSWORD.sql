USE [UsersManagement]
GO

/****** Object:  StoredProcedure [dbo].[GET_USER_BY_USERNAME_AND_PASSWORD]    Script Date: 06.1.2024 19:23:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		DIMCHE IVANOSKI
-- Create date: <Create Date,,>
-- Description:	RETURNS USER FROM USERS TABLE USER NAME AND PASSWORD
-- =============================================
CREATE PROCEDURE [dbo].[GET_USER_BY_USERNAME_AND_PASSWORD] 
(
	@USER_NAME NVARCHAR(50),
	@PASSWORD NVARCHAR(255)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [Id], [UserName] ,[FullName], [Email], [MobileNumber], [Language], [Culture], [Password], [DateCreated], [DateUpdated]
	FROM [dbo].[Users]
	WHERE [UserName] = @USER_NAME AND [Password] = @PASSWORD AND [IsDeleted] = 0
END
GO
