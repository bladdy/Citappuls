
using Citappuls.Data;
using Citappuls.Data.Entities;
using Citappuls.Helpers;
using Citappuls.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Citappuls.Enums;
using Vereyon.Web;

namespace Citappuls.Controllers
{
    [Authorize(Roles = "User,Admin")]
    //[Authorize(Roles = "Admin")]

    public class AppoitmentsController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;
        private readonly IFlashMessage _flashMessage;
        public AppoitmentsController(DataContext context, IFlashMessage flashMessage, ICombosHelper combosHelper, IUserHelper userHelper)
        {
            _userHelper = userHelper;
            _context = context;
            _combosHelper = combosHelper;
            _flashMessage = flashMessage;
        }
        public async Task<IActionResult> Index()
        {
            User user = await _userHelper.GetUserAsync(User.Identity.Name);
            return View(await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Hospital)
                .Include(a => a.Patient)
                .Include(a => a.User)
                .Include(a => a.Speciality)
                .OrderBy(a => a.Date)
                .ThenBy(a => a.Time)
                .Where(a => a.User.Id == user.Id)
                .ToListAsync());
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Appointment appointment = _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Hospital)
                .Include(a => a.Patient)
                .Include(a => a.User)
                .Include(a => a.Speciality).FirstOrDefault(a => a.Id == id);
            AddAppoitmentViewModel model = new()
            {
                Patient = await _context.Patients.FindAsync(appointment.Patient.Id),
                Doctors = await _combosHelper.GetComboDoctorAsync(),
                Hospitals = await _combosHelper.GetComboHospitalsAsync(),
                Specialities = await _combosHelper.GetComboSpecialitesAsync(),
                Time = appointment.Time,
                Date = appointment.Date,
                //AppointmentDate = DateTime.Now,
                Subsequent = appointment.Subsequent.Value,
                Nota = appointment.Remarks,
                User = await _userHelper.GetUserAsync(User.Identity.Name),
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, AddAppoitmentViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }
            try
            {
                if (ModelState.IsValid)
                {
                    Appointment appointment = _context.Appointments.Include(a => a.Patient).FirstOrDefault(a => a.Id == model.Id);
                    appointment.User = await _userHelper.GetUserAsync(User.Identity.Name);
                    appointment.StatusType = StatusType.Reagendada;
                    appointment.Subsequent = model.Subsequent;
                    appointment.Remarks = model.Nota;
                    appointment.Date = model.Date;
                    appointment.Time = model.Time;
                    //appointment.Patient = await _context.Patients.FindAsync(model.Patient.Id);
                    appointment.Doctor = await _context.Doctors.FindAsync(model.DoctorId);
                    appointment.Hospital = await _context.Hospitals.FindAsync(model.HospitalsId);
                    appointment.Speciality = await _context.Specialties.FindAsync(model.SpecialityId);
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception exception)
            {
                ModelState.AddModelError(string.Empty, exception.Message);
            }
            model.Specialities = await _combosHelper.GetComboSpecialitesAsync();
            model.Doctors = await _combosHelper.GetComboDoctorAsync();
            model.Hospitals = await _combosHelper.GetComboHospitalsAsync();
            return View(model);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Appointment appointment = await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Hospital)
                .Include(a => a.Patient)
                .Include(a => a.User)
                .Include(a => a.Patient.HealthInsurance)
                .Include(a => a.Speciality)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }
        public async Task<IActionResult> Confirm(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Appointment appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            if ((appointment.StatusType == StatusType.Reagendada)
                || (appointment.StatusType == StatusType.Nueva))
            {
                appointment.StatusType = StatusType.Confirmada;
                _context.Appointments.Update(appointment);
                await _context.SaveChangesAsync();
                _flashMessage.Confirmation("El estado la Cita ha sido cambiado a 'confirmado'.");
            }
            else
            {
                _flashMessage.Danger("Solo se pueden confirmar Citas que estén en estado 'Nueva o Re-Agendada'.");
            }

            return RedirectToAction(nameof(Details), new { Id = appointment.Id });
        }
        public async Task<IActionResult> Reagend(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Appointment appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            if ((appointment.StatusType != StatusType.Confirmada) || (appointment.StatusType != StatusType.Nueva))
            {
                appointment.StatusType = StatusType.Reagendada;
                _context.Appointments.Update(appointment);
                await _context.SaveChangesAsync();
                _flashMessage.Confirmation("El estado de la cita ha sido cambiado a 'Re-Agendada'.");
            }
            else
            {
                _flashMessage.Danger("Solo se pueden Re-Agendar Citas que estén en estado 'Nueva o Confirmada'.");
            }

            return RedirectToAction(nameof(Details), new { Id = appointment.Id });
        }
        public async Task<IActionResult> Consum(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Appointment appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            if (appointment.StatusType == StatusType.Confirmada)
            {
                appointment.StatusType = StatusType.Consumada;
                _context.Appointments.Update(appointment);
                await _context.SaveChangesAsync();
                _flashMessage.Confirmation("El estado de la Cita ha sido cambiado a 'Consumada'.");
            }
            else
            {
                _flashMessage.Danger("Solo se pueden Consumar la Citas que estén en estado 'Confirmada'.");
            }

            return RedirectToAction(nameof(Details), new { Id = appointment.Id });
        }
        public async Task<IActionResult> Cancel(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Appointment appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            if ((appointment.StatusType == StatusType.Reagendada)
                || (appointment.StatusType == StatusType.Nueva)
                || (appointment.StatusType == StatusType.Confirmada))
            {
                appointment.StatusType = StatusType.Cancelada;
                _context.Appointments.Update(appointment);
                await _context.SaveChangesAsync();
                _flashMessage.Confirmation("El estado de la Cita ha sido cambiado a 'Cancelada'.");
            }
            else
            {
                _flashMessage.Danger("Solo se pueden Cancelar las Citas que estén en estado 'Nueva, Confirmada o Re-Agendada'.");
            }

            return RedirectToAction(nameof(Details), new { Id = appointment.Id });
        }
    }
}
