using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CartaoWebAPI.Data;


namespace CartaoWebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CartaoContext>(Opt =>
            /* Aqui adicionamos nossa Classe CartaoContext como serviço da aplicação e conectamos ao banco 
             * de Dados MySql usando a função: UseMySQL()  do pacote MySql.EntityFrameWorkCore onde é necessario configurar
             * para seu usuario e senha do MySql, informar o nome do banco e o servidor do MySql*/
            Opt.UseMySQL("server=localhost;database=WebApiSystem;user=root;password=123456789"));
            services.AddControllers();
        }
    

// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
