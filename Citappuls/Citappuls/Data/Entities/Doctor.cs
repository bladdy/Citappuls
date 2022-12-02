using System.ComponentModel.DataAnnotations;

namespace Citappuls.Data.Entities
{
    public class Doctor
    {
        public int Id { get; set; }
        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }
        public string LastName { get; set; }
        [Display(Name = "Dirección")]
        [MaxLength(200, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Address { get; set; }
        [Display(Name = "Telefonos")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Phone { get; set; }
        public ICollection<HospitalDoctor> HospitalDoctors { get; set; }
        public string Exequatur { get; set; }

        public ICollection<SpecialityDoctor> SpecialityDoctor { get; set; }
        [Display(Name = "Hospitales")]
        public int HospitalNumber => HospitalDoctors == null ? 0 : HospitalDoctors.Count;
        [Display(Name = "Especialidades")]
        public int EspecialidadesNumber => SpecialityDoctor == null ? 0 : SpecialityDoctor.Count;
        [Display(Name = "Doctor")]
        public string FullName => $"{Name} {LastName}";

        [Display(Name = "Ciudad")]
        public City City { get; set; }
        [Display(Name = "Foto")]
        public Guid ImageId { get; set; }

        [Display(Name = "Foto")]
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://localhost:7286/img/noimage.png"
            : $"https://shoppingzulu.blob.core.windows.net/users/{ImageId}";
    }
}
