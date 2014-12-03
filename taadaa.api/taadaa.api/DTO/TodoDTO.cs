using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace taadaa.api.DTO
{
    public class TodoDTO
    {
        public int id { get; set; }
        public string description { get; set; }
        public bool done { get; set; }
        public string notes { get; set; }
        public TodoListDTO parent { get; set; }
    }
}