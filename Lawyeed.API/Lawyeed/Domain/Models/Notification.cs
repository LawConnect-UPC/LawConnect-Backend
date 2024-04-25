namespace Lawyeed.API.Lawyeed.Domain.Models;

public class Notification
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    
    public int ConsultId { get; set; }
    public Consult Consult { get; set; }
    
    public int PersonId { get; set; }
    public Person Person { get; set; }
    
}