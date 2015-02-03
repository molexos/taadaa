using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using taadaa.api.DTO;
using taadaa.api.Models;

namespace taadaa.api.Controllers
{
    [RoutePrefix("tasklists")]
    public class TodoListsController : ApiController
    {
        private TaadaaContext db = new TaadaaContext();

        #region Tasklist management

        // GET /tasklists
        [Route("")]
        [HttpGet]
        [Authorize]
        public IQueryable<TodoListDTO> GetTodoLists()
        {
            //var user = Request.GetRequestContext().Principal as ClaimsPrincipal;
            return db.TodoLists.Select(
                domain => new TodoListDTO()
                {
                    id = domain.Id
                });
        }

        // GET /taklists/5
        [Route("{id:int}")]
        [HttpGet]
        [ResponseType(typeof(TodoListDTO))]
        public async Task<IHttpActionResult> GetTodoList(int id)
        {
            TodoList todolist = await db.TodoLists.FindAsync(id);
            if (todolist == null)
            {
                return NotFound();
            }
          
            db.Entry(todolist).Collection(x => x.Tasks).Load();
            
            return Ok(new TodoListDTO(todolist));
        }

        // PUT tasklists/5
        [Route("{id:int}")]
        [HttpPut]
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

        // POST tasklists
        [Route("")]
        [HttpPost]
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

        // DELETE tasklists/5
        [Route("{id:int}")]
        [HttpDelete]
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

        #endregion

        #region Task management

        [Route("{id:int}/tasks")]
        [ResponseType(typeof(IQueryable<TodoDTO>))]
        public async Task<IHttpActionResult> GetTasks(int id)
        {
            var todolist = await db.TodoLists.Include(x => x.Tasks).FirstOrDefaultAsync<TodoList>(x => x.Id == id);
            if (todolist == null)
            {
                return NotFound();
            }

            return Ok(todolist.Tasks.Select(x => new TodoDTO { id = x.Id, description = x.Description, done = x.IsDone, notes = x.Notes }));
        }

        #endregion

        #region Private
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

        #endregion
    }
}