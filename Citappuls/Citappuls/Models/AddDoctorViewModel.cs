using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Citappuls.Models
{
    public class AddDoctorViewModel : EditeDoctorViewModel
    {
        [Display(Name = "Especialidades")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una Especialidad.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int SpecialityId { get; set; }

        public IEnumerable<SelectListItem> Specialities { get; set; }

        [Display(Name = "Hospitales")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una Hospital.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int HospitalsId { get; set; }

        public IEnumerable<SelectListItem> Hospitals { get; set; }
    }
}
