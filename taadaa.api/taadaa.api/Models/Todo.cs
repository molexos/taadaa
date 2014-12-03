using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace taadaa.api.Models
{
    public class Todo
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public bool IsDone { get; set; }

        public string Notes { get; set; }

        public TodoList Parent { get; set; }

    }
}