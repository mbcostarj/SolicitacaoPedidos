﻿using Flunt.Notifications;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace solicitacao_pedidos.Endpoints;

public static class ProblemDetailExtensions
{ 
    public static Dictionary<string, string[]> ConvertToProblemDetails(this IReadOnlyCollection<Notification> notifications)
    {
        return notifications
                .GroupBy(g => g.Key)
                .ToDictionary(g => g.Key, g => g.Select(x => x.Message).ToArray());
    }

    public static Dictionary<string, string[]> ConvertToProblemDetails(this IEnumerable<IdentityError> error)
    {
        var dictionary = new Dictionary<string, string[]>();
        dictionary.Add("Error", error.Select(e => e.Description).ToArray());
        return dictionary;
    }
}
