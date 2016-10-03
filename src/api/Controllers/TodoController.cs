using Microsoft.AspNetCore.Mvc;
using TodoApi.Repositories;
using TodoApi.Models;
using System.Collections.Generic;

namespace TodoApi.Controllers
{
    /// <summary>
    /// Provides an API for managing TodoItem. Written with .NET Core Web API. 
    /// </summary>
    /// <remarks>
    /// This sample is from the canonical Web API tutorial on Microsoft's ASP.NET documentation page.
    /// <see href="https://docs.asp.net/en/latest/tutorials/first-web-api.html" />  
    /// </remarks>
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        
        private readonly ITodoRepository todoRepository;
        
        public TodoController(ITodoRepository todoRepository)
        {
            this.todoRepository = todoRepository;
        }

        [HttpGet]
        public IEnumerable<TodoItem> GetAll()
        {
            return todoRepository.GetAll();
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public IActionResult GetById(string id)
        {
            var item = todoRepository.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] TodoItem item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            todoRepository.Add(item);
            return CreatedAtRoute("GetTodo", new { id = item.Key }, item);
        }

        /// <summary>
        /// PUT takes the complete entity, therefore we verify the `id` in the 
        /// URI matches the `id` being updated in `item`.
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] TodoItem item)
        {
            if (item == null || item.Key != id)
            {
                return BadRequest();
            }

            var todo = todoRepository.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            todoRepository.Update(item);
            return new NoContentResult();
        }

        /// <summary>
        /// Patch accepts a partial object, so we do *not* verify `id` matches
        /// `item.Key`.
        /// </summary>
        [HttpPatch("{id}")]
        public IActionResult Patch(string id, [FromBody] TodoItem item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            var todo = todoRepository.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            item.Key = todo.Key;

            todoRepository.Update(item);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var todo = todoRepository.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            todoRepository.Remove(id);
            return new NoContentResult();
        }
    }
}