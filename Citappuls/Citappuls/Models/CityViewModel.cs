using System.ComponentModel.DataAnnotations;

namespace Citappuls.Models
{
    public class CityViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Ciudad")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obigatorio.")]
        public string Name { get; set; }

        public int StateId { get; set; }
    }
}
