
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Citappuls.Data;
using Citappuls.Data.Entities;
using Citappuls.Enums;
using Citappuls.Helpers;
using Citappuls.Models;

namespace Citappuls.Controllers
{
    
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;
        private readonly IBlobHelpers _blobHelper;
        //private readonly IMailHelper _mailHelper;
        public UsersController(IUserHelper userHelper, DataContext context, ICombosHelper combosHelper, IBlobHelpers blobHelper/*, IMailHelper mailHelper*/)
        {
            _userHelper = userHelper;
            _context = context;
            _combosHelper = combosHelper;
            _blobHelper = blobHelper;
            //_mailHelper = mailHelper;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users
                .Include(u => u.City)
                .ThenInclude(c => c.State)
                .ThenInclude(s => s.Country)
                .ToListAsync());
        }
        public async Task<IActionResult> Create()
        {
            AddUserViewModel model = new AddUserViewModel
            {
                Id = Guid.Empty.ToString(),
                Countries = await _combosHelper.GetComboCountriesAsync(),
                States = await _combosHelper.GetComboStatesAsync(0),
                Cities = await _combosHelper.GetComboCitiesAsync(0),
                UserType = UserType.User,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddUserViewModel? model)
        {
            if (ModelState.IsValid)
            {
                Guid imageId = Guid.Empty;
                /*
                if (model.ImageFile != null)
                {
                    imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "users");
                }*/
                model.ImageId = imageId;
                User user = await _userHelper.AddUserAsync(model);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Este correo ya está siendo usado.");
                    model.Countries = await _combosHelper.GetComboCountriesAsync();
                    model.States = await _combosHelper.GetComboStatesAsync(model.CountryId);
                    model.Cities = await _combosHelper.GetComboCitiesAsync(model.StateId);
                    return View(model);
                }
                return RedirectToAction("Index", "Users");
            }
            model.Countries = await _combosHelper.GetComboCountriesAsync();
            model.States = await _combosHelper.GetComboStatesAsync(model.CountryId);
            model.Cities = await _combosHelper.GetComboCitiesAsync(model.StateId);

            return View(model);
        }
        public JsonResult GetStates(int countryId)
        {
            Country country = _context.Countries
                .Include(c => c.States)
                .FirstOrDefault(c => c.Id == countryId);
            if (country == null)
            {
                return null;
            }
            return Json(country.States.OrderBy(d => d.Name));
        }
        public JsonResult GetCities(int stateId)
        {
            State state = _context.States
                .Include(s => s.Cities)
                .FirstOrDefault(s => s.Id == stateId);
            if (state == null)
            {
                return null;
            }
            return Json(state.Cities.OrderBy(c => c.Name));
        }
    }
}
