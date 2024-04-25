namespace Lawyeed.API.Lawyeed.Domain.Models;

public class PersonLawyer : Person
{
    public string Specialty { get; set; }
    public int WonCases { get; set; }
    public int TotalCases { get; set; }
    public int LostCases { get; set; }

}
