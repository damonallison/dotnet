using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using TodoApi.Repositories;

namespace TodoApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            services.AddLogging();

            // Inject an implementation of ISwaggerProvider with default settings.
            services.AddSwaggerGen();
            this.ConfigureSwagger(services);

            // Typically we'd add a per-request instance here, however since 
            // we are using an in-memory data store, we need a singleton.
            services.AddSingleton<ITodoRepository, TodoRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();

            // Enables middleware to serve generated swagger Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve Swagger-UI assets (HTML, JS, CSS, etc)
            app.UseSwaggerUi();

        }

        private void ConfigureSwagger(IServiceCollection services) {
            services.ConfigureSwaggerGen(options => 
            {
                Swashbuckle.Swagger.Model.Contact contact = new Swashbuckle.Swagger.Model.Contact();
                contact.Email = "nc@chrobinson.com";
                contact.Name = "Navisphere Carrier";
                contact.Url = "http://api.chrobinson.com";

                Swashbuckle.Swagger.Model.Info info = new Swashbuckle.Swagger.Model.Info();
                info.Version = "v1";
                info.Title = "Todo API";
                info.Description = " An API for managing todo list items.";
                info.TermsOfService = "You use this API, you buy us Redbull.";
                info.Contact = contact;
                
                options.SingleApiVersion(info);

                // Tell swagger where to find generated comments for our API. 
                string basePath = PlatformServices.Default.Application.ApplicationBasePath;
                string documentationPath = System.IO.Path.Combine(basePath, "MyFirstApi.xml");
                System.Console.WriteLine($"xml documentation path : {documentationPath}");
                options.IncludeXmlComments(documentationPath);
            });
        }
    }
}
