using Microsoft.AspNetCore.Mvc;
using CRM_MVC.Models;

namespace CRM_MVC.Controllers;

public class UserController : Controller
{
    public CrmContext context = new();
    List<User> users = new List<User>();

    public UserController() { }

    public IActionResult Home() 
    {
        users = context.Users.ToList();
        return View(users); 
    }

    public IActionResult Detail(int id)
    {
        User userDetail = context.Users.Single(u => u.id == id);
        return View(userDetail);
    }
}
