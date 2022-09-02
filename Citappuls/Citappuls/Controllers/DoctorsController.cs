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
    public class DoctorsController : Controller
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;
        public DoctorsController(DataContext context, ICombosHelper combosHelper)
        {
            _context = context;
            _combosHelper = combosHelper;

        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Doctors
                .Include(h => h.HospitalDoctors)
                .Include(h => h.SpecialityDoctor)
                .ThenInclude(hs => hs.Speciality)
                .Include(u => u.City)
                .ThenInclude(c => c.State)
                .ThenInclude(s => s.Country)
                .ToListAsync());
        }
        public async Task<IActionResult> Create()
        {
            AddDoctorViewModel model = new()
            {
                Specialities = await _combosHelper.GetComboSpecialitesAsync(),
                Countries = await _combosHelper.GetComboCountriesAsync(),
                Hospitals = await _combosHelper.GetComboHospitalsAsync(),
                States = await _combosHelper.GetComboStatesAsync(0),
                Cities = await _combosHelper.GetComboCitiesAsync(0),
                //DoctorId = 1

            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(AddDoctorViewModel model)
        {
            if (ModelState.IsValid)
            {
                Guid imageId = Guid.Empty;
                /*
                if (model.ImageFile != null)
                {
                    imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "users");
                }*/
                Doctor doctor = new()
                {
                    Name = model.Name,
                    LastName = model.LastName,
                    Address = model.Address,
                    Phone = model.Phone,

                };

                doctor.SpecialityDoctor = new List<SpecialityDoctor>()
                {
                    new SpecialityDoctor
                    {
                        Speciality = await _context.Specialties.FindAsync(model.SpecialityId) ,
                    }
                };
                doctor.HospitalDoctors = new List<HospitalDoctor>()
                {
                    new HospitalDoctor
                    {                                           //model.HospitalsId
                        Hospital = await _context.Hospitals.FindAsync(model.HospitalsId) ,
                    }
                };
                doctor.City = await _context.Cities.FindAsync(model.CityId);
                try
                {
                    _context.Add(doctor);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {

                    throw;
                }
            }
            model.Specialities = await _combosHelper.GetComboSpecialitesAsync();
            model.Hospitals = await _combosHelper.GetComboDoctorAsync();
            model.Countries = await _combosHelper.GetComboHospitalsAsync();
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
