using Citappuls.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Citappuls.Models
{
    public class AddAppoitmentViewModel
    {
        [Key]
        public int Id { get; set; }
        public User User { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Hopital { get; set; }
        public IEnumerable<SelectListItem> Speciality { get; set; }
        public string Doctor { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }
        public string Schedule { get; set; }
        [DataType(DataType.MultilineText)]
        public string Nota { get; set; }
    }
}
