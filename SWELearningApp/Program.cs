using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SWELearningApp;

namespace SWELearning 
{ 
    public static class Program
    {
        public static void Main(string[] args)
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

            builder.Services.Register();

            IHost host = builder.Build();

            ITaggingApp app = host.Services.GetService<ITaggingApp>();

            app.Run();
        }

        public static void Register(this IServiceCollection services)
        {
            services.AddSingleton<ITaggingApp, TaggingApp>();
        }
    }
}

