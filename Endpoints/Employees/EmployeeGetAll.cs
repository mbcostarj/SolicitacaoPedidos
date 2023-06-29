using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using solicitacao_pedidos.Infra.Data;
using System.Security.Claims;

namespace solicitacao_pedidos.Endpoints.Employees;

public class CategoryGetAll
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "Employee001Policy")]
    public static async Task<IResult> Action(int? page, int? rows, QueryAllUsersWithClaimName queryGetAll)
    {

        var result = await queryGetAll.Execute(page.Value, rows.Value);
        return Results.Ok(result);
    }
}
