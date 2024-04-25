namespace Lawyeed.API.Lawyeed.Domain.Models;

public class Person
{
    public int Id { get; set; }
    public string FisrtName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Description { get; set; }
    public string UrlImage { get; set; }
    public string Type { get; set; }
    
    public IList<PersonPlan> PersonPlans { get; set; } = new List<PersonPlan>();
    
    public IList<Consult> ConsultsClient { get; set; } = new List<Consult>();
    public IList<Consult> ConsultsLawyer { get; set; } = new List<Consult>();
    public IList<Notification> Notifications { get; set; } = new List<Notification>();
    public IList<Message> Messages { get; set; } = new List<Message>();

    
    
}