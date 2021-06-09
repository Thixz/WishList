using Dapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Application.Data.Entities.Listas;
using System.Application.Data.Entities.Usuarios;
using System.Application.Views;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WishListTests.Repositories
{
    [TestClass]
    public class UsuarioRepositoryTest : WishListTests
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
        public async Task Usuario_Repository_Post_Success()
        {
            var usuarioEntity = new UsuarioEntity()
            {
                Id = Guid.NewGuid(),
                Nome = "Thiago",
                Documento = "14545454",
                Email = "teste@teste",
                Telefone = "123116540"
            };

            var response = await usuarioRepository.Create(usuarioEntity);

            Assert.IsNotNull(response);
        }
        [TestMethod]
        public async Task Usuario_Repository_Post_Error()
        {
            var response = await usuarioRepository.Create(null);

            Assert.AreEqual(response.Id, Guid.Empty);
        }
        [TestMethod]
        public async Task Usuario_Repository_Put_Success()
        {
            var usuarioEntity = new UsuarioEntity()
            {
                Id = Guid.NewGuid(),
                Nome = "Thiago",
                Documento = "14545454",
                Email = "teste@teste",
                Telefone = "123116540"
            };

            var response = await usuarioRepository.Create(usuarioEntity);

            var updated = await usuarioRepository.Update(response);

            Assert.IsNotNull(updated);
        }
        [TestMethod]
        public async Task Usuario_Repository_Put_Error()
        {
            var response = await usuarioRepository.Update(null);

            Assert.AreEqual(response.Id, Guid.Empty);
        }
        [TestMethod]
        public async Task Usuario_Repository_Delete_Success()
        {
            var usuarioEntity = new UsuarioEntity()
            {
                Id = Guid.NewGuid(),
                Nome = "Thiago",
                Documento = "14545454",
                Email = "teste@teste",
                Telefone = "123116540"
            };

            var response = await usuarioRepository.Create(usuarioEntity);

            var deleted = await usuarioRepository.Delete(response.Id);

            Assert.IsTrue(deleted);
        }
        [TestMethod]
        public async Task Usuario_Repository_Delete_Error()
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

            var listaDesejosEntity = new ListaEntity()
            {
                Id = Guid.NewGuid(),
                Itens = new List<ListaItemViewEntity>(),
                listaNome = "Presentes",
                usuarioId = usuario.Id
            };
            await listaRepository.Create(listaDesejosEntity);

            var deleted = await usuarioRepository.Delete(usuario.Id);

            Assert.IsTrue(!deleted);
        }
        [TestMethod]
        public async Task Usuario_Repository_Get_Success()
        {
            var usuarioEntity = new UsuarioEntity()
            {
                Id = Guid.NewGuid(),
                Nome = "Thiago",
                Documento = "14545454",
                Email = "teste@teste",
                Telefone = "123116540"
            };

            var response = await usuarioRepository.Create(usuarioEntity);

            var get = await usuarioRepository.Get(response.Id);

            Assert.IsNotNull(get);
        }
        [TestMethod]
        public async Task Usuario_Repository_GetByDocument_Success()
        {
            var usuarioEntity = new UsuarioEntity()
            {
                Id = Guid.NewGuid(),
                Nome = "Thiago",
                Documento = "14545454",
                Email = "teste@teste",
                Telefone = "123116540"
            };

            var response = await usuarioRepository.Create(usuarioEntity);

            var get = await usuarioRepository.GetByDocument(response.Documento);

            Assert.IsNotNull(get);
        }
        [TestMethod]
        public async Task Usuario_Repository_GetByEmail_Success()
        {
            var usuarioEntity = new UsuarioEntity()
            {
                Id = Guid.NewGuid(),
                Nome = "Thiago",
                Documento = "14545454",
                Email = "teste@teste",
                Telefone = "123116540"
            };

            var response = await usuarioRepository.Create(usuarioEntity);

            var get = await usuarioRepository.GetByEmail(response.Email);

            Assert.IsNotNull(get);
        }
    }
}
