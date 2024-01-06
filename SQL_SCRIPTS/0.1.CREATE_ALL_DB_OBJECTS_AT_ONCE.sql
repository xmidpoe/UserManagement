USE [UsersManagement]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 06.1.2024 22:03:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[FullName] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[MobileNumber] [nvarchar](20) NULL,
	[Language] [nvarchar](10) NULL,
	[Culture] [nvarchar](10) NULL,
	[Password] [nvarchar](255) NOT NULL,
	[DateCreated] [datetime] NULL,
	[DateUpdated] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK__Users__3214EC07E4211AD1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [DF_Users_Email] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  StoredProcedure [dbo].[DELETE_USER]    Script Date: 06.1.2024 22:03:20 ******/
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
/****** Object:  StoredProcedure [dbo].[GET_USER_BY_ID]    Script Date: 06.1.2024 22:03:20 ******/
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
/****** Object:  StoredProcedure [dbo].[GET_USER_BY_USERNAME_AND_PASSWORD]    Script Date: 06.1.2024 22:03:20 ******/
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
/****** Object:  StoredProcedure [dbo].[INSERT_NEW_USER]    Script Date: 06.1.2024 22:03:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		DIMCHE IVANOSKI
-- Create date: 25.12.2023
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
/****** Object:  StoredProcedure [dbo].[UPDATE_USER]    Script Date: 06.1.2024 22:03:20 ******/
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

/**INSERT TEST DATA**/
INSERT INTO [dbo].[Users]([Id],[UserName],[FullName] ,[Email],[MobileNumber],[Language],[Culture],[Password],[DateCreated],[IsDeleted])
VALUES (NEWID(),'User 1','Full Name 1','email1@email.com','123123','en-US','en-US','password1',GETUTCDATE(),0)
GO

INSERT INTO [dbo].[Users]([Id],[UserName],[FullName] ,[Email],[MobileNumber],[Language],[Culture],[Password],[DateCreated],[IsDeleted])
VALUES (NEWID(),'User 2','Full Name 2','email2@email.com','123123','en-US','en-US','password2',GETUTCDATE(),0)
GO