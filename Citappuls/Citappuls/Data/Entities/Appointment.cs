using Citappuls.Enums;
using System.ComponentModel.DataAnnotations;

namespace Citappuls.Data.Entities
{
    public class Appointment
    {
        public Patient Patient { get; set; }
        public Hospital Hospital { get; set; }
        public Speciality Speciality { get; set; }  
        public Doctor Doctor { get; set; }
        public DateTime AppointmentDate { get; set; }
        public DateTime AppointmentTime { get; set; }
        public StatusType StatusType { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Comentarios")]
        public string? Remarks { get; set; }
    }
}
