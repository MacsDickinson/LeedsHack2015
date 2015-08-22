using GroundJobs.MVC.Models;
using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GroundJobs.MVC.Controllers
{
    public class SurveyController : Controller
    {
        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(SurveyIndexViewModel model)
        {
            return View(model);
        }
    }
}
