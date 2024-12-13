-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Dec 13, 2024 at 03:32 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

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

--
-- Dumping data for table `administratorius`
--

INSERT INTO `administratorius` (`admin_user`, `slaptazodis`, `vardas`, `pavarde`, `fk_MOKYKLAmokyklos_id`) VALUES
('A-Ausra', 'Ausra123', 'Aušra', 'Aušrienė', 1),
('A-Birute', 'Birute123', 'Birutė', 'Birutienė', 1),
('A-Darius', 'Darius123', 'Darius', 'Dariušis', 1),
('A-Egle', 'Egle123', 'Eglė', 'Eglaitė', 1),
('A-Ieva', 'Ieva123', 'Ieva', 'Ievienė', 1),
('A-Jonas', 'Jonas123', 'Jonas', 'Jonaitis', 1),
('A-Mindaugas', 'Mindaugas123', 'Mindaugas', 'Mindaugaitis', 1),
('A-Petras', 'Petras123', 'Petras', 'Petraitis', 1),
('A-Rasa', 'Rasa123', 'Rasa', 'Rasienė', 1),
('A-Vytautas', 'Vytautas123', 'Vytautas', 'Vytautaitis', 1);

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

--
-- Dumping data for table `atsiliepimas`
--

INSERT INTO `atsiliepimas` (`atsiliepimo_id`, `data`, `turinys`, `tipas`, `fk_MOKINYSmokinio_useris`, `fk_MOKYTOJASmokytojo_useris`) VALUES
(11, '2024-12-13', 'Marius rodo puikius rezultatus, tačiau kartais turi daugiau dėmesio skirti užduotims atlikti.', 1, 'S-Marius', 'T-Giedrius'),
(12, '2024-12-12', 'Aistė labai gerai supranta teoriją, tačiau turi pasistengti su praktiniais uždaviniais.', 1, 'S-Aistė', 'T-Ingrida'),
(13, '2024-12-11', 'Jonas stengiasi, tačiau kartais pamiršta atlikti namų darbus laiku.', 1, 'S-Jonas', 'T-Ramūnas'),
(14, '2024-12-10', 'Viltė gerai išmoksta medžiagą, tačiau kartais pasiklysta sudėtingesnėse užduotyse.', 1, 'S-Viltė', 'T-Giedrė'),
(15, '2024-12-09', 'Dainius labai aktyvus, tačiau kartais reikia padėti susikaupti pamokose.', 2, 'S-Dainius', 'T-Saulius'),
(16, '2024-12-08', 'Laura puikiai atlieka užduotis, tačiau turi pasistengti aktyviau dalyvauti pamokose.', 1, 'S-Laura', 'T-Jūratė'),
(17, '2024-12-07', 'Edgaras rodo puikius rezultatus ir yra labai motyvuotas, tačiau turi pasistengti su grupiniu darbu.', 2, 'S-Edgaras', 'T-Rytis'),
(18, '2024-12-06', 'Simona stengiasi ir gerai atlieka užduotis, tačiau turi pasistengti su laikymo metodais.', 1, 'S-Simona', 'T-Diana'),
(19, '2024-12-05', 'Kamilė yra labai atsakinga ir užtikrinta savo sprendimuose, tačiau kartais per daug stengiasi, kas gali sukelti stresą.', 2, 'S-Kamilė', 'T-Lukas'),
(20, '2024-12-04', 'Mantas labai gerai supranta medžiagą ir moka paaiškinti kitiems, tačiau turėtų pasistengti mažiau kalbėti ir daugiau klausytis.', 1, 'S-Mantas', 'T-Rita');

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
(1, 'Pastaba'),
(2, 'Pagyrimas');

-- --------------------------------------------------------

--
-- Table structure for table `dalykas`
--

