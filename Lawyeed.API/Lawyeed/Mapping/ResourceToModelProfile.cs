using AutoMapper;
using Lawyeed.API.Lawyeed.Domain.Models;
using Lawyeed.API.Lawyeed.Resources;

namespace Lawyeed.API.Lawyeed.Mapping;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<SavePersonResource, Person>();
        CreateMap<SavePersonLawyerResource, PersonLawyer>();
        CreateMap<SavePlanResource, Plan>();
        CreateMap<SavePersonPlanResource, PersonPlan>();
        CreateMap<SaveConsultResource, Consult>();
        CreateMap<SaveNotificationResource, Notification>();
        CreateMap<SaveMessageResource, Message>();

    }
}