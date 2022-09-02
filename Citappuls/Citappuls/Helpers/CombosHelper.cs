using Citappuls.Data;
using Citappuls.Data.Entities;
using Citappuls.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Citappuls.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _context;

        public CombosHelper(DataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<SelectListItem>> GetComboCitiesAsync(int stateId)
        {
            List<SelectListItem> list = await _context.Cities
                .Where(x => x.State.Id == stateId)
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = $"{x.Id}"
                })
                .OrderBy(x => x.Text)
                .ToListAsync();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione una ciudad...]",
                Value = "0"
            });

            return list;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboCountriesAsync()
        {
            List<SelectListItem> list = await _context.Countries.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = $"{x.Id}"
            })
                .OrderBy(x => x.Text)
                .ToListAsync();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un país...]",
                Value = "0"
            });

            return list;

        }

        public async Task<IEnumerable<SelectListItem>> GetComboDoctorAsync()
        {
            List<SelectListItem> list = await _context.Doctors.Select(c => new SelectListItem
            {
                Text = c.Name.ToUpper() +" "+c.LastName.ToUpper(),
                Value = $"{c.Id}"

            })
               .OrderBy(c => c.Text)
               .ToListAsync();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione una Doctor...]",
                Value = "0"
            });

            return list;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboDoctorAsync(IEnumerable<Doctor> filter)
        {
            List<Doctor> categories = await _context.Doctors.ToListAsync();
            List<Doctor> categoriesFiltered = new();
            foreach (Doctor category in categories)
            {
                if (!filter.Any(c => c.Id == category.Id))
                {
                    categoriesFiltered.Add(category);
                }
            }
            List<SelectListItem> list = categoriesFiltered.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = $"{c.Id}"

            })
                .OrderBy(c => c.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione una Docotor...]",
                Value = "0"
            });

            return list;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboHospitalsAsync()
        {
            List<SelectListItem> list = await _context.Hospitals.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = $"{c.Id}"

            })
                .OrderBy(c => c.Text)
                .ToListAsync();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione una Hospitales...]",
                Value = "0"
            });

            return list;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboHospitalsAsync(IEnumerable<Doctor> filter)
        {
            List<Hospital> categories = await _context.Hospitals.ToListAsync();
            List<Hospital> categoriesFiltered = new();
            foreach (Hospital category in categories)
            {
                if (!filter.Any(c => c.Id == category.Id))
                {
                    categoriesFiltered.Add(category);
                }
            }
            List<SelectListItem> list = categoriesFiltered.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = $"{c.Id}"

            })
                .OrderBy(c => c.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione una Docotor...]",
                Value = "0"
            });

            return list;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboSpecialitesAsync()
        {
            List<SelectListItem> list = await _context.Specialties.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = $"{c.Id}"

            })
                .OrderBy(c => c.Text)
                .ToListAsync();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione una Especialidad...]",
                Value = "0"
            });

            return list;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboSpecialitesAsync(IEnumerable<Speciality> filter)
        {
            List<Speciality> categories = await _context.Specialties.ToListAsync();
            List<Speciality> categoriesFiltered = new();
            foreach (Speciality category in categories)
            {
                if (!filter.Any(c => c.Id == category.Id))
                {
                    categoriesFiltered.Add(category);
                }
            }
            List<SelectListItem> list = categoriesFiltered.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = $"{c.Id}"

            })
                .OrderBy(c => c.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione una categoría...]",
                Value = "0"
            });

            return list;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboStatesAsync(int countryId)
        {
            List<SelectListItem> list = await _context.States
               .Where(x => x.Country.Id == countryId)
               .Select(x => new SelectListItem
               {
                   Text = x.Name,
                   Value = $"{x.Id}"
               })
               .OrderBy(x => x.Text)
               .ToListAsync();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un estado/provincia...]",
                Value = "0"
            });

            return list;

        }
        //TODO: Que acrguen los tipos de usarios

        public async Task<IEnumerable<SelectListItem>> GetComboUsersAsync()
        {
            SelectListItem selListItem = new SelectListItem() { Value = "0", Text = "Admin" };
            SelectListItem selListItems = new SelectListItem() { Value = "1", Text = "User" };
            SelectListItem selListItemsc = new SelectListItem() { Value = "2", Text = "Pacient" };
            SelectListItem selListItemsd = new SelectListItem() { Value = "3", Text = "Doctor" };

            //Create a list of select list items - this will be returned as your select list
            List<SelectListItem> list = new List<SelectListItem>();

            //Add select list item to list of selectlistitems
            list.Add(selListItem);
            list.Add(selListItems);
            list.Add(selListItemsc);
            list.Add(selListItemsd);
            return list;
        }
    }
    
}
