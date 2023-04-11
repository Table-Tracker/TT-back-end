CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET utf8mb4;

DROP PROCEDURE IF EXISTS `POMELO_BEFORE_DROP_PRIMARY_KEY`;
DELIMITER //
CREATE PROCEDURE `POMELO_BEFORE_DROP_PRIMARY_KEY`(IN `SCHEMA_NAME_ARGUMENT` VARCHAR(255), IN `TABLE_NAME_ARGUMENT` VARCHAR(255))
BEGIN
	DECLARE HAS_AUTO_INCREMENT_ID TINYINT(1);
	DECLARE PRIMARY_KEY_COLUMN_NAME VARCHAR(255);
	DECLARE PRIMARY_KEY_TYPE VARCHAR(255);
	DECLARE SQL_EXP VARCHAR(1000);
	SELECT COUNT(*)
		INTO HAS_AUTO_INCREMENT_ID
		FROM `information_schema`.`COLUMNS`
		WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
			AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
			AND `Extra` = 'auto_increment'
			AND `COLUMN_KEY` = 'PRI'
			LIMIT 1;
	IF HAS_AUTO_INCREMENT_ID THEN
		SELECT `COLUMN_TYPE`
			INTO PRIMARY_KEY_TYPE
			FROM `information_schema`.`COLUMNS`
			WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
				AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
				AND `COLUMN_KEY` = 'PRI'
			LIMIT 1;
		SELECT `COLUMN_NAME`
			INTO PRIMARY_KEY_COLUMN_NAME
			FROM `information_schema`.`COLUMNS`
			WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
				AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
				AND `COLUMN_KEY` = 'PRI'
			LIMIT 1;
		SET SQL_EXP = CONCAT('ALTER TABLE `', (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA())), '`.`', TABLE_NAME_ARGUMENT, '` MODIFY COLUMN `', PRIMARY_KEY_COLUMN_NAME, '` ', PRIMARY_KEY_TYPE, ' NOT NULL;');
		SET @SQL_EXP = SQL_EXP;
		PREPARE SQL_EXP_EXECUTE FROM @SQL_EXP;
		EXECUTE SQL_EXP_EXECUTE;
		DEALLOCATE PREPARE SQL_EXP_EXECUTE;
	END IF;
END //
DELIMITER ;

DROP PROCEDURE IF EXISTS `POMELO_AFTER_ADD_PRIMARY_KEY`;
DELIMITER //
CREATE PROCEDURE `POMELO_AFTER_ADD_PRIMARY_KEY`(IN `SCHEMA_NAME_ARGUMENT` VARCHAR(255), IN `TABLE_NAME_ARGUMENT` VARCHAR(255), IN `COLUMN_NAME_ARGUMENT` VARCHAR(255))
BEGIN
	DECLARE HAS_AUTO_INCREMENT_ID INT(11);
	DECLARE PRIMARY_KEY_COLUMN_NAME VARCHAR(255);
	DECLARE PRIMARY_KEY_TYPE VARCHAR(255);
	DECLARE SQL_EXP VARCHAR(1000);
	SELECT COUNT(*)
		INTO HAS_AUTO_INCREMENT_ID
		FROM `information_schema`.`COLUMNS`
		WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
			AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
			AND `COLUMN_NAME` = COLUMN_NAME_ARGUMENT
			AND `COLUMN_TYPE` LIKE '%int%'
			AND `COLUMN_KEY` = 'PRI';
	IF HAS_AUTO_INCREMENT_ID THEN
		SELECT `COLUMN_TYPE`
			INTO PRIMARY_KEY_TYPE
			FROM `information_schema`.`COLUMNS`
			WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
				AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
				AND `COLUMN_NAME` = COLUMN_NAME_ARGUMENT
				AND `COLUMN_TYPE` LIKE '%int%'
				AND `COLUMN_KEY` = 'PRI';
		SELECT `COLUMN_NAME`
			INTO PRIMARY_KEY_COLUMN_NAME
			FROM `information_schema`.`COLUMNS`
			WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
				AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
				AND `COLUMN_NAME` = COLUMN_NAME_ARGUMENT
				AND `COLUMN_TYPE` LIKE '%int%'
				AND `COLUMN_KEY` = 'PRI';
		SET SQL_EXP = CONCAT('ALTER TABLE `', (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA())), '`.`', TABLE_NAME_ARGUMENT, '` MODIFY COLUMN `', PRIMARY_KEY_COLUMN_NAME, '` ', PRIMARY_KEY_TYPE, ' NOT NULL AUTO_INCREMENT;');
		SET @SQL_EXP = SQL_EXP;
		PREPARE SQL_EXP_EXECUTE FROM @SQL_EXP;
		EXECUTE SQL_EXP_EXECUTE;
		DEALLOCATE PREPARE SQL_EXP_EXECUTE;
	END IF;
