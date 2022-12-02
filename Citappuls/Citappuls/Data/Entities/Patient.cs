using System.ComponentModel.DataAnnotations;

namespace Citappuls.Data.Entities
{
    public class Patient: Person
    {
        public int Id { get; set; }
        [Display(Name = "Seguro Medico")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public HealthInsurance HealthInsurance { get; set; }
        [Display(Name = "Numero de Poliza")]
        //[Required(ErrorMessage = "El campo {0} es obligatorio.")]
        
        public string? HealthInsuranceNumber { get; set; }
    }
}
