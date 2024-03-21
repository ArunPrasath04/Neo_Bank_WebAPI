DROP PROCEDURE IF EXISTS [neo].[NB_API_Login]
GO

CREATE PROCEDURE [neo].[NB_API_Login]
@username nvarchar(255),
@email nvarchar(255),
@password nvarchar(max) null,
@isGoogle int,
@retVal int output
AS
BEGIN 
	IF (@isGoogle = 1)
		BEGIN
			IF NOT EXISTS(select 'X' from neo.AuthData where email = @email AND isGoogle = 1)
			BEGIN
				IF EXISTS(select 'X' from neo.AuthData where email = @email AND isGoogle = 0)
					BEGIN
						SET @retVal = -1 --Google user has already registered through application || login with password
					END
				ELSE
					BEGIN
						INSERT INTO neo.AuthData(username,email,active,isGoogle)
						VALUES(@username,@email,1,@isGoogle)

						SET @retVal = 1 --success
					END
			END
				
			ELSE IF EXISTS(select 'X' from neo.AuthData where email = @email AND isGoogle = 1)
				BEGIN
					SET @retVal = 1
				END
		END
	ELSE
		BEGIN
			IF EXISTS(select 'X' from neo.AuthData where email = @email)
				BEGIN
					DECLARE @savedPw nvarchar(max)
					SELECT @savedPw = password from neo.AuthData where email = @email AND isGoogle = 0

					IF (@password = @savedPw)
						BEGIN
							SET @retVal = 1
						END
					ELSE
						BEGIN
							SET @retVal = -2 --incorrect pw
						END
				END
			ELSE
				BEGIN
					SET @retVal = -3 --email dont exist 
				END
		END
END