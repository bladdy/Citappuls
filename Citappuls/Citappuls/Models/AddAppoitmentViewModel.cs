using Citappuls.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Citappuls.Models
{
    public class AddAppoitmentViewModel
    {
        [Key]
        public int Id { get; set; }
        public User User { get; set; }
        public Patient Patient { get; set; }

        [Display(Name = "Doctor")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una Doctor.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int DoctorId { get; set; }

        public IEnumerable<SelectListItem> Doctors { get; set; }
        [Display(Name = "Hospitales")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una Hospital.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int HospitalsId { get; set; }
        public IEnumerable<SelectListItem> Hospitals { get; set; }
        [Display(Name = "Especialidades")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una Especialidad.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int SpecialityId { get; set; }

        public IEnumerable<SelectListItem> Specialities { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Subsecuente")]
        public bool Subsequent { get; set; }
        public string? Nota { get; set; }
    }
}
