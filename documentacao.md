Objetivo:
Desenvolver uma API para gerir uma WishList. Neste cenário o usuário deve poder incluir um produto em sua lista de desejos e também informar se já ganhou/comprou aquele item. Também será necessário um endpoint para trazer um item desta lista de forma randômica.

END POINTS :

**1 - Usuário**
 O endpoint usuário deve conter as seguintes propriedades dentro de rotas Post, Update, Delete e Get.
 
 Post:
***Request:***
```
{
"nome" : "",
"documento" : "",
"telefone" : "",
"email" : "",
}
```
***Response:***
```
{
"id":"Guid",
"nome" : "Exemplo",
"documento" : "123456",
"telefone" : "98995-5555",
"email" : "thiago@teste",
"dataCriacao" : ""2021-06-08"",
}
```
 Put:
 ***Request:***
```
{
"id":"",
"nome" : "",
"documento" : "",
"telefone" : "",
"email" : "",
}
```
***Response:***
```
{
"id":"Guid",
"nome" : "Exemplo Atualizado",
"documento" : "123456",
"telefone" : "98995-5555",
"email" : "thiago@atuliazado",
"dataCriacao" : ""2021-06-08"",
"dataAtualizacao" : "2021-06-09",
}
```
 Get:
  ***Request:***
```
{
"id":""
}
```
***Response:***
```
{
"id":"Guid",
"nome" : "Exemplo",
"documento" : "123456",
"telefone" : "98995-5555",
"email" : "thiago@teste",
"dataCriacao" : ""2021-06-08"",
"dataAtualizacao" : "2021-06-09",
}
```
 Delete:
  ***Request:***
```
{
"id":""
}
```
***Response:***
```
{
"O usuário foi deletado com sucesso!"
}
```

 Table: wishlist.usuarios:
<Table>
  <THead>
    <Th>Column</Th>
    <Th>Type</Th>
    <Th>Size</Th>
    <th>Description</Th>
  </Thead>
  <Tbody>
    <tr>
       <td>id</td>
       <td>Guid</td>
       <td>char(36)</td>
       <td>Id do usuário</td>
    </tr>   
    <tr>
       <td>nome</td>
       <td>String</td>
       <td>varchar(70)</td>
       <td>Nome do Usuário</td>
    </tr> 
    <tr>
       <td>documento</td>
       <td>String</td>
        <td>varchar(15)</td>
       <td>Documento do Usuário</td>
    </tr>   
    <tr>
       <td>telefone</td>
       <td>String</td>
       <td>varchar(15)</td>
       <td>Número Telefone do Usuário</td>
    </tr>  
    <tr>
       <td>email</td>
       <td>String</td>
       <td>varchar(150)</td>
       <td>Usuário Email</td>
    </tr>  
    <tr>
       <td>dataCriacao</td>
       <td>DateTime</td>
       <td>datetime</td>
       <td>Data de Criação</td>
    </tr>  
    <tr>
       <td>dataAtualizacao</td>
       <td>DateTime</td>
       <td>datetime</td>
       <td>Data de atualização</td>
    </tr>  
  </Tbody>
</Table>

Script Database:
```
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
```
 **2- Produtos**
O endpoint Produtos deve conter as seguintes propriedades dentro de rotas Post,Put,Get,Delete.

Post:
***Request:***
```
   { 
   "tituloProduto" : "",
   "descricao" : "",
   }
```
***Response:***
```
   { 
   "id":"Guid",
   "tituloProduto" : "Exemplo",
   "descricao" : "Exemplo", 
   "dataCriacao" : ""2021-06-08"",
   }
```
Put:
***Request:***
```
   { 
   "id":"Guid",
   "tituloProduto" : "Exemplo",
   "descricao" : "Exemplo",  
   }
```
***Response:***
```
   { 
   "id":"Guid",
   "tituloProduto" : "Exemplo",
   "descricao" : "Exemplo",
   "dataCriacao" : ""2021-06-08"",
   "dataAtualizacao" : ""2021-06-09""
   }
```
 Get:
  ***Request:***
```
{
"id":""
}
```
***Response:***
```
{
   "id":"Guid",
   "tituloProduto" : "Exemplo",
   "descricao" : "Exemplo",
   "dataCriacao" : ""2021-06-08"",
   "dataAtualizacao" : ""2021-06-09""
}
```
 Delete:
  ***Request:***
```
{
"id":""
}
```
***Response:***
```
{
"O produto foi deletado com sucesso!"
}
```
Table: wishlist.produtos:
<Table>
  <THead>
    <Th>Column</Th>
    <Th>Type</Th>
    <Th>Size</Th>
    <th>Description</Th>
  </Thead>
  <Tbody>
    <tr>
       <td>id</td>
       <td>Guid</td>
       <td>char(36)</td>
       <td>Id do Produto</td>
    </tr>   
    <tr>
       <td>tituloProduto</td>
       <td>String</td>
       <td>varchar(70)</td>
       <td>Título do Produto</td>
    </tr> 
        <tr>
       <td>descricao</td>
       <td>String</td>
       <td>varchar(100)</td>
       <td>Descrição do Produto</td>
    </tr> 
    <tr>
       <td>dataCriacao</td>
       <td>DateTime</td>
       <td>datetime</td>
       <td>Data de Criação</td>
    </tr>  
    <tr>
       <td>dataAtualizacao</td>
       <td>DateTime</td>
       <td>datetime</td>
       <td>Data de Atualização</td>
    </tr>  
  </Tbody>
</Table>

