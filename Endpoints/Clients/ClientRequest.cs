namespace solicitacao_pedidos.Endpoints.Clients;

public record ClientRequest(string Email, string Password, string Name, string Cpf);

