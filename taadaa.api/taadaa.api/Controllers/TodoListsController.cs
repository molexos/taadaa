using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using taadaa.api.Models;

namespace taadaa.api.Controllers
{
    public class TodoListsController : ApiController
    {
        private TaadaaContext db = new TaadaaContext();

        // GET api/TodoLists
        public IQueryable<TodoList> GetTodoLists()
        {
            return db.TodoLists;
        }

        // GET api/TodoLists/5
        [ResponseType(typeof(TodoList))]
        public async Task<IHttpActionResult> GetTodoList(int id)
        {
            TodoList todolist = await db.TodoLists.FindAsync(id);
            if (todolist == null)
            {
                return NotFound();
            }

            return Ok(todolist);
        }

        // PUT api/TodoLists/5
        public async Task<IHttpActionResult> PutTodoList(int id, TodoList todolist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != todolist.Id)
            {
                return BadRequest();
            }

            db.Entry(todolist).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoListExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/TodoLists
        [ResponseType(typeof(TodoList))]
        public async Task<IHttpActionResult> PostTodoList(TodoList todolist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TodoLists.Add(todolist);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = todolist.Id }, todolist);
        }

        // DELETE api/TodoLists/5
        [ResponseType(typeof(TodoList))]
        public async Task<IHttpActionResult> DeleteTodoList(int id)
        {
            TodoList todolist = await db.TodoLists.FindAsync(id);
            if (todolist == null)
            {
                return NotFound();
            }

            db.TodoLists.Remove(todolist);
            await db.SaveChangesAsync();

            return Ok(todolist);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TodoListExists(int id)
        {
            return db.TodoLists.Count(e => e.Id == id) > 0;
        }
    }
}