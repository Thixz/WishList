-- Wishlist Database Script --
CREATE SCHEMA IF NOT EXISTS wishlist DEFAULT CHARACTER SET utf8;

USE wishlist;



CREATE TABLE IF NOT EXISTS wishlist.usuarios
(
id char(36) not null default 'uuid()' comment 'Identificador Unico Usuario',
nome varchar(70) not null comment 'Nome Usuario',
documento varchar(15) not null comment 'Documento Usuario',
telefone varchar(15) not null comment 'Telefone do Usuario',
email varchar(150) not null comment 'Usuario Email',
dataCriacao datetime not null default NOW() comment 'Data e hora da criacao do registro',
dataAtualizacao timestamp not null default current_timestamp on update current_timestamp comment 'Data e hora de atualizacao',
PRIMARY KEY (id)
)ENGINE=InnoDB DEFAULT CHARSET=utf8;



CREATE TABLE IF NOT EXISTS wishlist.produtos
(
id char(36) not null default 'uuid()' comment 'Identificador unico do produto',
tituloProduto varchar(70) not null comment 'Titulo do produto',
descricao varchar(100) not null comment 'Descricao do produto',
dataCriacao datetime not null default NOW() comment 'Data e hora da criacao do registro',
dataAtualizacao timestamp not null default current_timestamp on update current_timestamp comment 'Data e hora da atualizacao',
PRIMARY KEY (id)
)ENGINE=InnoDB DEFAULT CHARSET=utf8;



CREATE TABLE IF NOT EXISTS wishlist.listaDesejos
(
id char(36) not null default 'uuid()' comment 'Identificador Unico Lista',
usuarioId char(36) not null default 'uuid()' comment 'Identificador Unico Usuario',
listaNome varchar(40) not null comment 'Nome Lista',
dataCriacao datetime not null default NOW() comment 'Data de criacao do registro',
PRIMARY KEY (id),
CONSTRAINT `fk_Lista_Usuario` FOREIGN KEY (`usuarioId`) REFERENCES `wishlist`.`usuarios` (`id`)
)ENGINE=InnoDB DEFAULT CHARSET=utf8;



CREATE TABLE IF NOT EXISTS wishlist.listaItens
(
id char(36) not null default 'uuid()' comment 'Identificador Unico Item',
produtoId char(36) not null default 'uuid()' comment 'Identificador Unico Produto',
listaId char(36) not null default 'uuid()' comment 'Identificador Unico Lista',
comprado bool default false not null comment 'Produto comprado ou ganho',
dataCriacao datetime not null default NOW() comment 'Order created date and time',
dataAtualizacao timestamp not null default current_timestamp on update current_timestamp comment 'Data e hora de atualizacao',
PRIMARY KEY (id),
CONSTRAINT `fk_Item_Produto` FOREIGN KEY (`produtoId`) REFERENCES `wishlist`.`produtos` (`id`),
CONSTRAINT `fk_Item_Lista` FOREIGN KEY (`listaId`) REFERENCES `wishlist`.`listaDesejos` (`id`)
)ENGINE=InnoDB DEFAULT CHARSET=utf8;