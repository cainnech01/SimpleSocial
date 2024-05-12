using FTsR.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;


namespace FTsR.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<UserModel> _userManager;

        public ProfileController(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var s = User.FindFirstValue(ClaimTypes.NameIdentifier);
            UserModel tmp = await _userManager.FindByIdAsync(s);
            return View(tmp);
        }

        public async Task<IActionResult> Edit(string id)
        {
            UserModel user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            EditModel model = new EditModel { Id = user.Id, UserName = user.UserName, ProfilePicture = null };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditModel model)
        {
            if (ModelState.IsValid)
            {
                if (model != null)
                {
                    byte[] imageData = null;
                    using (var binaryReader = new BinaryReader(model.ProfilePicture.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)model.ProfilePicture.Length);
                    }

                    UserModel user = await _userManager.FindByIdAsync(model.Id);
                    if (user != null)
                    {
                        user.UserName = model.UserName;
                        user.ProfilePicture = imageData;

                        var result = await _userManager.UpdateAsync(user);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }
                        }
                    }
                }
            }
            return View(model);
        }
    }
}
