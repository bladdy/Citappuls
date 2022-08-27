using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Citappuls.Data.Entities
{
    public class State
    {
        public int Id { get; set; }
        [Display(Name = "Estados/Provincias")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }
        
        public Country Country { get; set; }
        public ICollection<City> Cities { get; set; }

        [Display(Name = "Ciudades")]
        public int CitiesNumber => Cities == null ? 0 : Cities.Count;
    }
}
