using System.ComponentModel.DataAnnotations;

namespace Citappuls.Data.Entities
{
    public class HealthInsurance
    {
        public int Id { get; set; }
        [Display(Name = "Seguro Medico")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }
        [Display(Name = "Porcentaje")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Double Percentage { get; set; }

        public ICollection<Patient> Patients { get; set; }
    }
}
