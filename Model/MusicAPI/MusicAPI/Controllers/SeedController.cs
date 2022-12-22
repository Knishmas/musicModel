//using CsvHelper;
//using CsvHelper.Configuration;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.Diagnostics.Metrics;
//using System.Formats.Asn1;
//using System.Globalization;
//using MusicAPI.Data;
//using MusicModel.Models;
//using Path = System.IO.Path;

//namespace MusicAPI.Controllers;

//[Route("api/[controller]")]
//[ApiController]
//public class SeedController : ControllerBase
//{
//    private readonly MuscContext _context;
//    private readonly string _pathName;

//    private readonly UserManager<MusicUser> _userManager;
//    private readonly RoleManager<IdentityRole> _roleManager;
//    private readonly IConfiguration _configuration;
//    public SeedController(MuscContext context, IHostEnvironment environment,
//        UserManager<MusicUser> userManager, RoleManager<IdentityRole> roleManager,
//        IConfiguration configuration)
//    {
//        _context = context;
//        _userManager = userManager;
//        _roleManager = roleManager;
//        _configuration = configuration;
//        _pathName = Path.Combine(environment.ContentRootPath, "Data/music.csv");
//    }


    
//    public async Task<IActionResult> CreateUsers()
//    {
//        const string roleUser = "RegisteredUser";
//        const string roleAdmin = "Administrator";

//        if (await _roleManager.FindByNameAsync(roleUser) is null)
//        {
//            await _roleManager.CreateAsync(new IdentityRole(roleUser));
//        }
//        if (await _roleManager.FindByNameAsync(roleAdmin) is null)
//        {
//            await _roleManager.CreateAsync(new IdentityRole(roleAdmin));
//        }

//        List<MusicUser> addedUserList = new();
//        (string name, string email) = ("admin", "admin@email.com");

//        if (await _userManager.FindByNameAsync(name) is null)
//        {
//            MusicUser userAdmin = new()
//            {
//                UserName = name,
//                Email = email,
//                SecurityStamp = Guid.NewGuid().ToString()
//            };
//            await _userManager.CreateAsync(userAdmin, _configuration["DefaultPasswords:Administrator"]!);
//            await _userManager.AddToRolesAsync(userAdmin, new[] { roleUser, roleAdmin });
//            userAdmin.EmailConfirmed = true;
//            userAdmin.LockoutEnabled = false;
//            addedUserList.Add(userAdmin);
//        }

//        (string name, string email) registered = ("user", "user@email.com");

//        if (await _userManager.FindByNameAsync(registered.name) is null)
//        {
//            MusicUser user = new()
//            {
//                UserName = registered.name,
//                Email = registered.email,
//                SecurityStamp = Guid.NewGuid().ToString()
//            };
//            await _userManager.CreateAsync(user, _configuration["DefaultPasswords:RegisteredUser"]!);
//            await _userManager.AddToRoleAsync(user, roleUser);
//            user.EmailConfirmed = true;
//            user.LockoutEnabled = false;
//            addedUserList.Add(user);
//        }

//        if (addedUserList.Count > 0)
//        {
//            await _context.SaveChangesAsync();
//        }

//        return new JsonResult(new
//        {
//            addedUserList.Count,
//            Users = addedUserList
//        });

//    }