END //
DELIMITER ;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220131195925_Initial') THEN

    CREATE TABLE `Franchises` (
        `Id` bigint NOT NULL,
        `Name` nvarchar(max) NULL,
        CONSTRAINT `PK_Franchises` PRIMARY KEY (`Id`)
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220131195925_Initial') THEN

    CREATE TABLE `Visitors` (
        `Id` bigint NOT NULL,
        `GeneralTrustFactor` real NOT NULL,
        `FullName` nvarchar(max) NULL,
        `Avatar` nvarchar(max) NULL,
        `Email` nvarchar(max) NULL,
        CONSTRAINT `PK_Visitors` PRIMARY KEY (`Id`)
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220131195925_Initial') THEN

    CREATE TABLE `Restaurants` (
        `Id` bigint NOT NULL,
        `CoordX` float NOT NULL,
        `CoordY` float NOT NULL,
        `Rating` real NOT NULL,
        `Cuisine` nvarchar(max) NULL,
        `PriceRange` nvarchar(max) NULL,
        `NumberOfTables` int NOT NULL,
        `Type` int NOT NULL,
        `Discount` int NOT NULL,
        `FranchiseId` bigint NOT NULL,
        `LayoutId` bigint NOT NULL,
        `ManagerId` bigint NOT NULL,
        CONSTRAINT `PK_Restaurants` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_Restaurants_Franchises_FranchiseId` FOREIGN KEY (`FranchiseId`) REFERENCES `Franchises` (`Id`) ON DELETE CASCADE
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220131195925_Initial') THEN

    CREATE TABLE `Layouts` (
        `Id` bigint NOT NULL,
        `LayoutData` tinyint NOT NULL,
        `RestaurantId` bigint NOT NULL,
        CONSTRAINT `PK_Layouts` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_Layouts_Restaurants_RestaurantId` FOREIGN KEY (`RestaurantId`) REFERENCES `Restaurants` (`Id`) ON DELETE CASCADE
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220131195925_Initial') THEN

    CREATE TABLE `Managers` (
        `Id` bigint NOT NULL,
        `ManagerState` int NOT NULL,
        `RestaurantId` bigint NOT NULL,
        `FullName` nvarchar(max) NULL,
        `Avatar` nvarchar(max) NULL,
        `Email` nvarchar(max) NULL,
        CONSTRAINT `PK_Managers` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_Managers_Restaurants_RestaurantId` FOREIGN KEY (`RestaurantId`) REFERENCES `Restaurants` (`Id`) ON DELETE CASCADE
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220131195925_Initial') THEN

    CREATE TABLE `RestaurantVisitors` (
        `Id` bigint NOT NULL,
        `RestaurantRate` real NOT NULL,
        `TimesVisited` int NOT NULL,
        `AverageMoneySpent` float NOT NULL,
        `RestaurantId` bigint NOT NULL,
        `VisitorId` bigint NOT NULL,
        CONSTRAINT `PK_RestaurantVisitors` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_RestaurantVisitors_Restaurants_RestaurantId` FOREIGN KEY (`RestaurantId`) REFERENCES `Restaurants` (`Id`) ON DELETE CASCADE,
        CONSTRAINT `FK_RestaurantVisitors_Visitors_VisitorId` FOREIGN KEY (`VisitorId`) REFERENCES `Visitors` (`Id`) ON DELETE CASCADE
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220131195925_Initial') THEN

    CREATE TABLE `VisitorFavouritess` (
        `Id` bigint NOT NULL,
        `VisitorId` bigint NOT NULL,
        `RestaurantId` bigint NOT NULL,
        CONSTRAINT `PK_VisitorFavouritess` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_VisitorFavouritess_Restaurants_RestaurantId` FOREIGN KEY (`RestaurantId`) REFERENCES `Restaurants` (`Id`) ON DELETE CASCADE,
        CONSTRAINT `FK_VisitorFavouritess_Visitors_VisitorId` FOREIGN KEY (`VisitorId`) REFERENCES `Visitors` (`Id`) ON DELETE CASCADE
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220131195925_Initial') THEN

    CREATE TABLE `VisitorHistorys` (
        `Id` bigint NOT NULL,
        `DateTime` datetime2 NOT NULL,
        `VisitorId` bigint NOT NULL,
        `RestaurantId` bigint NOT NULL,
        CONSTRAINT `PK_VisitorHistorys` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_VisitorHistorys_Restaurants_RestaurantId` FOREIGN KEY (`RestaurantId`) REFERENCES `Restaurants` (`Id`) ON DELETE CASCADE,
        CONSTRAINT `FK_VisitorHistorys_Visitors_VisitorId` FOREIGN KEY (`VisitorId`) REFERENCES `Visitors` (`Id`) ON DELETE CASCADE
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220131195925_Initial') THEN

    CREATE TABLE `Waiters` (
        `Id` bigint NOT NULL,
        `NumberOfServingTables` int NOT NULL,
        `WaiterState` int NOT NULL,
        `RestaurantId` bigint NOT NULL,
        `VisitorId` bigint NOT NULL,
        `FullName` nvarchar(max) NULL,
        `Avatar` nvarchar(max) NULL,
        `Email` nvarchar(max) NULL,
        CONSTRAINT `PK_Waiters` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_Waiters_Restaurants_RestaurantId` FOREIGN KEY (`RestaurantId`) REFERENCES `Restaurants` (`Id`) ON DELETE CASCADE,
        CONSTRAINT `FK_Waiters_Visitors_VisitorId` FOREIGN KEY (`VisitorId`) REFERENCES `Visitors` (`Id`) ON DELETE CASCADE
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220131195925_Initial') THEN

    CREATE TABLE `Tables` (
        `Id` bigint NOT NULL,
        `Number` int NOT NULL,
        `State` int NOT NULL,
        `ReserveDate` datetime2 NOT NULL,
        `NumberOfSeats` int NOT NULL,
        `Floor` int NOT NULL,
        `CoordX` float NOT NULL,
        `CoordY` float NOT NULL,
        `TableSize` float NOT NULL,
        `ServingWaiterId` bigint NOT NULL,
        `WaiterId` bigint NOT NULL,
        `RestaurantId` bigint NOT NULL,
        CONSTRAINT `PK_Tables` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_Tables_Restaurants_RestaurantId` FOREIGN KEY (`RestaurantId`) REFERENCES `Restaurants` (`Id`) ON DELETE CASCADE,
        CONSTRAINT `FK_Tables_Waiters_WaiterId` FOREIGN KEY (`WaiterId`) REFERENCES `Waiters` (`Id`) ON DELETE RESTRICT
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220131195925_Initial') THEN

    CREATE TABLE `Reservations` (
        `Id` bigint NOT NULL,
        `Date` datetime2 NOT NULL,
        `TableId` bigint NOT NULL,
        CONSTRAINT `PK_Reservations` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_Reservations_Tables_TableId` FOREIGN KEY (`TableId`) REFERENCES `Tables` (`Id`) ON DELETE CASCADE
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220131195925_Initial') THEN

    CREATE TABLE `ReservationVisitor` (
        `ReservationsId` bigint NOT NULL,
        `VisitorsId` bigint NOT NULL,
        CONSTRAINT `PK_ReservationVisitor` PRIMARY KEY (`ReservationsId`, `VisitorsId`),
        CONSTRAINT `FK_ReservationVisitor_Reservations_ReservationsId` FOREIGN KEY (`ReservationsId`) REFERENCES `Reservations` (`Id`) ON DELETE CASCADE,
        CONSTRAINT `FK_ReservationVisitor_Visitors_VisitorsId` FOREIGN KEY (`VisitorsId`) REFERENCES `Visitors` (`Id`) ON DELETE CASCADE
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220131195925_Initial') THEN

    CREATE UNIQUE INDEX `IX_Layouts_RestaurantId` ON `Layouts` (`RestaurantId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220131195925_Initial') THEN

    CREATE UNIQUE INDEX `IX_Managers_RestaurantId` ON `Managers` (`RestaurantId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220131195925_Initial') THEN

    CREATE INDEX `IX_Reservations_TableId` ON `Reservations` (`TableId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220131195925_Initial') THEN

    CREATE INDEX `IX_ReservationVisitor_VisitorsId` ON `ReservationVisitor` (`VisitorsId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220131195925_Initial') THEN

    CREATE INDEX `IX_Restaurants_FranchiseId` ON `Restaurants` (`FranchiseId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220131195925_Initial') THEN

    CREATE INDEX `IX_RestaurantVisitors_RestaurantId` ON `RestaurantVisitors` (`RestaurantId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220131195925_Initial') THEN

    CREATE INDEX `IX_RestaurantVisitors_VisitorId` ON `RestaurantVisitors` (`VisitorId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220131195925_Initial') THEN

    CREATE INDEX `IX_Tables_RestaurantId` ON `Tables` (`RestaurantId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220131195925_Initial') THEN

    CREATE INDEX `IX_Tables_WaiterId` ON `Tables` (`WaiterId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220131195925_Initial') THEN

    CREATE INDEX `IX_VisitorFavouritess_RestaurantId` ON `VisitorFavouritess` (`RestaurantId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220131195925_Initial') THEN

    CREATE INDEX `IX_VisitorFavouritess_VisitorId` ON `VisitorFavouritess` (`VisitorId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220131195925_Initial') THEN

    CREATE INDEX `IX_VisitorHistorys_RestaurantId` ON `VisitorHistorys` (`RestaurantId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220131195925_Initial') THEN

    CREATE INDEX `IX_VisitorHistorys_VisitorId` ON `VisitorHistorys` (`VisitorId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220131195925_Initial') THEN

    CREATE INDEX `IX_Waiters_RestaurantId` ON `Waiters` (`RestaurantId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220131195925_Initial') THEN

    CREATE INDEX `IX_Waiters_VisitorId` ON `Waiters` (`VisitorId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220131195925_Initial') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20220131195925_Initial', '5.0.13');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220202200604_RestaurantCuisineAndTableChange') THEN

    ALTER TABLE `Tables` DROP FOREIGN KEY `FK_Tables_Waiters_WaiterId`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220202200604_RestaurantCuisineAndTableChange') THEN

    ALTER TABLE `Tables` DROP INDEX `IX_Tables_WaiterId`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220202200604_RestaurantCuisineAndTableChange') THEN

    ALTER TABLE `Tables` DROP COLUMN `WaiterId`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220202200604_RestaurantCuisineAndTableChange') THEN

    ALTER TABLE `Restaurants` DROP COLUMN `Cuisine`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220202200604_RestaurantCuisineAndTableChange') THEN

    CREATE TABLE `Cuisines` (
        `Id` bigint NOT NULL,
        `Cuisine` int NOT NULL,
        CONSTRAINT `PK_Cuisines` PRIMARY KEY (`Id`)
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220202200604_RestaurantCuisineAndTableChange') THEN

    CREATE TABLE `CuisinesRestaurant` (
        `CuisinesId` bigint NOT NULL,
        `RestaurantsId` bigint NOT NULL,
        CONSTRAINT `PK_CuisinesRestaurant` PRIMARY KEY (`CuisinesId`, `RestaurantsId`),
        CONSTRAINT `FK_CuisinesRestaurant_Cuisines_CuisinesId` FOREIGN KEY (`CuisinesId`) REFERENCES `Cuisines` (`Id`) ON DELETE CASCADE,
        CONSTRAINT `FK_CuisinesRestaurant_Restaurants_RestaurantsId` FOREIGN KEY (`RestaurantsId`) REFERENCES `Restaurants` (`Id`) ON DELETE CASCADE
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220202200604_RestaurantCuisineAndTableChange') THEN

    CREATE INDEX `IX_Tables_ServingWaiterId` ON `Tables` (`ServingWaiterId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220202200604_RestaurantCuisineAndTableChange') THEN

    CREATE INDEX `IX_CuisinesRestaurant_RestaurantsId` ON `CuisinesRestaurant` (`RestaurantsId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220202200604_RestaurantCuisineAndTableChange') THEN

    ALTER TABLE `Tables` ADD CONSTRAINT `FK_Tables_Waiters_ServingWaiterId` FOREIGN KEY (`ServingWaiterId`) REFERENCES `Waiters` (`Id`) ON DELETE RESTRICT;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220202200604_RestaurantCuisineAndTableChange') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20220202200604_RestaurantCuisineAndTableChange', '5.0.13');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220207152518_Testing') THEN

    ALTER TABLE `Restaurants` MODIFY COLUMN `ManagerId` bigint NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220207152518_Testing') THEN

    ALTER TABLE `Restaurants` MODIFY COLUMN `LayoutId` bigint NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220207152518_Testing') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20220207152518_Testing', '5.0.13');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220207185940_ChangeWaiterVisitor') THEN

    ALTER TABLE `Tables` DROP FOREIGN KEY `FK_Tables_Waiters_ServingWaiterId`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220207185940_ChangeWaiterVisitor') THEN

    ALTER TABLE `Waiters` DROP FOREIGN KEY `FK_Waiters_Visitors_VisitorId`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220207185940_ChangeWaiterVisitor') THEN

    ALTER TABLE `Waiters` DROP INDEX `IX_Waiters_VisitorId`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220207185940_ChangeWaiterVisitor') THEN

    ALTER TABLE `Tables` DROP INDEX `IX_Tables_ServingWaiterId`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220207185940_ChangeWaiterVisitor') THEN

    ALTER TABLE `Waiters` DROP COLUMN `VisitorId`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220207185940_ChangeWaiterVisitor') THEN

    ALTER TABLE `Tables` DROP COLUMN `ServingWaiterId`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220207185940_ChangeWaiterVisitor') THEN

    ALTER TABLE `Tables` ADD `WaiterId` bigint NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220207185940_ChangeWaiterVisitor') THEN

    ALTER TABLE `Reservations` ADD `WaiterId` bigint NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220207185940_ChangeWaiterVisitor') THEN

    CREATE INDEX `IX_Tables_WaiterId` ON `Tables` (`WaiterId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220207185940_ChangeWaiterVisitor') THEN

    CREATE INDEX `IX_Reservations_WaiterId` ON `Reservations` (`WaiterId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220207185940_ChangeWaiterVisitor') THEN

    ALTER TABLE `Reservations` ADD CONSTRAINT `FK_Reservations_Waiters_WaiterId` FOREIGN KEY (`WaiterId`) REFERENCES `Waiters` (`Id`) ON DELETE RESTRICT;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220207185940_ChangeWaiterVisitor') THEN

    ALTER TABLE `Tables` ADD CONSTRAINT `FK_Tables_Waiters_WaiterId` FOREIGN KEY (`WaiterId`) REFERENCES `Waiters` (`Id`) ON DELETE RESTRICT;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220207185940_ChangeWaiterVisitor') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20220207185940_ChangeWaiterVisitor', '5.0.13');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220209142708_TablesChange') THEN

    ALTER TABLE `Tables` DROP COLUMN `ReserveDate`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220209142708_TablesChange') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20220209142708_TablesChange', '5.0.13');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220209164417_AddNewDbSetEntity') THEN

    ALTER TABLE `VisitorFavouritess` DROP FOREIGN KEY `FK_VisitorFavouritess_Restaurants_RestaurantId`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220209164417_AddNewDbSetEntity') THEN

    ALTER TABLE `VisitorFavouritess` DROP FOREIGN KEY `FK_VisitorFavouritess_Visitors_VisitorId`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220209164417_AddNewDbSetEntity') THEN

    CALL POMELO_BEFORE_DROP_PRIMARY_KEY(NULL, 'VisitorFavouritess');
    ALTER TABLE `VisitorFavouritess` DROP PRIMARY KEY;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220209164417_AddNewDbSetEntity') THEN

    ALTER TABLE `VisitorFavouritess` RENAME `VisitorFavourites`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220209164417_AddNewDbSetEntity') THEN

    ALTER TABLE `VisitorFavourites` RENAME INDEX `IX_VisitorFavouritess_VisitorId` TO `IX_VisitorFavourites_VisitorId`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220209164417_AddNewDbSetEntity') THEN

    ALTER TABLE `VisitorFavourites` RENAME INDEX `IX_VisitorFavouritess_RestaurantId` TO `IX_VisitorFavourites_RestaurantId`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220209164417_AddNewDbSetEntity') THEN

    ALTER TABLE `VisitorFavourites` ADD CONSTRAINT `PK_VisitorFavourites` PRIMARY KEY (`Id`);
    CALL POMELO_AFTER_ADD_PRIMARY_KEY(NULL, 'VisitorFavourites', 'Id');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220209164417_AddNewDbSetEntity') THEN

    ALTER TABLE `VisitorFavourites` ADD CONSTRAINT `FK_VisitorFavourites_Restaurants_RestaurantId` FOREIGN KEY (`RestaurantId`) REFERENCES `Restaurants` (`Id`) ON DELETE CASCADE;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220209164417_AddNewDbSetEntity') THEN

    ALTER TABLE `VisitorFavourites` ADD CONSTRAINT `FK_VisitorFavourites_Visitors_VisitorId` FOREIGN KEY (`VisitorId`) REFERENCES `Visitors` (`Id`) ON DELETE CASCADE;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220209164417_AddNewDbSetEntity') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20220209164417_AddNewDbSetEntity', '5.0.13');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220221193845_CuisineColumnChange') THEN

    DROP TABLE `CuisinesRestaurant`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220221193845_CuisineColumnChange') THEN

    ALTER TABLE `Cuisines` CHANGE `Cuisine` `CuisineEnum` int NOT NULL DEFAULT 0;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220221193845_CuisineColumnChange') THEN

    CREATE TABLE `CuisineRestaurant` (
        `CuisinesId` bigint NOT NULL,
        `RestaurantsId` bigint NOT NULL,
        CONSTRAINT `PK_CuisineRestaurant` PRIMARY KEY (`CuisinesId`, `RestaurantsId`),
        CONSTRAINT `FK_CuisineRestaurant_Cuisines_CuisinesId` FOREIGN KEY (`CuisinesId`) REFERENCES `Cuisines` (`Id`) ON DELETE CASCADE,
        CONSTRAINT `FK_CuisineRestaurant_Restaurants_RestaurantsId` FOREIGN KEY (`RestaurantsId`) REFERENCES `Restaurants` (`Id`) ON DELETE CASCADE
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220221193845_CuisineColumnChange') THEN

    CREATE INDEX `IX_CuisineRestaurant_RestaurantsId` ON `CuisineRestaurant` (`RestaurantsId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220221193845_CuisineColumnChange') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20220221193845_CuisineColumnChange', '5.0.13');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220301181024_Constraints') THEN

    ALTER TABLE `Franchises` MODIFY COLUMN `Name` nvarchar(450) NOT NULL DEFAULT '';

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220301181024_Constraints') THEN

    CREATE UNIQUE INDEX `IX_Franchises_Name` ON `Franchises` (`Name`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220301181024_Constraints') THEN

    CREATE UNIQUE INDEX `IX_Cuisines_CuisineEnum` ON `Cuisines` (`CuisineEnum`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220301181024_Constraints') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20220301181024_Constraints', '5.0.13');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220502171216_Change cuisine enum to string') THEN

    ALTER TABLE `Cuisines` DROP INDEX `IX_Cuisines_CuisineEnum`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220502171216_Change cuisine enum to string') THEN

    ALTER TABLE `Cuisines` DROP COLUMN `CuisineEnum`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220502171216_Change cuisine enum to string') THEN

    ALTER TABLE `Cuisines` ADD `CuisineName` nvarchar(450) NOT NULL DEFAULT '';

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220502171216_Change cuisine enum to string') THEN

    CREATE UNIQUE INDEX `IX_Cuisines_CuisineName` ON `Cuisines` (`CuisineName`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220502171216_Change cuisine enum to string') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20220502171216_Change cuisine enum to string', '5.0.13');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220517173425_AddImagesAndReworkRestaurants') THEN

    ALTER TABLE `Layouts` DROP FOREIGN KEY `FK_Layouts_Restaurants_RestaurantId`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220517173425_AddImagesAndReworkRestaurants') THEN

    ALTER TABLE `Reservations` DROP FOREIGN KEY `FK_Reservations_Waiters_WaiterId`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220517173425_AddImagesAndReworkRestaurants') THEN

    ALTER TABLE `ReservationVisitor` DROP FOREIGN KEY `FK_ReservationVisitor_Visitors_VisitorsId`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220517173425_AddImagesAndReworkRestaurants') THEN

    ALTER TABLE `Restaurants` DROP FOREIGN KEY `FK_Restaurants_Franchises_FranchiseId`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220517173425_AddImagesAndReworkRestaurants') THEN

    ALTER TABLE `RestaurantVisitors` DROP FOREIGN KEY `FK_RestaurantVisitors_Visitors_VisitorId`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220517173425_AddImagesAndReworkRestaurants') THEN

    ALTER TABLE `Tables` DROP FOREIGN KEY `FK_Tables_Waiters_WaiterId`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220517173425_AddImagesAndReworkRestaurants') THEN

    ALTER TABLE `VisitorFavourites` DROP FOREIGN KEY `FK_VisitorFavourites_Visitors_VisitorId`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220517173425_AddImagesAndReworkRestaurants') THEN

    ALTER TABLE `VisitorHistorys` DROP FOREIGN KEY `FK_VisitorHistorys_Visitors_VisitorId`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220517173425_AddImagesAndReworkRestaurants') THEN

    DROP TABLE `Managers`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220517173425_AddImagesAndReworkRestaurants') THEN

    DROP TABLE `Waiters`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220517173425_AddImagesAndReworkRestaurants') THEN

    ALTER TABLE `Tables` DROP INDEX `IX_Tables_WaiterId`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220517173425_AddImagesAndReworkRestaurants') THEN

    ALTER TABLE `Reservations` DROP INDEX `IX_Reservations_WaiterId`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220517173425_AddImagesAndReworkRestaurants') THEN

    CALL POMELO_BEFORE_DROP_PRIMARY_KEY(NULL, 'Visitors');
    ALTER TABLE `Visitors` DROP PRIMARY KEY;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220517173425_AddImagesAndReworkRestaurants') THEN

    ALTER TABLE `Tables` DROP COLUMN `CoordX`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220517173425_AddImagesAndReworkRestaurants') THEN

    ALTER TABLE `Tables` DROP COLUMN `CoordY`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220517173425_AddImagesAndReworkRestaurants') THEN

    ALTER TABLE `Tables` DROP COLUMN `WaiterId`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220517173425_AddImagesAndReworkRestaurants') THEN

    ALTER TABLE `Restaurants` DROP COLUMN `CoordX`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220517173425_AddImagesAndReworkRestaurants') THEN

    ALTER TABLE `Restaurants` DROP COLUMN `CoordY`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220517173425_AddImagesAndReworkRestaurants') THEN

    ALTER TABLE `Reservations` DROP COLUMN `WaiterId`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220517173425_AddImagesAndReworkRestaurants') THEN

    ALTER TABLE `Visitors` RENAME `User`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220517173425_AddImagesAndReworkRestaurants') THEN

    ALTER TABLE `User` CHANGE `Avatar` `Location` nvarchar(max) NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220517173425_AddImagesAndReworkRestaurants') THEN

    ALTER TABLE `Restaurants` MODIFY COLUMN `PriceRange` int NOT NULL DEFAULT 0;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220517173425_AddImagesAndReworkRestaurants') THEN

    ALTER TABLE `Restaurants` ADD `Address` nvarchar(max) NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220517173425_AddImagesAndReworkRestaurants') THEN

    ALTER TABLE `Restaurants` ADD `Description` nvarchar(max) NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220517173425_AddImagesAndReworkRestaurants') THEN

    ALTER TABLE `Restaurants` ADD `MainImageId` bigint NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220517173425_AddImagesAndReworkRestaurants') THEN

    ALTER TABLE `Restaurants` ADD `Name` nvarchar(max) NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220517173425_AddImagesAndReworkRestaurants') THEN

    ALTER TABLE `Franchises` ADD `Description` nvarchar(max) NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220517173425_AddImagesAndReworkRestaurants') THEN

    ALTER TABLE `User` MODIFY COLUMN `GeneralTrustFactor` real NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220517173425_AddImagesAndReworkRestaurants') THEN

    ALTER TABLE `User` ADD `AvatarId` bigint NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220517173425_AddImagesAndReworkRestaurants') THEN

    ALTER TABLE `User` ADD `DateOfBirth` datetime2 NOT NULL DEFAULT '0001-01-01 00:00:00';

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220517173425_AddImagesAndReworkRestaurants') THEN

    ALTER TABLE `User` ADD `Discriminator` nvarchar(max) NOT NULL DEFAULT '';

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220517173425_AddImagesAndReworkRestaurants') THEN

    ALTER TABLE `User` ADD `ManagerState` int NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220517173425_AddImagesAndReworkRestaurants') THEN

    ALTER TABLE `User` ADD `RestaurantId` bigint NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220517173425_AddImagesAndReworkRestaurants') THEN

    ALTER TABLE `User` ADD CONSTRAINT `PK_User` PRIMARY KEY (`Id`);
    CALL POMELO_AFTER_ADD_PRIMARY_KEY(NULL, 'User', 'Id');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220517173425_AddImagesAndReworkRestaurants') THEN

    CREATE TABLE `Image` (
        `Id` bigint NOT NULL,
        `Name` nvarchar(max) NULL,
        `RestaurantId` bigint NULL,
        CONSTRAINT `PK_Image` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_Image_Restaurants_RestaurantId` FOREIGN KEY (`RestaurantId`) REFERENCES `Restaurants` (`Id`) ON DELETE RESTRICT
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220517173425_AddImagesAndReworkRestaurants') THEN

    CREATE UNIQUE INDEX `IX_Restaurants_MainImageId` ON `Restaurants` (`MainImageId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220517173425_AddImagesAndReworkRestaurants') THEN

    CREATE UNIQUE INDEX `IX_User_AvatarId` ON `User` (`AvatarId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220517173425_AddImagesAndReworkRestaurants') THEN

    CREATE UNIQUE INDEX `IX_User_RestaurantId` ON `User` (`RestaurantId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220517173425_AddImagesAndReworkRestaurants') THEN

    CREATE INDEX `IX_Image_RestaurantId` ON `Image` (`RestaurantId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220517173425_AddImagesAndReworkRestaurants') THEN

    ALTER TABLE `Layouts` ADD CONSTRAINT `FK_Layouts_Restaurants_RestaurantId` FOREIGN KEY (`RestaurantId`) REFERENCES `Restaurants` (`Id`) ON DELETE RESTRICT;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220517173425_AddImagesAndReworkRestaurants') THEN

    ALTER TABLE `ReservationVisitor` ADD CONSTRAINT `FK_ReservationVisitor_User_VisitorsId` FOREIGN KEY (`VisitorsId`) REFERENCES `User` (`Id`) ON DELETE CASCADE;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220517173425_AddImagesAndReworkRestaurants') THEN

    ALTER TABLE `Restaurants` ADD CONSTRAINT `FK_Restaurants_Franchises_FranchiseId` FOREIGN KEY (`FranchiseId`) REFERENCES `Franchises` (`Id`) ON DELETE RESTRICT;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220517173425_AddImagesAndReworkRestaurants') THEN

    ALTER TABLE `Restaurants` ADD CONSTRAINT `FK_Restaurants_Image_MainImageId` FOREIGN KEY (`MainImageId`) REFERENCES `Image` (`Id`) ON DELETE RESTRICT;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220517173425_AddImagesAndReworkRestaurants') THEN

    ALTER TABLE `RestaurantVisitors` ADD CONSTRAINT `FK_RestaurantVisitors_User_VisitorId` FOREIGN KEY (`VisitorId`) REFERENCES `User` (`Id`) ON DELETE CASCADE;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220517173425_AddImagesAndReworkRestaurants') THEN

    ALTER TABLE `User` ADD CONSTRAINT `FK_User_Image_AvatarId` FOREIGN KEY (`AvatarId`) REFERENCES `Image` (`Id`) ON DELETE RESTRICT;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220517173425_AddImagesAndReworkRestaurants') THEN

    ALTER TABLE `User` ADD CONSTRAINT `FK_User_Restaurants_RestaurantId` FOREIGN KEY (`RestaurantId`) REFERENCES `Restaurants` (`Id`) ON DELETE RESTRICT;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220517173425_AddImagesAndReworkRestaurants') THEN

    ALTER TABLE `VisitorFavourites` ADD CONSTRAINT `FK_VisitorFavourites_User_VisitorId` FOREIGN KEY (`VisitorId`) REFERENCES `User` (`Id`) ON DELETE CASCADE;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220517173425_AddImagesAndReworkRestaurants') THEN

    ALTER TABLE `VisitorHistorys` ADD CONSTRAINT `FK_VisitorHistorys_User_VisitorId` FOREIGN KEY (`VisitorId`) REFERENCES `User` (`Id`) ON DELETE CASCADE;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220517173425_AddImagesAndReworkRestaurants') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20220517173425_AddImagesAndReworkRestaurants', '5.0.13');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220520124211_AddDateOfOpeningToRestaurants') THEN

    ALTER TABLE `Restaurants` ADD `DateOfOpening` datetime2 NOT NULL DEFAULT '0001-01-01 00:00:00';

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220520124211_AddDateOfOpeningToRestaurants') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20220520124211_AddDateOfOpeningToRestaurants', '5.0.13');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220520195715_RemoveVisitorFavourites') THEN

    DROP TABLE `VisitorFavourites`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220520195715_RemoveVisitorFavourites') THEN

    ALTER TABLE `Restaurants` ADD `VisitorId` bigint NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220520195715_RemoveVisitorFavourites') THEN

    CREATE INDEX `IX_Restaurants_VisitorId` ON `Restaurants` (`VisitorId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220520195715_RemoveVisitorFavourites') THEN

    ALTER TABLE `Restaurants` ADD CONSTRAINT `FK_Restaurants_User_VisitorId` FOREIGN KEY (`VisitorId`) REFERENCES `User` (`Id`) ON DELETE RESTRICT;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220520195715_RemoveVisitorFavourites') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20220520195715_RemoveVisitorFavourites', '5.0.13');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220522181907_ReservationFix') THEN

    DROP TABLE `ReservationVisitor`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220522181907_ReservationFix') THEN

    ALTER TABLE `Reservations` ADD `VisitorId` bigint NOT NULL DEFAULT 0;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220522181907_ReservationFix') THEN

    CREATE INDEX `IX_Reservations_VisitorId` ON `Reservations` (`VisitorId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220522181907_ReservationFix') THEN

    ALTER TABLE `Reservations` ADD CONSTRAINT `FK_Reservations_User_VisitorId` FOREIGN KEY (`VisitorId`) REFERENCES `User` (`Id`) ON DELETE CASCADE;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220522181907_ReservationFix') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20220522181907_ReservationFix', '5.0.13');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220525171958_AddContactInfoToRestaurants') THEN

    ALTER TABLE `Restaurants` ADD `Email` nvarchar(max) NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220525171958_AddContactInfoToRestaurants') THEN

    ALTER TABLE `Restaurants` ADD `Menu` nvarchar(max) NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220525171958_AddContactInfoToRestaurants') THEN

    ALTER TABLE `Restaurants` ADD `Phone` nvarchar(max) NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220525171958_AddContactInfoToRestaurants') THEN

    ALTER TABLE `Restaurants` ADD `Website` nvarchar(max) NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220525171958_AddContactInfoToRestaurants') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20220525171958_AddContactInfoToRestaurants', '5.0.13');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230401212417_InitialCreate') THEN

    ALTER TABLE `User` DROP INDEX `IX_User_AvatarId`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230401212417_InitialCreate') THEN

    ALTER TABLE `User` DROP INDEX `IX_User_RestaurantId`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230401212417_InitialCreate') THEN

    ALTER TABLE `Restaurants` DROP INDEX `IX_Restaurants_MainImageId`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230401212417_InitialCreate') THEN

    ALTER TABLE `VisitorHistorys` MODIFY COLUMN `DateTime` datetime(6) NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230401212417_InitialCreate') THEN

    ALTER TABLE `User` MODIFY COLUMN `Location` longtext CHARACTER SET utf8mb4 NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230401212417_InitialCreate') THEN

    ALTER TABLE `User` MODIFY COLUMN `GeneralTrustFactor` float NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230401212417_InitialCreate') THEN

    ALTER TABLE `User` MODIFY COLUMN `FullName` longtext CHARACTER SET utf8mb4 NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230401212417_InitialCreate') THEN

    ALTER TABLE `User` MODIFY COLUMN `Email` longtext CHARACTER SET utf8mb4 NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230401212417_InitialCreate') THEN

    ALTER TABLE `User` MODIFY COLUMN `Discriminator` longtext CHARACTER SET utf8mb4 NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230401212417_InitialCreate') THEN

    ALTER TABLE `User` MODIFY COLUMN `DateOfBirth` datetime(6) NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230401212417_InitialCreate') THEN

    ALTER TABLE `Tables` MODIFY COLUMN `TableSize` double NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230401212417_InitialCreate') THEN

    ALTER TABLE `RestaurantVisitors` MODIFY COLUMN `RestaurantRate` float NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230401212417_InitialCreate') THEN

    ALTER TABLE `RestaurantVisitors` MODIFY COLUMN `AverageMoneySpent` double NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230401212417_InitialCreate') THEN

    ALTER TABLE `Restaurants` MODIFY COLUMN `Website` longtext CHARACTER SET utf8mb4 NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230401212417_InitialCreate') THEN

    ALTER TABLE `Restaurants` MODIFY COLUMN `Rating` float NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230401212417_InitialCreate') THEN

    ALTER TABLE `Restaurants` MODIFY COLUMN `Phone` longtext CHARACTER SET utf8mb4 NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230401212417_InitialCreate') THEN

    ALTER TABLE `Restaurants` MODIFY COLUMN `Name` longtext CHARACTER SET utf8mb4 NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230401212417_InitialCreate') THEN

    ALTER TABLE `Restaurants` MODIFY COLUMN `Menu` longtext CHARACTER SET utf8mb4 NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230401212417_InitialCreate') THEN

    ALTER TABLE `Restaurants` MODIFY COLUMN `Email` longtext CHARACTER SET utf8mb4 NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230401212417_InitialCreate') THEN

    ALTER TABLE `Restaurants` MODIFY COLUMN `Description` longtext CHARACTER SET utf8mb4 NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230401212417_InitialCreate') THEN

    ALTER TABLE `Restaurants` MODIFY COLUMN `DateOfOpening` datetime(6) NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230401212417_InitialCreate') THEN

    ALTER TABLE `Restaurants` MODIFY COLUMN `Address` longtext CHARACTER SET utf8mb4 NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230401212417_InitialCreate') THEN

    ALTER TABLE `Reservations` MODIFY COLUMN `Date` datetime(6) NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230401212417_InitialCreate') THEN

    ALTER TABLE `Layouts` MODIFY COLUMN `LayoutData` tinyint unsigned NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230401212417_InitialCreate') THEN

    ALTER TABLE `Image` MODIFY COLUMN `Name` longtext CHARACTER SET utf8mb4 NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230401212417_InitialCreate') THEN

    ALTER TABLE `Franchises` MODIFY COLUMN `Name` varchar(255) CHARACTER SET utf8mb4 NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230401212417_InitialCreate') THEN

    ALTER TABLE `Franchises` MODIFY COLUMN `Description` longtext CHARACTER SET utf8mb4 NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230401212417_InitialCreate') THEN

    ALTER TABLE `Cuisines` MODIFY COLUMN `CuisineName` varchar(255) CHARACTER SET utf8mb4 NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230401212417_InitialCreate') THEN

    CREATE UNIQUE INDEX `IX_User_AvatarId` ON `User` (`AvatarId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230401212417_InitialCreate') THEN

    CREATE UNIQUE INDEX `IX_User_RestaurantId` ON `User` (`RestaurantId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230401212417_InitialCreate') THEN

    CREATE UNIQUE INDEX `IX_Restaurants_MainImageId` ON `Restaurants` (`MainImageId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230401212417_InitialCreate') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20230401212417_InitialCreate', '5.0.13');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

DROP PROCEDURE `POMELO_BEFORE_DROP_PRIMARY_KEY`;

DROP PROCEDURE `POMELO_AFTER_ADD_PRIMARY_KEY`;

