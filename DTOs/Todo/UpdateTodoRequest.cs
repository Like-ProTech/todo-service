using System.ComponentModel.DataAnnotations;
using Todo_Service.Enums;

namespace Todo_Service.DTOs.Todo
{
    public class UpdateTodoRequest
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(200, ErrorMessage = "Title must not exceed 200 characters.")]
        public string Title { get; set; }

        [StringLength(1000, ErrorMessage = "Description must not exceed 1000 characters.")]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateLimite { get; set; }

        [EnumDataType(typeof(TodoStats), ErrorMessage = "Status must be a valid TodoStats value.")]
        public TodoStats Status { get; set; }
    }
}
