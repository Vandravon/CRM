using Microsoft.EntityFrameworkCore;
namespace CRM_API.Models;

    public class CrmContext : DbContext
{
    public virtual DbSet<User> Users {get; set;}
    public virtual DbSet<Client> Clients {get; set;}
    public virtual DbSet<Order> Orders {get; set;}

    public CrmContext()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
        Console.WriteLine("DB lancée!");

        if (Users.Count() == 0)
        {
            Users.Add(new User() { email = "john@example.com", password = "$2a$10$7z..gPxlD9XzBbMNCFoVReCNdJ1RwO6x5j5G5MP1Q5F6lJfXq3pp2", firstName = "John", lastName = "Smith", confirmedPassword = "abc123", grants = "ADMIN" });
            Users.Add(new User() { email = "sarah@example.com", password = "$2a$10$O8KV1WfT13y4HQRJ0cSv8OwT4Yt22sTvH4TK4Ly4oeGtWVfBzm.p6", firstName = "Sarah", lastName = "Johnson", confirmedPassword = "def456", grants = "USER" });
            Users.Add(new User() { email = "mark@example.com", password = "$2a$10$JWjyGX33DfyNql.MFbbJ..6RCE/HZ.waA6U5fXU3h6lIAKjyL6DNe", firstName = "Mark", lastName = "Davis", confirmedPassword = "123abc", grants = "USER" });
            SaveChanges();
        }

        if (Clients.Count() == 0)
        {
            Clients.Add(new Client() { name="Air France-KLM", state="ACTIVE", tva=20, totalCaHt=500000, comment="Compagnie aérienne française", user_id=2 });
            Clients.Add(new Client() { name="Renault", state="ACTIVE", tva=10, totalCaHt=150000, comment="Fabricant de voitures et de véhicules utilitaires", user_id=3 });
            Clients.Add(new Client() { name="Carrefour", state="ACTIVE", tva=20, totalCaHt=250000, comment="Chaîne de supermarchés et d'hypermarchés", user_id=2 });
            Clients.Add(new Client() { name="TotalEnergies", state="INACTIVE", tva=20, totalCaHt=1000000, comment="Compagnie pétrolière et gazière française", user_id=3 });
            Clients.Add(new Client() { name="Société Générale", state="ACTIVE", tva=20, totalCaHt=300000, comment="Banque et institution financière française", user_id=3 });
            SaveChanges();
        }

        if (Orders.Count() == 0)
        {
            Orders.Add(new Order() { typePresta="développement web", client="Acme Corp", nbJour=10, tjmHt=1200, tva=20, state="CONFIRMED", comment="Création d'un site de commerce électronique", client_id=4 });
            Orders.Add(new Order() { typePresta="formation", client="XYZ Inc.", nbJour=7, tjmHt=800, tva=10, state="OPTION", comment="Formation sur les dernières technologies en matière de sécurité informatique", client_id=5 });
            Orders.Add(new Order() { typePresta="audit", client="ABC Company", nbJour=3, tjmHt=1500, tva=20, state="CONFIRMED", comment="Audit de sécurité informatique pour une banque régionale", client_id=1 });
            Orders.Add(new Order() { typePresta="développement mobile", client="123 Industries", nbJour=14, tjmHt=1000, tva=20, state="OPTION", comment="Développement d'une application de suivi de la santé pour les patients atteints de cancer", client_id=3 });
            Orders.Add(new Order() { typePresta="consulting", client="Beta Ltd.", nbJour=5, tjmHt=2000, tva=20, state="CONFIRMED", comment="Conseils en matière de stratégie d'entreprise et de développement de produits", client_id=2 });
            SaveChanges();
        }
    }	

    protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
    {
    optionBuilder.UseMySQL("Server=localhost;Database=CRM;User=root;Password=admin1234");
    }

    protected override void OnModelCreating(ModelBuilder modelbuilder)
    {
        modelbuilder.Entity<User>(entity => 
        {
            entity.HasKey(u => u.id).HasName("PRIMARY");
        });

        modelbuilder.Entity<Client>(entity =>
        {
            entity.HasKey(c => c.id).HasName("PRIMARY");
            entity.HasOne(c => c.User).WithMany(u => u.clients).HasForeignKey(c => c.user_id);
        });

        modelbuilder.Entity<Order>(entity =>
        {
            entity.HasKey(o => o.id).HasName("PRIMARY");
            entity.HasOne(o => o.Client).WithMany(c => c.orders).HasForeignKey(o => o.client_id);
        }
        );
    }
}
