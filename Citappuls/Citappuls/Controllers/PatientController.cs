using Citappuls.Data;
using Citappuls.Data.Entities;
using Citappuls.Enums;
using Citappuls.Helpers;
using Citappuls.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Citappuls.Controllers
{
    [Authorize(Roles = "User, Admin")]
    public class PatientController : Controller
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;
        private readonly IUserHelper _userHelper;
        public PatientController(DataContext context, ICombosHelper combosHelper, IUserHelper userHelper)
        {
            _userHelper = userHelper;
            _context = context;
            _combosHelper = combosHelper;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Patients
                .Include(u => u.SexType)
                .Include(u => u.MaritalStatus)
                .Include(u => u.City)
                .ThenInclude(c => c.State)
                .ThenInclude(s => s.Country)
                .Include(hs => hs.HealthInsurance)
                .ToListAsync());
        }
        public async Task<IActionResult> CreateAppoitment(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Patient patient = await _context.Patients.FindAsync(id);
            AddAppoitmentViewModel model = new()
            {
                Patient = patient,
                Doctors = await _combosHelper.GetComboDoctorAsync(),
                Hospitals = await _combosHelper.GetComboHospitalsAsync(),
                Specialities = await _combosHelper.GetComboSpecialitesAsync(),
                Date = DateTime.Now,
                Subsequent= false,
                 
            };

            if (patient == null)
            {
                return NotFound();
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAppoitment(int? id, AddAppoitmentViewModel model)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                User user = await _userHelper.GetUserAsync(User.Identity.Name);
                Appointment appointment = new()
                {
                    Time = model.Time,
                    Date = model.Date,
                    AppointmentDate = DateTime.Now,
                    Subsequent = model.Subsequent,
                    Remarks = model.Nota,
                    User = user,
                };
                appointment.StatusType = StatusType.Nueva;
                appointment.Patient = await _context.Patients.FindAsync(id);
                appointment.Doctor = await _context.Doctors.FindAsync(model.DoctorId);
                appointment.Hospital = await _context.Hospitals.FindAsync(model.HospitalsId);
                appointment.Speciality = await _context.Specialties.FindAsync(model.SpecialityId);
                try
                {
                    _context.Add(appointment);
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
            model.Hospitals = await _combosHelper.GetComboHospitalsAsync();
            return View(model);

        }
        public async Task<IActionResult> Create()
        {
            AddPatientViewModel model = new()
            {
                HealthInsurances = await _combosHelper.GetComboHealthInsuranceAsync(),
                Countries = await _combosHelper.GetComboCountriesAsync(),
                SexTypes = await _combosHelper.GetComboSexTypeAsync(),
                MaritalStatusType = await _combosHelper.GetComboMaritalStatusAsync(),
                States = await _combosHelper.GetComboStatesAsync(0),
                Cities = await _combosHelper.GetComboCitiesAsync(0),
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddPatientViewModel model)
        {
            if (ModelState.IsValid)
            {
                Guid imageId = Guid.Empty;
                
                Patient patient = new()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = model.Address,
                    PhoneNumber = model.Phone,
                    Document = model.Document,
                    Nationality = model.Nationality,
                    DateofBirth = model.DateofBirth,
                    HealthInsuranceNumber = model.HealthInsuranceNumber
                };
                patient.City = await _context.Cities.FindAsync(model.CityId);
                patient.SexType = await _context.SexTypes.FindAsync(model.SexTypeId);
                patient.MaritalStatus = await _context.MaritalStatusTypes.FindAsync(model.MaritalStatusTypeId);
                patient.HealthInsurance = await _context.HealthInsurances.FindAsync(model.HealthInsurancesId);
                try
                {
                    _context.Add(patient);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {

                    throw;
                }
            }
            model.HealthInsurances = await _combosHelper.GetComboHealthInsuranceAsync();
            model.Countries = await _combosHelper.GetComboCountriesAsync();
            model.SexTypes = await _combosHelper.GetComboSexTypeAsync();
            model.MaritalStatusType = await _combosHelper.GetComboMaritalStatusAsync();
            model.States = await _combosHelper.GetComboStatesAsync(0);
            model.Cities = await _combosHelper.GetComboCitiesAsync(0);
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Patient patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            AddPatientViewModel model = new()
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                DateofBirth = patient.DateofBirth,
                Document = patient.Document,
                Phone = patient.PhoneNumber,
                SexTypes = await _combosHelper.GetComboSexTypeAsync(),
                MaritalStatusType = await _combosHelper.GetComboMaritalStatusAsync(),
                HealthInsuranceNumber = patient.HealthInsuranceNumber,
                HealthInsurances = await _combosHelper.GetComboHealthInsuranceAsync(),
                Nationality = patient.Nationality,
                Cities = await _combosHelper.GetComboCitiesAsync(0),
                States = await _combosHelper.GetComboStatesAsync(0),
                Countries = await _combosHelper.GetComboCountriesAsync(),
                Address = patient.Address,

            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AddPatientViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            try
            {
                Patient patient = await _context.Patients.FindAsync(model.Id);
                patient.PhoneNumber = model.Phone;
                patient.LastName = model.LastName;
                patient.Address = model.Address;
                patient.DateofBirth = model.DateofBirth;
                patient.Document = model.Document;
                patient.Nationality = model.Nationality;
                patient.HealthInsuranceNumber = model.HealthInsuranceNumber;
                patient.City = await _context.Cities.FindAsync(model.CityId);
                patient.SexType = await _context.SexTypes.FindAsync(model.SexTypeId);
                patient.MaritalStatus = await _context.MaritalStatusTypes.FindAsync(model.MaritalStatusTypeId);
                patient.HealthInsurance = await _context.HealthInsurances.FindAsync(model.HealthInsurancesId);

                _context.Update(patient);
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
        public JsonResult GetDoctors(int id)
        {
            Doctor doctor = _context.Doctors
                .Include(d => d.SpecialityDoctor)
                .ThenInclude(d=> d.Speciality)
                .FirstOrDefault(d => d.Id == id);
            if (doctor == null)
            {
                return null;
            }
            return Json(doctor.SpecialityDoctor.OrderBy(d => d.Doctor.FullName));
        }
    }
}
