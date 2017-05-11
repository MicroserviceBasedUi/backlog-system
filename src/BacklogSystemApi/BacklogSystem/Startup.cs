using System.IO;
using JiraAdapter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;

namespace BacklogSystem
{
    public class Startup
    {
        public Startup(IHostingEnvironment env){
            var builder = new ConfigurationBuilder()
                                    .SetBasePath(env.ContentRootPath)
                                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                    .AddEnvironmentVariables();

            this.Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
			services.AddCors(options =>
			{
				options.AddPolicy("Default",
					builder => builder
						.AllowAnyOrigin()
						.AllowAnyHeader()
						.AllowAnyMethod());
			});

            services.AddMvc();
            services.AddSwaggerGen((c) => {
                c.SwaggerDoc("v1", new Info { Title = "Backlog System API", Version = "v1" });

                //Set the comments path for the swagger json and ui.
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "BacklogSystem.xml");
                c.IncludeXmlComments(xmlPath);
            });

            services.AddSingleton<JiraConfiguration>((_) => new JiraConfiguration
            {
                JiraUser = this.Configuration.GetValue<string>("Jira:User") ?? this.Configuration.GetValue<string>("JIRA_USER"),
                JiraPassword = this.Configuration.GetValue<string>("Jira:Password") ?? this.Configuration.GetValue<string>("JIRA_PASSWORD"),
                BaseUrl = this.Configuration.GetValue<string>("Jira:BaseUrl")
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
			app.UseCors("Default");
            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Backlog System API v1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
        }
    }
}
