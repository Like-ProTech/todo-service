using Microsoft.AspNetCore.Mvc;
using Todo_Service.DTOs.API;
using Todo_Service.DTOs.Todo;
using Todo_Service.Models;
using Todo_Service.Repositories;

namespace Todo_Service.Controllers
{
    [ApiController]
    [Route("api/todos")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoRepository _todoRepository;
        public TodoController(ITodoRepository repo)
        {
            this._todoRepository = repo;
        }
        [HttpGet]

        public async Task< IActionResult> Get()
        {
            var todoList = (await this._todoRepository.GetAllTodos()).Select(t => TodoMapper.ToIndexList(t)).ToList();
            return Ok(new CustomAPIResponse<List<IndexTodo>> { Code = StatusCodes.Status200OK, Data = todoList, Message = "Data fetched with succesfully" });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTodoRequest todoRequest)
        {
            var created = await this._todoRepository.CreateTodo(TodoMapper.FromRequestToModel(todoRequest));
            var response = new CustomAPIResponse<Todo>
            {
                Code = StatusCodes.Status201Created,
                Data = created,
                Message = "Todo created successfully"
            };

            return CreatedAtAction(
                nameof(GetById),
                new { Uuid = created.Id },
                response
            );
        }
        [HttpGet("{Uuid}")]
        public IActionResult GetById(string Uuid)
        {
            var todo=this._todoRepository.GetById(Uuid);
            if (todo != null)
                return Ok(new CustomAPIResponse<Todo> { Code=StatusCodes.Status200OK , Data = todo , Message = $"Todo with {Uuid} found"});

            return NotFound(new CustomAPIResponse<Todo> { Code = StatusCodes.Status404NotFound, Data = todo, Message = $"Todo with {Uuid} was not found" });
        }
        [HttpPut("{Id}")]
        public async Task< IActionResult> Update(Guid Id , UpdateTodoRequest todoRequest)
        {
            if (!Id.Equals(todoRequest.Id))
                return BadRequest(new CustomAPIResponse<object> { Code = StatusCodes.Status400BadRequest, Data = null, Message = "Request wasnt good" });
            var updated = await this._todoRepository.UpdateTodo(TodoMapper.FromUpdateRequestToModel(todoRequest));
            if (updated == null)
                return NotFound(new CustomAPIResponse<object> { Code = StatusCodes.Status404NotFound, Data = null, Message = $"Todo with {Id} was not found" });
            return NoContent();
        }
        [HttpDelete("{Id}")]
        public IActionResult Delete([FromRoute] string Id)
        {
            if (this._todoRepository.DeleteTodo(Id))
                return NoContent();

            return NotFound(new CustomAPIResponse<object>
            {
                Code = StatusCodes.Status404NotFound,
                Data = null,
                Message = $"Todo with id {Id} not found"
            });
        }
       
    }
}
