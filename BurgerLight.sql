-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jun 07, 2023 at 05:06 PM
-- Server version: 10.4.28-MariaDB
-- PHP Version: 8.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `burgerlightdb`
--

-- --------------------------------------------------------

--
-- Table structure for table `tblcheckout`
--

CREATE TABLE `tblcheckout` (
  `userid` int(11) NOT NULL,
  `prodId` int(11) NOT NULL,
  `quantity` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `tblmenulist`
--

CREATE TABLE `tblmenulist` (
  `prodId` int(11) NOT NULL,
  `prodName` varchar(30) NOT NULL,
  `prodImage` text NOT NULL,
  `price` float NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tblmenulist`
--

INSERT INTO `tblmenulist` (`prodId`, `prodName`, `prodImage`, `price`) VALUES
(0, 'Hamburger', '', 69),
(1, 'Chickenburger', '', 79),
(2, 'Borgir', '', 99),
(3, 'BurgerLight Special', '', 199);

-- --------------------------------------------------------

--
-- Table structure for table `tblorderlist`
--

CREATE TABLE `tblorderlist` (
  `userid` int(11) NOT NULL,
  `prodId` int(11) NOT NULL,
  `quantity` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `tbluserinfo`
--

CREATE TABLE `tbluserinfo` (
  `user_no` varchar(11) NOT NULL,
  `lastname` varchar(50) NOT NULL,
  `firstname` varchar(50) NOT NULL,
  `email` varchar(50) NOT NULL,
  `tel_num` varchar(11) NOT NULL,
  `street` varchar(255) NOT NULL,
  `city` varchar(30) NOT NULL,
  `province` varchar(30) NOT NULL,
  `zip_code` varchar(4) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `tbluserlogin`
--

CREATE TABLE `tbluserlogin` (
  `userid` int(11) NOT NULL,
  `username` varchar(15) NOT NULL,
  `hashedpass` varchar(64) NOT NULL,
  `salt` varchar(200) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tbluserlogin`
--



--
-- Indexes for dumped tables
--

--
-- Indexes for table `tbluserlogin`
--
ALTER TABLE `tbluserlogin`
  ADD PRIMARY KEY (`userid`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `tbluserlogin`
--
ALTER TABLE `tbluserlogin`
  MODIFY `userid` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
