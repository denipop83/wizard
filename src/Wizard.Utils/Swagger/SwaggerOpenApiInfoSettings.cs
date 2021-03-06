namespace wizard.Utils.Swagger
{
    /// <summary>
    /// Настройки для OpenApiInfo в Swagger, устанавливаются в файле конфигурации.
    /// </summary>
    public class SwaggerOpenApiInfoSettings
    {
        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? ContactName { get; set; }

        public string? ContactEmail { get; set; }
    }
}