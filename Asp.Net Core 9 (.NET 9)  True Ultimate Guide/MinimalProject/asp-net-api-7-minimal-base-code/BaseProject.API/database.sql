-- MySQL dump 10.13  Distrib 8.0.32, for Win64 (x86_64)
--
-- Host: localhost    Database: basedatabase
-- ------------------------------------------------------
-- Server version	8.0.32

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `account`
--

DROP TABLE IF EXISTS `account`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `account` (
  `Id` char(36) NOT NULL,
  `Email` varchar(255) DEFAULT NULL,
  `Name` varchar(200) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `Password` varchar(255) NOT NULL,
  `Role` varchar(50) NOT NULL,
  `CreateDate` datetime DEFAULT NULL,
  `ModifiedDate` datetime DEFAULT NULL,
  `RefreshToken` varchar(255) DEFAULT NULL,
  `RefreshTokenExpiryTime` datetime DEFAULT NULL,
  `EmailValidated` tinyint DEFAULT '0',
  `PhoneNumber` varchar(20) DEFAULT NULL,
  `Address` varchar(100) DEFAULT NULL,
  `CardHolder` varchar(100) DEFAULT NULL,
  `CardNumber` varchar(50) DEFAULT NULL,
  `CardExpiredDate` datetime DEFAULT NULL,
  `CVC` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id_UNIQUE` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `account`
--

LOCK TABLES `account` WRITE;
/*!40000 ALTER TABLE `account` DISABLE KEYS */;
INSERT INTO `account` VALUES ('00e5f45e-0273-45a0-803a-165f7d942e3a',NULL,NULL,'$2a$10$M6KXAPETT2z4eL04k5KNFOc6lILsieYP.YgXrgQMrgG52re.lmXni','Customer','2023-10-02 16:33:44','2023-10-02 16:33:44',NULL,NULL,0,NULL,'',NULL,NULL,NULL,NULL),('9aaa2250-e358-4b79-9968-a15ba5f47cee','khanhtoan.le.6555@gmail.com','Toan Khanh Le','$2a$10$f14qvLwsdPynI7fePWp6IOtRqO.TEZ7kqpYo0GOtHaJmpvS/YQjIa','Customer','2023-10-02 16:36:23','2023-10-02 16:36:23',NULL,NULL,0,'0868772457','123123','toan khanh le',NULL,'2017-08-01 00:00:00','123'),('9f14e438-f783-46ff-b652-d149adeb4032',NULL,NULL,'$2a$10$QIjQfqmKOC.hvZYoOgAISejJJLtY3BdADMFD59asRFohCMgdHKN1u','Customer','2023-10-02 16:32:10','2023-10-02 16:32:10',NULL,NULL,0,NULL,'',NULL,NULL,NULL,NULL);
/*!40000 ALTER TABLE `account` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `comment`
--

DROP TABLE IF EXISTS `comment`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `comment` (
  `Id` char(36) NOT NULL,
  `ProductId` char(36) NOT NULL,
  `AccountId` char(36) NOT NULL,
  `Stars` int DEFAULT NULL,
  `Title` varchar(100) DEFAULT NULL,
  `commentText` text,
  PRIMARY KEY (`Id`),
  KEY `FK_Product_Comment_idx` (`ProductId`),
  KEY `FK_Comment_Account_idx` (`AccountId`),
  CONSTRAINT `FK_Comment_Account` FOREIGN KEY (`AccountId`) REFERENCES `account` (`Id`),
  CONSTRAINT `FK_Comment_Product` FOREIGN KEY (`ProductId`) REFERENCES `product` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `comment`
--

LOCK TABLES `comment` WRITE;
/*!40000 ALTER TABLE `comment` DISABLE KEYS */;
/*!40000 ALTER TABLE `comment` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `news`
--

DROP TABLE IF EXISTS `news`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `news` (
  `Id` char(36) NOT NULL,
  `Title` varchar(100) DEFAULT NULL,
  `Content` text,
  `AuthorId` char(36) DEFAULT NULL,
  `images` text,
  `tags` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id_UNIQUE` (`Id`),
  KEY `FK_News_Account_idx` (`AuthorId`),
  CONSTRAINT `FK_News_Account` FOREIGN KEY (`AuthorId`) REFERENCES `account` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `news`
--

LOCK TABLES `news` WRITE;
/*!40000 ALTER TABLE `news` DISABLE KEYS */;
/*!40000 ALTER TABLE `news` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `product`
--

DROP TABLE IF EXISTS `product`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `product` (
  `Id` char(36) NOT NULL,
  `Name` varchar(100) DEFAULT NULL,
  `VietnameseName` varchar(100) DEFAULT NULL,
  `CreateDate` datetime DEFAULT NULL,
  `ModifiedDate` datetime DEFAULT NULL,
  `Code` varchar(200) DEFAULT NULL,
  `Price` decimal(20,2) DEFAULT NULL,
  `Keyword` varchar(200) DEFAULT NULL,
  `Guarantee` int DEFAULT NULL,
  `Color` varchar(200) DEFAULT NULL,
  `IsOutOfStock` tinyint DEFAULT NULL,
  `Images` varchar(500) DEFAULT NULL,
  `Description` mediumtext,
  `DescriptionEmbedVideos` mediumtext,
  `DescriptionImages` mediumtext,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `product`
--

LOCK TABLES `product` WRITE;
/*!40000 ALTER TABLE `product` DISABLE KEYS */;
INSERT INTO `product` VALUES ('44519496-5a5a-4696-9c01-3bfb5c3be5e3','Akko 3078','Akko 3078',NULL,NULL,'123',123.00,'123',1,'ref',1,'d123','asdf','asdf','asdf'),('774acdbd-2904-49c4-bd87-4ba6a7f37e11','E-dra','E-dra',NULL,NULL,'332',123.00,'332',1,'yello',0,'d123','d123','d123','d123');
/*!40000 ALTER TABLE `product` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `producttechnology`
--

DROP TABLE IF EXISTS `producttechnology`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `producttechnology` (
  `id` char(36) NOT NULL,
  `productId` char(36) DEFAULT NULL,
  `technologyId` char(36) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `test123_idx` (`technologyId`),
  KEY `test_idx` (`productId`),
  CONSTRAINT `FK_product_productTechnology` FOREIGN KEY (`productId`) REFERENCES `product` (`Id`),
  CONSTRAINT `FK_technology_productTechnology` FOREIGN KEY (`technologyId`) REFERENCES `technology` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `producttechnology`
--

LOCK TABLES `producttechnology` WRITE;
/*!40000 ALTER TABLE `producttechnology` DISABLE KEYS */;
INSERT INTO `producttechnology` VALUES ('02056190-4a5d-46c3-96a0-8ecedf3e4c55','774acdbd-2904-49c4-bd87-4ba6a7f37e11','e1273cdb-e971-45ac-8441-645485eb4321'),('351c5e28-4476-4e55-bfff-90d039cb8a3a','44519496-5a5a-4696-9c01-3bfb5c3be5e3','e1273cdb-e971-45ac-8441-645485eb4321');
/*!40000 ALTER TABLE `producttechnology` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `role`
--

DROP TABLE IF EXISTS `role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `role` (
  `Id` char(36) NOT NULL,
  `Name` varchar(100) DEFAULT NULL,
  `Description` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id_UNIQUE` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `role`
--

LOCK TABLES `role` WRITE;
/*!40000 ALTER TABLE `role` DISABLE KEYS */;
INSERT INTO `role` VALUES ('8c0cc825-0ecd-4ca3-a7d6-99934ed15771','Admin','Full Permission');
/*!40000 ALTER TABLE `role` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `technology`
--

DROP TABLE IF EXISTS `technology`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `technology` (
  `id` char(36) NOT NULL,
  `Type` varchar(100) DEFAULT NULL,
  `Name` varchar(200) DEFAULT NULL,
  `Value` varchar(100) DEFAULT NULL,
  `CreateDate` datetime DEFAULT NULL,
  `ModifiedDate` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `technology`
--

LOCK TABLES `technology` WRITE;
/*!40000 ALTER TABLE `technology` DISABLE KEYS */;
INSERT INTO `technology` VALUES ('e1273cdb-e971-45ac-8441-645485eb4321','Switch','BlueSwitch','BlueSwitch',NULL,NULL);
/*!40000 ALTER TABLE `technology` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-10-02 17:28:32
