namespace solicitacao_pedidos.Endpoints.Products;

public record ProductResponse(Guid id, string name, string category, string description, decimal Price, bool hasStock, bool active);
