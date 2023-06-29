using Microsoft.AspNetCore.Authorization;
using solicitacao_pedidos.Domain.Products;
using solicitacao_pedidos.Infra.Data;

namespace solicitacao_pedidos.Endpoints.Categories;

public class EmployeeGetAll
{
    public static string Template => "/categories";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "Employee001Policy")]
    public static IResult Action(ApplicationDbContext context)
    {
        var categories = context.Category.ToList();
        var response = categories.Select(c => new CategoryResponse { Id = c.Id, Name = c.Name, Active = c.Active });

        return Results.Ok(response);
    
    }
}
