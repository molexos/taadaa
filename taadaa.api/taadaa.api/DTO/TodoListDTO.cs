using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace taadaa.api.DTO
{
    public class TodoListDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public ICollection<TodoDTO> tasks { get; set; }
        public UserDTO owner { get; set; }
    }
}