using Microsoft.AspNetCore.Mvc;
using PSTDotNetTrainingBatch5.MvcApp.Models;
using System.Diagnostics;

namespace PSTDotNetTrainingBatch5.MvcApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.Message = "Home From ViewBag";
            ViewData["Message2"] = "Home From Viewdata";

            HomeResponseModel model = new HomeResponseModel();

            model.AlertMessage = "Home From Model";

            //return Redirect("/Home/Privacy");

            return View(model);
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

        public IActionResult Index2() 
        {
            return View();
        }
    }
}
