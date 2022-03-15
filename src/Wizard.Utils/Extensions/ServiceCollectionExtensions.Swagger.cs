using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using wizard.Utils.Swagger;

namespace wizard.Utils.Extensions
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCommonSwagger(
            this IServiceCollection services,
            IConfiguration configuration,
            List<string>? xmlDocAssemblyNames = null,
            Action<SwaggerGenOptions>? setupAction = null,
            bool useSetupActionInsteadOfDefault = false)
        {
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            services.Configure<SwaggerOpenApiInfoSettings>(
                options => { configuration.GetSection(nameof(SwaggerOpenApiInfoSettings)).Bind(options); }
            );

            if (setupAction != null && useSetupActionInsteadOfDefault)
            {
                services.AddCommonSwaggerGen(setupAction);
            }
            else
            {
                services.AddCommonSwaggerGen(options => ConfigureCommonSwaggerOptions(options, xmlDocAssemblyNames, setupAction));
            }

            return services;
        }

        private static IServiceCollection AddCommonSwaggerGen(
            this IServiceCollection services,
            Action<SwaggerGenOptions>? setupAction = null)
        {
            if (setupAction != null)
            {
                services.Configure(setupAction);
            }

            services.AddSwaggerGen();

            return services;
        }

        private static void ConfigureCommonSwaggerOptions(
            SwaggerGenOptions options,
            List<string>? xmlDocAssemblyNames = null,
            Action<SwaggerGenOptions>? setupAction = null)
        {
            options.CustomSchemaIds(type => type.FullName);

            // Add a custom operation filter which sets default values
            options.OperationFilter<SwaggerDefaultValues>();

            // JWT Bearer Authorization
            options.AddSecurityDefinition(
                "Bearer",
                new OpenApiSecurityScheme
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                }
            );

            options.AddSecurityRequirement(
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                }
            );

            if (xmlDocAssemblyNames != null)
            {
                foreach (string assemblyName in xmlDocAssemblyNames)
                {
                    // Set the comments path for the Swagger JSON and UI.
                    string xmlFile = $"{assemblyName}.xml";
                    string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                    options.IncludeXmlComments(xmlPath);
                }
            }

            setupAction?.Invoke(options);
        }
    }
}