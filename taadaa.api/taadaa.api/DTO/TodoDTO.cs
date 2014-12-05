using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using taadaa.api.Models;

namespace taadaa.api.DTO
{
    public class TodoDTO
    {
        public int? id { get; set; }
        public string description { get; set; }
        public bool? done { get; set; }
        public string notes { get; set; }

        public TodoDTO()
        {
        }

        public TodoDTO(Todo domain)
        {
            id = domain.Id;
            description = domain.Description;
            done = domain.IsDone;
            notes = domain.Notes;
        }
    }
}