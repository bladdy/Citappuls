using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Citappuls.Models
{
    public class AddHospitalSpecialitiesViewModel
    {
        public int HospitalId { get; set; }

        [Display(Name = "Especialidad")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una Especialidad.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int SpecialityId { get; set; }

        public IEnumerable<SelectListItem> Specialities { get; set; }
    }
}
