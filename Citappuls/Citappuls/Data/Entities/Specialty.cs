using System.ComponentModel.DataAnnotations;

namespace Citappuls.Data.Entities
{
    public class Speciality
    {
        public int Id { get; set; }
        [Display(Name = "Especialidad")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }
    }
}
