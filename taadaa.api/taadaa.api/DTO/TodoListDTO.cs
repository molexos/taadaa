using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using taadaa.api.Models;

namespace taadaa.api.DTO
{
    public class TodoListDTO
    {
        public int? id { get; set; }
        public string name { get; set; }
        public ICollection<TodoDTO> tasks { get; set; }

        public TodoListDTO()
        {

        }

        public TodoListDTO(TodoList domain)
        {
            id = domain.Id;
            name = domain.Name;
            tasks = domain.Tasks.Select(x => new TodoDTO() { id = x.Id, description = x.Description, done = x.IsDone, notes = x.Notes }).ToList<TodoDTO>();
        }
    }
}