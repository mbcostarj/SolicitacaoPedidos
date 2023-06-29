using Flunt.Validations;
using System.ComponentModel.DataAnnotations;

namespace solicitacao_pedidos.Domain.Products;

public class Category : Entity
{
    public string Name { get; private set; }
	public bool Active { get; private set; }

	public Category(string name, string createdBy, string editedBy)
    {

        Name = name;
        Active = true;
        CreatedBy = createdBy;
        EditedBy = editedBy;
        CreatedDate = DateTime.Now;
        EditedDate = DateTime.Now;

        Validate();

    }

    private void Validate()
    {
        var contract = new Contract<Category>()
            .IsNotNullOrEmpty(Name, "Name", "Nome é obrigatório.")
            .IsGreaterOrEqualsThan(Name, 3, "Name", "O nome da categoria precisa ter mais que 3 caracteres.");
        AddNotifications(contract);
    }

    public void EditInfo(string name, bool active, string editedBy)
	{
		Active = active;
        Name = name;
        EditedBy = editedBy;
        EditedDate = DateTime.Now;

        Validate();
    }
}
