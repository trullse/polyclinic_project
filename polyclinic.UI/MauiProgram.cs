using CommunityToolkit.Maui;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using polyclinic.Application.Abstractions;
using polyclinic.Application.Services;
using polyclinic.Domain.Abstractions;
using polyclinic.Domain.Entities;
using polyclinic.Persistence.Data;
using polyclinic.Persistence.Repository;
using polyclinic.UI.ViewModels;
using polyclinic.UI.Views;
using System.Reflection;

namespace polyclinic.UI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            string settingsStream = "polyclinic.UI.appsettings.json";

            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            var a = Assembly.GetExecutingAssembly();
            using var stream = a.GetManifestResourceStream(settingsStream);
            builder.Configuration.AddJsonStream(stream);

            AddDbContext(builder);
            SetupServices(builder.Services);
            //SeedData(builder.Services);

#if DEBUG
		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        private static void SetupServices(IServiceCollection services)
        {
            // Services
            services.AddSingleton<IUnitOfWork, EfUnitOfWork>();
            services.AddSingleton<IAppointmentService, AppointmentService>();
            services.AddSingleton<IClientService, ClientService>();
            services.AddSingleton<IDoctorService, DoctorService>();

            // Views
            services.AddTransient<AppointmentsView>();
            services.AddTransient<AppointmentDetailsView>();
            services.AddTransient<AddClientView>();
            services.AddTransient<AddAppointmentView>();
            services.AddTransient<EditAppointmentView>();

            // ViewModels
            services.AddSingleton<AppointmentsViewModel>();
            services.AddTransient<AppointmentDetailsViewModel>();
            services.AddTransient<AddClientViewModel>();
            services.AddTransient<AddAppointmentViewModel>();
            services.AddTransient<EditAppointmentViewModel>();
        }

        private static void AddDbContext(MauiAppBuilder builder)
        {
            var connStr = builder.Configuration.GetConnectionString("SqliteConnection");
            string dataDirectory = String.Empty;
            dataDirectory = FileSystem.AppDataDirectory + "/";
            connStr = String.Format(connStr, dataDirectory);
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(connStr)
                .Options;
            builder.Services.AddSingleton<AppDbContext>((s) => 
            new AppDbContext(options));
        }

        public async static void SeedData(IServiceCollection services)
        {
            using var provider = services.BuildServiceProvider();
            var unitOfWork = provider.GetService<IUnitOfWork>();
            await unitOfWork.RemoveDatbaseAsync();
            await unitOfWork.CreateDatabaseAsync();
            // Add clients
            IReadOnlyList<Client> clients = new List<Client>()
            {
                new Client()
                {
                    Id=1, Name="Alex", Surname="Leon", BirthDate=DateTime.Now.AddYears(-18)
                },
                new Client()
                {
                    Id=2, Name="John", Surname="Smith", BirthDate=DateTime.Now.AddYears(-25)
                }
            };
            foreach (var client in clients)
                await unitOfWork.ClientRepository.AddAsync(client);
            await unitOfWork.SaveAllAsync();
            //Add doctors
            IReadOnlyList<Doctor> doctors = new List<Doctor>()
            {
                new Doctor()
                {
                    Name="Tattiana", Surname="Semchenko", Specialization="Pediatr", Qualification="First"
                },
                new Doctor()
                {
                    Name="Mike", Surname="Smith", Specialization="Surgeon", Qualification="Second"
                }
            };
            foreach (var doctor in doctors)
                await unitOfWork.DoctorRepository.AddAsync(doctor);
            await unitOfWork.SaveAllAsync();
            //Add appointments
            Random rand = new Random();
            int k = 1;
            foreach (var client in clients)
                for (int j = 0; j < 10; j++)
                    await unitOfWork.AppointmentRepository.AddAsync(new Appointment()
                    {
                        Id = k,
                        Diagnosis = $"Diagnosis {k++}",
                        ClientId = client.Id,
                        DoctorId = 1,
                        AppointmentDate = DateTime.Now,//.AddDays(rand.NextInt64() % 60 - 30),
                        TreatmentCost = rand.NextDouble() * 10,
                        AppointmentStatus = (Appointment.Status)(rand.NextInt64() % 5)
                    });
            await unitOfWork.SaveAllAsync();
        }
    }
}