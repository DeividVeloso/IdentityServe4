using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Defino as cconfigurações do CLiente
            /*
                Exemplo:
                    - o Console.ClientUI vai acessar a API de Usuário
            */
            var clients = new List<Client>();
            clients.Add(new Client()
            {
                ClientId = "Console.ClientUI",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                AllowedScopes = { "API.User" } //Permitido ter acesso a API.usuário
            });

            // Adiciona a autorização para API's
            var apiResources = new List<ApiResource>();
            apiResources.Add(new ApiResource("API.User", "Manager User"));


            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryApiResources(apiResources)
                .AddInMemoryClients(clients);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();
        }
    }
}
