using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace FleetManager.Controllers;

[Authorize(Roles = "Admin")]
public class RoleController : Controller
{
    private readonly RoleManager<IdentityRole> _roleManager;
    public RoleController(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var roles = await _roleManager.Roles.ToListAsync();
        return View(roles);
    }

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create([Required] string roleName)
    {
        if (ModelState.IsValid)
        {
            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "This role already exists.");
            }
        }
        return View((object)roleName);
    }
}