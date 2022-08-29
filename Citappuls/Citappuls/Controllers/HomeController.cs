using Citappuls.Data.Entities;
using Citappuls.Helpers;
using Citappuls.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Citappuls.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserHelper _userHelper;
        private readonly ICombosHelper _combosHelper;
        public HomeController(IUserHelper userHelper, ILogger<HomeController> logger, ICombosHelper combosHelper)
        {
            _userHelper = userHelper;
            _logger = logger;
            _combosHelper = combosHelper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [Route("error/404")]
        public IActionResult Error404()
        {
            return View();
        }
        public async Task<IActionResult> App()
        {
            User user = await _userHelper.GetUserAsync(User.Identity.Name);
            AppoitmentRequest model = new AppoitmentRequest
            {
                Specialities = await _combosHelper.GetComboSpecialitesAsync(),
                User = user,
                Date = DateTime.Today,
                Time = Convert.ToDateTime(DateTime.Now.ToString("HH:mm")),
                //Id = Guid.Empty.ToString(),
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> App(AppoitmentRequest? model)
        {
            User user = await _userHelper.GetUserAsync(User.Identity.Name);
            //model.User = user;
            if (ModelState.IsValid)
            {
                /*
                if (model.ImageFile != null)
                {
                    imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "users");
                }*/

                return RedirectToAction("Index", "Home");
            }
            model.Specialities = await _combosHelper.GetComboSpecialitesAsync();
            model.User = user;
            model.Date = DateTime.Today;
            model.Time = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
            return View(model);
        }
    }
}