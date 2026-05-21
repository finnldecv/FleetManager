using FleetManager.Models;
using FleetManager.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    public AdminController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var allUsers = await _userManager.Users.ToListAsync();
        return View(allUsers);
    }
    [HttpGet]
    public async Task<IActionResult> ManageRoles(string userId)
    {
        ViewBag.userId = userId;
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return NotFound();
 
        ViewBag.UserName = user.UserName;

        var model = new List<ManageUserRolesViewModel>();

        foreach (var role in await _roleManager.Roles.ToListAsync())
        {
            var userRolesViewModel = new ManageUserRolesViewModel
            {
                RoleId = role.Id,
                RoleName = role.Name!
            };

            if (await _userManager.IsInRoleAsync(user, role.Name!))
            {
                userRolesViewModel.IsSelected = true;
            }
            else
            {
                userRolesViewModel.IsSelected = false;
            }
            model.Add(userRolesViewModel);
        }
        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> ManageRoles(List<ManageUserRolesViewModel> model, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return NotFound();

        var roles = await _userManager.GetRolesAsync(user);
        
        var result = await _userManager.RemoveFromRolesAsync(user, roles);
        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "Cannot remove user from existing roles");
            return View(model);
        }

        var selectedRoles = model.Where(x => x.IsSelected).Select(y => y.RoleName);

        result = await _userManager.AddToRolesAsync(user, selectedRoles);
        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "Cannot add user to selected roles");
            return View(model);
        }
        return RedirectToAction("Index");
    }
}