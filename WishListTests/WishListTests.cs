using Microsoft.Extensions.DependencyInjection;
using System;
using System.Application.Data.MySql;
using System.Application.Data.Repositories.ListaItens;
using System.Application.Data.Repositories.Listas;
using System.Application.Data.Repositories.Produtos;
using System.Application.Data.Repositories.Usuarios;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using System.Application.Helpers.Default;

namespace WishListTests
{
    public class WishListTests : Startup
    {
        protected UsuarioRepository usuarioRepository;
        protected ProdutoRepository produtoRepository;
        protected ListaRepository listaRepository;
        protected ListaItemRepository listaItemRepository;
        protected MySqlContext context;
        protected DefaultResponse defaultResponse;


        public WishListTests()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddTransient<UsuarioRepository>();
            services.AddTransient<ProdutoRepository>();
            services.AddTransient<ListaRepository>();
            services.AddTransient<ListaItemRepository>();
            services.AddTransient<MySqlContext>();
            services.AddTransient<DefaultResponse>();
            services.AddSingleton<ILoggerFactory, LoggerFactory>();
            var serviceProvider = services.BuildServiceProvider();
            usuarioRepository = (UsuarioRepository)serviceProvider.GetService(typeof(UsuarioRepository));
            produtoRepository = (ProdutoRepository)serviceProvider.GetService(typeof(ProdutoRepository));
            context = (MySqlContext)serviceProvider.GetService(typeof(MySqlContext));
            listaRepository = (ListaRepository)serviceProvider.GetService(typeof(ListaRepository));
            defaultResponse = (DefaultResponse)serviceProvider.GetService(typeof(DefaultResponse));
            listaItemRepository = (ListaItemRepository)serviceProvider.GetService(typeof(ListaItemRepository));
        }
    }
}
