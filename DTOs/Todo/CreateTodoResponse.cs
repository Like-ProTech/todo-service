using Todo_Service.Enums;

namespace Todo_Service.DTOs.Todo
{
    public class CreateTodoResponse
    {
        public Guid Id { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DateLimite { get; set; }
        public TodoStats Status { get; set; }
        public DateTime CreatedAt { get; set; }= DateTime.Now;
    }
}
