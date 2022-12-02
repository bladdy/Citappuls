using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace Citappuls.Models
{
    public class EditPatientViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Nombres")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string FirstName { get; set; }

        [Display(Name = "Apellidos")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string LastName { get; set; }

        [Display(Name = "Cedula o Pasaporte")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Document { get; set; }
        [Display(Name = "Telefonos")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Phone { get; set; }
        [Display(Name = "Nacionalidad")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Nationality { get; set; }

        [Display(Name = "Fecha de Nacimiento")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateofBirth { get; set; }

        [Display(Name = "Dirección")]
        [MaxLength(200, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Address { get; set; }

        [Display(Name = "País")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes de seleccionar un país.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int CountryId { get; set; }

        public IEnumerable<SelectListItem> Countries { get; set; }

        [Display(Name = "Estado / Provincia")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes de seleccionar un Estado / Provincia.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int StateId { get; set; }

        public IEnumerable<SelectListItem> States { get; set; }

        [Display(Name = "Ciuadad")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes de seleccionar una ciudad.")]
        public int CityId { get; set; }
        public IEnumerable<SelectListItem> Cities { get; set; }

        [Display(Name = "Genero")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int SexTypeId { get; set; }
        public IEnumerable<SelectListItem> SexTypes { get; set; }

        [Display(Name = "Estado Civil")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int MaritalStatusTypeId { get; set; }
        public IEnumerable<SelectListItem> MaritalStatusType { get; set; }
    }
}
