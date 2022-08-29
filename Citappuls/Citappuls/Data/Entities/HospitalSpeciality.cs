namespace Citappuls.Data.Entities
{
    public class HospitalSpeciality
    {
        public int Id { get; set; }

        public Hospital Hospital { get; set; }

        public Speciality Speciality { get; set; }

    }
}
