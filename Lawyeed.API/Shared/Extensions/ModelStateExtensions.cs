using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Lawyeed.API.Shared.Extensions;

public static class ModelStateExtensions
{
    public static List<string> GetErrorMessages(this ModelStateDictionary dictionary)
    {
        return dictionary.SelectMany(many => many.Value.Errors)
            .Select(many => many.ErrorMessage)
            .ToList();
    }
}