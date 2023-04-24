using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using polyclinic.Application.Abstractions;
using polyclinic.Application.Services;
using polyclinic.Domain.Abstractions;
using polyclinic.Persistence.Repository;

namespace polyclinic.UI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            SetupServices(builder.Services);

#if DEBUG
		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        private static void SetupServices(IServiceCollection services)
        {
            // Services
            services.AddSingleton<IUnitOfWork, FakeUnitOfWork>();
            services.AddSingleton<IAppointmentService, AppointmentService>();
            services.AddSingleton<IClientService, ClientService>();
            services.AddSingleton<IDoctorService, DoctorService>();

            // Views

            // ViewModels
            services.AddSingleton<AppointmentsViewModel>();
        }
    }
}