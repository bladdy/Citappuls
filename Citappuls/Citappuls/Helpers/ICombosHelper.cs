using Citappuls.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Citappuls.Helpers
{
    public interface ICombosHelper
    {
        //Se obtienen todas los Generos
        Task<IEnumerable<SelectListItem>> GetComboSexTypeAsync();
        //Se obtienen todas los Estado Civil    
        Task<IEnumerable<SelectListItem>> GetComboMaritalStatusAsync();

        //Se obtienen todas las especialidades
        Task<IEnumerable<SelectListItem>> GetComboSpecialitesAsync();
        //Se obtienen todas las HealthInsurance
        Task<IEnumerable<SelectListItem>> GetComboHealthInsuranceAsync();
        //Se obtienen las especialidades por id
        Task<IEnumerable<SelectListItem>> GetComboSpecialitesAsync(IEnumerable<Speciality> filter);
        //Se obtienen todas los Doctores
        Task<IEnumerable<SelectListItem>> GetComboDoctorAsync();
        //Se obtienen todas los Doctores por id
        Task<IEnumerable<SelectListItem>> GetComboDoctorAsync(IEnumerable<Doctor> filter);
        //Se Obtienen todos los paises
        Task<IEnumerable<SelectListItem>> GetComboCountriesAsync();
        //Se Obtienen todos los estastdos por el id paises
        Task<IEnumerable<SelectListItem>> GetComboStatesAsync(int countryId);
        //Se Obtienen todos las Ciudades por el id de estados
        Task<IEnumerable<SelectListItem>> GetComboCitiesAsync(int stateId);
        //Se obtienen todas los Hospitales
        Task<IEnumerable<SelectListItem>> GetComboHospitalsAsync();
        //Se obtienen todas los Hospitales por id
        Task<IEnumerable<SelectListItem>> GetComboHospitalsAsync(IEnumerable<Hospital> filter);
        Task<IEnumerable<SelectListItem>> GetComboUsersAsync();
    }
}