CREATE TABLE `dalykas` (
  `dalyko_id` int(20) NOT NULL,
  `pavadinimas` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_lithuanian_ci;

--
-- Dumping data for table `dalykas`
--

INSERT INTO `dalykas` (`dalyko_id`, `pavadinimas`) VALUES
(1, 'Matematika'),
(2, 'Lietuvių kalba'),
(3, 'Anglų kalba'),
(4, 'Biologija'),
(5, 'Fizika'),
(6, 'Chemija'),
(7, 'Istorija'),
(8, 'Geografija'),
(9, 'Informatika'),
(10, 'Kūno kultūra'),
(11, 'Etika'),
(12, 'Technologijos'),
(13, 'Ekonomika'),
(14, 'Muzika'),
(15, 'Dailė');

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
(1, 'Pirmadienis'),
(2, 'Antradienis'),
(3, 'Trečiadienis'),
(4, 'Ketvirtadienis'),
(5, 'Penktadienis');

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

--
-- Dumping data for table `ivertinimas`
--

INSERT INTO `ivertinimas` (`ivertinimo_id`, `pazymys`, `data`, `fk_MOKINYSmokinio_useris`, `fk_PAMOKApamokos_id`, `fk_IVERTINIMO_SVORISsvorio_id`) VALUES
(21, 8, '2024-12-01', 'S-Marius', 1, 1),
(22, 7, '2024-12-02', 'S-Aistė', 2, 2),
(23, 10, '2024-12-03', 'S-Jonas', 3, 3),
(24, 6, '2024-12-04', 'S-Viltė', 4, 4),
(25, 9, '2024-12-05', 'S-Dainius', 5, 1),
(26, 7, '2024-12-06', 'S-Laura', 6, 2),
(27, 8, '2024-12-07', 'S-Edgaras', 7, 3),
(28, 9, '2024-12-08', 'S-Simona', 8, 4),
(29, 10, '2024-12-09', 'S-Kamilė', 9, 1),
(30, 8, '2024-12-10', 'S-Mantas', 10, 2),
(31, 7, '2024-12-11', 'S-Tomas', 11, 3),
(32, 8, '2024-12-12', 'S-Kristina', 12, 4),
(33, 6, '2024-12-13', 'S-Vytautas', 13, 1),
(34, 7, '2024-12-14', 'S-Eglė', 14, 2),
(35, 9, '2024-12-15', 'S-Lukas', 15, 3),
(36, 10, '2024-12-16', 'S-Agne', 16, 4),
(37, 8, '2024-12-17', 'S-Giedrius', 17, 1),
(38, 7, '2024-12-18', 'S-Rūta', 18, 2),
(39, 9, '2024-12-19', 'S-Mindaugas', 19, 3),
(40, 10, '2024-12-20', 'S-Sandra', 20, 4);

-- --------------------------------------------------------

--
-- Table structure for table `ivertinimo_svoris`
--

CREATE TABLE `ivertinimo_svoris` (
  `svorio_id` int(20) NOT NULL,
  `svoris` double NOT NULL,
  `tipas` int(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_lithuanian_ci;

--
-- Dumping data for table `ivertinimo_svoris`
--

INSERT INTO `ivertinimo_svoris` (`svorio_id`, `svoris`, `tipas`) VALUES
(1, 0.2, 1),
(2, 0.4, 2),
(3, 0.3, 3),
(4, 0.1, 4);

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
(3, 'rašinys'),
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
(5, 'a', 30, 'T-Giedrius'),
(5, 'b', 30, 'T-Ingrida'),
(5, 'c', 30, 'T-Ramūnas'),
(5, 'd', 30, 'T-Giedrė'),
(6, 'a', 30, 'T-Saulius'),
(6, 'b', 30, 'T-Jūratė'),
(6, 'c', 30, 'T-Rytis'),
(6, 'd', 30, 'T-Diana'),
(7, 'a', 30, 'T-Lukas'),
(7, 'b', 30, 'T-Rita'),
(8, 'a', 30, 'T-Giedrius'),
(8, 'b', 30, 'T-Ingrida'),
(8, 'c', 30, 'T-Ramūnas'),
(8, 'd', 30, 'T-Giedrė');

-- --------------------------------------------------------

--
-- Table structure for table `mokinio_tevas`
--

CREATE TABLE `mokinio_tevas` (
  `fk_MOKINYSmokinio_useris` varchar(255) NOT NULL,
  `fk_TEVAStevo_useris` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_lithuanian_ci;

--
-- Dumping data for table `mokinio_tevas`
--

INSERT INTO `mokinio_tevas` (`fk_MOKINYSmokinio_useris`, `fk_TEVAStevo_useris`) VALUES
('S-Agne', 'P-Lina'),
('S-Aistė', 'P-Rasa'),
('S-Dainius', 'P-Marius'),
('S-Dainora', 'P-Egidijus'),
('S-Daniela', 'P-Asta'),
('S-Danielius', 'P-Audrius'),
('S-Edgaras', 'P-Egidijus'),
('S-Eglė', 'P-Asta'),
('S-Giedrius', 'P-Egidijus'),
('S-Ilona', 'P-Lina'),
('S-Jonas', 'P-Dainius'),
('S-Justas', 'P-Marius'),
('S-Kamilė', 'P-Algirdas'),
('S-Kristina', 'P-Rasa'),
('S-Laura', 'P-Lina'),
('S-Lina', 'P-Gintarė'),
('S-Lukas', 'P-Marius'),
('S-Mantas', 'P-Viktorija'),
('S-Marius', 'P-Audrius'),
('S-Mindaugas', 'P-Algirdas'),
('S-Monika', 'P-Rasa'),
('S-Paulius', 'P-Dainius'),
('S-Rokas', 'P-Algirdas'),
('S-Rūta', 'P-Gintarė'),
('S-Sandra', 'P-Viktorija'),
('S-Simona', 'P-Gintarė'),
('S-Tomas', 'P-Audrius'),
('S-Vaidotas', 'P-Viktorija'),
('S-Viltė', 'P-Asta'),
('S-Vytautas', 'P-Dainius');

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
  `asmens_kodas` bigint(50) NOT NULL,
  `miestas` varchar(255) NOT NULL,
  `gatve` varchar(255) NOT NULL,
  `namas` varchar(255) NOT NULL,
  `butas` varchar(255) NOT NULL,
  `fk_MOKYKLAmokyklos_id` int(20) NOT NULL,
  `fk_KLASEraide` varchar(255) NOT NULL,
  `fk_KLASEkelinta` int(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_lithuanian_ci;

--
-- Dumping data for table `mokinys`
--

INSERT INTO `mokinys` (`mokinio_useris`, `slaptazodis`, `vardas`, `pavarde`, `gimimo_data`, `asmens_kodas`, `miestas`, `gatve`, `namas`, `butas`, `fk_MOKYKLAmokyklos_id`, `fk_KLASEraide`, `fk_KLASEkelinta`) VALUES
('S-Agne', 'Agne123', 'Agne', 'Agne', '2002-06-21', 48906150016, 'Alytus', 'Alytaus g.', '36', '7', 1, 'd', 8),
('S-Aistė', 'Aistė123', 'Aistė', 'Aistė', '2005-04-15', 48602150002, 'Kaunas', 'Laisvės al.', '15', '2', 1, 'b', 5),
('S-Dainius', 'Dainius123', 'Dainius', 'Dainius', '2004-07-14', 37605150005, 'Panevėžys', 'J. Basanavičiaus g.', '30', '5', 1, 'a', 6),
('S-Dainora', 'Dainora123', 'Dainora', 'Dainora', '2002-09-30', 38107150027, 'Marijampolė', 'Marijampolės g.', '41', '8', 1, 'c', 7),
('S-Daniela', 'Daniela123', 'Daniela', 'Daniela', '2004-02-28', 48204150024, 'Šiauliai', 'Vilniaus g.', '27', '5', 1, 'd', 6),
('S-Danielius', 'Danielius123', 'Danielius', 'Danielius', '2004-11-14', 38912150021, 'Vilnius', 'Gedimino pr.', '14', '2', 1, 'a', 6),
('S-Edgaras', 'Edgaras123', 'Edgaras', 'Edgaras', '2004-09-25', 38107150007, 'Marijampolė', 'Marijampolės g.', '40', '7', 1, 'c', 6),
('S-Eglė', 'Eglė123', 'Eglė', 'Eglė', '2003-04-18', 48204150014, 'Šiauliai', 'Vilniaus g.', '28', '5', 1, 'b', 8),
('S-Giedrius', 'Giedrius123', 'Giedrius', 'Giedrius', '2005-07-13', 38107150017, 'Marijampolė', 'Marijampolės g.', '42', '8', 1, 'a', 5),
('S-Ilona', 'Ilona123', 'Ilona', 'Ilona', '2002-08-25', 48906150026, 'Alytus', 'Alytaus g.', '37', '7', 1, 'b', 7),
('S-Jonas', 'Jonas123', 'Jonas', 'Jonas', '2005-05-20', 37903150003, 'Klaipėda', 'Taikos pr.', '20', '3', 1, 'c', 5),
('S-Justas', 'Justas123', 'Justas', 'Justas', '2002-07-19', 37605150025, 'Panevėžys', 'J. Basanavičiaus g.', '33', '6', 1, 'a', 7),
('S-Kamilė', 'Kamilė123', 'Kamilė', 'Kamilė', '2003-11-18', 37509150009, 'Utena', 'Utenos g.', '50', '9', 1, 'a', 7),
('S-Kristina', 'Kristina123', 'Kristina', 'Kristina', '2005-02-25', 48602150012, 'Kaunas', 'Laisvės al.', '16', '3', 1, 'd', 7),
('S-Laura', 'Laura123', 'Laura', 'Laura', '2004-08-20', 48906150006, 'Alytus', 'Alytaus g.', '35', '6', 1, 'b', 6),
('S-Lina', 'Lina123', 'Lina', 'Lina', '2002-10-12', 49008150028, 'Telšiai', 'Telšių g.', '47', '9', 1, 'd', 7),
('S-Lukas', 'Lukas123', 'Lukas', 'Lukas', '2002-05-08', 37605150015, 'Panevėžys', 'J. Basanavičiaus g.', '32', '6', 1, 'c', 8),
('S-Mantas', 'Mantas123', 'Mantas', 'Mantas', '2003-12-05', 48310150010, 'Biržai', 'Biržų g.', '55', '10', 1, 'b', 7),
('S-Marius', 'Marius123', 'Marius', 'Marius', '2005-03-12', 38912150001, 'Vilnius', 'Gedimino pr.', '10', '1', 1, 'a', 5),
('S-Mindaugas', 'Mindaugas123', 'Mindaugas', 'Mindaugas', '2005-09-15', 37509150019, 'Utena', 'Utenos g.', '52', '10', 1, 'c', 5),
('S-Monika', 'Monika123', 'Monika', 'Monika', '2004-12-02', 48602150022, 'Kaunas', 'Laisvės al.', '18', '3', 1, 'b', 6),
('S-Paulius', 'Paulius123', 'Paulius', 'Paulius', '2004-01-13', 37903150023, 'Klaipėda', 'Taikos pr.', '24', '4', 1, 'c', 6),
('S-Rokas', 'Rokas123', 'Rokas', 'Rokas', '2003-01-09', 37509150029, 'Utena', 'Utenos g.', '51', '10', 1, 'a', 8),
('S-Rūta', 'Rūta123', 'Rūta', 'Rūta', '2005-08-22', 49008150018, 'Telšiai', 'Telšių g.', '46', '9', 1, 'b', 5),
('S-Sandra', 'Sandra123', 'Sandra', 'Sandra', '2005-10-30', 48310150020, 'Biržai', 'Biržų g.', '56', '11', 1, 'd', 5),
('S-Simona', 'Simona123', 'Simona', 'Simona', '2004-10-05', 49008150008, 'Telšiai', 'Telšių g.', '45', '8', 1, 'd', 6),
('S-Tomas', 'Tomas123', 'Tomas', 'Tomas', '2005-01-10', 38912150011, 'Vilnius', 'Gedimino pr.', '12', '2', 1, 'c', 7),
('S-Vaidotas', 'Vaidotas123', 'Vaidotas', 'Vaidotas', '2003-02-14', 48310150030, 'Biržai', 'Biržų g.', '55', '11', 1, 'b', 8),
('S-Viltė', 'Viltė123', 'Viltė', 'Viltė', '2005-06-10', 48204150004, 'Šiauliai', 'Vilniaus g.', '25', '4', 1, 'd', 5),
('S-Vytautas', 'Vytautas123', 'Vytautas', 'Vytautas', '2003-03-03', 37903150013, 'Klaipėda', 'Taikos pr.', '22', '4', 1, 'a', 8);

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

--
-- Dumping data for table `mokykla`
--

INSERT INTO `mokykla` (`mokyklos_id`, `pavadinimas`, `telefono_nr`, `el_pastas`, `miestas`, `gatve`, `namas`) VALUES
(1, 'Kauno Jono Basanavičiaus gimnazija', '837123456', 'info@basanaviciausgimnazija.lt', 'Kaunas', 'Laisvės al.', '102');

-- --------------------------------------------------------

--
-- Table structure for table `mokytojas`
--

CREATE TABLE `mokytojas` (
  `mokytojo_useris` varchar(255) NOT NULL,
  `slaptazodis` varchar(255) NOT NULL,
  `vardas` varchar(255) NOT NULL,
  `pavarde` varchar(255) NOT NULL,
  `asmens_kodas` bigint(20) NOT NULL,
  `telefono_nr` varchar(255) NOT NULL,
  `el_pastas` varchar(255) NOT NULL,
  `miestas` varchar(255) NOT NULL,
  `gatve` varchar(255) NOT NULL,
  `namas` varchar(255) NOT NULL,
  `butas` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_lithuanian_ci;

--
-- Dumping data for table `mokytojas`
--

INSERT INTO `mokytojas` (`mokytojo_useris`, `slaptazodis`, `vardas`, `pavarde`, `asmens_kodas`, `telefono_nr`, `el_pastas`, `miestas`, `gatve`, `namas`, `butas`) VALUES
('T-Diana', 'Diana123', 'Diana', 'Dianaitė', 49008150008, '861901234', 'diana.dianaite@example.com', 'Telšiai', 'Telšių g.', '45', '8'),
('T-Giedrė', 'Giedre123', 'Giedrė', 'Giedraitė', 48204150004, '861567890', 'giedre.giedraite@example.com', 'Šiauliai', 'Vilniaus g.', '25', '4'),
('T-Giedrius', 'Giedrius123', 'Giedrius', 'Gedraitis', 38912150001, '861234567', 'giedrius.gedraitis@example.com', 'Vilnius', 'Gedimino pr.', '10', '1'),
('T-Ingrida', 'Ingrida123', 'Ingrida', 'Ingridaitė', 48602150002, '861345678', 'ingrida.ingridaite@example.com', 'Kaunas', 'Laisvės al.', '15', '2'),
('T-Jūratė', 'Jurate123', 'Jūratė', 'Juraitė', 48906150006, '861789012', 'jurate.juraite@example.com', 'Alytus', 'Alytaus g.', '35', '6'),
('T-Lukas', 'Lukas123', 'Lukas', 'Lukauskas', 37509150009, '861012345', 'lukas.lukauskas@example.com', 'Utena', 'Utenos g.', '50', '9'),
('T-Ramūnas', 'Ramunas123', 'Ramūnas', 'Ramanauskas', 37903150003, '861456789', 'ramunas.ramanauskas@example.com', 'Klaipėda', 'Taikos pr.', '20', '3'),
('T-Rita', 'Rita123', 'Rita', 'Ritienė', 48310150010, '861123456', 'rita.ritiene@example.com', 'Biržai', 'Biržų g.', '55', '10'),
('T-Rytis', 'Rytis123', 'Rytis', 'Ryčkauskas', 38107150007, '861890123', 'rytis.ryckauskas@example.com', 'Marijampolė', 'Marijampolės g.', '40', '7'),
('T-Saulius', 'Saulius123', 'Saulius', 'Sauliūnas', 37605150005, '861678901', 'saulius.sauliunas@example.com', 'Panevėžys', 'J. Basanavičiaus g.', '30', '5');

-- --------------------------------------------------------

--
-- Table structure for table `mokytojo_dalykas`
--

CREATE TABLE `mokytojo_dalykas` (
  `fk_DALYKASdalyko_id` int(20) NOT NULL,
  `fk_MOKYTOJASmokytojo_useris` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_lithuanian_ci;

--
-- Dumping data for table `mokytojo_dalykas`
--

INSERT INTO `mokytojo_dalykas` (`fk_DALYKASdalyko_id`, `fk_MOKYTOJASmokytojo_useris`) VALUES
(1, 'T-Giedrius'),
(1, 'T-Jūratė'),
(1, 'T-Lukas'),
(2, 'T-Ingrida'),
(3, 'T-Ramūnas'),
(4, 'T-Giedrė'),
(4, 'T-Giedrius'),
(5, 'T-Saulius'),
(6, 'T-Jūratė'),
(7, 'T-Rytis'),
(8, 'T-Diana'),
(8, 'T-Rytis'),
(9, 'T-Lukas'),
(10, 'T-Rita');

-- --------------------------------------------------------

--
-- Table structure for table `mokytojo_mokykla`
--

CREATE TABLE `mokytojo_mokykla` (
  `fk_MOKYKLAmokyklos_id` int(20) NOT NULL,
  `fk_MOKYTOJASmokytojo_useris` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_lithuanian_ci;

--
-- Dumping data for table `mokytojo_mokykla`
--

INSERT INTO `mokytojo_mokykla` (`fk_MOKYKLAmokyklos_id`, `fk_MOKYTOJASmokytojo_useris`) VALUES
(1, 'T-Diana'),
(1, 'T-Giedrė'),
(1, 'T-Giedrius'),
(1, 'T-Ingrida'),
(1, 'T-Jūratė'),
(1, 'T-Lukas'),
(1, 'T-Ramūnas'),
(1, 'T-Rita'),
(1, 'T-Rytis'),
(1, 'T-Saulius');

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

--
-- Dumping data for table `pamoka`
--

INSERT INTO `pamoka` (`pamokos_id`, `pradzios_laikas`, `pabaigos_laikas`, `diena`, `fk_MOKYTOJASmokytojo_useris`, `fk_DALYKASdalyko_id`) VALUES
(1, '08:00', '08:45', 1, 'T-Giedrius', 1),
(2, '09:00', '09:45', 1, 'T-Giedrius', 4),
(3, '10:00', '10:45', 1, 'T-Ingrida', 2),
(4, '11:00', '11:45', 1, 'T-Ramūnas', 3),
(5, '12:00', '12:45', 1, 'T-Giedrė', 4),
(6, '13:00', '13:45', 1, 'T-Saulius', 5),
(7, '08:00', '08:45', 2, 'T-Jūratė', 6),
(8, '09:00', '09:45', 2, 'T-Jūratė', 1),
(9, '10:00', '10:45', 2, 'T-Rytis', 7),
(10, '11:00', '11:45', 2, 'T-Rytis', 8),
(11, '12:00', '12:45', 2, 'T-Diana', 8),
(12, '13:00', '13:45', 2, 'T-Lukas', 9),
(13, '08:00', '08:45', 3, 'T-Lukas', 1),
(14, '09:00', '09:45', 3, 'T-Rita', 10),
(15, '10:00', '10:45', 3, 'T-Giedrius', 1),
(16, '11:00', '11:45', 3, 'T-Giedrius', 4),
(17, '12:00', '12:45', 3, 'T-Ingrida', 2),
(18, '13:00', '13:45', 3, 'T-Ramūnas', 3),
(19, '08:00', '08:45', 4, 'T-Saulius', 5),
(20, '09:00', '09:45', 4, 'T-Saulius', 6),
(21, '10:00', '10:45', 4, 'T-Giedrė', 4),
(22, '11:00', '11:45', 4, 'T-Giedrė', 1),
(23, '12:00', '12:45', 4, 'T-Rytis', 7),
(24, '13:00', '13:45', 4, 'T-Rytis', 8);

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

--
-- Dumping data for table `tevas`
--

INSERT INTO `tevas` (`tevo_useris`, `slaptazodis`, `vardas`, `pavarde`, `telefono_nr`, `el_pastas`, `miestas`, `gatve`, `namas`, `butas`) VALUES
('P-Algirdas', 'Algirdas123', 'Algirdas', 'Algirdukas', '861012345', 'algirdas.algirdukas@example.com', 'Utena', 'Utenos g.', '50', '9'),
('P-Asta', 'Asta123', 'Asta', 'Astaitė', '861567890', 'asta.astaitė@example.com', 'Šiauliai', 'Vilniaus g.', '25', '4'),
('P-Audrius', 'Audrius123', 'Audrius', 'Audriukaitis', '861234567', 'audrius.audriukaitis@example.com', 'Vilnius', 'Gedimino pr.', '10', '1'),
('P-Dainius', 'Dainius123', 'Dainius', 'Dainiukas', '861456789', 'dainius.dainiukas@example.com', 'Klaipėda', 'Taikos pr.', '20', '3'),
('P-Egidijus', 'Egidijus123', 'Egidijus', 'Egidijaitis', '861890123', 'egidijus.egidijaitis@example.com', 'Marijampolė', 'Marijampolės g.', '40', '7'),
('P-Gintarė', 'Gintarė123', 'Gintarė', 'Gintaraitė', '861901234', 'gintare.gintaraite@example.com', 'Telšiai', 'Telšių g.', '45', '8'),
('P-Lina', 'Lina123', 'Lina', 'Linienė', '861789012', 'lina.linienė@example.com', 'Alytus', 'Alytaus g.', '35', '6'),
('P-Marius', 'Marius123', 'Marius', 'Mariukas', '861678901', 'marius.mariukas@example.com', 'Panevėžys', 'J. Basanavičiaus g.', '30', '5'),
('P-Rasa', 'Rasa123', 'Rasa', 'Rasienė', '861345678', 'rasa.rasiene@example.com', 'Kaunas', 'Laisvės al.', '15', '2'),
('P-Viktorija', 'Viktorija123', 'Viktorija', 'Viktorienė', '861123456', 'viktorija.viktorienė@example.com', 'Biržai', 'Biržų g.', '55', '10');

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
-- Dumping data for table `tvarkarastis`
--

INSERT INTO `tvarkarastis` (`fk_PAMOKApamokos_id`, `fk_KLASEraide`, `fk_KLASEkelinta`) VALUES
(1, 'a', 5),
(17, 'a', 5),
(2, 'b', 5),
(18, 'b', 5),
(3, 'c', 5),
(4, 'd', 5),
(5, 'a', 6),
(6, 'b', 6),
(7, 'c', 6),
(19, 'c', 6),
(8, 'd', 6),
(20, 'd', 6),
(9, 'a', 7),
(10, 'b', 7),
(11, 'c', 7),
(12, 'd', 7),
(13, 'a', 8),
(14, 'b', 8),
(15, 'c', 8),
(16, 'd', 8);

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
  ADD KEY `apibudina` (`fk_MOKINYSmokinio_useris`),
  ADD KEY `atsiliepimas_ibfk_1` (`tipas`),
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
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `atsiliepimas`
--
ALTER TABLE `atsiliepimas`
  MODIFY `atsiliepimo_id` int(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- AUTO_INCREMENT for table `dalykas`
--
ALTER TABLE `dalykas`
  MODIFY `dalyko_id` int(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;

--
-- AUTO_INCREMENT for table `ivertinimas`
--
ALTER TABLE `ivertinimas`
  MODIFY `ivertinimo_id` int(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=41;

--
-- AUTO_INCREMENT for table `ivertinimo_svoris`
--
ALTER TABLE `ivertinimo_svoris`
  MODIFY `svorio_id` int(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `mokykla`
--
ALTER TABLE `mokykla`
  MODIFY `mokyklos_id` int(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `pamoka`
--
ALTER TABLE `pamoka`
  MODIFY `pamokos_id` int(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=25;

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
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
