using FTsR.Data;
using FTsR.Models;
using Microsoft.AspNetCore.Mvc;

namespace FTsR.Controllers
{
    public class PinController : Controller
    {
        private PinDbContext _pinDbContext;

        public PinController(PinDbContext pinDbContext)
        {
            _pinDbContext = pinDbContext;
        }

        public IActionResult Index(int id)
        {
            var pin = _pinDbContext.Pin.FindAsync(id);
            
            return View(pin.Result);
        }

        //public IActionResult Save(int id)
        //{
        //    var pin = _pinDbContext.Pin.FindAsync(id);
            
        //    return pin.Result;
        //}
    }
}
