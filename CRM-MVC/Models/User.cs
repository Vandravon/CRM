namespace CRM_MVC.Models;

public class User
{
    public int id {get; set;}
    public string email {get; set;} = null!;
    public string password {get; set;} = null!;
    public string firstName {get; set;} = null!;
    public string lastName {get; set;} = null!;
    public string confirmedPassword {get; set;} = null!;
    public string grants {get; set;} = null!;
    public List<Client> clients { get; } = new();


    public User()
    {

    }
    
    public User(string email, string password, string firstName, string lastName, string confirmedPassword, string grants)
    {
        this.email = email;
        this.password = password;
        this.firstName = firstName;
        this.lastName = lastName;
        this.confirmedPassword = confirmedPassword;
        this.grants = grants;
    }

}