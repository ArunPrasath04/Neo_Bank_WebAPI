DROP PROCEDURE IF EXISTS [neo].[NB_API_GetSaltbyEmail]
GO

CREATE PROCEDURE [neo].[NB_API_GetSaltbyEmail]
@email nvarchar(255),
@code nvarchar(1000) output
AS
BEGIN 
	SELECT @code = emailSalt from neo.AuthData where email = @email;

	IF (@code IS NULL) SET @code = '';
END