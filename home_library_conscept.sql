-- ---
-- Globals
-- ---

-- SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";
-- SET FOREIGN_KEY_CHECKS=0;

-- ---
-- Table 'owned_books'
--
-- ---

DROP TABLE IF EXISTS `owned_books`;

CREATE TABLE `owned_books` (
  `id` INTEGER NULL AUTO_INCREMENT DEFAULT NULL,
  `book_id` INTEGER NULL DEFAULT NULL,
  `physical_bool` BLOB NULL DEFAULT NULL,
  `storage_id` INTEGER NULL DEFAULT NULL,
  PRIMARY KEY (`id`)
);

-- ---
-- Table 'borrowed_books'
--
-- ---

DROP TABLE IF EXISTS `borrowed_books`;

CREATE TABLE `borrowed_books` (
  `id` INTEGER NULL AUTO_INCREMENT DEFAULT NULL,
  `book_id` INTEGER NULL DEFAULT NULL,
  `due_date` DATETIME NULL DEFAULT NULL,
  `returned_bool` BLOB NULL DEFAULT NULL,
  `source_id` INTEGER NULL DEFAULT NULL,
  PRIMARY KEY (`id`)
);

-- ---
-- Table 'categories'
-- many to many
-- ---

DROP TABLE IF EXISTS `categories`;

CREATE TABLE `categories` (
  `id` INTEGER NULL AUTO_INCREMENT DEFAULT NULL,
  `genre` VARCHAR(255) NULL DEFAULT NULL,
  PRIMARY KEY (`id`)
) COMMENT 'many to many';

-- ---
-- Table 'books_to_sell'
--
-- ---

DROP TABLE IF EXISTS `books_to_sell`;

CREATE TABLE `books_to_sell` (
  `id` INTEGER NULL AUTO_INCREMENT DEFAULT NULL,
  `book_id` INTEGER NULL DEFAULT NULL,
  `sold_bool` BLOB NULL DEFAULT NULL,
  PRIMARY KEY (`id`)
);

-- ---
-- Table 'reading_list'
--
-- ---

DROP TABLE IF EXISTS `reading_list`;

CREATE TABLE `reading_list` (
  `id` INTEGER NULL AUTO_INCREMENT DEFAULT NULL,
  `book_id` INTEGER NULL DEFAULT NULL,
  PRIMARY KEY (`id`)
);

-- ---
-- Table 'lent_books'
--
-- ---

DROP TABLE IF EXISTS `lent_books`;

CREATE TABLE `lent_books` (
  `id` INTEGER NULL AUTO_INCREMENT DEFAULT NULL,
  `owned_book_id` INTEGER NULL DEFAULT NULL,
  `returned_bool` bit NULL DEFAULT NULL,
  `recipient` VARCHAR NULL DEFAULT NULL,
  PRIMARY KEY (`id`)
);

-- ---
-- Table 'all_books'
--
-- ---

DROP TABLE IF EXISTS `all_books`;

CREATE TABLE `all_books` (
  `id` INTEGER NULL AUTO_INCREMENT DEFAULT NULL,
  `title` VARCHAR(255) NULL DEFAULT NULL,
  `author` VARCHAR(255) NULL DEFAULT NULL,
  `read_bool` SET NULL DEFAULT NULL,
  PRIMARY KEY (`id`)
);

-- ---
-- Table 'books_categories'
-- many to many
-- ---

DROP TABLE IF EXISTS `books_categories`;

CREATE TABLE `books_categories` (
  `id` INTEGER NULL AUTO_INCREMENT DEFAULT NULL,
  `book_id` INTEGER NULL DEFAULT NULL,
  `category_id` INTEGER NULL DEFAULT NULL,
  PRIMARY KEY (`id`)
) COMMENT 'many to many';

-- ---
-- Table 'storage'
-- places to keep your books
-- ---

DROP TABLE IF EXISTS `storage`;

