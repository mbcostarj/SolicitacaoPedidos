using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using solicitacao_pedidos.Domain.Orders;
using solicitacao_pedidos.Domain.Products;
using solicitacao_pedidos.Domain.Users;
using solicitacao_pedidos.Infra.Data;
using System.Security.Claims;
using static System.Net.WebRequestMethods;

namespace solicitacao_pedidos.Endpoints.Orders;

public class OrderPost
{
    public static string Template => "/orders";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "CpfPolicy")]
    public static async Task<IResult> Action(OrderRequest orderRequest, HttpContext http, ApplicationDbContext context)
    {
        var clientId   = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var clientName = http.User.Claims.First(c => c.Type == "Name").Value;

        /*if ( orderRequest.ProductIds == null || !orderRequest.ProductIds.Any() )
            return Results.BadRequest("Você não adicionou nenhum produto ao seu pedido.");
        if ( string.IsNullOrEmpty(orderRequest.DeliveryAddress) )
            return Results.BadRequest("Informe o endereço de enterga.");*/
        List<Product> productsFound = null;

        if ( orderRequest.ProductIds != null || orderRequest.ProductIds.Any() )
            productsFound = context.Products.Where(p => orderRequest.ProductIds.Contains(p.Id)).ToList();

        var order = new Order(clientId, clientName, productsFound, orderRequest.DeliveryAddress);
        if(!order.IsValid)
        {
            return Results.ValidationProblem(order.Notifications.ConvertToProblemDetails());
        }

        await context.Orders.AddAsync(order);
        await context.SaveChangesAsync();

        return Results.Created($"/orders/{order.Id}", order.Id);

    }
}
