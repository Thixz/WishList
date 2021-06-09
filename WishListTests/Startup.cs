using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Application.Data.MySql;
using System.Application.Data.Repositories.ListaItens;
using System.Application.Data.Repositories.Listas;
using System.Application.Data.Repositories.Produtos;
using System.Application.Data.Repositories.Usuarios;
using System.Application.Helpers.Default;
using System.Collections.Generic;
using System.Text;

namespace WishListTests
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup()
        {

        }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }



        public void ConfigureServices(IServiceCollection services)
        {
            BeforeConfigureServices(services);
            services.AddScoped<MySqlContext>();
            services.AddScoped<UsuarioRepository>();
            services.AddScoped<ProdutoRepository>();
            services.AddScoped<ListaRepository>();
            services.AddScoped<ListaItemRepository>();
            services.AddScoped<DefaultResponse>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void BeforeConfigureServices(IServiceCollection services)
        {

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