CREATE TABLE `storage` (
  `id` INTEGER NULL AUTO_INCREMENT DEFAULT NULL,
  `place_name` VARCHAR(255) NULL DEFAULT NULL,
  PRIMARY KEY (`id`)
) COMMENT 'places to keep your books';

-- ---
-- Table 'wishlist'
--
-- ---

DROP TABLE IF EXISTS `wishlist`;

CREATE TABLE `wishlist` (
  `id` INTEGER NULL AUTO_INCREMENT DEFAULT NULL,
  `book_id` INTEGER NULL DEFAULT NULL,
  PRIMARY KEY (`id`)
);

-- ---
-- Table 'borrow_sources'
--
-- ---

DROP TABLE IF EXISTS `borrow_sources`;

CREATE TABLE `borrow_sources` (
  `id` INTEGER NULL AUTO_INCREMENT DEFAULT NULL,
  `name` VARCHAR(255) NULL DEFAULT NULL,
  PRIMARY KEY (`id`)
);

-- ---
-- Foreign Keys
-- ---

ALTER TABLE `owned_books` ADD FOREIGN KEY (book_id) REFERENCES `all_books` (`id`);
ALTER TABLE `owned_books` ADD FOREIGN KEY (storage_id) REFERENCES `storage` (`id`);
ALTER TABLE `borrowed_books` ADD FOREIGN KEY (book_id) REFERENCES `all_books` (`id`);
ALTER TABLE `borrowed_books` ADD FOREIGN KEY (source_id) REFERENCES `borrow_sources` (`id`);
ALTER TABLE `books_to_sell` ADD FOREIGN KEY (book_id) REFERENCES `all_books` (`id`);
ALTER TABLE `reading_list` ADD FOREIGN KEY (book_id) REFERENCES `all_books` (`id`);
ALTER TABLE `lent_books` ADD FOREIGN KEY (owned_book_id) REFERENCES `owned_books` (`id`);
ALTER TABLE `books_categories` ADD FOREIGN KEY (book_id) REFERENCES `all_books` (`id`);
ALTER TABLE `books_categories` ADD FOREIGN KEY (category_id) REFERENCES `categories` (`id`);
ALTER TABLE `wishlist` ADD FOREIGN KEY (book_id) REFERENCES `all_books` (`id`);

-- ---
-- Table Properties
-- ---

-- ALTER TABLE `owned_books` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
-- ALTER TABLE `borrowed_books` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
-- ALTER TABLE `categories` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
-- ALTER TABLE `books_to_sell` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
-- ALTER TABLE `reading_list` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
-- ALTER TABLE `lent_books` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
-- ALTER TABLE `all_books` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
-- ALTER TABLE `books_categories` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
-- ALTER TABLE `storage` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
-- ALTER TABLE `wishlist` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
-- ALTER TABLE `borrow_sources` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- ---
-- Test Data
-- ---

-- INSERT INTO `owned_books` (`id`,`book_id`,`physical_bool`,`storage_id`) VALUES
-- ('','','','');
-- INSERT INTO `borrowed_books` (`id`,`book_id`,`due_date`,`returned_bool`,`source_id`) VALUES
-- ('','','','','');
-- INSERT INTO `categories` (`id`,`genre`) VALUES
-- ('','');
-- INSERT INTO `books_to_sell` (`id`,`book_id`,`sold_bool`) VALUES
-- ('','','');
-- INSERT INTO `reading_list` (`id`,`book_id`) VALUES
-- ('','');
-- INSERT INTO `lent_books` (`id`,`owned_book_id`,`returned_bool`,`recipient`) VALUES
-- ('','','','');
-- INSERT INTO `all_books` (`id`,`title`,`author`,`read_bool`) VALUES
-- ('','','','');
-- INSERT INTO `books_categories` (`id`,`book_id`,`category_id`) VALUES
-- ('','','');
-- INSERT INTO `storage` (`id`,`place_name`) VALUES
-- ('','');
-- INSERT INTO `wishlist` (`id`,`book_id`) VALUES
-- ('','');
-- INSERT INTO `borrow_sources` (`id`,`name`) VALUES
-- ('','');
