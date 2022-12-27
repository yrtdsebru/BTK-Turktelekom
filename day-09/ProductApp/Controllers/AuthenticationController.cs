using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace ProductApp.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager; //oturum islemleri icin login'de bunu kullaniyoruz

        public AuthenticationController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginDto model)
        {
            if (ModelState.IsValid)  //bos gonderirse yakalar.
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user is not null)   //kullanici null degilse
                {
                    //once mevcut kullanici varsa onu logout ederiz.
                    await _signInManager.SignOutAsync();  //mevcut kullaniciyi disari at.s
                    var result = await _signInManager
                        .PasswordSignInAsync(user, model.Password, false, false);   //sifreye bagli olarak kullaniciyi iceri alicak
                                                                                    //simdi result'a bakacagiz result succeeded ise gonder degilse errorlari gostericek.
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("Erro", "Username or password is invalid.");
                    }
                }
            }


            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromForm] RegisterDto model)  //Bu bilgi formdan gelicek, yamasan da olur
        {
            if (ModelState.IsValid)
            {
                var applicationUser = new ApplicationUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                }; //simdi elimde bir tane application user var bunu CreateAsyn'e verecegiz.
                var result = await _userManager
                    .CreateAsync(applicationUser, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                }
            }
            return View(model);
        }
    }
}
