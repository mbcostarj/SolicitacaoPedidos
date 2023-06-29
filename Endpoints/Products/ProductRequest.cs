﻿namespace solicitacao_pedidos.Endpoints.Products;

public record ProductRequest(string Name, Guid CategoryId, string Description, decimal Price, bool HasStock, bool Active);
