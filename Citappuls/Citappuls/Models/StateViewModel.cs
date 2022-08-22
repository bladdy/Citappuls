using System.ComponentModel.DataAnnotations;

namespace Citappuls.Models
{
    public class StateViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Provincia/Estado")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obigatorio.")]
        public string Name { get; set; }

        public int CountryId { get; set; }
    }
}
