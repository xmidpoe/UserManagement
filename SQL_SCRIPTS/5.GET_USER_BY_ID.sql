USE [UsersManagement]
GO

/****** Object:  StoredProcedure [dbo].[GET_USER_BY_ID]    Script Date: 06.1.2024 19:21:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		DIMCHE IVANOSKI
-- Create date: <Create Date,,>
-- Description:	RETURNS USER FROM USERS TABLE BY ID
-- =============================================
CREATE PROCEDURE [dbo].[GET_USER_BY_ID] 
(
	@ID UNIQUEIDENTIFIER
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [Id], [UserName] ,[FullName], [Email], [MobileNumber], [Language], [Culture], [Password], [DateCreated], [DateUpdated]
	FROM [dbo].[Users]
	WHERE [Id] = @ID AND [IsDeleted] = 0
END
GO