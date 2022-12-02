using System.ComponentModel.DataAnnotations;

namespace Citappuls.Data.Entities
{
    public class SexType
    {
        public int Id { get; set; }
        [Display(Name = "Genero")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }
    }
}
