using Flunt.Notifications;

namespace solicitacao_pedidos.Domain;

public abstract class Entity : Notifiable<Notification>
{

    public Entity() {
        Id = Guid.NewGuid();
    } 

    public Guid Id { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string EditedBy { get; set; }
    public DateTime EditedDate { get; set; }
    public bool Active { get; set; } = true;

    /*public Entity(Guid id, string createdBy, DateTime createdDate, string editedBy, DateTime editedDate, bool active)
    {
        Id = id;
        CreatedBy = createdBy;
        CreatedDate = createdDate;
        EditedBy = editedBy;
        EditedDate = editedDate;
        Active = active;
    }*/
}
