IF OBJECT_ID('dbo.Person', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Person (
        Id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        FirstName NVARCHAR(80) NOT NULL,
        LastName NVARCHAR(80) NOT NULL,
        Gender NVARCHAR(6) NOT NULL,
        Address NVARCHAR(100) NOT NULL,
        Ativo BIT NOT NULL DEFAULT(1)
    );
END;
