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
    public class ListaRepositoryTest : WishListTests
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
        public async Task Lista_Repository_Post_Success()
        {
            var listaEntity = new ListaEntity()
            {
                Id = Guid.NewGuid(),
                listaNome = "Presentes",
                Itens = new List<ListaItemViewEntity>(),
                usuarioId = Guid.NewGuid()
            };

            var response = await listaRepository.Create(listaEntity);

            Assert.IsNotNull(response);
        }
        [TestMethod]
        public async Task Lista_Repository_Post_Error()
        {
            var response = await listaRepository.Create(null);

            Assert.AreEqual(response.Id, Guid.Empty);
        }
        [TestMethod]
        public async Task Lista_Repository_Delete_Success()
        {
            var listaEntity = new ListaEntity()
            {
                Id = Guid.NewGuid(),
                listaNome = "Presentes",
                Itens = new List<ListaItemViewEntity>(),
                usuarioId = Guid.NewGuid()
            };

            var response = await listaRepository.Create(listaEntity);

            var deleted = await listaRepository.Delete(response.Id);

            Assert.IsTrue(deleted);
        }
        [TestMethod]
        public async Task Lista_Repository_Delete_Error()
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
            await listaItemRepository.Create(itemEntity);

            var deleted = await listaRepository.Delete(lista.Id);

            Assert.IsTrue(!deleted);
        }
        [TestMethod]
        public async Task Lista_Repository_Get_Success()
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

            var response = await listaRepository.Create(listaEntity);

            var get = await listaRepository.Get(response.Id);

            Assert.IsNotNull(get);
        }
        [TestMethod]
        public async Task Lista_Repository_GetListByUserId_Success()
        {
            var usuarioEntity = new UsuarioEntity()
            {
                Id = Guid.NewGuid(),
                Documento = "55454",
                Email = "teste@teste",
                Nome = "Thiago",
                Telefone = "4545454"
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

            var get = await listaRepository.GetListByUserId(usuario.Id);

            Assert.IsNotNull(get);
        }
    }
}
