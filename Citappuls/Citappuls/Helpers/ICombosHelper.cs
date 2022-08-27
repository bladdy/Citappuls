using Citappuls.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Citappuls.Helpers
{
    public interface ICombosHelper
    {
        Task<IEnumerable<SelectListItem>> GetComboSpecialitesAsync();
        Task<IEnumerable<SelectListItem>> GetComboSpecialitesAsync(IEnumerable<Speciality> filter);

        Task<IEnumerable<SelectListItem>> GetComboCountriesAsync();

        Task<IEnumerable<SelectListItem>> GetComboStatesAsync(int countryId);

        Task<IEnumerable<SelectListItem>> GetComboCitiesAsync(int stateId);
        Task<IEnumerable<SelectListItem>> GetComboUsersAsync();
    }
}
