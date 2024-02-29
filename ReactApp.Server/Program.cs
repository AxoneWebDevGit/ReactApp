
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using ReactApp.SwaggerConfig;
using Microsoft.Extensions.Options;
using ReactApp.Server.Controllers.V1;
using ReactApp.Server.Controllers.V2;

namespace ReactApp.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(opt => {
                //opt.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); // Use the first action when conflicts occur

                opt.DocumentFilter<SwaggerDocumentFilter>();
                opt.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Test API",
                    Description = "An ASP.NET Core Web API",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "For Change",
                        Url = new Uri("https://learn.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-8.0&tabs=visual-studio")
                        //Name = "Example Contact",
                        //Url = new Uri("https://example.com/contact")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Example License",
                        Url = new Uri("https://example.com/license")
                    }
                });

                // using System.Reflection;
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                opt.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
            builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigOptions>();

            builder.Services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
                //options.ApiVersionReader = new HeaderApiVersionReader("x-api-version");

            });
            builder.Services.AddVersionedApiExplorer(opt => {
                opt.SubstituteApiVersionInUrl = true;
            });
            builder.Services.AddEndpointsApiExplorer();

            var app = builder.Build();

            var versionDescProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(o =>
                {
                    foreach (var desc in versionDescProvider.ApiVersionDescriptions)
                    {
                        o.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json", $"Api {desc.GroupName} Docs");
                    }
                    //o.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");
                    //o.SwaggerEndpoint("/swagger/v2/swagger.json", "V2 Docs");
                });
            }


            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
