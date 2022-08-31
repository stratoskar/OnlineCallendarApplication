using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Callendar.Data
{
    public class Event
    {
        [Key] // Event_ID is the primary key of the table User
        public int Event_ID { get; set; }
        [Required]
        public DateTime Start_Time { get; set; }
        [Required]
        [ForeignKey("User")]
        public string Owner_Username { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Collaborators { get; set; }
        [Required]
        public DateTime Duration { get; set; }

        public virtual User User { get; set; }
        
    }
}
