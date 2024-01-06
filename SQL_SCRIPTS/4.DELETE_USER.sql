USE [UsersManagement]
GO

/****** Object:  StoredProcedure [dbo].[DELETE_USER]    Script Date: 06.1.2024 19:20:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Dimche Ivanoski
-- Create date: <Create Date,,>
-- Description:	Sets the user as deleted
-- =============================================
CREATE PROCEDURE [dbo].[DELETE_USER]
(
	@ID UNIQUEIDENTIFIER,
	@IS_DELETED BIT
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [dbo].[Users]
	   SET [IsDeleted] = ISNULL(@IS_DELETED, [IsDeleted])
	   WHERE [Id] = @ID

END
GO