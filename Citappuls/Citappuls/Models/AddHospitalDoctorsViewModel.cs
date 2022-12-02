using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Citappuls.Models
{
    public class AddHospitalDoctorsViewModel
    {
        public int HospitalId { get; set; }

        [Display(Name = "Doctor")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una Doctor.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int DoctorId { get; set; }

        public IEnumerable<SelectListItem> Doctors { get; set; }

    }
}
