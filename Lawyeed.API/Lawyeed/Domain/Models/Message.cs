namespace Lawyeed.API.Lawyeed.Domain.Models;

public class Message
{
    public int Id { get; set; }
    
    public string MessageToSend { get; set; }
    
    // Relations
    public int PersonId { get; set; }
    public Person Person { get; set; }
    
    public int ConsultId { get; set; }
    public Consult Consult { get; set; }
}