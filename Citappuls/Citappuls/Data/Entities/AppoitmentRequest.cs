using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Citappuls.Data.Entities
{
    public class AppoitmentRequest
    {
        [Key]
        public int Id { get; set; }
        public User? User { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Hopital { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Telefono")]
        [Required(ErrorMessage = "Mobile Number is required.")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Digite un formato valido de {0} 000-000-0000.")]
        public string Phone { get; set; }

        [Display(Name = "Especialidad")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes de seleccionar un Especialidad.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int SpecialityId { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public IEnumerable<SelectListItem> Specialities { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Doctor { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime Date { get; set; }
        [DataType(DataType.Time)]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime Time { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Schedule { get; set; }
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Nota { get; set; }
    }
}
