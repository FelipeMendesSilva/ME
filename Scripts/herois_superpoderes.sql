
CREATE TABLE `herois_superpoderes` (
  `heroi_id` int NOT NULL,
  `superpoder_id` int NOT NULL,
  PRIMARY KEY (`heroi_id`,`superpoder_id`),
  KEY `fk_superpoder` (`superpoder_id`),
  CONSTRAINT `fk_heroi` FOREIGN KEY (`heroi_id`) REFERENCES `herois` (`id`),
  CONSTRAINT `fk_superpoder` FOREIGN KEY (`superpoder_id`) REFERENCES `superpoderes` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;