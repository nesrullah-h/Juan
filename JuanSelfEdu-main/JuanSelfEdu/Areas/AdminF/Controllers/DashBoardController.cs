using Microsoft.AspNetCore.Mvc;

namespace JuanSelfEdu.Areas.AdminF.Controllers
{
    [Area("AdminF")]
    public class DashBoardController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
    }
}
