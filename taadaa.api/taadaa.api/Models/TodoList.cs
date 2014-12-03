using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace taadaa.api.Models
{
    public class TodoList
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Todo> Tasks { get; set; }

        public User Owner { get; set; }
    }
}