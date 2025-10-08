IF OBJECT_ID('dbo.Users', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Users (
        IdUsuario INT IDENTITY(1,1) NOT NULL,
        User_Name VARCHAR(50) NOT NULL DEFAULT '0',
        Password VARCHAR(130) NOT NULL DEFAULT '0',
        Full_Name VARCHAR(120) NOT NULL,
        Refresh_Token VARCHAR(500) NOT NULL DEFAULT '0',
        Refresh_Token_expiry_time DATETIME NULL DEFAULT NULL,

        CONSTRAINT PK_Users PRIMARY KEY (IdUsuario),
        CONSTRAINT UQ_users_user_name UNIQUE (User_Name)
    );
END;