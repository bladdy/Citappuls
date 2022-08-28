using System.ComponentModel.DataAnnotations;

namespace Citappuls.Data.Entities
{
    public class AppoitmentRequest
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Hopital { get; set; }
        public string Especialidad { get; set; }
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
