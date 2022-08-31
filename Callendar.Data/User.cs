using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Callendar.Data
{
    // attributes of table User
    public class User
    {
        [Key] // Username is the primary key of the table User
        public string Username { get; set; }
        [Required]
        public string Fullname { get; set; }
        [Required]
        public string Password { get; set; }

    }
}