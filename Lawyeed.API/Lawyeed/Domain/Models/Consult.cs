namespace Lawyeed.API.Lawyeed.Domain.Models;

public class Consult
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string State { get; set; }
    
    public int ClientId { get; set; }
    public Person Client { get; set; }
    
    public int LawyerId { get; set; }
    public Person Lawyer { get; set; }

    public IList<Notification> Notifications { get; set; } = new List<Notification>();
    public IList<Message> Messages { get; set; } = new List<Message>();

}