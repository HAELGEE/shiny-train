using Entity;
using Microsoft.AspNetCore.Mvc;
using Snackis.Models;
using System.Diagnostics;

namespace Snackis.Controllers;
public class HomeController : Controller
{  
    public IActionResult Index()
    {
        //ViewData
        return View();
    }

    



    public IActionResult Info()
    {
        return View();
    }

























    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
