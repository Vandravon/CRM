using System.Text.Json.Serialization;

namespace CRM_API.Models;

public class Client 
{

    public int id {get; set;}
    public string name {get; set;} = null!;
    public string state {get; set;} = null!;
    public double tva {get; set;}
    public int totalCaHt {get; set;}
    public string comment {get; set;} = null!;
    public int user_id {get; set;}

    [JsonIgnore]
    public User? User {get; set;}
    public List<Order> orders { get; } = new();

    public Client()
    {

    }

    public Client(string name, string state, double tva, int totalCaHt, string comment, int user_id)
    {
        this.name = name;
        this.state = state;
        this.tva = tva;
        this.totalCaHt = totalCaHt;
        this.comment = comment;
        this.user_id = user_id;

    }

}