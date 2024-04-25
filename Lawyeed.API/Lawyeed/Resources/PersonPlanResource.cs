namespace Lawyeed.API.Lawyeed.Resources;

public class PersonPlanResource
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public PersonResource Person { get; set; }
    public PlanResource Plan { get; set; }
}