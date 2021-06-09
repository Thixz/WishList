using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Application.Contracts.ListaItens;
using System.Application.Data.Entities.ListaItens;
using System.Application.Data.MySql;
using System.Application.Data.Repositories.ListaItens;
using System.Application.Services.ListaItens;
using System.Application.Services.Listas;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WishListTests.Services
{
    [TestClass]
    public class ListaItemServiceMockTest
    {
        [TestMethod]
        public async Task ListaItem_Post_Success()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockListaItemRepository = new Mock<ListaItemRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<ListaItemService>>();

            mockListaItemRepository.CallBase = true;

            var mockListaItemService = new Mock<ListaItemService>(mocklogger.Object, mockListaItemRepository.Object);

            ListaItemPostRequest _postRequest = new ListaItemPostRequest
            {
                listaId = Guid.NewGuid(),
                produtoId = Guid.NewGuid(),
                Comprado = true
            };

            ListaItemEntity _listaItemEntity = new ListaItemEntity(_postRequest)
            {
                dataCriacao = DateTime.Now
            };

            mockListaItemRepository
                .Setup(x => x.GetListItemByProductID(It.IsAny<Guid>()))
                .Returns(Task.FromResult(null as ListaItemEntity));

            mockListaItemRepository
                .Setup(x => x.Create(It.IsAny<ListaItemEntity>()))
                .Returns(Task.FromResult(new ListaItemEntity {Id = Guid.NewGuid() }));

            var result = await mockListaItemService.Object.Create(_postRequest);
            Assert.IsTrue(result.success);
        }
        [TestMethod]
        public async Task ListaItem_Post_ListProductEmptyId()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockListaItemRepository = new Mock<ListaItemRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<ListaItemService>>();

            mockListaItemRepository.CallBase = true;

            var mockListaItemService = new Mock<ListaItemService>(mocklogger.Object, mockListaItemRepository.Object);

            ListaItemPostRequest _postRequest = new ListaItemPostRequest
            {
                listaId = Guid.Empty,
                produtoId = Guid.Empty,
                Comprado = true
            };

            var result = await mockListaItemService.Object.Create(_postRequest);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task ListaItem_Post_DuplicatedProduct()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockListaItemRepository = new Mock<ListaItemRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<ListaItemService>>();

            mockListaItemRepository.CallBase = true;

            var mockListaItemService = new Mock<ListaItemService>(mocklogger.Object, mockListaItemRepository.Object);

            ListaItemPostRequest _postRequest = new ListaItemPostRequest
            {
                listaId = Guid.NewGuid(),
                produtoId = Guid.NewGuid(),
                Comprado = true
            };

            ListaItemEntity _listaItemEntity = new ListaItemEntity(_postRequest)
            {
                dataCriacao = DateTime.Now
            };

            mockListaItemRepository
                .Setup(x => x.GetListItemByProductID(It.IsAny<Guid>()))
                .Returns(Task.FromResult(_listaItemEntity));

            var result = await mockListaItemService.Object.Create(_postRequest);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task ListaItem_Post_DatabaseError()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockListaItemRepository = new Mock<ListaItemRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<ListaItemService>>();

            mockListaItemRepository.CallBase = true;

            var mockListaItemService = new Mock<ListaItemService>(mocklogger.Object, mockListaItemRepository.Object);

            ListaItemPostRequest _postRequest = new ListaItemPostRequest
            {
                listaId = Guid.NewGuid(),
                produtoId = Guid.NewGuid(),
                Comprado = true
            };

            ListaItemEntity _listaItemEntity = new ListaItemEntity(_postRequest)
            {
                dataCriacao = DateTime.Now
            };

            mockListaItemRepository
                .Setup(x => x.GetListItemByProductID(It.IsAny<Guid>()))
                .Returns(Task.FromResult(null as ListaItemEntity));

            mockListaItemRepository
                .Setup(x => x.Create(It.IsAny<ListaItemEntity>()))
                .Returns(Task.FromResult(new ListaItemEntity { Id = Guid.Empty }));

            var result = await mockListaItemService.Object.Create(_postRequest);
            Assert.IsTrue(!result.success);
        }


        [TestMethod]
        public async Task ListaItem_Delete_Success()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockListaItemRepository = new Mock<ListaItemRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<ListaItemService>>();

            mockListaItemRepository.CallBase = true;

            var mockListaItemService = new Mock<ListaItemService>(mocklogger.Object, mockListaItemRepository.Object);

            mockListaItemRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new ListaItemEntity {Id = Guid.NewGuid() }));

            mockListaItemRepository
                .Setup(x => x.Delete(It.IsAny<Guid>()))
                .Returns(Task.FromResult(true));

            var result = await mockListaItemService.Object.Delete(Guid.NewGuid());
            Assert.IsTrue(result.success);
        }
        [TestMethod]
        public async Task ListaItem_Delete_EmptyId()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockListaItemRepository = new Mock<ListaItemRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<ListaItemService>>();

            mockListaItemRepository.CallBase = true;

            var mockListaItemService = new Mock<ListaItemService>(mocklogger.Object, mockListaItemRepository.Object);

            var result = await mockListaItemService.Object.Delete(Guid.Empty);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task ListaItem_Delete_IdNotFound()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockListaItemRepository = new Mock<ListaItemRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<ListaItemService>>();

            mockListaItemRepository.CallBase = true;

            var mockListaItemService = new Mock<ListaItemService>(mocklogger.Object, mockListaItemRepository.Object);

            mockListaItemRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(null as ListaItemEntity));

            var result = await mockListaItemService.Object.Delete(Guid.NewGuid());
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task ListaItem_Delete_DatabaseError()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockListaItemRepository = new Mock<ListaItemRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<ListaItemService>>();

            mockListaItemRepository.CallBase = true;

            var mockListaItemService = new Mock<ListaItemService>(mocklogger.Object, mockListaItemRepository.Object);

            mockListaItemRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new ListaItemEntity { Id = Guid.NewGuid() }));

            mockListaItemRepository
                .Setup(x => x.Delete(It.IsAny<Guid>()))
                .Returns(Task.FromResult(false));

            var result = await mockListaItemService.Object.Delete(Guid.NewGuid());
            Assert.IsTrue(!result.success);
        }


        [TestMethod]
        public async Task ListaItem_Get_Success()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockListaItemRepository = new Mock<ListaItemRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<ListaItemService>>();

            mockListaItemRepository.CallBase = true;

            var mockListaItemService = new Mock<ListaItemService>(mocklogger.Object, mockListaItemRepository.Object);

            mockListaItemRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new ListaItemEntity { Id = Guid.NewGuid() }));

            var result = await mockListaItemService.Object.Get(Guid.NewGuid());
            Assert.IsTrue(result.success);
        }
        [TestMethod]
        public async Task ListaItem_Get_EmptyId()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockListaItemRepository = new Mock<ListaItemRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<ListaItemService>>();

            mockListaItemRepository.CallBase = true;

            var mockListaItemService = new Mock<ListaItemService>(mocklogger.Object, mockListaItemRepository.Object);

            var result = await mockListaItemService.Object.Get(Guid.Empty);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task ListaItem_Get_IdNotFound()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockListaItemRepository = new Mock<ListaItemRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<ListaItemService>>();

            mockListaItemRepository.CallBase = true;

            var mockListaItemService = new Mock<ListaItemService>(mocklogger.Object, mockListaItemRepository.Object);

            mockListaItemRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(null as ListaItemEntity));

            var result = await mockListaItemService.Object.Get(Guid.NewGuid());
            Assert.IsTrue(!result.success);
        }
    }
}
