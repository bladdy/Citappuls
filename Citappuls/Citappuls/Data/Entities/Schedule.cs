using System.ComponentModel.DataAnnotations;

namespace Citappuls.Data.Entities
{
    public class Schedule
    {
        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }
        [Display(Name = "Dia")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(12, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        public string Day { get; set; }
        [Display(Name = "Inicio")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Start { get; set; }
        [Display(Name = "Final")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string End { get; set; }

        [Display(Name = "Horario")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string FullSchedule => $"{Day} - {Start} a {End}";

        [Display(Name = "Hospital")]
        public Hospital Hospital { get; set; }


    }
}
