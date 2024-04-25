
using Lawyeed.API.Lawyeed.Domain.Models;

namespace Lawyeed.API.Lawyeed.Domain.Models;

public class PersonPlan
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    
    //Relations
    public int PersonId { get; set; }
    public Person Person { get; set; }
    public int PlanId { get; set; }
    public Plan Plan { get; set; }
}