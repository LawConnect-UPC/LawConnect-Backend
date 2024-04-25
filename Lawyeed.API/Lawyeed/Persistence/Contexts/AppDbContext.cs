using Lawyeed.API.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using Lawyeed.API.Lawyeed.Domain.Models;

namespace Lawyeed.API.Lawyeed.Persistence.Contexts;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Person> Persons { get; set; }
    public DbSet<PersonLawyer> Lawyers { get; set; }
    
    public DbSet<Plan> Plans { get; set; }
    public DbSet<PersonPlan> PersonPlans { get; set; }
    public DbSet<Consult> Consults { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Message> Messages { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        //Person
        builder.Entity<Person>().ToTable("Persons");
        builder.Entity<Plan>().ToTable("Plans");
        builder.Entity<PersonPlan>().ToTable("PersonPlan");

        
        //Relations
        builder.Entity<Person>().HasDiscriminator(p => p.Type)
            .HasValue<Person>("client")
            .HasValue<PersonLawyer>("lawyer");
        builder.Entity<PersonPlan>()
            .HasOne(p => p.Plan).WithMany(p => p.PersonPlans).HasForeignKey(p => p.PlanId);
        builder.Entity<PersonPlan>()
            .HasOne(p => p.Person).WithMany(p => p.PersonPlans).HasForeignKey(P => P.PersonId);
        //Person
        builder.Entity<Person>().HasKey(p => p.Id);
        builder.Entity<Person>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Person>().Property(p => p.FisrtName).IsRequired().HasMaxLength(40);
        builder.Entity<Person>().Property(p => p.LastName).IsRequired().HasMaxLength(40);
        builder.Entity<Person>().Property(p => p.Email).IsRequired().HasMaxLength(50);
        builder.Entity<Person>().Property(p => p.Password).IsRequired().HasMaxLength(40);
        builder.Entity<Person>().Property(p => p.Description).IsRequired().HasMaxLength(200);
        builder.Entity<Person>().Property(p => p.UrlImage).IsRequired().HasMaxLength(100);
        builder.Entity<Person>().Property(p => p.Type).IsRequired().HasMaxLength(20);
        
        //Person Lawyer
        builder.Entity<PersonLawyer>().Property(l => l.Specialty).HasMaxLength(30);
        builder.Entity<PersonLawyer>().Property(l => l.WonCases);
        builder.Entity<PersonLawyer>().Property(l => l.TotalCases);
        builder.Entity<PersonLawyer>().Property(l => l.LostCases);
        
        //Plans
        builder.Entity<Plan>().HasKey(p => p.Id);
        builder.Entity<Plan>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();;
        builder.Entity<Plan>().Property(p => p.Name).IsRequired().HasMaxLength(70);
        builder.Entity<Plan>().Property(p => p.Description).IsRequired().HasMaxLength(70);
        builder.Entity<Plan>().Property(p => p.Price).IsRequired();
        
        //Consult
        builder.Entity<Consult>()
            .HasOne(p => p.Client)
            .WithMany(p => p.ConsultsClient)
            .HasForeignKey(p => p.ClientId);
        
        builder.Entity<Consult>()
            .HasOne(p => p.Lawyer)
            .WithMany(p => p.ConsultsLawyer)
            .HasForeignKey(p => p.LawyerId);
        
        
        builder.Entity<Consult>().ToTable("Consults");
        builder.Entity<Consult>().HasKey(p => p.Id);
        builder.Entity<Consult>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Consult>().Property(p => p.Title).IsRequired().HasMaxLength(40);
        builder.Entity<Consult>().Property(p => p.Description).IsRequired().HasMaxLength(200);
        builder.Entity<Consult>().Property(p => p.State).IsRequired().HasMaxLength(20);
        
        
        //Notification
        builder.Entity<Notification>()
            .HasOne(p => p.Person)
            .WithMany(p => p.Notifications)
            .HasForeignKey(p => p.PersonId);
        
        builder.Entity<Notification>()
            .HasOne(p => p.Consult)
            .WithMany(p => p.Notifications)
            .HasForeignKey(p => p.ConsultId);

        builder.Entity<Notification>().ToTable("Notifications");
        builder.Entity<Notification>().HasKey(p => p.Id);
        builder.Entity<Notification>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Notification>().Property(p => p.Title).IsRequired().HasMaxLength(40);
        builder.Entity<Notification>().Property(p => p.Description).IsRequired().HasMaxLength(200);

        // Message
        builder.Entity<Message>()
            .HasOne(p => p.Consult)
            .WithMany(p => p.Messages)
            .HasForeignKey(p => p.ConsultId);

        builder.Entity<Message>()
            .HasOne(p => p.Person)
            .WithMany(p => p.Messages)
            .HasForeignKey(p => p.PersonId);

        builder.Entity<Message>().ToTable("Messages");
        builder.Entity<Message>().HasKey(p => p.Id);
        builder.Entity<Message>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Message>().Property(p => p.MessageToSend).IsRequired().HasMaxLength(300);

        //Apply Naming Conventions
        builder.UseSnakeCaseNamingConvention();
    }
}