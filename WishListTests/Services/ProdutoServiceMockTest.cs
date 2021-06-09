using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Application.Contracts.Produtos;
using System.Application.Contracts.Usuarios;
using System.Application.Data.Entities.ListaItens;
using System.Application.Data.Entities.Produtos;
using System.Application.Data.MySql;
using System.Application.Data.Repositories.ListaItens;
using System.Application.Data.Repositories.Produtos;
using System.Application.Services.Produtos;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WishListTests.Services
{
    [TestClass]
    public class ProdutoServiceMockTest
    {
        [TestMethod]
        public async Task Produto_Post_Success()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockProdutoRepository = new Mock<ProdutoRepository>(mockMySqlContext.Object);
            var mockListaItemRepository = new Mock<ListaItemRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<ProdutoService>>();

            mockProdutoRepository.CallBase = true;
            mockListaItemRepository.CallBase = true;

            var mockProdutoService = new Mock<ProdutoService>(mocklogger.Object, mockProdutoRepository.Object, mockListaItemRepository.Object);

            ProdutoPostRequest _postRequest = new ProdutoPostRequest
            {
                tituloProduto = "Livro Clean Code",
                Descricao = "Livro Clean Code Novo 411pgs"
            };

            ProdutoEntity _produtoEntity = new ProdutoEntity(_postRequest)
            {
                dataCriacao = DateTime.Now
            };

            mockProdutoRepository
                .Setup(x => x.Create(It.IsAny<ProdutoEntity>()))
                .Returns(Task.FromResult(_produtoEntity));

            var result = await mockProdutoService.Object.Create(_postRequest);
            Assert.IsTrue(result.success);
        }
        [TestMethod]
        public async Task Produto_Post_ValidatorError()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockProdutoRepository = new Mock<ProdutoRepository>(mockMySqlContext.Object);
            var mockListaItemRepository = new Mock<ListaItemRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<ProdutoService>>();

            mockProdutoRepository.CallBase = true;
            mockListaItemRepository.CallBase = true;

            var mockProdutoService = new Mock<ProdutoService>(mocklogger.Object, mockProdutoRepository.Object, mockListaItemRepository.Object);

            ProdutoPostRequest _postRequest = new ProdutoPostRequest
            {
                tituloProduto = "",
                Descricao = ""
            };


            mockProdutoRepository
                .Setup(x => x.Create(It.IsAny<ProdutoEntity>()))
                .Returns(Task.FromResult(new ProdutoEntity {Id = Guid.NewGuid() }));

            var result = await mockProdutoService.Object.Create(_postRequest);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Produto_Post_DatabaseError()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockProdutoRepository = new Mock<ProdutoRepository>(mockMySqlContext.Object);
            var mockListaItemRepository = new Mock<ListaItemRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<ProdutoService>>();

            mockProdutoRepository.CallBase = true;
            mockListaItemRepository.CallBase = true;

            var mockProdutoService = new Mock<ProdutoService>(mocklogger.Object, mockProdutoRepository.Object, mockListaItemRepository.Object);

            ProdutoPostRequest _postRequest = new ProdutoPostRequest
            {
                tituloProduto = "Livro Clean Code",
                Descricao = "Livro Clean Code Novo 411pgs"
            };

            mockProdutoRepository
                .Setup(x => x.Create(It.IsAny<ProdutoEntity>()))
                .Returns(Task.FromResult(new ProdutoEntity {Id = Guid.Empty }));

            var result = await mockProdutoService.Object.Create(_postRequest);
            Assert.IsTrue(!result.success);
        }


        [TestMethod]
        public async Task Produto_Put_Success()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockProdutoRepository = new Mock<ProdutoRepository>(mockMySqlContext.Object);
            var mockListaItemRepository = new Mock<ListaItemRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<ProdutoService>>();

            mockProdutoRepository.CallBase = true;
            mockListaItemRepository.CallBase = true;

            var mockProdutoService = new Mock<ProdutoService>(mocklogger.Object, mockProdutoRepository.Object, mockListaItemRepository.Object);

            ProdutoPutRequest _putRequest = new ProdutoPutRequest
            {
                Id = Guid.NewGuid(),
                tituloProduto = "Livro Atualizado",
                Descricao = "Novo Livro"
            };

            ProdutoEntity _produtoEntity = new ProdutoEntity(_putRequest)
            {
                dataAtualizacao = DateTime.Now
            };

            mockProdutoRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(_produtoEntity));

            mockProdutoRepository
                .Setup(x => x.Update(It.IsAny<ProdutoEntity>()))
                .Returns(Task.FromResult(_produtoEntity));

            var result = await mockProdutoService.Object.Update(_putRequest);
            Assert.IsTrue(result.success);
        }
        [TestMethod]
        public async Task Produto_Put_EmptyId()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockProdutoRepository = new Mock<ProdutoRepository>(mockMySqlContext.Object);
            var mockListaItemRepository = new Mock<ListaItemRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<ProdutoService>>();

            mockProdutoRepository.CallBase = true;
            mockListaItemRepository.CallBase = true;

            var mockProdutoService = new Mock<ProdutoService>(mocklogger.Object, mockProdutoRepository.Object, mockListaItemRepository.Object);

            ProdutoPutRequest _putRequest = new ProdutoPutRequest
            {
                Id = Guid.Empty,
                tituloProduto = "Livro Atualizado",
                Descricao = "Novo Livro"
            };

            var result = await mockProdutoService.Object.Update(_putRequest);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Produto_Put_ValidatorError()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockProdutoRepository = new Mock<ProdutoRepository>(mockMySqlContext.Object);
            var mockListaItemRepository = new Mock<ListaItemRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<ProdutoService>>();

            mockProdutoRepository.CallBase = true;
            mockListaItemRepository.CallBase = true;

            var mockProdutoService = new Mock<ProdutoService>(mocklogger.Object, mockProdutoRepository.Object, mockListaItemRepository.Object);

            ProdutoPutRequest _putRequest = new ProdutoPutRequest
            {
                Id = Guid.NewGuid(),
                tituloProduto = "",
                Descricao = ""
            };

            var result = await mockProdutoService.Object.Update(_putRequest);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Produto_Put_IdNotFound()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockProdutoRepository = new Mock<ProdutoRepository>(mockMySqlContext.Object);
            var mockListaItemRepository = new Mock<ListaItemRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<ProdutoService>>();

            mockProdutoRepository.CallBase = true;
            mockListaItemRepository.CallBase = true;

            var mockProdutoService = new Mock<ProdutoService>(mocklogger.Object, mockProdutoRepository.Object, mockListaItemRepository.Object);

            ProdutoPutRequest _putRequest = new ProdutoPutRequest
            {
                Id = Guid.NewGuid(),
                tituloProduto = "Livro Atualizado",
                Descricao = "Novo Livro"
            };

            mockProdutoRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(null as ProdutoEntity));

            var result = await mockProdutoService.Object.Update(_putRequest);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Produto_Put_DatabaseError()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockProdutoRepository = new Mock<ProdutoRepository>(mockMySqlContext.Object);
            var mockListaItemRepository = new Mock<ListaItemRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<ProdutoService>>();

            mockProdutoRepository.CallBase = true;
            mockListaItemRepository.CallBase = true;

            var mockProdutoService = new Mock<ProdutoService>(mocklogger.Object, mockProdutoRepository.Object, mockListaItemRepository.Object);

            ProdutoPutRequest _putRequest = new ProdutoPutRequest
            {
                Id = Guid.NewGuid(),
                tituloProduto = "Livro Atualizado",
                Descricao = "Novo Livro"
            };

            ProdutoEntity _produtoEntity = new ProdutoEntity(_putRequest)
            {
                dataAtualizacao = DateTime.Now
            };

            mockProdutoRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(_produtoEntity));

            mockProdutoRepository
                .Setup(x => x.Update(It.IsAny<ProdutoEntity>()))
                .Returns(Task.FromResult(new ProdutoEntity {Id = Guid.Empty }));

            var result = await mockProdutoService.Object.Update(_putRequest);
            Assert.IsTrue(!result.success);
        }


        [TestMethod]
        public async Task Produto_Delete_Success()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockProdutoRepository = new Mock<ProdutoRepository>(mockMySqlContext.Object);
            var mockListaItemRepository = new Mock<ListaItemRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<ProdutoService>>();

            mockProdutoRepository.CallBase = true;
            mockListaItemRepository.CallBase = true;

            var mockProdutoService = new Mock<ProdutoService>(mocklogger.Object, mockProdutoRepository.Object, mockListaItemRepository.Object);

            mockListaItemRepository
                .Setup(x => x.GetListItemByProductID(It.IsAny<Guid>()))
                .Returns(Task.FromResult(null as ListaItemEntity));

            mockProdutoRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new ProdutoEntity { Id = Guid.NewGuid() }));

            mockProdutoRepository
                .Setup(x => x.Delete(It.IsAny<Guid>()))
                .Returns(Task.FromResult(true));

            var result = await mockProdutoService.Object.Delete(Guid.NewGuid());
            Assert.IsTrue(result.success);
        }
        [TestMethod]
        public async Task Produto_Delete_EmptyId()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockProdutoRepository = new Mock<ProdutoRepository>(mockMySqlContext.Object);
            var mockListaItemRepository = new Mock<ListaItemRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<ProdutoService>>();

            mockProdutoRepository.CallBase = true;
            mockListaItemRepository.CallBase = true;

            var mockProdutoService = new Mock<ProdutoService>(mocklogger.Object, mockProdutoRepository.Object, mockListaItemRepository.Object);

            var result = await mockProdutoService.Object.Delete(Guid.Empty);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Produto_Delete_ActiveWishListItem()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockProdutoRepository = new Mock<ProdutoRepository>(mockMySqlContext.Object);
            var mockListaItemRepository = new Mock<ListaItemRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<ProdutoService>>();

            mockProdutoRepository.CallBase = true;
            mockListaItemRepository.CallBase = true;

            var mockProdutoService = new Mock<ProdutoService>(mocklogger.Object, mockProdutoRepository.Object, mockListaItemRepository.Object);

            mockListaItemRepository
                .Setup(x => x.GetListItemByProductID(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new ListaItemEntity {Id = Guid.NewGuid() }));

            var result = await mockProdutoService.Object.Delete(Guid.NewGuid());
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Produto_Delete_IdNotFound()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockProdutoRepository = new Mock<ProdutoRepository>(mockMySqlContext.Object);
            var mockListaItemRepository = new Mock<ListaItemRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<ProdutoService>>();

            mockProdutoRepository.CallBase = true;
            mockListaItemRepository.CallBase = true;

            var mockProdutoService = new Mock<ProdutoService>(mocklogger.Object, mockProdutoRepository.Object, mockListaItemRepository.Object);

            mockListaItemRepository
                .Setup(x => x.GetListItemByProductID(It.IsAny<Guid>()))
                .Returns(Task.FromResult(null as ListaItemEntity));

            mockProdutoRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(null as ProdutoEntity));

            var result = await mockProdutoService.Object.Delete(Guid.NewGuid());
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Produto_Delete_DatabaseError()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockProdutoRepository = new Mock<ProdutoRepository>(mockMySqlContext.Object);
            var mockListaItemRepository = new Mock<ListaItemRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<ProdutoService>>();

            mockProdutoRepository.CallBase = true;
            mockListaItemRepository.CallBase = true;

            var mockProdutoService = new Mock<ProdutoService>(mocklogger.Object, mockProdutoRepository.Object, mockListaItemRepository.Object);

            mockListaItemRepository
                .Setup(x => x.GetListItemByProductID(It.IsAny<Guid>()))
                .Returns(Task.FromResult(null as ListaItemEntity));

            mockProdutoRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new ProdutoEntity { Id = Guid.NewGuid() }));

            mockProdutoRepository
                .Setup(x => x.Delete(It.IsAny<Guid>()))
                .Returns(Task.FromResult(false));

            var result = await mockProdutoService.Object.Delete(Guid.NewGuid());
            Assert.IsTrue(!result.success);
        }


        [TestMethod]
        public async Task Produto_Get_Success()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockProdutoRepository = new Mock<ProdutoRepository>(mockMySqlContext.Object);
            var mockListaItemRepository = new Mock<ListaItemRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<ProdutoService>>();

            mockProdutoRepository.CallBase = true;
            mockListaItemRepository.CallBase = true;

            var mockProdutoService = new Mock<ProdutoService>(mocklogger.Object, mockProdutoRepository.Object, mockListaItemRepository.Object);

            mockProdutoRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new ProdutoEntity { Id = Guid.NewGuid() }));

            var result = await mockProdutoService.Object.Get(Guid.NewGuid());
            Assert.IsTrue(result.success);
        }
        [TestMethod]
        public async Task Produto_Get_EmptyId()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockProdutoRepository = new Mock<ProdutoRepository>(mockMySqlContext.Object);
            var mockListaItemRepository = new Mock<ListaItemRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<ProdutoService>>();

            mockProdutoRepository.CallBase = true;
            mockListaItemRepository.CallBase = true;

            var mockProdutoService = new Mock<ProdutoService>(mocklogger.Object, mockProdutoRepository.Object, mockListaItemRepository.Object);

            var result = await mockProdutoService.Object.Get(Guid.Empty);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Produto_Get_IdNotFound()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockProdutoRepository = new Mock<ProdutoRepository>(mockMySqlContext.Object);
            var mockListaItemRepository = new Mock<ListaItemRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<ProdutoService>>();

            mockProdutoRepository.CallBase = true;
            mockListaItemRepository.CallBase = true;

            var mockProdutoService = new Mock<ProdutoService>(mocklogger.Object, mockProdutoRepository.Object, mockListaItemRepository.Object);

            mockProdutoRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(null as ProdutoEntity));

            var result = await mockProdutoService.Object.Get(Guid.NewGuid());
            Assert.IsTrue(!result.success);
        }
    }
}
