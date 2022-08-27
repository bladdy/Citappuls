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
            await CheckContriesAsync();
            await CheckSpecialitiesAsync();
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
