using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using solicitacao_pedidos.Infra.Data;
using System.Security.Claims;

namespace solicitacao_pedidos.Endpoints.Employees;

public class ProductGetSoldReport
{
    public static string Template => "/products/soldreport";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "Employee001Policy")]
    public static async Task<IResult> Action(QueryProductSoldReport productSoldReport)
    {
        var result = await productSoldReport.Execute();
        return Results.Ok(result);
    }
}
