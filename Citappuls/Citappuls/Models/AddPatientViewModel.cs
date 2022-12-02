using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Citappuls.Models
{
    public class AddPatientViewModel: EditPatientViewModel
    {

        [Display(Name = "Seguro Medico")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un Seguro Medico.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int HealthInsurancesId { get; set; }

        public IEnumerable<SelectListItem> HealthInsurances { get; set; }

        [Display(Name = "Numero de Poliza")]
        public string? HealthInsuranceNumber { get; set; }
    }
}
