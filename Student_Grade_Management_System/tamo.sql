-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: localhost
-- Generation Time: Dec 09, 2024 at 11:40 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.0.30

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `tamo`
--

-- --------------------------------------------------------

--
-- Table structure for table `administratorius`
--

CREATE TABLE `administratorius` (
  `admin_user` varchar(255) NOT NULL,
  `slaptazodis` varchar(255) NOT NULL,
  `vardas` varchar(255) NOT NULL,
  `pavarde` varchar(255) NOT NULL,
  `fk_MOKYKLAmokyklos_id` int(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_lithuanian_ci;

-- --------------------------------------------------------

--
-- Table structure for table `atsiliepimas`
--

CREATE TABLE `atsiliepimas` (
  `atsiliepimo_id` int(20) NOT NULL,
  `data` date NOT NULL,
  `turinys` varchar(255) NOT NULL,
  `tipas` int(20) NOT NULL,
  `fk_MOKINYSmokinio_useris` varchar(255) NOT NULL,
  `fk_MOKYTOJASmokytojo_useris` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_lithuanian_ci;

-- --------------------------------------------------------

--
-- Table structure for table `atsiliepimo_tipas`
--

CREATE TABLE `atsiliepimo_tipas` (
  `id_atsiliepimo_tipas` int(20) NOT NULL,
  `name` char(9) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_lithuanian_ci;

--
-- Dumping data for table `atsiliepimo_tipas`
--

INSERT INTO `atsiliepimo_tipas` (`id_atsiliepimo_tipas`, `name`) VALUES
(1, 'pastaba'),
(2, 'pagyrimas');

-- --------------------------------------------------------

--
-- Table structure for table `dalykas`
--

CREATE TABLE `dalykas` (
  `dalyko_id` int(20) NOT NULL,
  `pavadinimas` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_lithuanian_ci;

-- --------------------------------------------------------

--
-- Table structure for table `darbo_diena`
--

CREATE TABLE `darbo_diena` (
  `id_darbo_diena` int(20) NOT NULL,
  `name` char(14) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_lithuanian_ci;

--
-- Dumping data for table `darbo_diena`
--

INSERT INTO `darbo_diena` (`id_darbo_diena`, `name`) VALUES
(1, 'pirmadienis'),
(2, 'antradienis'),
(3, 'treciadienis'),
(4, 'ketvirtadienis'),
(5, 'penktadienis');

-- --------------------------------------------------------

--
-- Table structure for table `ivertinimas`
--

CREATE TABLE `ivertinimas` (
  `ivertinimo_id` int(20) NOT NULL,
  `pazymys` int(10) NOT NULL,
  `data` date NOT NULL,
  `fk_MOKINYSmokinio_useris` varchar(255) NOT NULL,
  `fk_PAMOKApamokos_id` int(20) NOT NULL,
  `fk_IVERTINIMO_SVORISsvorio_id` int(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_lithuanian_ci;

-- --------------------------------------------------------

--
-- Table structure for table `ivertinimo_svoris`
--

CREATE TABLE `ivertinimo_svoris` (
  `svorio_id` int(20) NOT NULL,
  `svoris` double NOT NULL,
  `tipas` int(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_lithuanian_ci;

-- --------------------------------------------------------

--
-- Table structure for table `ivertinimo_tipas`
--

CREATE TABLE `ivertinimo_tipas` (
  `id_ivertinimo_tipas` int(20) NOT NULL,
  `name` char(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_lithuanian_ci;

--
-- Dumping data for table `ivertinimo_tipas`
--

INSERT INTO `ivertinimo_tipas` (`id_ivertinimo_tipas`, `name`) VALUES
(1, 'testas'),
(2, 'kontrolinis'),
(3, 'rasinys'),
(4, 'projektas');

-- --------------------------------------------------------

--
-- Table structure for table `klase`
--

CREATE TABLE `klase` (
  `kelinta` int(10) NOT NULL DEFAULT 0,
  `raide` varchar(255) NOT NULL,
  `mokiniu_skaicius` int(20) NOT NULL DEFAULT 0,
  `atsakingo_mokytojo_useris` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_lithuanian_ci;

--
-- Dumping data for table `klase`
--

INSERT INTO `klase` (`kelinta`, `raide`, `mokiniu_skaicius`, `atsakingo_mokytojo_useris`) VALUES
(5, 'a', 24, 'TeacherMan'),
(5, 'b', 24, 'TeacherMan2');

-- --------------------------------------------------------

--
-- Table structure for table `mokinio_tevas`
--

CREATE TABLE `mokinio_tevas` (
  `fk_MOKINYSmokinio_useris` varchar(255) NOT NULL,
  `fk_TEVAStevo_useris` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_lithuanian_ci;

-- --------------------------------------------------------

--
-- Table structure for table `mokinys`
--

CREATE TABLE `mokinys` (
  `mokinio_useris` varchar(255) NOT NULL,
  `slaptazodis` varchar(255) NOT NULL,
  `vardas` varchar(255) NOT NULL,
  `pavarde` varchar(255) NOT NULL,
  `gimimo_data` date NOT NULL,
  `asmens_kodas` int(20) NOT NULL,
  `miestas` varchar(255) NOT NULL,
  `gatve` varchar(255) NOT NULL,
  `namas` varchar(255) NOT NULL,
  `butas` varchar(255) NOT NULL,
  `fk_MOKYKLAmokyklos_id` int(20) NOT NULL,
  `fk_KLASEraide` varchar(255) NOT NULL,
  `fk_KLASEkelinta` int(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_lithuanian_ci;

-- --------------------------------------------------------

--
-- Table structure for table `mokykla`
--

CREATE TABLE `mokykla` (
  `mokyklos_id` int(20) NOT NULL,
  `pavadinimas` varchar(255) NOT NULL,
  `telefono_nr` varchar(255) NOT NULL,
  `el_pastas` varchar(255) NOT NULL,
  `miestas` varchar(255) NOT NULL,
  `gatve` varchar(255) NOT NULL,
  `namas` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_lithuanian_ci;

-- --------------------------------------------------------

--
-- Table structure for table `mokytojas`
--

CREATE TABLE `mokytojas` (
  `mokytojo_useris` varchar(255) NOT NULL,
  `slaptazodis` varchar(255) NOT NULL,
  `vardas` varchar(255) NOT NULL,
  `pavarde` varchar(255) NOT NULL,
  `asmens_kodas` int(20) NOT NULL,
  `telefono_nr` varchar(255) NOT NULL,
  `el_pastas` varchar(255) NOT NULL,
  `miestas` varchar(255) NOT NULL,
  `gatve` varchar(255) NOT NULL,
  `namas` varchar(255) NOT NULL,
  `butas` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_lithuanian_ci;

-- --------------------------------------------------------

--
-- Table structure for table `mokytojo_dalykas`
--

CREATE TABLE `mokytojo_dalykas` (
  `fk_DALYKASdalyko_id` int(20) NOT NULL,
  `fk_MOKYTOJASmokytojo_useris` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_lithuanian_ci;

-- --------------------------------------------------------

--
-- Table structure for table `mokytojo_mokykla`
--

CREATE TABLE `mokytojo_mokykla` (
  `fk_MOKYKLAmokyklos_id` int(20) NOT NULL,
  `fk_MOKYTOJASmokytojo_useris` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_lithuanian_ci;

-- --------------------------------------------------------

--
-- Table structure for table `pamoka`
--

CREATE TABLE `pamoka` (
  `pamokos_id` int(20) NOT NULL,
  `pradzios_laikas` varchar(255) NOT NULL,
  `pabaigos_laikas` varchar(255) NOT NULL,
  `diena` int(20) NOT NULL,
  `fk_MOKYTOJASmokytojo_useris` varchar(255) NOT NULL,
  `fk_DALYKASdalyko_id` int(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_lithuanian_ci;

-- --------------------------------------------------------

--
-- Table structure for table `tevas`
--

CREATE TABLE `tevas` (
  `tevo_useris` varchar(255) NOT NULL,
  `slaptazodis` varchar(255) NOT NULL,
  `vardas` varchar(255) NOT NULL,
  `pavarde` varchar(255) NOT NULL,
  `telefono_nr` varchar(255) NOT NULL,
  `el_pastas` varchar(255) NOT NULL,
  `miestas` varchar(255) NOT NULL,
  `gatve` varchar(255) NOT NULL,
  `namas` varchar(255) NOT NULL,
  `butas` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_lithuanian_ci;

-- --------------------------------------------------------

--
-- Table structure for table `tvarkarastis`
--

CREATE TABLE `tvarkarastis` (
  `fk_PAMOKApamokos_id` int(20) NOT NULL,
  `fk_KLASEraide` varchar(255) NOT NULL,
  `fk_KLASEkelinta` int(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_lithuanian_ci;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `administratorius`
--
ALTER TABLE `administratorius`
  ADD PRIMARY KEY (`admin_user`),
  ADD KEY `administruoja` (`fk_MOKYKLAmokyklos_id`);

--
-- Indexes for table `atsiliepimas`
--
ALTER TABLE `atsiliepimas`
  ADD PRIMARY KEY (`atsiliepimo_id`),
  ADD KEY `tipas` (`tipas`),
  ADD KEY `apibudina` (`fk_MOKINYSmokinio_useris`),
  ADD KEY `raso` (`fk_MOKYTOJASmokytojo_useris`);

--
-- Indexes for table `atsiliepimo_tipas`
--
ALTER TABLE `atsiliepimo_tipas`
  ADD PRIMARY KEY (`id_atsiliepimo_tipas`);

--
-- Indexes for table `dalykas`
--
ALTER TABLE `dalykas`
  ADD PRIMARY KEY (`dalyko_id`);

--
-- Indexes for table `darbo_diena`
--
ALTER TABLE `darbo_diena`
  ADD PRIMARY KEY (`id_darbo_diena`);

--
-- Indexes for table `ivertinimas`
--
ALTER TABLE `ivertinimas`
  ADD PRIMARY KEY (`ivertinimo_id`),
  ADD KEY `gauna` (`fk_MOKINYSmokinio_useris`),
  ADD KEY `suteikia` (`fk_PAMOKApamokos_id`),
  ADD KEY `atitinka` (`fk_IVERTINIMO_SVORISsvorio_id`);

--
-- Indexes for table `ivertinimo_svoris`
--
ALTER TABLE `ivertinimo_svoris`
  ADD PRIMARY KEY (`svorio_id`),
  ADD KEY `tipas` (`tipas`);

--
-- Indexes for table `ivertinimo_tipas`
--
ALTER TABLE `ivertinimo_tipas`
  ADD PRIMARY KEY (`id_ivertinimo_tipas`);

--
-- Indexes for table `klase`
--
ALTER TABLE `klase`
  ADD PRIMARY KEY (`kelinta`,`raide`);

--
-- Indexes for table `mokinio_tevas`
--
ALTER TABLE `mokinio_tevas`
  ADD PRIMARY KEY (`fk_MOKINYSmokinio_useris`,`fk_TEVAStevo_useris`);

--
-- Indexes for table `mokinys`
--
ALTER TABLE `mokinys`
  ADD PRIMARY KEY (`mokinio_useris`),
  ADD KEY `mokosi` (`fk_MOKYKLAmokyklos_id`),
  ADD KEY `priklauso` (`fk_KLASEkelinta`,`fk_KLASEraide`);

--
-- Indexes for table `mokykla`
--
ALTER TABLE `mokykla`
  ADD PRIMARY KEY (`mokyklos_id`);

--
-- Indexes for table `mokytojas`
--
ALTER TABLE `mokytojas`
  ADD PRIMARY KEY (`mokytojo_useris`);

--
-- Indexes for table `mokytojo_dalykas`
--
ALTER TABLE `mokytojo_dalykas`
  ADD PRIMARY KEY (`fk_DALYKASdalyko_id`,`fk_MOKYTOJASmokytojo_useris`);

--
-- Indexes for table `mokytojo_mokykla`
--
ALTER TABLE `mokytojo_mokykla`
  ADD PRIMARY KEY (`fk_MOKYKLAmokyklos_id`,`fk_MOKYTOJASmokytojo_useris`);

--
-- Indexes for table `pamoka`
--
ALTER TABLE `pamoka`
  ADD PRIMARY KEY (`pamokos_id`),
  ADD KEY `diena` (`diena`),
  ADD KEY `veda` (`fk_MOKYTOJASmokytojo_useris`),
  ADD KEY `itraukia` (`fk_DALYKASdalyko_id`);

--
-- Indexes for table `tevas`
--
ALTER TABLE `tevas`
  ADD PRIMARY KEY (`tevo_useris`);

--
-- Indexes for table `tvarkarastis`
--
ALTER TABLE `tvarkarastis`
  ADD PRIMARY KEY (`fk_KLASEkelinta`,`fk_KLASEraide`,`fk_PAMOKApamokos_id`),
  ADD KEY `vyksta` (`fk_PAMOKApamokos_id`);

--
-- Constraints for dumped tables
--

--
-- Constraints for table `administratorius`
--
ALTER TABLE `administratorius`
  ADD CONSTRAINT `administruoja` FOREIGN KEY (`fk_MOKYKLAmokyklos_id`) REFERENCES `mokykla` (`mokyklos_id`);

--
-- Constraints for table `atsiliepimas`
--
ALTER TABLE `atsiliepimas`
  ADD CONSTRAINT `apibudina` FOREIGN KEY (`fk_MOKINYSmokinio_useris`) REFERENCES `mokinys` (`mokinio_useris`),
  ADD CONSTRAINT `atsiliepimas_ibfk_1` FOREIGN KEY (`tipas`) REFERENCES `atsiliepimo_tipas` (`id_atsiliepimo_tipas`),
  ADD CONSTRAINT `raso` FOREIGN KEY (`fk_MOKYTOJASmokytojo_useris`) REFERENCES `mokytojas` (`mokytojo_useris`);

--
-- Constraints for table `ivertinimas`
--
ALTER TABLE `ivertinimas`
  ADD CONSTRAINT `atitinka` FOREIGN KEY (`fk_IVERTINIMO_SVORISsvorio_id`) REFERENCES `ivertinimo_svoris` (`svorio_id`),
  ADD CONSTRAINT `gauna` FOREIGN KEY (`fk_MOKINYSmokinio_useris`) REFERENCES `mokinys` (`mokinio_useris`),
  ADD CONSTRAINT `suteikia` FOREIGN KEY (`fk_PAMOKApamokos_id`) REFERENCES `pamoka` (`pamokos_id`);

--
-- Constraints for table `ivertinimo_svoris`
--
ALTER TABLE `ivertinimo_svoris`
  ADD CONSTRAINT `ivertinimo_svoris_ibfk_1` FOREIGN KEY (`tipas`) REFERENCES `ivertinimo_tipas` (`id_ivertinimo_tipas`);

--
-- Constraints for table `mokinio_tevas`
--
ALTER TABLE `mokinio_tevas`
  ADD CONSTRAINT `atstovauja` FOREIGN KEY (`fk_MOKINYSmokinio_useris`) REFERENCES `mokinys` (`mokinio_useris`);

--
-- Constraints for table `mokinys`
--
ALTER TABLE `mokinys`
  ADD CONSTRAINT `mokosi` FOREIGN KEY (`fk_MOKYKLAmokyklos_id`) REFERENCES `mokykla` (`mokyklos_id`),
  ADD CONSTRAINT `priklauso` FOREIGN KEY (`fk_KLASEkelinta`,`fk_KLASEraide`) REFERENCES `klase` (`kelinta`, `raide`);

--
-- Constraints for table `mokytojo_dalykas`
--
ALTER TABLE `mokytojo_dalykas`
  ADD CONSTRAINT `desto` FOREIGN KEY (`fk_DALYKASdalyko_id`) REFERENCES `dalykas` (`dalyko_id`);

--
-- Constraints for table `mokytojo_mokykla`
--
ALTER TABLE `mokytojo_mokykla`
  ADD CONSTRAINT `dirba` FOREIGN KEY (`fk_MOKYKLAmokyklos_id`) REFERENCES `mokykla` (`mokyklos_id`);

--
-- Constraints for table `pamoka`
--
ALTER TABLE `pamoka`
  ADD CONSTRAINT `itraukia` FOREIGN KEY (`fk_DALYKASdalyko_id`) REFERENCES `dalykas` (`dalyko_id`),
  ADD CONSTRAINT `pamoka_ibfk_1` FOREIGN KEY (`diena`) REFERENCES `darbo_diena` (`id_darbo_diena`),
  ADD CONSTRAINT `veda` FOREIGN KEY (`fk_MOKYTOJASmokytojo_useris`) REFERENCES `mokytojas` (`mokytojo_useris`);

--
-- Constraints for table `tvarkarastis`
--
ALTER TABLE `tvarkarastis`
  ADD CONSTRAINT `turi` FOREIGN KEY (`fk_KLASEkelinta`,`fk_KLASEraide`) REFERENCES `klase` (`kelinta`, `raide`),
  ADD CONSTRAINT `vyksta` FOREIGN KEY (`fk_PAMOKApamokos_id`) REFERENCES `pamoka` (`pamokos_id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
