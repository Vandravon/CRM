using Microsoft.AspNetCore.Mvc;
using CRM_MVC.Models;

namespace CRM_MVC.Controllers;

public class OrderController : Controller
{
    public CrmContext context = new();
    public List<Order> orders = new List<Order>();

    public OrderController() { }

    public IActionResult Home()
    {
        orders = context.Orders.ToList();
        return View(orders);
    }

}
