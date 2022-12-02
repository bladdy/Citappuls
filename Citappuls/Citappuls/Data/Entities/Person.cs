using Citappuls.Enums;
using System.ComponentModel.DataAnnotations;

namespace Citappuls.Data.Entities
{
    public class Person
    {
        public int Id { get; set; }
        [Display(Name = "Nombres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string FirstName { get; set; }
        [Display(Name = "Apellidos")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string LastName { get; set; }
        [Display(Name = "Cedula o Pasaporte")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Document { get; set; }
        [Display(Name = "Nacionalidad")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Nationality { get; set; }

        [Display(Name = "Fecha de Nacimiento")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateofBirth { get; set; }

        [Display(Name = "Estado Civil")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public MaritalStatusType MaritalStatus { get; set; }

        [Display(Name = "Genero")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public SexType SexType { get; set; }

        [Display(Name = "Telefono")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Ciudad")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public City City { get; set; }
        [Display(Name = "Direccion")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Address { get; set; }
        [Display(Name = "Nombre Completo")]
        public string FullName => $"{FirstName} {LastName}";
        [Display(Name = "Edad")]
        public int Age => DateTime.Today.AddTicks(-DateofBirth.Ticks).Year - 1;
    }
}
