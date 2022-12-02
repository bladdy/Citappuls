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
                .Include(u => u.City)
                .ThenInclude(c => c.State)
                .ThenInclude(s => s.Country)
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
            }
            model.Specialities = await _combosHelper.GetComboSpecialitesAsync();
            model.Doctors = await _combosHelper.GetComboDoctorAsync();
            model.Countries = await _combosHelper.GetComboCountriesAsync();
            model.States = await _combosHelper.GetComboStatesAsync(model.CountryId);
            model.Cities = await _combosHelper.GetComboCitiesAsync(model.StateId);
            return View(model);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Hospital hospital = await _context.Hospitals.FindAsync(id);
            if (hospital == null)
            {
                return NotFound();
            }

            EditeHospitalViewModel model = new()
            {
                Id = hospital.Id,
                Name = hospital.Name,
                Address = hospital.Address,
                Phone = hospital.Phone,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateHospitalViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            try
            {
                Hospital hospital = await _context.Hospitals.FindAsync(model.Id);
                hospital.Phone = model.Phone;
                hospital.Name = model.Name;
                hospital.Address = model.Address;
                hospital.City = await _context.Cities.FindAsync(model.CityId);
                _context.Update(hospital);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                {
                    ModelState.AddModelError(string.Empty, "Ya existe un Hospital con el mismo nombre.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                }
            }
            catch (Exception exception)
            {
                ModelState.AddModelError(string.Empty, exception.Message);
            }

            return View(model);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Hospital hospital = await _context.Hospitals
                .Include(h => h.HospitalDoctors)
                .ThenInclude(hd => hd.Doctor)
                .Include(h => h.HospitalSpecialities)
                .ThenInclude(hs => hs.Speciality)
                .Include(u => u.City)
                .FirstOrDefaultAsync(hs => hs.Id == id);
            if (hospital == null)
            {
                return NotFound();
            }

            return View(hospital);
        }
        public async Task<IActionResult> AddSpeciality(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Hospital hospital = await _context.Hospitals.FindAsync(id);
            if (hospital == null)
            {
                return NotFound();
            }

            AddHospitalSpecialitiesViewModel model = new()
            {
                HospitalId = hospital.Id,
                Specialities = await _combosHelper.GetComboSpecialitesAsync(),
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSpeciality(AddHospitalSpecialitiesViewModel model)
        {
            if (ModelState.IsValid)
            {
                Hospital hospital = await _context.Hospitals.FindAsync(model.HospitalId);
                HospitalSpeciality hospitalSpeciality = new()
                {
                    Speciality = await _context.Specialties.FindAsync(model.SpecialityId),
                    Hospital = hospital,
                };

                try
                {
                    _context.Add(hospitalSpeciality);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Details), new { Id = hospital.Id });
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe una especialidad con el mismo nombre en este Hospital.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            model.Specialities = await _combosHelper.GetComboSpecialitesAsync();
            return View(model);
        }
        //Doctores

        public async Task<IActionResult> AddDoctor(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Hospital hospital = await _context.Hospitals.FindAsync(id);
            if (hospital == null)
            {
                return NotFound();
            }

            AddHospitalDoctorsViewModel model = new()
            {
                HospitalId = hospital.Id,
                Doctors = await _combosHelper.GetComboDoctorAsync(),
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDoctor(AddHospitalDoctorsViewModel model)
        {
            if (ModelState.IsValid)
            {
                Hospital hospital = await _context.Hospitals.FindAsync(model.HospitalId);
                HospitalDoctor hospitalDoctor = new()
                {
                    Doctor = await _context.Doctors.FindAsync(model.DoctorId),
                    Hospital = hospital,
                };

                try
                {
                    _context.Add(hospitalDoctor);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Details), new { Id = hospital.Id });
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un Doctor con el mismo nombre en este Hospital.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            model.Doctors = await _combosHelper.GetComboDoctorAsync();
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Hospital hospital = await _context.Hospitals
                .Include(p => p.HospitalSpecialities)
                .FirstOrDefaultAsync(p => p.Id == id);
            /*
            foreach (HospitalSpeciality hospitalSpeciality in hospital.HospitalSpecialitieses)
            {
                await _blobHelper.DeleteBlobAsync(productImage.ImageId, "products");
            }*/

            _context.Hospitals.Remove(hospital);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeleteSpeciality(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            HospitalSpeciality hospitalSpeciality = await _context.HospitalSpecialities
                .Include(hs => hs.Hospital)
                .FirstOrDefaultAsync(hs => hs.Id == id);
            if (hospitalSpeciality == null)
            {
                return NotFound();
            }

            _context.HospitalSpecialities.Remove(hospitalSpeciality);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { Id = hospitalSpeciality.Hospital.Id });
        }

        //AddHospitalDoctorsViewModel
        //AddHospitalSpecialitiesViewModel


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
