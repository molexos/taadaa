using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using taadaa.api.Models;

namespace taadaa.api.DTO
{
    public class UserDTO
    {
        public int? id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        public ICollection<TodoListDTO> lists { get; set; }

        public UserDTO()
        {
        }

        public UserDTO(User domain)
        {
            id = domain.Id;
            name = domain.Name;
            username = domain.UserName;
            email = domain.Email;
            password = domain.Password;
        }

    }
}