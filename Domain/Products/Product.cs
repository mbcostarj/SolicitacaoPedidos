using Flunt.Validations;
using solicitacao_pedidos.Domain.Orders;

namespace solicitacao_pedidos.Domain.Products;

public class Product : Entity
{
    public string Name { get; private set; }
    public Guid CategoryId { get; private set; }
    public Category Category  { get; private set; }
    public string Description { get; private set;}
    public decimal Price { get; private set; }
    public bool HasStock { get; private set; }
    public bool Active { get; private set; } = true;
    public ICollection<Order> Orders { get; internal set; }

    private Product() { }

    public Product(string name, Category category, string description, decimal price, bool hasStock, string createdBy)
    {
        Name = name;
        Category = category;
        Description = description;
        Price = price;
        HasStock = hasStock;

        CreatedBy = createdBy;
        EditedBy = createdBy;
        CreatedDate = DateTime.Now;
        EditedDate = DateTime.Now;

        Validate();
    }

    private void Validate()
    {
        var contract = new Contract<Product>()
                .IsNotNullOrEmpty(Name, "Name")
                .IsGreaterOrEqualsThan(Name, 3, "Name")
                .IsNotNull(Category, "Category", "Categoria não encontrada.")
                .IsNotNullOrEmpty(Description, "Description")
                .IsGreaterOrEqualsThan(Price, 1, "Price")
                .IsGreaterOrEqualsThan(Description, 3, "Description")
                .IsNotNullOrEmpty(CreatedBy, "CreatedBy")
                .IsNotNullOrEmpty(EditedBy, "EditedBy");
        AddNotifications(contract);
    }
    public void EditInfo(string name, Category category, string description, bool hasStock, string createdBy)
    {
        Name = name;
        Category = category;
        Description = description;
        HasStock = hasStock;

        CreatedBy = createdBy;
        EditedBy = createdBy;
        CreatedDate = DateTime.Now;
        EditedDate = DateTime.Now;

        Validate();
    }
}
