using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Citappuls.Models
{
    public class CreateHospitalViewModel : EditeHospitalViewModel
    {
        [Display(Name = "Especialidades")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una Especialidad.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int SpecialityId { get; set; }

        public IEnumerable<SelectListItem> Specialities { get; set; }

        [Display(Name = "Doctores")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una Doctor.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int DoctorId { get; set; }

        public IEnumerable<SelectListItem> Doctors { get; set; }
       
    }
}
