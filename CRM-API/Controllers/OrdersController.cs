using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRM_API.Models;
using Microsoft.AspNetCore.Authorization;

namespace CRM_API.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]

public class OrdersController : ControllerBase 
{

    public CrmContext context = new();

    public OrdersController()
    {

    }

/*    [HttpGet]
    public DbSet<Order> Get()
    {
        return context.Orders;
    }*/

    [Authorize]
    [HttpGet]
    [Route("{id}")]
    public Order Get(int id)
    {
        return context.Orders.Single(o => o.id == id);
    }

    [Authorize]
    [HttpPost]
    public string Post([FromBody] Order newOrder)
    {
        using (var transaction = context.Database.BeginTransaction())
        try
        {
            context.Orders.Add(newOrder);
            context.SaveChanges();
            transaction.Commit();
            return "Commande ajoutée";
        }
        catch (Exception)
        {
            transaction.Rollback();
            return "Erreur, la commande n'a pas été ajoutée";
        }
    }

    [Authorize]
    [HttpPut]
    [Route("edit/{id}")]
    public string Put(int id, Order orderChanged)
    {
        using (var transaction = context.Database.BeginTransaction())
        try
        {
            Order oldOrder = context.Orders.Single(u => u.id == id);
            oldOrder.typePresta = orderChanged.typePresta;
            oldOrder.client = orderChanged.client;
            oldOrder.nbJour = orderChanged.nbJour;
            oldOrder.tjmHt = orderChanged.tjmHt;
            oldOrder.tva = orderChanged.tva;
            oldOrder.state = orderChanged.state;
            oldOrder.comment = orderChanged.comment;
            oldOrder.client_id = orderChanged.client_id;
            context.SaveChanges();
            transaction.Commit();
            return "Commande modifiée";
        }
        catch (Exception)
        {
            transaction.Rollback();
            return "Erreur, la commande n'a pas été modifée";
        }
    }

    [Authorize]
    [HttpDelete]
    [Route("{id}")]
    public string Delete(int id)
    {
        using (var transaction = context.Database.BeginTransaction())
        try
        {
            Order orderDelete = context.Orders.Single(u => u.id == id);
            context.Orders.Remove(orderDelete);
            context.SaveChanges();
            transaction.Commit();
            return "Commande supprimée";
        }
        catch (Exception)
        {
            transaction.Rollback();
            return "Erreur, le client n'a pas été supprimé";
        }
    }
}

