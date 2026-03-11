using Todo_Service.DTOs.Todo;
using Todo_Service.Enums;
using Todo_Service.Models;

public static class TodoMapper
{
    public static IndexTodo ToIndexList(Todo model)
    {
        return new IndexTodo
        {
            Id = model.Id,
            Title = model.Title,
            Description = model.Description,
            Status = model.Status,
            DateLimite = model.DateLimite
        };
    }

    public static Todo FromRequestToModel(CreateTodoRequest dto)
    {
        return new Todo
        {
            Title = dto.Title,
            Description = dto.Description,
            DateLimite = dto.DateLimite,
            Status = dto.Status,
        };
    }

    public static CreateTodoResponse ToCreateResponse(Todo model)
    {
        return new CreateTodoResponse
        {
            Id = model.Id,
            Title = model.Title,
            Status = model.Status,
            Description = model.Description,
            DateLimite = model.DateLimite
        };
    }

    public static Todo FromUpdateRequestToModel(UpdateTodoRequest dto)
    {
        return new Todo
        {
            Id = dto.Id,
            Title = dto.Title,
            Description = dto.Description,
            DateLimite = dto.DateLimite,
            Status = dto.Status,
        };
    }
}