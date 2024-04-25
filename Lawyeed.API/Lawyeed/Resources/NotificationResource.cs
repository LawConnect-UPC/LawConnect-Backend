using Lawyeed.API.Lawyeed.Domain.Models;

namespace Lawyeed.API.Lawyeed.Resources;

public class NotificationResource
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    
    public ConsultResource Consult { get; set; }
    
    public PersonResource Person { get; set; }
}