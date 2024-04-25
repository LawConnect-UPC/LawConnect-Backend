namespace Lawyeed.API.Lawyeed.Resources;

public class SaveConsultResource
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string State { get; set; }
    
    public int ClientId { get; set; }
    
    public int LawyerId { get; set; }
}