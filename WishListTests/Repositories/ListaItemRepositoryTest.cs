using Dapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Application.Data.Entities.ListaItens;
using System.Application.Data.Entities.Listas;
using System.Application.Data.Entities.Produtos;
using System.Application.Data.Entities.Usuarios;
using System.Application.Views;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WishListTests.Repositories
{
    [TestClass]
    public class ListaItemRepositoryTest : WishListTests
    {
        [TestInitialize]
        public void Initiliaze()
        {
            var connection = context.Conectar();
            connection.ExecuteAsync("delete from wishlist.listaItens");
            connection.ExecuteAsync("delete from wishlist.produtos");
            connection.ExecuteAsync("delete from wishlist.listaDesejos");
            connection.ExecuteAsync("delete from wishlist.usuarios");
        }

        [TestMethod]
        public async Task ListaItem_Repository_Post_Success()
        {
            var usuarioEntity = new UsuarioEntity()
            {
                Id = Guid.NewGuid(),
                Nome = "Thiago",
                Documento = "14545454",
                Email = "teste@teste",
                Telefone = "123116540"
            };
            var usuario = await usuarioRepository.Create(usuarioEntity);

            var listaEntity = new ListaEntity()
            {
                Id = Guid.NewGuid(),
                listaNome = "Presentes",
                Itens = new List<ListaItemViewEntity>(),
                usuarioId = usuario.Id
            };
            var lista = await listaRepository.Create(listaEntity);

            var produtoEntity = new ProdutoEntity()
            {
                Id = Guid.NewGuid(),
                Descricao = "411pgs",
                tituloProduto = "Livro"
            };
            var produto = await produtoRepository.Create(produtoEntity);

            var itemEntity = new ListaItemEntity()
            {
                Id = Guid.NewGuid(),
                listaId = lista.Id,
                produtoId = produto.Id,
                Comprado = true
            };
            var response = await listaItemRepository.Create(itemEntity);

            Assert.IsNotNull(response);
        }
        [TestMethod]
        public async Task ListaItem_Repository_Post_Error()
        {
            var response = await listaItemRepository.Create(null);

            Assert.AreEqual(response.Id, Guid.Empty);
        }
        [TestMethod]
        public async Task ListaItem_Repository_Put_Success()
        {
            var usuarioEntity = new UsuarioEntity()
            {
                Id = Guid.NewGuid(),
                Nome = "Thiago",
                Documento = "14545454",
                Email = "teste@teste",
                Telefone = "123116540"
            };
            var usuario = await usuarioRepository.Create(usuarioEntity);

            var listaEntity = new ListaEntity()
            {
                Id = Guid.NewGuid(),
                listaNome = "Presentes",
                Itens = new List<ListaItemViewEntity>(),
                usuarioId = usuario.Id
            };
            var lista = await listaRepository.Create(listaEntity);

            var produtoEntity = new ProdutoEntity()
            {
                Id = Guid.NewGuid(),
                Descricao = "411pgs",
                tituloProduto = "Livro"
            };
            var produto = await produtoRepository.Create(produtoEntity);

            var itemEntity = new ListaItemEntity()
            {
                Id = Guid.NewGuid(),
                listaId = lista.Id,
                produtoId = produto.Id,
                Comprado = true
            };
            var response = await listaItemRepository.Create(itemEntity);

            var updated = await listaItemRepository.Update(response);

            Assert.IsNotNull(updated);
        }
        [TestMethod]
        public async Task ListaItem_Repository_Put_Error()
        {
            var response = await listaItemRepository.Update(null);

            Assert.AreEqual(response.Id, Guid.Empty);
        }
        [TestMethod]
        public async Task ListaItem_Repository_Delete_Success()
        {
            var usuarioEntity = new UsuarioEntity()
            {
                Id = Guid.NewGuid(),
                Nome = "Thiago",
                Documento = "14545454",
                Email = "teste@teste",
                Telefone = "123116540"
            };
            var usuario = await usuarioRepository.Create(usuarioEntity);

            var listaEntity = new ListaEntity()
            {
                Id = Guid.NewGuid(),
                listaNome = "Presentes",
                Itens = new List<ListaItemViewEntity>(),
                usuarioId = usuario.Id
            };
            var lista = await listaRepository.Create(listaEntity);

            var produtoEntity = new ProdutoEntity()
            {
                Id = Guid.NewGuid(),
                Descricao = "411pgs",
                tituloProduto = "Livro"
            };
            var produto = await produtoRepository.Create(produtoEntity);

            var itemEntity = new ListaItemEntity()
            {
                Id = Guid.NewGuid(),
                listaId = lista.Id,
                produtoId = produto.Id,
                Comprado = true
            };
            var response = await listaItemRepository.Create(itemEntity);

            var deleted = await listaItemRepository.Delete(itemEntity.Id);

            Assert.IsTrue(deleted);
        }
        [TestMethod]
        public async Task ListaItem_Repository_Get_Success()
        {
            var usuarioEntity = new UsuarioEntity()
            {
                Id = Guid.NewGuid(),
                Nome = "Thiago",
                Documento = "14545454",
                Email = "teste@teste",
                Telefone = "123116540"
            };
            var usuario = await usuarioRepository.Create(usuarioEntity);

            var listaEntity = new ListaEntity()
            {
                Id = Guid.NewGuid(),
                listaNome = "Presentes",
                Itens = new List<ListaItemViewEntity>(),
                usuarioId = usuario.Id
            };
            var lista = await listaRepository.Create(listaEntity);

            var produtoEntity = new ProdutoEntity()
            {
                Id = Guid.NewGuid(),
                Descricao = "411pgs",
                tituloProduto = "Livro"
            };
            var produto = await produtoRepository.Create(produtoEntity);

            var itemEntity = new ListaItemEntity()
            {
                Id = Guid.NewGuid(),
                listaId = lista.Id,
                produtoId = produto.Id,
                Comprado = true
            };
            var response = await listaItemRepository.Create(itemEntity);

            var get = await listaItemRepository.Get(itemEntity.Id);

            Assert.IsNotNull(get);
        }
        [TestMethod]
        public async Task ListaItem_Repository_GetListItemByListId_Success()
        {
            var usuarioEntity = new UsuarioEntity()
            {
                Id = Guid.NewGuid(),
                Nome = "Thiago",
                Documento = "14545454",
                Email = "teste@teste",
                Telefone = "123116540"
            };
            var usuario = await usuarioRepository.Create(usuarioEntity);

            var listaEntity = new ListaEntity()
            {
                Id = Guid.NewGuid(),
                listaNome = "Presentes",
                Itens = new List<ListaItemViewEntity>(),
                usuarioId = usuario.Id
            };
            var lista = await listaRepository.Create(listaEntity);

            var produtoEntity = new ProdutoEntity()
            {
                Id = Guid.NewGuid(),
                Descricao = "411pgs",
                tituloProduto = "Livro"
            };
            var produto = await produtoRepository.Create(produtoEntity);

            var itemEntity = new ListaItemEntity()
            {
                Id = Guid.NewGuid(),
                listaId = lista.Id,
                produtoId = produto.Id,
                Comprado = true
            };
            var response = await listaItemRepository.Create(itemEntity);

            var get = await listaItemRepository.GetListItemByListID(lista.Id);

            Assert.IsNotNull(get);
        }
        [TestMethod]
        public async Task ListaItem_Repository_GetListItemByProductId_Success()
        {
            var usuarioEntity = new UsuarioEntity()
            {
                Id = Guid.NewGuid(),
                Nome = "Thiago",
                Documento = "14545454",
                Email = "teste@teste",
                Telefone = "123116540"
            };
            var usuario = await usuarioRepository.Create(usuarioEntity);

            var listaEntity = new ListaEntity()
            {
                Id = Guid.NewGuid(),
                listaNome = "Presentes",
                Itens = new List<ListaItemViewEntity>(),
                usuarioId = usuario.Id
            };
            var lista = await listaRepository.Create(listaEntity);

            var produtoEntity = new ProdutoEntity()
            {
                Id = Guid.NewGuid(),
                Descricao = "411pgs",
                tituloProduto = "Livro"
            };
            var produto = await produtoRepository.Create(produtoEntity);

            var itemEntity = new ListaItemEntity()
            {
                Id = Guid.NewGuid(),
                listaId = lista.Id,
                produtoId = produto.Id,
                Comprado = true
            };
            var response = await listaItemRepository.Create(itemEntity);

            var get = await listaItemRepository.GetListItemByProductID(produto.Id);

            Assert.IsNotNull(get);
        }
        [TestMethod]
        public async Task ListaItem_Repository_GetListItemViewByListId_Success()
        {
            var usuarioEntity = new UsuarioEntity()
            {
                Id = Guid.NewGuid(),
                Nome = "Thiago",
                Documento = "14545454",
                Email = "teste@teste",
                Telefone = "123116540"
            };
            var usuario = await usuarioRepository.Create(usuarioEntity);

            var listaEntity = new ListaEntity()
            {
                Id = Guid.NewGuid(),
                listaNome = "Presentes",
                Itens = new List<ListaItemViewEntity>(),
                usuarioId = usuario.Id
            };
            var lista = await listaRepository.Create(listaEntity);

            var produtoEntity = new ProdutoEntity()
            {
                Id = Guid.NewGuid(),
                Descricao = "411pgs",
                tituloProduto = "Livro"
            };
            var produto = await produtoRepository.Create(produtoEntity);

            var itemEntity = new ListaItemEntity()
            {
                Id = Guid.NewGuid(),
                listaId = lista.Id,
                produtoId = produto.Id,
                Comprado = true
            };
            var response = await listaItemRepository.Create(itemEntity);

            var get = await listaItemRepository.GetListItemViewByListID(lista.Id);

            Assert.IsNotNull(get);
        }
    }
}
