using AutoMapper;
using Lawyeed.API.Lawyeed.Domain.Models;
using Lawyeed.API.Lawyeed.Resources;


namespace Lawyeed.API.Lawyeed.Mapping;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<Person, PersonResource>();
        CreateMap<PersonLawyer, PersonLawyerResource>();
        CreateMap<Plan, PlanResource>();
        CreateMap<PersonPlan, PersonPlanResource>();
        CreateMap<Consult, ConsultResource>();
        CreateMap<Notification, NotificationResource>();
        CreateMap<Message, MessageResource>();

    }
}