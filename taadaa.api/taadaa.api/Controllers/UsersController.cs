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
using taadaa.api.DTO;
using taadaa.api.Models;

namespace taadaa.api.Controllers
{
    public class UsersController : ApiController
    {
        private TaadaaContext db = new TaadaaContext();

        // GET api/Users
        public IQueryable<UserDTO> GetUsers()
        {
            var users = from u in db.Users.Include(x => x.TodoLists)
                        select new UserDTO()
                        {
                            id = u.Id,
                            name = u.Name,
                            email = u.Email,
                            username = u.UserName
                        };
            return users; 
        }

        // GET api/Users/5
        [ResponseType(typeof(UserDTO))]
        public async Task<IHttpActionResult> GetUser(int id)
        {
            User user = await db.Users.Include(x => x.TodoLists).FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(new UserDTO
            {
                id = user.Id,
                name = user.Name,
                email = user.Email,
                lists = user.TodoLists.Select(t => new TodoListDTO() { id = t.Id, name = t.Name }).ToList<TodoListDTO>(),
                username = user.UserName
            });
        }

        // PUT api/Users/5
        public async Task<IHttpActionResult> PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST api/Users
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(user);
            await db.SaveChangesAsync();

            var dto = new UserDTO()
            {
                id = user.Id,
                name = user.Name,
                email = user.Email,
                username = user.UserName
            };

            return CreatedAtRoute("DefaultApi", new { id = user.Id }, dto);
        }

        // DELETE api/Users/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> DeleteUser(int id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            await db.SaveChangesAsync();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}