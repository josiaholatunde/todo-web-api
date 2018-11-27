using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;
            if(_context.TodoItems.Count() == 0 ) {
                _context.TodoItems.Add(new TodoItem{Name="Code", IsComplete=true});
                _context.SaveChanges();
            }
            
            
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> Get()
        {
            return _context.TodoItems.ToList();
        }

        // GET api/values/5
        [HttpGet("{id}", Name="GetTodo")]
        public ActionResult<TodoItem> Get(int id)
        {
            var item = _context.TodoItems.Find(id);
            if (item != null) {
                return item;
            }
            return NotFound();
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] TodoItem item)
        {
            if(item != null) {
                _context.TodoItems.Add(item);
                _context.SaveChanges();
                return CreatedAtRoute("GetTodo", new { Id = item.Id},item);
            }
            return BadRequest(ModelState);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] TodoItem item)
        {
            if (item != null) {
                var todo = _context.TodoItems.Find(id);
                if(todo != null) {
                    todo.IsComplete = item.IsComplete;
                    todo.Name = item.Name ?? todo.Name;
                    _context.TodoItems.Update(todo);
                    _context.SaveChanges();
                    return NoContent();
                }
            }
            return NotFound();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
