using Todo_Service.Enums;

namespace Todo_Service.DTOs.Todo
{
    public class IndexTodo
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; }
        public DateTime? DateLimite { get; set; }
        public TodoStats Status { get; set; }
    }
}
