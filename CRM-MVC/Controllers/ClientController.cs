using Microsoft.AspNetCore.Mvc;
using CRM_MVC.Models;
using Mysqlx.Crud;

namespace CRM_MVC.Controllers;

public class ClientController : Controller

{
    public CrmContext context = new();
    public List<Client> clients = new List<Client>();

    public ClientController() { }

    public IActionResult Home()
    {
        clients = context.Clients.ToList();
        return View(clients);
    }


}
