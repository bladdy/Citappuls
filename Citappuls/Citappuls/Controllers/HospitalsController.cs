using Citappuls.Data;
using Citappuls.Data.Entities;
using Citappuls.Helpers;
using Citappuls.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Citappuls.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HospitalsController : Controller
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;
        public HospitalsController(DataContext context, ICombosHelper combosHelper)
        {
            _context = context;
            _combosHelper = combosHelper;

        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Hospitals
                .Include(h => h.HospitalDoctors)
                .Include(h => h.HospitalSpecialities)
                .ThenInclude(hs => hs.Speciality)
                .ToListAsync());
        }
        public async Task<IActionResult> Create()
        {
            CreateHospitalViewModel model = new()
            {
                Specialities = await _combosHelper.GetComboSpecialitesAsync(),
                Countries = await _combosHelper.GetComboCountriesAsync(),
                States = await _combosHelper.GetComboStatesAsync(0),
                Cities = await _combosHelper.GetComboCitiesAsync(0),
                Doctors = await _combosHelper.GetComboDoctorAsync()
                //DoctorId = 1

            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateHospitalViewModel model)
        {
            if (ModelState.IsValid)
            {
                Guid imageId = Guid.Empty;
                /*
                if (model.ImageFile != null)
                {
                    imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "users");
                }*/
                Hospital hospital = new()
                {
                    Name = model.Name,
                    Address = model.Address,
                    Phone = model.Phone,

                };
                
                hospital.HospitalSpecialities = new List<HospitalSpeciality>()
                {
                    new HospitalSpeciality
                    {
                        Speciality = await _context.Specialties.FindAsync(model.SpecialityId) ,
                    }
                };
                hospital.HospitalDoctors = new List<HospitalDoctor>()
                {
                    new HospitalDoctor
                    {                                           //model.DoctorId
                        Doctor = await _context.Doctors.FindAsync(model.DoctorId) ,
                    }
                };
                hospital.City = await _context.Cities.FindAsync(model.CityId);
                try
                {
                    _context.Add(hospital);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {

                    throw;
                }
                return RedirectToAction("Index", "Users");
            }
            model.Specialities = await _combosHelper.GetComboSpecialitesAsync();
            model.Doctors = await _combosHelper.GetComboDoctorAsync();
            model.Countries = await _combosHelper.GetComboCountriesAsync();
            model.States = await _combosHelper.GetComboStatesAsync(model.CountryId);
            model.Cities = await _combosHelper.GetComboCitiesAsync(model.StateId);
            return View(model);
        }
        public JsonResult GetStates(int countryId)
        {
            Country country = _context.Countries
                .Include(c => c.States)
                .FirstOrDefault(c => c.Id == countryId);
            if (country == null)
            {
                return null;
            }
            return Json(country.States.OrderBy(d => d.Name));
        }
        public JsonResult GetCities(int stateId)
        {
            State state = _context.States
                .Include(s => s.Cities)
                .FirstOrDefault(s => s.Id == stateId);
            if (state == null)
            {
                return null;
            }
            return Json(state.Cities.OrderBy(c => c.Name));
        }

    }
}
