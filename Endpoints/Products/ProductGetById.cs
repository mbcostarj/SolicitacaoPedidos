using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using solicitacao_pedidos.Infra.Data;

namespace solicitacao_pedidos.Endpoints.Products;

public class ProductGetById
{
    public static string Template => "/products/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static IResult Action([FromRoute]Guid id, ApplicationDbContext context, HttpContext http)
    {
        var product = context.Products.Include(p => p.Category).Where(p => p.Id == id).ToList();
        var results = product.Select(p => new ProductResponse(p.Id, p.Name, p.Category.Name, p.Description, p.Price, p.HasStock, p.Active));
        return Results.Ok(results);

    }
}
