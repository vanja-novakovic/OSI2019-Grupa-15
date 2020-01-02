-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
DROP SCHEMA IF EXISTS `eve` ;

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `eve` DEFAULT CHARACTER SET utf8 ;
USE `eve` ;

-- -----------------------------------------------------
-- Table `ACCOUNT`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ACCOUNT` ;

CREATE TABLE IF NOT EXISTS `ACCOUNT` (
  `IdAccount` INT NOT NULL AUTO_INCREMENT,
  `Username` VARCHAR(255) NOT NULL,
  `Password` VARCHAR(255) NOT NULL,
  PRIMARY KEY (`IdAccount`),
  UNIQUE INDEX `Username_UNIQUE` (`Username` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `CATEGORY`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `CATEGORY` ;

CREATE TABLE IF NOT EXISTS `CATEGORY` (
  `IdCategory` INT NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(255) NOT NULL,
  PRIMARY KEY (`IdCategory`),
  UNIQUE INDEX `Name_UNIQUE` (`Name` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ADDRESS`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ADDRESS` ;

CREATE TABLE IF NOT EXISTS `ADDRESS` (
  `IdAddress` INT NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(255) NOT NULL,
  PRIMARY KEY (`IdAddress`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `CITY`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `CITY` ;

CREATE TABLE IF NOT EXISTS `CITY` (
  `IdCity` INT NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(255) NOT NULL,
  PRIMARY KEY (`IdCity`),
  UNIQUE INDEX `Name_UNIQUE` (`Name` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ADDRESS_CITY`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ADDRESS_CITY` ;

CREATE TABLE IF NOT EXISTS `ADDRESS_CITY` (
  `IdAddress` INT NOT NULL,
  `IdCity` INT NOT NULL,
  PRIMARY KEY (`IdAddress`, `IdCity`),
  INDEX `fk_ADDRESS_has_CITY_CITY1_idx` (`IdCity` ASC) VISIBLE,
  INDEX `fk_ADDRESS_has_CITY_ADDRESS1_idx` (`IdAddress` ASC) VISIBLE,
  CONSTRAINT `fk_ADDRESS_has_CITY_ADDRESS1`
    FOREIGN KEY (`IdAddress`)
    REFERENCES `ADDRESS` (`IdAddress`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_ADDRESS_has_CITY_CITY1`
    FOREIGN KEY (`IdCity`)
    REFERENCES `CITY` (`IdCity`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `EVENT`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `EVENT` ;

CREATE TABLE IF NOT EXISTS `EVENT` (
  `IdEvent` INT NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(255) NOT NULL,
  `ScheduledOn` DATETIME NOT NULL,
  `IdCategory` INT NOT NULL,
  `Duration` INT NULL,
  `Organizers` VARCHAR(2000) NULL,
  `Description` VARCHAR(4000) NULL,
  `IdAddress` INT NOT NULL,
  `IdCity` INT NOT NULL,
  PRIMARY KEY (`IdEvent`),
  INDEX `fk_Event_Category_idx` (`IdCategory` ASC) VISIBLE,
  INDEX `fk_EVENT_ADDRESS_CITY1_idx` (`IdAddress` ASC, `IdCity` ASC) VISIBLE,
  CONSTRAINT `fk_Event_Category`
    FOREIGN KEY (`IdCategory`)
    REFERENCES `CATEGORY` (`IdCategory`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_EVENT_ADDRESS_CITY1`
    FOREIGN KEY (`IdAddress` , `IdCity`)
    REFERENCES `ADDRESS_CITY` (`IdAddress` , `IdCity`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `QUESTION`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `QUESTION` ;

CREATE TABLE IF NOT EXISTS `QUESTION` (
  `IdQuestion` INT NOT NULL AUTO_INCREMENT,
  `Content` VARCHAR(2000) NOT NULL,
  PRIMARY KEY (`IdQuestion`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ANSWER`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ANSWER` ;

CREATE TABLE IF NOT EXISTS `ANSWER` (
  `IdAnswer` INT NOT NULL AUTO_INCREMENT,
  `Content` VARCHAR(2000) NOT NULL,
  `True` TINYINT NOT NULL,
  `IdQuestion` INT NOT NULL,
  PRIMARY KEY (`IdAnswer`),
  INDEX `fk_ANSWER_QUESTION1_idx` (`IdQuestion` ASC) VISIBLE,
  CONSTRAINT `fk_ANSWER_QUESTION1`
    FOREIGN KEY (`IdQuestion`)
    REFERENCES `QUESTION` (`IdQuestion`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
