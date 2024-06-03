using Mango.Services.EmailApi.Messaging;
using System.Runtime.CompilerServices;

namespace Mango.Services.EmailApi.Extensions
{
    public static class AplicationBuilderExtensions
    {
        private static IAzureServiceBusConsumer AzureServiceBusConsumer { get; set; }
        public static IApplicationBuilder UseAzureServiceBusConsumer(this IApplicationBuilder app)
        {
            AzureServiceBusConsumer = app.ApplicationServices.GetService<IAzureServiceBusConsumer>();
            var hostApplicationLifetime = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            hostApplicationLifetime.ApplicationStarted.Register(OnStart);
            hostApplicationLifetime.ApplicationStopped.Register(OnStop);

            return app;
        }

        private static void OnStart()
        {
            AzureServiceBusConsumer.Start();
        }

        private static void OnStop()
        {
            AzureServiceBusConsumer.Stop();
        }
    }
}
