using LT_PhotoAlbum.Abstractions;
using LT_PhotoAlbum.Arguments;
using LT_PhotoAlbum.Data;
using LT_PhotoAlbum.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace LT_PhotoAlbum
{
    public class StartUp
    {
        private static IConfiguration _configuration;
        public static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();
            _configuration = LoadConfiguration();
            ConfigureServices(services);

            return services;
        }

        private static IConfiguration LoadConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json")
                .Build();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_configuration);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSingleton<IConsoleWrapper, ConsoleWrapper>();
            services.AddHttpClient("LT", c => c.BaseAddress = new Uri(_configuration.GetSection("BaseUrl").Value));

            services.AddScoped<IAlbumPhotoGet, AlbumPhotoGet>();

            services.AddScoped<IConsoleWriter<PhotoArgument>, PhotoConsoleWriter>();
            services.AddScoped<IConsoleWriter<AlbumArgument>, AlbumConsoleWriter>();
        }
    }
}
