namespace Citappuls.Data.Entities
{
    public class SpecialityDoctor
    {
        public int Id { get; set; }

        public Doctor Doctor { get; set; }

        public Speciality Speciality { get; set; }
    }
}
