using System.ComponentModel.DataAnnotations;

namespace Citappuls.Data.Entities
{
    public class MaritalStatusType
    {
        public int Id { get; set; }
        [Display(Name = "Estado Civil")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }
    }
}
