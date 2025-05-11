namespace HowToAuth.Presentation.Configurations.SwaggerOptions;

public class ConfigureDescriptionOptions(IApiVersionDescriptionProvider provider) : IConfigureOptions<SwaggerGenOptions>
{
    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            var info = new OpenApiInfo()
            {
                Title = "HowToAuth",
                Version = description.ApiVersion.ToString(),
            };

            options.SwaggerDoc(description.GroupName, info);
        }
    }
}
