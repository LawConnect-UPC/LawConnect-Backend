namespace Lawyeed.API.Lawyeed.Resources;

public class SaveNotificationResource
{
    public string Title { get; set; }
    public string Description { get; set; }
    
    public int ConsultId { get; set; }

    public int PersonId { get; set; }
}