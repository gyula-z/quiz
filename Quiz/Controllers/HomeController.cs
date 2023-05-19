using Microsoft.AspNetCore.Mvc;

namespace Quiz.Controllers;

public class HomeController : Controller
{
    public IActionResult Game()
    {
        //return View();
        return Ok("Game endpoint hit");
    }

    public IActionResult Questions()
    {
        //return View();
        return Ok("Questions endpoint hit");
    }
}