using Microsoft.AspNetCore.Mvc;

namespace VcardQRGenerator.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

}
