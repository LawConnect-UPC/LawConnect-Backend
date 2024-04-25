using Lawyeed.API.Lawyeed.Domain.Models;

namespace Lawyeed.API.Lawyeed.Resources;

public class MessageResource
{
    public int Id { get; set; }
    public string MessageToSend { get; set; }
    
    public ConsultResource Consult { get; set; }   
    public PersonResource Person { get; set; }
}