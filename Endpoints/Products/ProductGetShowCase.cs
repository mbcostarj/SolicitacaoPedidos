using Microsoft.AspNetCore.Authorization;
using solicitacao_pedidos.Infra.Data;

namespace solicitacao_pedidos.Endpoints.Products;

public class ProductGetShowCase
{
    public static string Template => "/products/showcase";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;
    
    [AllowAnonymous]
    public static IResult Action(ApplicationDbContext context, int page = 1, int row = 5, string orderBy = "name")
    {

        if (row > 20)
            return Results.Problem(title: "O atributo row deve ser menor que 20", statusCode: 400);

        var queryBase = context.Products.Include(p => p.Category)
            .Where(p => p.HasStock && p.Category.Active);

        if (orderBy == "name")
            queryBase = queryBase.OrderBy(p => p.Name);
        else if (orderBy == "price")
            queryBase = queryBase.OrderBy(p => p.Price);
        else
            return Results.Problem(title: "O parametro 'orderBy' deve ser 'price' ou 'name' ", statusCode: 400);

        var queryFilter = queryBase.Skip((page - 1) * row).Take(row);
        var products    = queryFilter.ToList();
        var results     = products.Select(p => new ProductResponse(p.Id, p.Name, p.Category.Name, p.Description, p.Price, p.HasStock, p.Active ));

        return Results.Ok(results); 
    
    }
}
