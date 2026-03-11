using Todo_Service.Enums;

namespace Todo_Service.Models
{
    public class Todo
    {
        public Guid Id { get; set; } = Guid.NewGuid(); //Auto increment here
        public string Title { get; set; } 
        public string Description { get; set; } 
        public DateTime? DateLimite { get; set; } 
        public TodoStats Status { get; set; }
        public Guid UserId { get; set; } = Guid.NewGuid(); //Static for now
    }
}
