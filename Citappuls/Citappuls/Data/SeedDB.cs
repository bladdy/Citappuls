using Citappuls.Data.Entities;
using Citappuls.Enums;
using Citappuls.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Citappuls.Data
{
    public class SeedDB
    {
        public readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDB(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }
        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckHealthInsuranceAsync();
            await CheckContriesAsync();
            await CheckSpecialitiesAsync();
            await CheckDoctorsAsync();
            await CheckHospitalsAsync();
            await CheckRolesAsync();
            await CheckUserAsync("223 0127612 1", "Bladimir", "Almanzar", "bladdy@yopmail.com",
                "809 605 1571", "Calle Luna Calle Sol", UserType.Admin);
            await CheckUserAsync("223 0127612 1", "Monica", "Mata", "monica@yopmail.com",
               "809 605 1571", "Calle Luna Calle Sol", UserType.User);
            await CheckUserAsync("223 0127612 1", "Ines", "Almanzar", "ines@yopmail.com",
                "809 605 1571", "Calle Luna Calle Sol", UserType.Doctor);
            await CheckUserAsync("223 0127612 1", "Pablo", "Almanzar", "pablo@yopmail.com",
                "809 605 1571", "Calle Luna Calle Sol", UserType.Pacient);
        }

        private async Task CheckDoctorsAsync()
        {
            if (!_context.Doctors.Any())
            {
                await AddDoctorsAsync("Doctor Dummy", "Dumy Dummy", "000-000-0000", "Calle Luna Calle Sol", new List<string>() { "Pediatria", "Ginecobstetra" },
                    new List<string>() { });
            }
        }

        private async Task AddDoctorsAsync(string name, string lastName, string phone, string adrress,
            List<string> specialities, List<string> hospitals)
        {
            Doctor doctor = new()
            {
                Address = adrress,

                Name = name,
                LastName = lastName,
                Phone = phone,
                City = _context.Cities.FirstOrDefault(),
                HospitalDoctors = new List<HospitalDoctor>(),
                SpecialityDoctor = new List<SpecialityDoctor>()
            };

            foreach (string? speciality in specialities)
            {
                doctor.SpecialityDoctor.Add(new SpecialityDoctor
                { Speciality = await _context.Specialties.FirstOrDefaultAsync(c => c.Name == speciality) });
            }

            foreach (string? hospital in hospitals)
            {

                doctor.HospitalDoctors.Add(new HospitalDoctor
                { Hospital = await _context.Hospitals.FirstOrDefaultAsync(c => c.Name == hospital) });
            }

            _context.Doctors.Add(doctor);
        }

        private async Task CheckHospitalsAsync()
        {
            if (!_context.Hospitals.Any())
            {
                await AddHospitalsAsync("Hospital Dummy", "000-000-0000", "Calle Luna Calle Sol", new List<string>() { "Pediatria", "Ginecobstetra" }, new List<string>() { });
            }
        }

        private async Task AddHospitalsAsync(string name, string phone, string adrress,
            List<string> specialities, List<string> doctors)
        {
            Hospital hospital = new()
            {
                Address = adrress,
                Name = name,
                Phone = phone,
                City = _context.Cities.FirstOrDefault(),
                HospitalDoctors = new List<HospitalDoctor>(),
                HospitalSpecialities = new List<HospitalSpeciality>()
            };

            foreach (string? speciality in specialities)
            {
                hospital.HospitalSpecialities.Add(new HospitalSpeciality { Speciality = await _context.Specialties.FirstOrDefaultAsync(c => c.Name == speciality) });
            }


            foreach (string? doctor in doctors)
            {

                hospital.HospitalDoctors.Add(new HospitalDoctor { Doctor = await _context.Doctors.FirstOrDefaultAsync(c => c.Name == doctor) });
            }

            _context.Hospitals.Add(hospital);
        }


        private async Task<User> CheckUserAsync(string document, string firstName, string lastName, string email,
        string phone, string address, UserType userType)
        {
            User user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    City = _context.Cities.FirstOrDefault(),
                    UserType = userType
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());

                /*string token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);*/

            }

            return user;
        }

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
            await _userHelper.CheckRoleAsync(UserType.Doctor.ToString());
            await _userHelper.CheckRoleAsync(UserType.Pacient.ToString());
        }
        private async Task CheckSpecialitiesAsync()
        {
            if (!_context.Specialties.Any())
            {
                _context.Specialties.Add(new Speciality { Name = "Pediatria" });
                _context.Specialties.Add(new Speciality { Name = "Ortopeda" });
                _context.Specialties.Add(new Speciality { Name = "Ginecobstetra" });
                _context.Specialties.Add(new Speciality { Name = "Medicina General" });
                _context.Specialties.Add(new Speciality { Name = "Nutriciónista" });
                _context.Specialties.Add(new Speciality { Name = "Cardiologia" });
                _context.Specialties.Add(new Speciality { Name = "Matologia" });
                _context.Specialties.Add(new Speciality { Name = "Oncologia" });
                _context.Specialties.Add(new Speciality { Name = "Psicologo" });

                await _context.SaveChangesAsync();
            }
        }
        private async Task CheckHealthInsuranceAsync()
        {
            if (!_context.Specialties.Any())
            {
                _context.HealthInsurances.Add(new HealthInsurance { Name = "N/A", Percentage = 0.0 });
                _context.HealthInsurances.Add(new HealthInsurance { Name = "Humano Seguros", Percentage = 0.0 });
                _context.HealthInsurances.Add(new HealthInsurance { Name = "Palic", Percentage = 0.0 });
                _context.HealthInsurances.Add(new HealthInsurance { Name = "MAPFRE Salud ARS", Percentage = 0.0 });
                _context.HealthInsurances.Add(new HealthInsurance { Name = "ARS SEMMA", Percentage = 0.0 });
                _context.HealthInsurances.Add(new HealthInsurance { Name = "FUTURO ARS", Percentage = 0.0 });
                _context.HealthInsurances.Add(new HealthInsurance { Name = "ARS Simag", Percentage = 0.0 });
                _context.HealthInsurances.Add(new HealthInsurance { Name = "ARS Renacer", Percentage = 0.0 });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckContriesAsync()
        {
            if (!_context.Countries.Any())
            {
                _context.Countries.Add(new Country
                {
                    Name = "Republica Dominicana",
                    States = new List<State>()
                    {
                        new State() {
                            Name ="Santo Domingo",
                            Cities = new List<City>()
                            {
                                new City {Name = "Santo Domingo Este"},
                                new City {Name = "Santo Domingo Norte"},
                                new City {Name = "Santo Domingo Oeste"},
                                new City {Name = "Distito Nacional"}
                            }
                        },
                        new State() {
                            Name ="Santiago",
                            Cities = new List<City>()
                            {
                                new City {Name = "Cien fuegos"},
                                new City {Name = "Licey"}
                            }
                        }
                    }
                });
                _context.Countries.Add(new Country
                {
                    Name = "Estados Unidos",
                    States = new List<State>()
                    {
                        new State() {
                            Name = "Florida",
                            Cities = new List<City>()
                            {
                                new City() { Name = "Orlando" },
                                new City() { Name = "Miami" },
                                new City() { Name = "Tampa" },
                                new City() { Name = "Fort Lauderdale" },
                                new City() { Name = "Key West" },
                            }
                        },
                        new State() {
                            Name = "Texas",
                            Cities = new List<City>() {
                                new City() { Name = "Houston" },
                                new City() { Name = "San Antonio" },
                                new City() { Name = "Dallas" },
                                new City() { Name = "Austin" },
                                new City() { Name = "El Paso" },
                            }
                        },
                        new State() {
                            Name = "California",
                            Cities = new List<City>() {
                                new City() { Name = "Los Angeles" },
                                new City() { Name = "San Francisco" },
                                new City() { Name = "San Diego" },
                                new City() { Name = "San Bruno" },
                                new City() { Name = "Sacramento" },
                                new City() { Name = "Fresno" },
                            }
                        },
                    }
                });

                await _context.SaveChangesAsync();
            }
        }
    }
}
