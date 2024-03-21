DROP TABLE neo.AuthData

CREATE TABLE AuthData (
	id int identity(1,1) PRIMARY KEY,
	username nvarchar(255),
	email nvarchar(255) ,
	password nvarchar(max) null,
	emailSalt nvarchar(max) null,
	isGoogle tinyint,
	active int
)