# NetCoreWithADONET
.net core project with ado.net stored proc calls

Table and stored proc creation below

GO

/****** Object:  Table [dbo].[Users]    Script Date: 6/30/2018 10:49:15 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
	[EmailId] [nvarchar](500) NULL,
	[Mobile] [nvarchar](20) NULL,
	[Address] [nvarchar](max) NULL,
	[IsActive] [bit] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


GetUsers Stored Procedure.

SET ANSI_NULLS ON  
GO  
SET QUOTED_IDENTIFIER ON  
GO  
-- =============================================  
-- Author:      <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- EXEC GetUsers  
-- =============================================  
ALTER PROCEDURE [dbo].[GetUsers]  
AS  
BEGIN  
    SET NOCOUNT ON;  
    SELECT * FROM Users(NOLOCK) ORDER BY Id ASC  
END  
SaveUser Stored Procedure (Add/Edit)

SET ANSI_NULLS ON  
GO  
SET QUOTED_IDENTIFIER ON  
GO  
-- =============================================  
-- Author:      <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- =============================================  
ALTER PROCEDURE [dbo].[SaveUser]  
(  
@Id INT,  
@Name NVARCHAR(MAX),  
@EmailId NVARCHAR(MAX),  
@Mobile NVARCHAR(20),  
@Address NVARCHAR(MAX),  
@ReturnCode NVARCHAR(20) OUTPUT  
)  
AS  
BEGIN  
    SET @ReturnCode = 'C200'  
    IF(@Id <> 0)  
    BEGIN  
        IF EXISTS (SELECT 1 FROM Users WHERE EmailId = @EmailId AND Id <> @Id)  
        BEGIN  
            SET @ReturnCode = 'C201'  
            RETURN  
        END  
        IF EXISTS (SELECT 1 FROM Users WHERE Mobile = @Mobile AND Id <> @Id)  
        BEGIN  
            SET @ReturnCode = 'C202'  
            RETURN  
        END  
  
        UPDATE Users SET  
        Name = @Name,  
        EmailId = @EmailId,  
        Mobile = @Mobile,  
        Address = @Address,  
        IsActive = 1  
        WHERE Id = @Id  
  
        SET @ReturnCode = 'C200'  
    END  
    ELSE  
    BEGIN  
        IF EXISTS (SELECT 1 FROM Users WHERE EmailId = @EmailId)  
        BEGIN  
            SET @ReturnCode = 'C201'  
            RETURN  
        END  
        IF EXISTS (SELECT 1 FROM Users WHERE Mobile = @Mobile)  
        BEGIN  
            SET @ReturnCode = 'C202'  
            RETURN  
        END  
  
        INSERT INTO Users (Name,EmailId,Mobile,Address,IsActive)  
        VALUES (@Name,@EmailId,@Mobile,@Address,1)  
  
        SET @ReturnCode = 'C200'  
    END  
END  
  
  
Delete User Stored Procedure  
SET ANSI_NULLS ON  
GO  
SET QUOTED_IDENTIFIER ON  
GO  
-- =============================================  
-- Author:      <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- =============================================  
ALTER PROCEDURE [dbo].[DeleteUser]  
(  
@Id INT,  
@ReturnCode NVARCHAR(20) OUTPUT  
)  
AS  
BEGIN  
    SET NOCOUNT ON;  
    SET @ReturnCode = 'C200'  
    IF NOT EXISTS (SELECT 1 FROM Users WHERE Id = @Id)  
    BEGIN  
        SET @ReturnCode ='C203'  
        RETURN  
    END  
    ELSE  
    BEGIN  
        DELETE FROM Users WHERE Id = @Id  
        SET @ReturnCode = 'C200'  
        RETURN  
    END  
END  
