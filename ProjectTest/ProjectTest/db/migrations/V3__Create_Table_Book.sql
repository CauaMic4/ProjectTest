IF OBJECT_ID('dbo.Book', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Book (
        Id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        Author NVARCHAR(80) NOT NULL,
        Title NVARCHAR(80) NOT NULL,
        Launch_date DATE NOT NULL,
        Price DECIMAL(10,2) NOT NULL,
        Ativo BIT NOT NULL DEFAULT(1)
    );
END;
