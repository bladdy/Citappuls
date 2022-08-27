using System.ComponentModel.DataAnnotations;

namespace Citappuls.Data.Entities
{
    public class Doctor
    {
        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }

        //[Display(Name = "Hospital")]
        //public ICollection<Hospital> Hospital { get; set; }

        //[Display(Name = "Horarios")]
        //public ICollection<Schedule> Schedules { get; set; }

        public string Exequatur { get; set; }
        //public ICollection<Speciality> Specialities { get; set; }


    }
}
