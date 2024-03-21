DROP PROCEDURE IF EXISTS [neo].[NB_API_CreateUser]
GO

CREATE PROCEDURE [neo].[NB_API_CreateUser]
@username nvarchar(255),
@email nvarchar(255),
@password nvarchar(max) null,
@firstName nvarchar(255),
@lastName nvarchar(255),
@retVal int output
AS
BEGIN 
	IF NOT EXISTS(select 'X' from neo.AuthData where email = @email)
		BEGIN
			INSERT INTO neo.AuthData(username,email,password, isGoogle, active)
			VALUES(@username,@email,password,0,1)
			SET @retVal = 1
		END	
	ELSE 
		BEGIN
			SET @retVal = -1 -- user already exists
		END
END