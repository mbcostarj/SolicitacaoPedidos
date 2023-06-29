namespace solicitacao_pedidos.Endpoints.Orders;

public record OrderRequest(List<Guid> ProductIds, string DeliveryAddress);