Script Database:
```
CREATE TABLE IF NOT EXISTS wishlist.produtos
(
id char(36) not null default 'uuid()' comment 'Identificador unico do produto',
tituloProduto varchar(70) not null comment 'Titulo do produto',
descricao varchar(100) not null comment 'Descricao do produto',
dataCriacao datetime not null default NOW() comment 'Data e hora da criacao do registro',
dataAtualizacao timestamp not null default current_timestamp on update current_timestamp comment 'Data e hora da atualizacao',
PRIMARY KEY (id)
)ENGINE=InnoDB DEFAULT CHARSET=utf8;
```
  
**3 - Listas de Desejos**
O endpoint Lista de Desejos deve conter as seguintes propriedades dentro de rotas Post, Get, Delete.

 Post:
***Request:***
```
{
"usuarioId" : "",
"listaNome" : ""
}
```
***Response:***
```
{
"id":"Guid",
"usuarioId" : "Guid",
"listaNome" : "Presentes",
"dataCriacao" : "2021-06-08"
}
```
 Get:
  ***Request:***
```
{
"id":""
}
```
***Response:***
```
{
"id":"uuid()",
"usuarioId" : "Guid",
"listaNome" : "Presentes",
"dataCriacao" : ""2021-06-08"",
}
```
 Delete:
  ***Request:***
```
{
"id":""
}
```
***Response:***
```
{
"Lista excluida com sucesso!"
}
```
Table: wishlist.listaDesejos:
<Table>
  <THead>
    <Th>Column</Th>
    <Th>Type</Th>
    <Th>Size</Th>
    <th>Description</Th>
  </Thead>
  <Tbody>
    <tr>
       <td>id</td>
       <td>Guid</td>
       <td>char(36)</td>
       <td>Id da Lista</td>
    </tr>   
    <tr>
       <td>usuarioId</td>
       <td>Guid</td>
       <td>char(36)</td>
       <td>Id do Usuário</td>
    </tr>
        <tr>
       <td>listaNome</td>
       <td>String</td>
       <td>varchar(40)</td>
       <td>Nome da Lista</td>
    </tr>
    <tr>
       <td>creationDate</td>
       <td>DateTime</td>
       <td>datetime</td>
       <td>Data de criação</td>
    </tr>  
  </Tbody>
</Table>

Script Database:
```
CREATE TABLE IF NOT EXISTS wishlist.listaDesejos
(
id char(36) not null default 'uuid()' comment 'Identificador Unico Lista',
usuarioId char(36) not null default 'uuid()' comment 'Identificador Unico Usuario',
listaNome varchar(40) not null comment 'Nome Lista',
dataCriacao datetime not null default NOW() comment 'Data de criacao do registro',
PRIMARY KEY (id),
CONSTRAINT `fk_Lista_Usuario` FOREIGN KEY (`usuarioId`) REFERENCES `wishlist`.`usuarios` (`id`)
)ENGINE=InnoDB DEFAULT CHARSET=utf8;
```
**4 - Itens Lista**
O endpoint Itens Lista deve conter as seguinte propriedades dentro de rotas Post, Put, Delete e Get.


 Post:
***Request:***
```
   {
   "produtoId" : "",
   "listaId" : "",
   "comprado" : "",
   }
```
***Response:***
```
   {
   "id":"Guid" 
   "produtoId" : "Guid",
   "listaId" : "Guid",
   "comprado" : "false",
   "dataCriacao" : ""2021-06-08"",
   }
```
 Put:
 ***Request:***
```
{
   "id":"Guid" 
   "produtoId" : "",
   "listaId" : "",
   "comprado" : "",
}
```
***Response:***
```
{
  "id":"Guid" 
  "produtoId" : "Guid",
  "listaId" : "Guid",
  "comprado" : "true",
  "dataCriacao" : ""2021-06-08"",
  "dataAtualizacao" : ""2021-06-09"",
}
```
 Get:
  ***Request:***
```
{
   "id":""  
}
```
***Response:***
```
{
  "id":"Guid" 
  "produtoId" : "Guid",
  "listaId" : "Guid",
  "comprado" : "true",
  "dataCriacao" : ""2021-06-08"",
  "dataAtualizacao" : ""2021-06-09"", 
}
```
 Delete:
  ***Request:***
```
{
   "id":""  
}
```
***Response:***
```
{
"O item da lista foi excluido com sucesso!"
}
```
Table: wishilist.listaItens:
<Table>
  <THead>
    <Th>Column</Th>
    <Th>Type</Th>
    <Th>Size</Th>
    <th>Description</Th>
  </Thead>
  <Tbody>
    <tr>
       <td>id</td>
       <td>Guid</td>
       <td>char(36)</td>
       <td>Lista Item Id</td>
    </tr>   
    <tr>
       <td>produtoId</td>
       <td>Guid</td>
       <td>char(36)</td>
       <td>Id do Produto</td>
    </tr> 
    <tr>
       <td>listaId</td>
       <td>Guid</td>
       <td>char(36)</td>
       <td>Id da Lista</td>
    </tr> 
    <tr>
       <td>comprado</td>
       <td>Boolean</td>
        <td>bool</td>
       <td>Item comprado ou ganho</td>
    </tr>   
    <tr>
       <td>dataCriacao</td>
       <td>DateTime</td>
       <td>datetime</td>
       <td>Data de Criação</td>
    </tr>  
        <tr>
       <td>dataAtualizacao</td>
       <td>DateTime</td>
       <td>datetime</td>
       <td>Data de Atualização</td>
    </tr>  
  </Tbody>
</Table>

Script Database:
```
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
```

