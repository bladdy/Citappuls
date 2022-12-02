using Citappuls.Enums;
using System.ComponentModel.DataAnnotations;

namespace Citappuls.Data.Entities
{
    public class Appointment
    {
        public int Id { get; set; }
        [Display(Name = "Paciente")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes de seleccionar un Especialidad.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Patient Patient { get; set; }
        [Display(Name = "Clinica o Hospital")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes de seleccionar un Especialidad.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Hospital Hospital { get; set; }
        [Display(Name = "Especialidad")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes de seleccionar un Especialidad.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Speciality Speciality { get; set; }
        [Display(Name = "Docotr")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes de seleccionar un Especialidad.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Doctor Doctor { get; set; }
        public User User { get; set; }
        public DateTime AppointmentDate { get; set; }
        [Display(Name = "Estatus")]
        public StatusType StatusType { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Fecha")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes de seleccionar un Fecha.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime Date { get; set; }
        [DataType(DataType.Time)]
        [Display(Name = "Hora")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes de seleccionar un Hora.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime Time { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Comentarios")]
        public string? Remarks { get; set; }

        [Display(Name = "Subsecuente")]
        public bool? Subsequent { get; set; }
    }
}
