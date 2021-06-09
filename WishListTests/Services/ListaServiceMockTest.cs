using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Application.Contracts.Listas;
using System.Application.Data.Entities.ListaItens;
using System.Application.Data.Entities.Listas;
using System.Application.Data.MySql;
using System.Application.Data.Repositories.ListaItens;
using System.Application.Data.Repositories.Listas;
using System.Application.Services.Listas;
using System.Application.Views;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WishListTests.Services
{
    [TestClass]
    public class ListaServiceMockTest
    {
        [TestMethod]
        public async Task Lista_Post_Success()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockListaRepository = new Mock<ListaRepository>(mockMySqlContext.Object);
            var mockListaItemRepository = new Mock<ListaItemRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<ListaService>>();

            mockListaRepository.CallBase = true;
            mockListaItemRepository.CallBase = true;

            var mockListaService = new Mock<ListaService>(mocklogger.Object, mockListaRepository.Object, mockListaItemRepository.Object);

            ListaPostRequest _postRequest = new ListaPostRequest
            {
                usuarioId = Guid.NewGuid(),
                listaNome = "Presentes"
            };

            ListaEntity _listaEntity = new ListaEntity(_postRequest)
            {
                dataCriacao = DateTime.Now
            };

            mockListaRepository
                .Setup(x => x.Create(It.IsAny<ListaEntity>()))
                .Returns(Task.FromResult(_listaEntity));

            var result = await mockListaService.Object.Create(_postRequest);
            Assert.IsTrue(result.success);
        }
        [TestMethod]
        public async Task Lista_Post_EmptyUsuarioId()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockListaRepository = new Mock<ListaRepository>(mockMySqlContext.Object);
            var mockListaItemRepository = new Mock<ListaItemRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<ListaService>>();

            mockListaRepository.CallBase = true;
            mockListaItemRepository.CallBase = true;

            var mockListaService = new Mock<ListaService>(mocklogger.Object, mockListaRepository.Object, mockListaItemRepository.Object);

            ListaPostRequest _postRequest = new ListaPostRequest
            {
                usuarioId = Guid.Empty,
                listaNome = "Presentes"
            };

            var result = await mockListaService.Object.Create(_postRequest);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Lista_Post_DatabaseError()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockListaRepository = new Mock<ListaRepository>(mockMySqlContext.Object);
            var mockListaItemRepository = new Mock<ListaItemRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<ListaService>>();

            mockListaRepository.CallBase = true;
            mockListaItemRepository.CallBase = true;

            var mockListaService = new Mock<ListaService>(mocklogger.Object, mockListaRepository.Object, mockListaItemRepository.Object);

            ListaPostRequest _postRequest = new ListaPostRequest
            {
                usuarioId = Guid.NewGuid(),
                listaNome = "Presentes"
            };

            ListaEntity _listaEntity = new ListaEntity(_postRequest)
            {
                dataCriacao = DateTime.Now
            };

            mockListaRepository
                .Setup(x => x.Create(It.IsAny<ListaEntity>()))
                .Returns(Task.FromResult(new ListaEntity {Id = Guid.Empty }));

            var result = await mockListaService.Object.Create(_postRequest);
            Assert.IsTrue(!result.success);
        }


        [TestMethod]
        public async Task Lista_Delete_Success()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockListaRepository = new Mock<ListaRepository>(mockMySqlContext.Object);
            var mockListaItemRepository = new Mock<ListaItemRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<ListaService>>();

            mockListaRepository.CallBase = true;
            mockListaItemRepository.CallBase = true;

            var mockListaService = new Mock<ListaService>(mocklogger.Object, mockListaRepository.Object, mockListaItemRepository.Object);

            mockListaItemRepository
                .Setup(x => x.GetListItemByListID(It.IsAny<Guid>()))
                .Returns(Task.FromResult(null as ListaItemEntity));

            mockListaRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new ListaEntity {Id = Guid.NewGuid() }));

            mockListaRepository
                .Setup(x => x.Delete(It.IsAny<Guid>()))
                .Returns(Task.FromResult(true));

            var result = await mockListaService.Object.Delete(Guid.NewGuid());
            Assert.IsTrue(result.success);
        }
        [TestMethod]
        public async Task Lista_Delete_EmptyId()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockListaRepository = new Mock<ListaRepository>(mockMySqlContext.Object);
            var mockListaItemRepository = new Mock<ListaItemRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<ListaService>>();

            mockListaRepository.CallBase = true;
            mockListaItemRepository.CallBase = true;

            var mockListaService = new Mock<ListaService>(mocklogger.Object, mockListaRepository.Object, mockListaItemRepository.Object);

            var result = await mockListaService.Object.Delete(Guid.Empty);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Lista_Delete_ActiveWishListItem()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockListaRepository = new Mock<ListaRepository>(mockMySqlContext.Object);
            var mockListaItemRepository = new Mock<ListaItemRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<ListaService>>();

            mockListaRepository.CallBase = true;
            mockListaItemRepository.CallBase = true;

            var mockListaService = new Mock<ListaService>(mocklogger.Object, mockListaRepository.Object, mockListaItemRepository.Object);

            mockListaItemRepository
                .Setup(x => x.GetListItemByListID(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new ListaItemEntity {Id = Guid.NewGuid() }));

            var result = await mockListaService.Object.Delete(Guid.NewGuid());
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Lista_Delete_IdNotFound()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockListaRepository = new Mock<ListaRepository>(mockMySqlContext.Object);
            var mockListaItemRepository = new Mock<ListaItemRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<ListaService>>();

            mockListaRepository.CallBase = true;
            mockListaItemRepository.CallBase = true;

            var mockListaService = new Mock<ListaService>(mocklogger.Object, mockListaRepository.Object, mockListaItemRepository.Object);

            mockListaItemRepository
                .Setup(x => x.GetListItemByListID(It.IsAny<Guid>()))
                .Returns(Task.FromResult(null as ListaItemEntity));

            mockListaRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(null as ListaEntity));

            var result = await mockListaService.Object.Delete(Guid.NewGuid());
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Lista_Delete_DatabaseError()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockListaRepository = new Mock<ListaRepository>(mockMySqlContext.Object);
            var mockListaItemRepository = new Mock<ListaItemRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<ListaService>>();

            mockListaRepository.CallBase = true;
            mockListaItemRepository.CallBase = true;

            var mockListaService = new Mock<ListaService>(mocklogger.Object, mockListaRepository.Object, mockListaItemRepository.Object);

            mockListaItemRepository
                .Setup(x => x.GetListItemByListID(It.IsAny<Guid>()))
                .Returns(Task.FromResult(null as ListaItemEntity));

            mockListaRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new ListaEntity { Id = Guid.NewGuid() }));

            mockListaRepository
                .Setup(x => x.Delete(It.IsAny<Guid>()))
                .Returns(Task.FromResult(false));

            var result = await mockListaService.Object.Delete(Guid.NewGuid());
            Assert.IsTrue(!result.success);
        }


        [TestMethod]
        public async Task Lista_Get_Success()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockListaRepository = new Mock<ListaRepository>(mockMySqlContext.Object);
            var mockListaItemRepository = new Mock<ListaItemRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<ListaService>>();

            mockListaRepository.CallBase = true;
            mockListaItemRepository.CallBase = true;

            var mockListaService = new Mock<ListaService>(mocklogger.Object, mockListaRepository.Object, mockListaItemRepository.Object);

            var listaItemEntity = new ListaItemViewEntity()
            {
                tituloProduto = "Livro Clean Code",
                descricao = "411 pgs",
                comprado = true
            };

            var _list = new List<ListaItemViewEntity>();
            _list.Add(listaItemEntity);

            mockListaRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new ListaEntity { Id = Guid.NewGuid() }));

            mockListaItemRepository
                .Setup(x => x.GetListItemViewByListID(It.IsAny<Guid>()))
                .Returns(Task.FromResult(_list));

            var result = await mockListaService.Object.Get(Guid.NewGuid());
            Assert.IsTrue(result.success);
        }
        [TestMethod]
        public async Task Lista_Get_EmptyId()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockListaRepository = new Mock<ListaRepository>(mockMySqlContext.Object);
            var mockListaItemRepository = new Mock<ListaItemRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<ListaService>>();

            mockListaRepository.CallBase = true;
            mockListaItemRepository.CallBase = true;

            var mockListaService = new Mock<ListaService>(mocklogger.Object, mockListaRepository.Object, mockListaItemRepository.Object);

            var result = await mockListaService.Object.Get(Guid.Empty);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Lista_Get_IdNotFound()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockListaRepository = new Mock<ListaRepository>(mockMySqlContext.Object);
            var mockListaItemRepository = new Mock<ListaItemRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<ListaService>>();

            mockListaRepository.CallBase = true;
            mockListaItemRepository.CallBase = true;

            var mockListaService = new Mock<ListaService>(mocklogger.Object, mockListaRepository.Object, mockListaItemRepository.Object);

            mockListaRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(null as ListaEntity));

            var result = await mockListaService.Object.Get(Guid.NewGuid());
            Assert.IsTrue(!result.success);
        }


        [TestMethod]
        public async Task Lista_GetRandomItem_Success()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockListaRepository = new Mock<ListaRepository>(mockMySqlContext.Object);
            var mockListaItemRepository = new Mock<ListaItemRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<ListaService>>();

            mockListaRepository.CallBase = true;
            mockListaItemRepository.CallBase = true;

            var mockListaService = new Mock<ListaService>(mocklogger.Object, mockListaRepository.Object, mockListaItemRepository.Object);

            var listaItemEntity = new ListaItemViewEntity()
            {
                tituloProduto = "Livro Clean Code",
                descricao = "411 pgs",
                comprado = true
            };

            var _list = new List<ListaItemViewEntity>();
            _list.Add(listaItemEntity);

            mockListaRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new ListaEntity { Id = Guid.NewGuid() }));

            mockListaItemRepository
                .Setup(x => x.GetListItemViewByListID(It.IsAny<Guid>()))
                .Returns(Task.FromResult(_list));

            var result = await mockListaService.Object.GetRandomItem(Guid.NewGuid());
            Assert.IsTrue(result.success);
        }
        [TestMethod]
        public async Task Lista_GetRandomItem_EmptyId()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockListaRepository = new Mock<ListaRepository>(mockMySqlContext.Object);
            var mockListaItemRepository = new Mock<ListaItemRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<ListaService>>();

            mockListaRepository.CallBase = true;
            mockListaItemRepository.CallBase = true;

            var mockListaService = new Mock<ListaService>(mocklogger.Object, mockListaRepository.Object, mockListaItemRepository.Object);

            var result = await mockListaService.Object.GetRandomItem(Guid.Empty);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Lista_GetRandomItem_IdNotFound()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockListaRepository = new Mock<ListaRepository>(mockMySqlContext.Object);
            var mockListaItemRepository = new Mock<ListaItemRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<ListaService>>();

            mockListaRepository.CallBase = true;
            mockListaItemRepository.CallBase = true;

            var mockListaService = new Mock<ListaService>(mocklogger.Object, mockListaRepository.Object, mockListaItemRepository.Object);

            mockListaRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(null as ListaEntity));

            var result = await mockListaService.Object.GetRandomItem(Guid.NewGuid());
            Assert.IsTrue(!result.success);
        }
    }
}
