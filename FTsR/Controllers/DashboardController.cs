using Microsoft.AspNetCore.Mvc;

namespace FTsR.Controllers;

public class DashboardController : Controller
{
    public IActionResult Index(){
        return View();
    }
}
