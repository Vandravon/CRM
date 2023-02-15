using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRM_API.Models;
using Microsoft.AspNetCore.Authorization;

namespace CRM_API.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]

public class UsersController : ControllerBase 
{

    public CrmContext context = new();

    public UsersController()
    {

    }

    /*    [HttpGet]
        public DbSet<User> Get()
        {
            return context.Users;
        }*/

    [Authorize]
    [HttpGet]
    [Route("{id}")]
    public User Get(int id)
    {
        return context.Users.Single(u => u.id == id);
    }

    [Authorize]
    [HttpPost]
    public string Post([FromBody] User newUser)
    {
        using (var transaction = context.Database.BeginTransaction())
        try
        {
            context.Users.Add(newUser);
            context.SaveChanges();
            transaction.Commit();
            return "Utilisateur ajouté!";
        }
        catch (Exception)
        {
            transaction.Rollback();
            return "Erreur, l'utilisateur' n'a pas été ajouté";
        }

    }

    [Authorize]
    [HttpPut]
    [Route("edit/{id}")]
    public string Put(int id, User userChanged)
    {
        using (var transaction = context.Database.BeginTransaction())
        try
        {
            User oldUser = context.Users.Single(u => u.id == id);
            oldUser.email = userChanged.email;
            oldUser.password = userChanged.password;
            oldUser.firstName = userChanged.firstName;
            oldUser.lastName = userChanged.confirmedPassword;
            oldUser.grants = userChanged.grants;
            context.SaveChanges();
            transaction.Commit();
            return "Utilisateur modifié";
        }
        catch (Exception)
        {
            transaction.Rollback();
            return "Erreur, l'utilisateur n'a pas été modifié";
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
            User userDelete = context.Users.Single(u => u.id == id);
            context.Users.Remove(userDelete);
            context.SaveChanges();
            transaction.Commit();
            return "Utilisateur supprimé";
        }
        catch (Exception)
        {
            transaction.Rollback();
            return "Erreur, l'utilisateur n'a pas été supprimé";
        }
    }

}

