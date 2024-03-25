DROP TABLE IF EXISTS neo.UserTable;

CREATE TABLE neo.UserTable (
    id int PRIMARY KEY,
    firstName nvarchar(100) NULL,
    lastName nvarchar(100) NULL,
    image nvarchar(max) NULL,
    joinedDate datetime,
    employment nvarchar(100),
    experience nvarchar(100),
    incomePerMonth float
);