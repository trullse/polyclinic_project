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
using SkiaSharp.Views.Maui.Controls.Hosting;
using System;

namespace polyclinic.UI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            string settingsStream = "polyclinic.UI.appsettings.json";

            var builder = MauiApp.CreateBuilder();
            builder
                .UseSkiaSharp(true)
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
            services.AddSingleton<IShiftService, ShiftService>();
            services.AddSingleton<IStatisticsService, StatisticsService>();

            // Views
            services.AddTransient<AppointmentsView>();
            services.AddTransient<AppointmentDetailsView>();
            services.AddTransient<AddClientView>();
            services.AddTransient<AddAppointmentView>();
            services.AddTransient<EditAppointmentView>();
            services.AddTransient<ClientSelectView>();
            services.AddTransient<SubmitAppointmentView>();
            services.AddTransient<ClientsView>();
            services.AddTransient<DoctorsView>();
            services.AddTransient<StatisticsView>();
            services.AddTransient<ClientDetailsView>();
            services.AddTransient<DoctorDetailsView>();
            services.AddTransient<AddDoctorView>();
            services.AddTransient<AddShiftView>();

            // ViewModels
            services.AddSingleton<AppointmentsViewModel>();
            services.AddTransient<AppointmentDetailsViewModel>();
            services.AddTransient<AddClientViewModel>();
            services.AddTransient<AddAppointmentViewModel>();
            services.AddTransient<EditAppointmentViewModel>();
            services.AddTransient<ClientSelectViewModel>();
            services.AddTransient<SubmitAppointmentViewModel>();
            services.AddTransient<ClientsViewModel>();
            services.AddTransient<DoctorsViewModel>();
            services.AddTransient<StatisticsViewModel>();
            services.AddTransient<ClientDetailsViewModel>();
            services.AddTransient<DoctorDetailsViewModel>();
            services.AddTransient<AddDoctorViewModel>();
            services.AddTransient<AddShiftViewModel>();
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
            var appointmentService = provider.GetService<IAppointmentService>();
            var shiftService = provider.GetService<IShiftService>();
            await unitOfWork.RemoveDatbaseAsync();
            await unitOfWork.CreateDatabaseAsync();
            // Add clients
            IReadOnlyList<Client> clients = new List<Client>()
            {
                new Client()
                {
                    Name="Aleksei", Surname="Leonenko", BirthDate=new DateTime(2003, 11, 26)
                },
                new Client()
                {
                    Name="Alyona", Surname="Makarenko", BirthDate=DateTime.Now.AddYears(-19)
                },
                new Client()
                {
                    Name="Ulyana", Surname="Sidorova", BirthDate=DateTime.Now.AddYears(-18)
                },
                new Client()
                {
                    Name="Lyubov", Surname="Berdnik", BirthDate=DateTime.Now.AddYears(-19)
                },
                new Client()
                {
                    Name="Maxim", Surname="Konovalyuk", BirthDate=DateTime.Now.AddYears(-19)
                },
                new Client()
                {
                    Name="Aleksandr", Surname="Bogdanov", BirthDate=DateTime.Now.AddYears(-18)
                },
                new Client()
                {
                    Name="Evgeny", Surname="Mileyko", BirthDate=DateTime.Now.AddYears(-19)
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
                    Name="Tattiana", Surname="Semchenko", Specialization="Therapist", Qualification="First"
                },
                new Doctor()
                {
                    Name="Tattiana", Surname="Kirienko", Specialization="Pediatrician", Qualification="Second"
                },
                new Doctor()
                {
                    Name="Ekaterina", Surname="Stebunova", Specialization="Surgeon", Qualification="First"
                },
                new Doctor()
                {
                    Name="Valeria", Surname="Pushina", Specialization="Otorhinolaryngologist", Qualification="First"
                }
            };
            foreach (var doctor in doctors)
                await unitOfWork.DoctorRepository.AddAsync(doctor);
            await unitOfWork.SaveAllAsync();
            //Add shifts
            /*var doctors2 = await unitOfWork.DoctorRepository.ListAllAsync();
            foreach(var doctor in doctors)
            {
                for (int i = 0; i < 3; i++)
                    await shiftService.AddOnInterval(doctor, DateTime.Today.AddMonths(-i).AddDays(-10), DateTime.Today.AddMonths(-2).AddDays(-9), true);
            }*/

            /*IReadOnlyList<Shift> shifts = new List<Shift>()
            {
                new Shift()
                {
                    DoctorId = 1,
                    Date = DateOnly.FromDateTime(DateTime.Today),
                    Type = Shift.ShiftType.First
                },
                new Shift()
                {
                    DoctorId = 1,
                    Date = DateOnly.FromDateTime(DateTime.Today.AddDays(1)),
                    Type = Shift.ShiftType.Second
                },
                new Shift()
                {
                    DoctorId = 2,
                    Date = DateOnly.FromDateTime(DateTime.Today),
                    Type = Shift.ShiftType.Second
                },
                new Shift()
                {
                    DoctorId = 2,
                    Date = DateOnly.FromDateTime(DateTime.Today.AddDays(1)),
                    Type = Shift.ShiftType.First
                }
            };
            foreach (var shift in shifts)
                await shiftService.AddAsync(shift);
            await unitOfWork.SaveAllAsync();*/
            //Add talons

            //Add appointments

            /*var shifts = await shiftService.GetAllAsync();
            Random random = new Random();
            int k = 0;
            foreach (var shift in shifts)
            {
                for (int i = 0; i < 3; i++)
                {
                    await appointmentService.AddWithTalonAsync(new Appointment()
                    {
                        Id = k++,
                        Diagnosis = "Everything is fine",
                        ClientId = random.Next() % 7,
                        DoctorId = shift.DoctorId,
                        AppointmentDate = shift.Date.ToDateTime(shift.Talons[i].AppointmentTime),
                        TreatmentCost = random.Next() % 100,
                        AppointmentStatus = Appointment.Status.Ended
                    }, shift.Talons[i]);
                }
            }*/

            /*Random rand = new Random();
            int k = 1;
            foreach (var client in clients)
                for (int j = 0; j < 3; j++)
                    await appointmentService.AddWithTalonAsync(new Appointment()
                    {
                        Id = k,
                        Diagnosis = $"Diagnosis {k++}",
                        ClientId = client.Id,
                        DoctorId = 1,
                        AppointmentDate = DateTime.Today,//Now.AddMinutes(rand.NextInt64() % 1800),//.AddDays(rand.NextInt64() % 60 - 30),
                        TreatmentCost = rand.NextDouble() * 10,
                        AppointmentStatus = (Appointment.Status)(rand.NextInt64() % 5)
                    }, shiftService.GetByDoctorAndDayAsync(1, DateOnly.FromDateTime(DateTime.Today)).Result.Talons[k]);
            await unitOfWork.SaveAllAsync();*/

            /*int k = 1;
            Random rand = new Random();
            foreach (var doctor in doctors)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (var date = DateTime.Today.AddMonths(-i).AddDays(-10); date < DateTime.Today.AddMonths(-i).AddDays(-1); date = date.AddDays(1))
                    {
                        for (var j = 0; j < 3; j++)
                        {
                            await appointmentService.AddAsync(new Appointment
                            {
                                Id = k++,
                                Diagnosis = "Everything is fine",
                                ClientId = rand.Next() % 7,
                                DoctorId = doctor.Id,
                                AppointmentDate = date.AddHours(9.5).AddMinutes(15 * j),
                                TreatmentCost = rand.Next() % 100,
                                AppointmentStatus = Appointment.Status.Ended
                            });
                        }
                    }
                }
            }*/
        }
    }
}