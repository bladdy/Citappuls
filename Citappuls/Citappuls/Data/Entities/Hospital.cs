using System.ComponentModel.DataAnnotations;

namespace Citappuls.Data.Entities
{
    public class Hospital
    {
        public int Id { get; set; }
        [Display(Name = "Nombres")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }

        [Display(Name = "Telefonos")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Phone { get; set; }

        [Display(Name = "Dirección")]
        [MaxLength(200, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Address { get; set; }
        public ICollection<HospitalSpeciality> HospitalSpecialities { get; set; }

        [Display(Name = "Especialidades")]
        public int EspecialidadesNumber => HospitalSpecialities == null ? 0 : HospitalSpecialities.Count;
        public ICollection<HospitalDoctor>? HospitalDoctors { get; set; }
        [Display(Name = "Doctores")]
        public int DoctorNumber => HospitalDoctors == null ? 0 : HospitalDoctors.Count;

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
