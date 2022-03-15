using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace wizard.Utils.Swagger
{
    /// <summary>
    /// Configures the Swagger generation options.
    /// </summary>
    /// <remarks>This allows API versioning to define a Swagger document per API version after the
    /// <see cref="IApiVersionDescriptionProvider"/> service has been resolved from the service container.</remarks>
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;
        private readonly SwaggerOpenApiInfoSettings _openApiInfoOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigureSwaggerOptions"/> class.
        /// </summary>
        /// <param name="provider">The <see cref="IApiVersionDescriptionProvider">provider</see> used to generate Swagger documents.</param>
        /// <param name="openApiInfoOptions">The Open Api Info options from the appsettings.json</param>
        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider, IOptions<SwaggerOpenApiInfoSettings> openApiInfoOptions)
        {
            _provider = provider;
            _openApiInfoOptions = openApiInfoOptions.Value;
        }

        /// <inheritdoc />
        public void Configure(SwaggerGenOptions options)
        {
            bool openApiInfoOptionsNotProvided = string.IsNullOrEmpty(_openApiInfoOptions.Title);

            if (openApiInfoOptionsNotProvided)
            {
                return;
            }

            var openApiInfo = new OpenApiInfo
            {
                Title = _openApiInfoOptions.Title,
                Description = _openApiInfoOptions.Description,
                Contact = new OpenApiContact
                {
                    Name = _openApiInfoOptions.ContactName,
                    Email = _openApiInfoOptions.ContactEmail
                }
            };

            // Add a swagger document for each discovered API version
            // Note: you might choose to skip or document deprecated API versions differently
            foreach (ApiVersionDescription description in _provider.ApiVersionDescriptions)
            {
                openApiInfo.Version = description.ApiVersion.ToString();
                options.SwaggerDoc(description.GroupName, openApiInfo);
            }
        }
    }
}