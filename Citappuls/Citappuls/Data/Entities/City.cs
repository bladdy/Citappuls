using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Citappuls.Data.Entities
{
    public class City
    {
        public int Id { get; set; }
        [Display(Name = "Estado/Provincia")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }
        [JsonIgnore]
        public State State { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
