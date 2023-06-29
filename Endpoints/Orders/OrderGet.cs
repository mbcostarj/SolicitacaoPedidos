using Microsoft.AspNetCore.Authorization;
using solicitacao_pedidos.Endpoints.Products;
using solicitacao_pedidos.Infra.Data;
using System.Security.Claims;

namespace solicitacao_pedidos.Endpoints.Orders;

public class OrderGet
{
    public static string Template => "/orders/{id}";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize]
    public static async Task<IResult> Action( 
        Guid Id, 
        HttpContext http, 
        ApplicationDbContext context, 
        UserManager<IdentityUser> userManager)
    {
        var loggedClientId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier);
        var employeeCode = http.User.Claims.FirstOrDefault(c => c.Type == "EmployeeCode");

        var order = context.Orders.Include(o => o.Products).FirstOrDefault(o => o.Id == Id);

        if (order.ClientId != loggedClientId.Value && employeeCode == null)
            return Results.Forbid();

        var client = await userManager.FindByIdAsync(order.ClientId);
        var productResponse = order.Products.Select(p => new OrderProduct(p.Id, p.Name));
        var orderResponse = new OrderResponse(order.Id, client.Email, productResponse, order.DeliveryAddress);

        return Results.Ok(orderResponse);
    }
}
