namespace Citappuls.Data.Entities
{
    public class HospitalDoctor
    {
        public int Id { get; set; }

        public Hospital Hospital { get; set; }

        public Doctor Doctor { get; set; }
    }
}
