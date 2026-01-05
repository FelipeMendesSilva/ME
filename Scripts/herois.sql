CREATE TABLE `herois` (
  `id` int NOT NULL AUTO_INCREMENT,
  `nome` varchar(100) NOT NULL,
  `nome_heroi` varchar(100) NOT NULL,
  `data_nascimento` date DEFAULT NULL,
  `altura` double NOT NULL,
  `peso` double NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=26 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;