using Microsoft.AspNetCore.Mvc;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private static List<TodoItem> todoItems = new List<TodoItem>();
    private static int nextId = 1;


    [HttpGet]
    public ActionResult<IEnumerable<TodoItem>> GetTodo()
    {
        if (todoItems.Count == 0)
        {
            var initialTodoItem = new TodoItem
            {
                Title = "test to do list",
                IsCompleted = true
            };
            initialTodoItem.Id = nextId++;
            todoItems.Add(initialTodoItem);
        }
        return Ok(todoItems);
    }

    [HttpGet("{id}")]
    public ActionResult GetTodoID(int id)
    {
        var todoItem = todoItems.FirstOrDefault(item => item.Id == id);
        if (todoItem == null)
        {
            return NotFound();
        }
        return Ok(todoItem);
    }

    [HttpPost]
    public ActionResult<TodoItem> AddIodo([FromForm] TodoItem item)
    {
        item.Id = nextId++;
        todoItems.Add(item);
        return CreatedAtAction(nameof(GetTodo), new { id = item.Id }, item);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateIdod(int id, [FromForm] TodoItem item)
    {
        var existingItem = todoItems.Find(i => i.Id == id);
        if (existingItem == null)
        {
            return NotFound();
        }
        existingItem.Title = item.Title;
        existingItem.IsCompleted = item.IsCompleted;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteTodo(int id)
    {
        var itemToRemove = todoItems.Find(i => i.Id == id);
        if (itemToRemove == null)
        {
            return NotFound();
        }
        todoItems.Remove(itemToRemove);
        return NoContent();
    }
}

