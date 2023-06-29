using Dapper;
using Microsoft.Data.SqlClient;
using solicitacao_pedidos.Endpoints.Employees;
using solicitacao_pedidos.Endpoints.Products;

namespace solicitacao_pedidos.Infra.Data;

public class QueryProductSoldReport
{
    private readonly IConfiguration configuration;

    public QueryProductSoldReport(IConfiguration configuration)
	{
		this.configuration = configuration;
    }

    public async Task<IEnumerable<ProductSoldReportResponse>> Execute()
    {
        var db = new SqlConnection(configuration["Connection:Default"]);

        var query = @"select p.Id, p.Name, 
                    count(*) Amount
                    from Orders o 
                    inner join OrderProducts op on o.Id = op.OrdersId
                    inner join Products p on p.Id = op.ProductsId
                    group by p.Id, p.Name
                    order by AMount desc";

        return await db.QueryAsync<ProductSoldReportResponse>(query);
    }
}
