using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Application.Contracts.Usuarios;
using System.Application.Data.Entities.Listas;
using System.Application.Data.Entities.Usuarios;
using System.Application.Data.MySql;
using System.Application.Data.Repositories.Listas;
using System.Application.Data.Repositories.Usuarios;
using System.Application.Services.Usuarios;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WishListTests.Services
{
    [TestClass]
    public class UsuarioServiceMockTest
    {
        [TestMethod]
        public async Task Usuario_Post_Success()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockUsuarioRepository = new Mock<UsuarioRepository>(mockMySqlContext.Object);
            var mockListaRepository = new Mock<ListaRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<UsuarioService>>();

            mockUsuarioRepository.CallBase = true;
            mockListaRepository.CallBase = true;

            var mockUsuarioService = new Mock<UsuarioService>(mocklogger.Object, mockUsuarioRepository.Object, mockListaRepository.Object);

            UsuarioPostRequest _postRequest = new UsuarioPostRequest
            {
                Nome = "Thiago",
                Documento = "132465",
                Email = "thiago@thiago",
                Telefone = "367896320"
            };

            UsuarioEntity _usuarioEntity = new UsuarioEntity(_postRequest)
            {
                dataCriacao = DateTime.Now
            };

            mockUsuarioRepository
                .Setup(x => x.GetByDocument(It.IsAny<string>()))
                .Returns(Task.FromResult(null as UsuarioEntity));

            mockUsuarioRepository
                .Setup(x => x.GetByEmail(It.IsAny<string>()))
                .Returns(Task.FromResult(null as UsuarioEntity));

            mockUsuarioRepository
                .Setup(x => x.Create(It.IsAny<UsuarioEntity>()))
                .Returns(Task.FromResult(_usuarioEntity));

            var result = await mockUsuarioService.Object.Create(_postRequest);
            Assert.IsTrue(result.success);
        }
        [TestMethod]
        public async Task Usuario_Post_ValidatorError()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockUsuarioRepository = new Mock<UsuarioRepository>(mockMySqlContext.Object);
            var mockListaRepository = new Mock<ListaRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<UsuarioService>>();

            mockUsuarioRepository.CallBase = true;
            mockListaRepository.CallBase = true;

            var mockUsuarioService = new Mock<UsuarioService>(mocklogger.Object, mockUsuarioRepository.Object, mockListaRepository.Object);

            UsuarioPostRequest _postRequest = new UsuarioPostRequest
            {
                Nome = "",
                Documento = "",
                Email = "",
                Telefone = ""
            };

            var result = await mockUsuarioService.Object.Create(_postRequest);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Usuario_Post_DuplicatedDocument()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockUsuarioRepository = new Mock<UsuarioRepository>(mockMySqlContext.Object);
            var mockListaRepository = new Mock<ListaRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<UsuarioService>>();

            mockUsuarioRepository.CallBase = true;
            mockListaRepository.CallBase = true;

            var mockUsuarioService = new Mock<UsuarioService>(mocklogger.Object, mockUsuarioRepository.Object, mockListaRepository.Object);

            UsuarioPostRequest _postRequest = new UsuarioPostRequest
            {
                Nome = "Thiago",
                Documento = "132465",
                Email = "thiago@thiago",
                Telefone = "367896320"
            };

            mockUsuarioRepository
                .Setup(x => x.GetByDocument(It.IsAny<string>()))
                .Returns(Task.FromResult(new UsuarioEntity {Id = Guid.NewGuid() }));

            var result = await mockUsuarioService.Object.Create(_postRequest);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Usuario_Post_DuplicatedEmail()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockUsuarioRepository = new Mock<UsuarioRepository>(mockMySqlContext.Object);
            var mockListaRepository = new Mock<ListaRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<UsuarioService>>();

            mockUsuarioRepository.CallBase = true;
            mockListaRepository.CallBase = true;

            var mockUsuarioService = new Mock<UsuarioService>(mocklogger.Object, mockUsuarioRepository.Object, mockListaRepository.Object);

            UsuarioPostRequest _postRequest = new UsuarioPostRequest
            {
                Nome = "Thiago",
                Documento = "132465",
                Email = "thiago@thiago",
                Telefone = "367896320"
            };

            mockUsuarioRepository
                .Setup(x => x.GetByDocument(It.IsAny<string>()))
                .Returns(Task.FromResult(null as UsuarioEntity));

            mockUsuarioRepository
                .Setup(x => x.GetByEmail(It.IsAny<string>()))
                .Returns(Task.FromResult(new UsuarioEntity {Id = Guid.NewGuid() }));

            var result = await mockUsuarioService.Object.Create(_postRequest);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Usuario_Post_DatabaseError()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockUsuarioRepository = new Mock<UsuarioRepository>(mockMySqlContext.Object);
            var mockListaRepository = new Mock<ListaRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<UsuarioService>>();

            mockUsuarioRepository.CallBase = true;
            mockListaRepository.CallBase = true;

            var mockUsuarioService = new Mock<UsuarioService>(mocklogger.Object, mockUsuarioRepository.Object, mockListaRepository.Object);

            UsuarioPostRequest _postRequest = new UsuarioPostRequest
            {
                Nome = "Thiago",
                Documento = "132465",
                Email = "thiago@thiago",
                Telefone = "367896320"
            };

            mockUsuarioRepository
                .Setup(x => x.GetByDocument(It.IsAny<string>()))
                .Returns(Task.FromResult(null as UsuarioEntity));

            mockUsuarioRepository
                .Setup(x => x.GetByEmail(It.IsAny<string>()))
                .Returns(Task.FromResult(null as UsuarioEntity));

            mockUsuarioRepository
                .Setup(x => x.Create(It.IsAny<UsuarioEntity>()))
                .Returns(Task.FromResult(new UsuarioEntity {Id = Guid.Empty }));

            var result = await mockUsuarioService.Object.Create(_postRequest);
            Assert.IsTrue(!result.success);
        }


        [TestMethod]
        public async Task Usuario_Put_Success()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockUsuarioRepository = new Mock<UsuarioRepository>(mockMySqlContext.Object);
            var mockListaRepository = new Mock<ListaRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<UsuarioService>>();

            mockUsuarioRepository.CallBase = true;
            mockListaRepository.CallBase = true;

            var mockUsuarioService = new Mock<UsuarioService>(mocklogger.Object, mockUsuarioRepository.Object, mockListaRepository.Object);

            UsuarioPutRequest _putRequest = new UsuarioPutRequest
            {
                Id = Guid.NewGuid(),
                Nome = "Thiago",
                Documento = "132465",
                Email = "thiago@thiago",
                Telefone = "367896320"
            };

            UsuarioEntity _usuarioEntity = new UsuarioEntity(_putRequest)
            {
                dataAtualizacao = DateTime.Now
            };

            mockUsuarioRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(_usuarioEntity));

            mockUsuarioRepository
                .Setup(x => x.GetByDocument(It.IsAny<string>()))
                .Returns(Task.FromResult(null as UsuarioEntity));

            mockUsuarioRepository
               .Setup(x => x.GetByEmail(It.IsAny<string>()))
               .Returns(Task.FromResult(null as UsuarioEntity));

            mockUsuarioRepository
                .Setup(x => x.Update(It.IsAny<UsuarioEntity>()))
                .Returns(Task.FromResult(_usuarioEntity));

            var result = await mockUsuarioService.Object.Update(_putRequest);
            Assert.IsTrue(result.success);
        }
        [TestMethod]
        public async Task Usuario_Put_EmptyId()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockUsuarioRepository = new Mock<UsuarioRepository>(mockMySqlContext.Object);
            var mockListaRepository = new Mock<ListaRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<UsuarioService>>();

            mockUsuarioRepository.CallBase = true;
            mockListaRepository.CallBase = true;

            var mockUsuarioService = new Mock<UsuarioService>(mocklogger.Object, mockUsuarioRepository.Object, mockListaRepository.Object);

            UsuarioPutRequest _putRequest = new UsuarioPutRequest
            {
                Id = Guid.Empty,
                Nome = "Thiago",
                Documento = "132465",
                Email = "thiago@thiago",
                Telefone = "367896320"
            };

            var result = await mockUsuarioService.Object.Update(_putRequest);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Usuario_Put_ValidatorError()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockUsuarioRepository = new Mock<UsuarioRepository>(mockMySqlContext.Object);
            var mockListaRepository = new Mock<ListaRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<UsuarioService>>();

            mockUsuarioRepository.CallBase = true;
            mockListaRepository.CallBase = true;

            var mockUsuarioService = new Mock<UsuarioService>(mocklogger.Object, mockUsuarioRepository.Object, mockListaRepository.Object);

            UsuarioPutRequest _putRequest = new UsuarioPutRequest
            {
                Id = Guid.NewGuid(),
                Nome = "",
                Documento = "",
                Email = "",
                Telefone = ""
            };

            var result = await mockUsuarioService.Object.Update(_putRequest);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Usuario_Put_IdNotFound()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockUsuarioRepository = new Mock<UsuarioRepository>(mockMySqlContext.Object);
            var mockListaRepository = new Mock<ListaRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<UsuarioService>>();

            mockUsuarioRepository.CallBase = true;
            mockListaRepository.CallBase = true;

            var mockUsuarioService = new Mock<UsuarioService>(mocklogger.Object, mockUsuarioRepository.Object, mockListaRepository.Object);

            UsuarioPutRequest _putRequest = new UsuarioPutRequest
            {
                Id = Guid.NewGuid(),
                Nome = "Thiago",
                Documento = "132465",
                Email = "thiago@thiago",
                Telefone = "367896320"
            };

            mockUsuarioRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(null as UsuarioEntity));

            var result = await mockUsuarioService.Object.Update(_putRequest);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Usuario_Put_DuplicatedDocument()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockUsuarioRepository = new Mock<UsuarioRepository>(mockMySqlContext.Object);
            var mockListaRepository = new Mock<ListaRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<UsuarioService>>();

            mockUsuarioRepository.CallBase = true;
            mockListaRepository.CallBase = true;

            var mockUsuarioService = new Mock<UsuarioService>(mocklogger.Object, mockUsuarioRepository.Object, mockListaRepository.Object);

            UsuarioPutRequest _putRequest = new UsuarioPutRequest
            {
                Id = Guid.NewGuid(),
                Nome = "Thiago",
                Documento = "132465",
                Email = "thiago@thiago",
                Telefone = "367896320"
            };

            mockUsuarioRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new UsuarioEntity {Id = Guid.NewGuid() }));

            mockUsuarioRepository
                .Setup(x => x.GetByDocument(It.IsAny<string>()))
                .Returns(Task.FromResult(new UsuarioEntity { Id = Guid.NewGuid() }));

            var result = await mockUsuarioService.Object.Update(_putRequest);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Usuario_Put_DuplicatedEmail()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockUsuarioRepository = new Mock<UsuarioRepository>(mockMySqlContext.Object);
            var mockListaRepository = new Mock<ListaRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<UsuarioService>>();

            mockUsuarioRepository.CallBase = true;
            mockListaRepository.CallBase = true;

            var mockUsuarioService = new Mock<UsuarioService>(mocklogger.Object, mockUsuarioRepository.Object, mockListaRepository.Object);

            UsuarioPutRequest _putRequest = new UsuarioPutRequest
            {
                Id = Guid.NewGuid(),
                Nome = "Thiago",
                Documento = "132465",
                Email = "thiago@thiago",
                Telefone = "367896320"
            };

            mockUsuarioRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new UsuarioEntity { Id = Guid.NewGuid() }));

            mockUsuarioRepository
                .Setup(x => x.GetByDocument(It.IsAny<string>()))
                .Returns(Task.FromResult(null as UsuarioEntity));

            mockUsuarioRepository
               .Setup(x => x.GetByEmail(It.IsAny<string>()))
               .Returns(Task.FromResult(new UsuarioEntity { Id = Guid.NewGuid() }));

            var result = await mockUsuarioService.Object.Update(_putRequest);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Usuario_Put_DatabaseError()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockUsuarioRepository = new Mock<UsuarioRepository>(mockMySqlContext.Object);
            var mockListaRepository = new Mock<ListaRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<UsuarioService>>();

            mockUsuarioRepository.CallBase = true;
            mockListaRepository.CallBase = true;

            var mockUsuarioService = new Mock<UsuarioService>(mocklogger.Object, mockUsuarioRepository.Object, mockListaRepository.Object);

            UsuarioPutRequest _putRequest = new UsuarioPutRequest
            {
                Id = Guid.NewGuid(),
                Nome = "Thiago",
                Documento = "132465",
                Email = "thiago@thiago",
                Telefone = "367896320"
            };

            UsuarioEntity _usuarioEntity = new UsuarioEntity(_putRequest)
            {
                dataAtualizacao = DateTime.Now
            };

            mockUsuarioRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(_usuarioEntity));

            mockUsuarioRepository
                .Setup(x => x.GetByDocument(It.IsAny<string>()))
                .Returns(Task.FromResult(null as UsuarioEntity));

            mockUsuarioRepository
               .Setup(x => x.GetByEmail(It.IsAny<string>()))
               .Returns(Task.FromResult(null as UsuarioEntity));

            mockUsuarioRepository
                .Setup(x => x.Update(It.IsAny<UsuarioEntity>()))
                .Returns(Task.FromResult(new UsuarioEntity {Id = Guid.Empty }));

            var result = await mockUsuarioService.Object.Update(_putRequest);
            Assert.IsTrue(!result.success);
        }


        [TestMethod]
        public async Task Usuario_Delete_Success()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockUsuarioRepository = new Mock<UsuarioRepository>(mockMySqlContext.Object);
            var mockListaRepository = new Mock<ListaRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<UsuarioService>>();

            mockUsuarioRepository.CallBase = true;
            mockListaRepository.CallBase = true;

            var mockUsuarioService = new Mock<UsuarioService>(mocklogger.Object, mockUsuarioRepository.Object, mockListaRepository.Object);

            mockListaRepository
                .Setup(x => x.GetListByUserId(It.IsAny<Guid>()))
                .Returns(Task.FromResult(null as ListaEntity));

            mockUsuarioRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new UsuarioEntity {Id = Guid.NewGuid() }));

            mockUsuarioRepository
                .Setup(x => x.Delete(It.IsAny<Guid>()))
                .Returns(Task.FromResult(true));

            var result = await mockUsuarioService.Object.Delete(Guid.NewGuid());
            Assert.IsTrue(result.success);
        } 
        [TestMethod]
        public async Task Usuario_Delete_EmptyId()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockUsuarioRepository = new Mock<UsuarioRepository>(mockMySqlContext.Object);
            var mockListaRepository = new Mock<ListaRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<UsuarioService>>();

            mockUsuarioRepository.CallBase = true;
            mockListaRepository.CallBase = true;

            var mockUsuarioService = new Mock<UsuarioService>(mocklogger.Object, mockUsuarioRepository.Object, mockListaRepository.Object);


            var result = await mockUsuarioService.Object.Delete(Guid.Empty);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Usuario_Delete_ActiveWishList()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockUsuarioRepository = new Mock<UsuarioRepository>(mockMySqlContext.Object);
            var mockListaRepository = new Mock<ListaRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<UsuarioService>>();

            mockUsuarioRepository.CallBase = true;
            mockListaRepository.CallBase = true;

            var mockUsuarioService = new Mock<UsuarioService>(mocklogger.Object, mockUsuarioRepository.Object, mockListaRepository.Object);

            mockListaRepository
                .Setup(x => x.GetListByUserId(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new ListaEntity {Id = Guid.NewGuid() }));

            var result = await mockUsuarioService.Object.Delete(Guid.NewGuid());
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Usuario_Delete_IdNotFound()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockUsuarioRepository = new Mock<UsuarioRepository>(mockMySqlContext.Object);
            var mockListaRepository = new Mock<ListaRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<UsuarioService>>();

            mockUsuarioRepository.CallBase = true;
            mockListaRepository.CallBase = true;

            var mockUsuarioService = new Mock<UsuarioService>(mocklogger.Object, mockUsuarioRepository.Object, mockListaRepository.Object);

            mockListaRepository
                .Setup(x => x.GetListByUserId(It.IsAny<Guid>()))
                .Returns(Task.FromResult(null as ListaEntity));

            mockUsuarioRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(null as UsuarioEntity));

            var result = await mockUsuarioService.Object.Delete(Guid.NewGuid());
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Usuario_Delete_DatabaseError()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockUsuarioRepository = new Mock<UsuarioRepository>(mockMySqlContext.Object);
            var mockListaRepository = new Mock<ListaRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<UsuarioService>>();

            mockUsuarioRepository.CallBase = true;
            mockListaRepository.CallBase = true;

            var mockUsuarioService = new Mock<UsuarioService>(mocklogger.Object, mockUsuarioRepository.Object, mockListaRepository.Object);

            mockListaRepository
                .Setup(x => x.GetListByUserId(It.IsAny<Guid>()))
                .Returns(Task.FromResult(null as ListaEntity));

            mockUsuarioRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new UsuarioEntity { Id = Guid.NewGuid() }));

            mockUsuarioRepository
                .Setup(x => x.Delete(It.IsAny<Guid>()))
                .Returns(Task.FromResult(false));

            var result = await mockUsuarioService.Object.Delete(Guid.NewGuid());
            Assert.IsTrue(!result.success);
        }


        [TestMethod]
        public async Task Usuario_Get_Success()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockUsuarioRepository = new Mock<UsuarioRepository>(mockMySqlContext.Object);
            var mockListaRepository = new Mock<ListaRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<UsuarioService>>();

            mockUsuarioRepository.CallBase = true;
            mockListaRepository.CallBase = true;

            var mockUsuarioService = new Mock<UsuarioService>(mocklogger.Object, mockUsuarioRepository.Object, mockListaRepository.Object);

            mockUsuarioRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new UsuarioEntity { Id = Guid.NewGuid() }));

            var result = await mockUsuarioService.Object.Get(Guid.NewGuid());
            Assert.IsTrue(result.success);
        }
        [TestMethod]
        public async Task Usuario_Get_EmptyId()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockUsuarioRepository = new Mock<UsuarioRepository>(mockMySqlContext.Object);
            var mockListaRepository = new Mock<ListaRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<UsuarioService>>();

            mockUsuarioRepository.CallBase = true;
            mockListaRepository.CallBase = true;

            var mockUsuarioService = new Mock<UsuarioService>(mocklogger.Object, mockUsuarioRepository.Object, mockListaRepository.Object);

            var result = await mockUsuarioService.Object.Get(Guid.Empty);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Usuario_Get_IdNotFound()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockUsuarioRepository = new Mock<UsuarioRepository>(mockMySqlContext.Object);
            var mockListaRepository = new Mock<ListaRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<UsuarioService>>();

            mockUsuarioRepository.CallBase = true;
            mockListaRepository.CallBase = true;

            var mockUsuarioService = new Mock<UsuarioService>(mocklogger.Object, mockUsuarioRepository.Object, mockListaRepository.Object);

            mockUsuarioRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(null as UsuarioEntity));

            var result = await mockUsuarioService.Object.Get(Guid.NewGuid());
            Assert.IsTrue(!result.success);
        }
    }
}
