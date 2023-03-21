using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRM_API.Models;
using Microsoft.AspNetCore.Authorization;

namespace CRM_API.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]

public class ClientsController : ControllerBase 
{

    public CrmContext context = new();

    public ClientsController()
    {

    }

    [Authorize]
    [HttpGet]
    public DbSet<Client> Get()
    {
        return context.Clients;
    }

    [Authorize]
    [HttpGet]
    [Route("{id}")]
    public Client Get(int id)
    {
        Client? returnClient;
        try
        {
            returnClient =  context.Clients.Single(c => c.id == id);
        }
        catch (Exception)
        {
            Console.WriteLine("Client non trouvé!");
            returnClient = context.Clients.Single(c => c.id == 1);
        }
        return returnClient;
    }

    [Authorize]
    [HttpPost]
    public string Post([FromBody] Client newClient)
    {
        using (var transaction = context.Database.BeginTransaction())
        try
        {
            context.Clients.Add(newClient);
            context.SaveChanges();
            transaction.Commit();
            return "Client ajouté";
        }
        catch (Exception)
        {
            transaction.Rollback();
            return "Erreur, le client n'a pas été ajouté";
        }

    }

    [Authorize]
    [HttpPut]
    [Route("edit/{id}")]
    public string Put(int id, Client clientChanged)
    {
        using (var transaction = context.Database.BeginTransaction())
        try
        {
            Client oldClient = context.Clients.Single(c => c.id == id);
            oldClient.name = clientChanged.name;
            oldClient.state = clientChanged.state;
            oldClient.tva = clientChanged.tva;
            oldClient.totalCaHt = clientChanged.totalCaHt;
            oldClient.comment = clientChanged.comment;
            oldClient.user_id = clientChanged.user_id;
            context.SaveChanges();
            transaction.Commit();
            return "Client modifié";
        }
        catch (Exception)
        {
            transaction.Rollback();
            return "Erreur, le client n'a pas été modifié";
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
            Client clientDelete = context.Clients.Single(u => u.id == id);
            context.Clients.Remove(clientDelete);
            context.SaveChanges();
            transaction.Commit();
            return "Client supprimé";
        }
        catch (Exception)
        {
            transaction.Rollback();
            return "Erreur, le client n'a pas été supprimé";
        }
    }

}

