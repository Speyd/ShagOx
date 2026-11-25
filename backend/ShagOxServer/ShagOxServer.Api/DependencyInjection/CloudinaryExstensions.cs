using CloudinaryDotNet;
using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Options.Pictures;

namespace ShagOxServer.Api.DependencyInjection;
public static class CloudinaryExstensions
{
    public static IServiceCollection AddCloudinary(
       this IServiceCollection services,
       IConfiguration configuration)
    {
        services.Configure<CloudinaryOptions>(
            configuration.GetSection(CloudinaryOptions.SectionName));


        services.AddSingleton(sp =>
        {
            var options = sp
                .GetRequiredService<IOptions<CloudinaryOptions>>()
                .Value;


            var account = new Account(
                options.CloudName,
                options.ApiKey,
                options.ApiSecret
            );


            return new Cloudinary(account);
        });


        return services;
    }
}
