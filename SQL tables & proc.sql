--============================================================================
--      Ensure the target database exists (optional step)
--============================================================================
IF DB_ID('DexDb') IS NULL
    CREATE DATABASE DexDb;
GO

USE DexDb;
GO

--============================================================================
--          Cleanup existing tables and procedures if they exist 
--============================================================================
IF OBJECT_ID('DEXLaneMeter', 'U') IS NOT NULL
    DROP TABLE DEXLaneMeter;
GO
IF OBJECT_ID('DEXMeter', 'U') IS NOT NULL
    DROP TABLE DEXMeter;
GO
IF OBJECT_ID('SaveDEXLaneMeter', 'P') IS NOT NULL
    DROP PROCEDURE SaveDEXLaneMeter;
GO
IF OBJECT_ID('SaveDEXMeter', 'P') IS NOT NULL
    DROP PROCEDURE SaveDEXMeter;
GO
--============================================================================
-- Create the DEXMeter and DEXLaneMeter tables
--============================================================================


--============================================================================
-- Table: DEXMeter
-- Stores DEX-level data (machine, timestamp, serial number, etc.)
--============================================================================
CREATE TABLE DEXMeter (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Machine CHAR(1) NOT NULL,                             -- Machine identifier: 'A' or 'B'
    DEXDateTime DATETIME NOT NULL,                        -- DEX file timestamp
    MachineSerialNumber VARCHAR(50),                      -- Field ID101
    ValueOfPaidVends DECIMAL(10, 2)                       -- Field VA101
);
--============================================================================
-- Enforce uniqueness of Machine and DEXDateTime to prevent duplicates
--============================================================================
ALTER TABLE DEXMeter
ADD CONSTRAINT UQ_Machine_DEXDateTime UNIQUE (Machine, DEXDateTime);
GO

--============================================================================
-- Table: DEXLaneMeter
-- Stores lane-level sales data extracted from DEX file
--============================================================================

CREATE TABLE DEXLaneMeter (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    DexMeterId INT NOT NULL,                              -- Foreign key to DEXMeter
    ProductIdentifier VARCHAR(20),                        -- Field PA101
    Price DECIMAL(10, 2),                                 -- Field PA102
    NumberOfVends INT,                                    -- Field PA201
    ValueOfPaidSales DECIMAL(10, 2),                      -- Field PA202
    CONSTRAINT FK_Lane_Meter FOREIGN KEY (DexMeterId)
        REFERENCES DEXMeter(Id)
        ON DELETE CASCADE
);
--Index to speed up lookups by DexMeterId
CREATE NONCLUSTERED INDEX IX_DexLaneMeter_DexMeterId ON DEXLaneMeter(DexMeterId);  
GO


--============================================================================
-- Stored Procedure: SaveDEXMeter
-- Inserts a DEXMeter and returns the new ID
--============================================================================
CREATE PROCEDURE SaveDEXMeter
    @Machine CHAR(1),
    @DEXDateTime DATETIME,
    @MachineSerialNumber VARCHAR(50),
    @ValueOfPaidVends DECIMAL(10,2)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ExistingId INT;
    -- Prevent duplicate inserts (especially in high-concurrency)
    SELECT @ExistingId = Id
    FROM DEXMeter
    WHERE Machine = @Machine AND DEXDateTime = @DEXDateTime;

    IF @ExistingId IS NOT NULL
    BEGIN
        SELECT @ExistingId;
        RETURN;
    END

    INSERT INTO DEXMeter (Machine, DEXDateTime, MachineSerialNumber, ValueOfPaidVends)
    VALUES (@Machine, @DEXDateTime, @MachineSerialNumber, @ValueOfPaidVends);

    SELECT SCOPE_IDENTITY();
END
GO


--============================================================================
 --Stored Procedure: SaveDEXLaneMeter
 --Inserts a DEXLaneMeter record and associates it with a DEXMeter
 --============================================================================
CREATE PROCEDURE SaveDEXLaneMeter
    @DexMeterId INT,
    @ProductIdentifier VARCHAR(20),
    @Price DECIMAL(10,2),
    @NumberOfVends INT,
    @ValueOfPaidSales DECIMAL(10,2)
AS
BEGIN
    INSERT INTO DEXLaneMeter (DexMeterId, ProductIdentifier, Price, NumberOfVends, ValueOfPaidSales)
    VALUES (@DexMeterId, @ProductIdentifier, @Price, @NumberOfVends, @ValueOfPaidSales);
END
GO

--verify the data once the endpoint was called
SELECT * FROM DEXMeter;
SELECT * FROM DEXLaneMeter;