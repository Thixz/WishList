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
    public class ProdutoRepositoryTest : WishListTests
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
        public async Task Produto_Repository_Post_Success()
        {
            var produtoEntity = new ProdutoEntity()
            {
                Id = Guid.NewGuid(),
                Descricao = "411pgs",
                tituloProduto = "Livro"
            };

            var response = await produtoRepository.Create(produtoEntity);

            Assert.IsNotNull(response);
        }
        [TestMethod]
        public async Task Produto_Repository_Post_Error()
        {
            var response = await produtoRepository.Create(null);

            Assert.AreEqual(response.Id, Guid.Empty);
        }
        [TestMethod]
        public async Task Produto_Repository_Put_Success()
        {
            var produtoEntity = new ProdutoEntity()
            {
                Id = Guid.NewGuid(),
                Descricao = "411pgs",
                tituloProduto = "Livro"
            };

            var response = await produtoRepository.Create(produtoEntity);

            var updated = await produtoRepository.Update(response);

            Assert.IsNotNull(updated);
        }
        [TestMethod]
        public async Task Produto_Repository_Put_Error()
        {
            var response = await produtoRepository.Update(null);

            Assert.AreEqual(response.Id, Guid.Empty);
        }
        [TestMethod]
        public async Task Produto_Repository_Delete_Success()
        {
            var produtoEntity = new ProdutoEntity()
            {
                Id = Guid.NewGuid(),
                Descricao = "411pgs",
                tituloProduto = "Livro"
            };

            var response = await produtoRepository.Create(produtoEntity);

            var deleted = await produtoRepository.Delete(response.Id);

            Assert.IsTrue(deleted);
        }
        [TestMethod]
        public async Task Produto_Repository_Delete_Error()
        {
            var produtoEntity = new ProdutoEntity()
            {
                Id = Guid.NewGuid(),
                Descricao = "411pgs",
                tituloProduto = "Livro"
            };
            var produto = await produtoRepository.Create(produtoEntity);

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
                Itens = new List<ListaItemViewEntity>(),
                listaNome = "Presentes",
                usuarioId = usuario.Id
            };
            var lista = await listaRepository.Create(listaEntity);

            var itemEntity = new ListaItemEntity()
            {
                Id = Guid.NewGuid(),
                listaId = lista.Id,
                produtoId = produto.Id,
                Comprado = true
            };
            var listaItem = await listaItemRepository.Create(itemEntity);

            var deleted = await produtoRepository.Delete(produto.Id);

            Assert.IsTrue(!deleted);
        }
        [TestMethod]
        public async Task Produto_Repository_Get_Success()
        {
            var produtoEntity = new ProdutoEntity()
            {
                Id = Guid.NewGuid(),
                Descricao = "411pgs",
                tituloProduto = "Livro"
            };

            var response = await produtoRepository.Create(produtoEntity);

            var get = await produtoRepository.Get(response.Id);

            Assert.IsNotNull(get);
        }
    }
}
