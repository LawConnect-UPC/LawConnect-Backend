using Lawyeed.API.Lawyeed.Domain.Models;

namespace Lawyeed.API.Lawyeed.Resources;

public class ConsultResource
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string State { get; set; }
    
    public PersonResource Client { get; set; }
    
    public PersonResource Lawyer { get; set; }
}