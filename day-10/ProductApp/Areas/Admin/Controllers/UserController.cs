using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ProductApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }

        public async Task<IActionResult> AddRoleToUser(string id)  //controller-action-id(burdaki user name'i çekiyo)
        {
            var user = await _userManager.FindByNameAsync(id);
            var roles = await _roleManager.Roles.ToListAsync();
            ViewBag.RoleList = roles;
            ViewBag.Roles = new SelectList(roles, "Id", "Name");   //...,role tablosundaki ıd alani, rol tablosundaki name alani
             return View(user);
        }

        //burda selectList id->name'e ya şimdi name->id yapcaz.
        [HttpPost]
        public async Task<IActionResult> AddRoleToUser(string username, string role)  //
        {
            var selectedRole = await _roleManager.FindByIdAsync(role);
            var selectedUser = await _userManager.FindByNameAsync(username);

            if(selectedRole is not null && selectedUser is not null)
            {
                var result = await _userManager
                    .AddToRoleAsync(selectedUser, selectedRole.Name);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                }
            }
            return RedirectToAction("Index");
            
        }

        public async Task<IActionResult> DeleteRole(string username, string role)
        {
            var user = await _userManager.FindByNameAsync(username);
            var roles = await _roleManager.FindByIdAsync(role);

            var rslt = await _userManager
                .RemoveFromRoleAsync(user, roles.Name);

            if (rslt.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var err in rslt.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }
            return View(roles);
        }
    }
}
