using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Callendar.Data
{

    // attributes of table Event
    public class Event
    {
        [Key] // Event_ID is the primary key of the table User
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int Event_ID { get; set; }

        [Required]
        public DateTime Date_Hour { get; set; }

        [Required]
        [ForeignKey("User")]
        public string Owner_Username { get; set; }

        // collaborators can be anyone (not only users of this application)
        public string[] Collaborators { get; set; } 
        [Required]
        public int Duration { get; set; }

        // This is the Foreign key
        public virtual User User { get; set; }
    }
}
