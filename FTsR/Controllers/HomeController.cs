using System.Diagnostics;
using FTsR.Data;
using FTsR.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FTsR.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly PinDbContext _pinDbContext;
    private readonly UserManager<UserModel> _userManager;
    public HomeController(ILogger<HomeController> logger, PinDbContext pinDbContext, UserManager<UserModel> userManager)
    {
        _logger = logger;
        _pinDbContext = pinDbContext;
        _userManager = userManager;
    }

    [HttpGet]
    public IActionResult Upload()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Upload(CreatePinModel model)
    {
        var user = _userManager.GetUserAsync(User);
        byte[] imageData = null;
        using (var binaryReader = new BinaryReader(model.Image.OpenReadStream()))
        {
            imageData = binaryReader.ReadBytes((int)model.Image.Length);
        }

        Pin pin = new Pin
        {
            Author = User.Identity.Name,
            Title = model.Title,
            Description = model.Description,
            AuthorProfileImage = user.Result.ProfilePicture,    
            Image = imageData
        };

        _pinDbContext.Pin.Add(pin);
        await _pinDbContext.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    public IActionResult Index()
    {
        var temp = _pinDbContext.Pin.Count() == 0 ? null : _pinDbContext.Pin.ToList();
        return View(temp);
    }
    public IActionResult Print()
    {
        
        return View(1);
    }

    public IActionResult Notification(){
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
}
