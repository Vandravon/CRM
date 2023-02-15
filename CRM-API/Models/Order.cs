using System.Text.Json.Serialization;

namespace CRM_API.Models;

public class Order 
{

    public int id {get; set;}
    public string typePresta {get; set;} = null!;
    public string client {get; set;} = null!;
    public int nbJour {get; set;}
    public double tjmHt {get; set;}
    public double tva {get; set;}
    public string state {get; set;} = null!;
    public string comment {get; set;} = null!;
    public int client_id {get; set;}
    
    [JsonIgnore]
    public Client? Client {get; set;}

    public Order()
    {

    }

    public Order(string typePresta, int nbJour, double tjmHt, double tva, string state, string comment, int client_id)
    {
        this.typePresta = typePresta;
        this.nbJour = nbJour;
        this.tjmHt = tjmHt;
        this.tva = tva;
        this.state = state;
        this.comment = comment;
        this.client_id = client_id;
    }

}