using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

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

        // Each event-meeting can hold up to 3 collaborators
        // Collaborators should be active users of the application, thus need to be
        // enrolled users in the User's table (Foreign Key)
        [ForeignKey("User1")]
        public string Collaborator1 { get; set; }

        [ForeignKey("User2")]
        public string Collaborator2 { get; set; }

        [ForeignKey("User3")]
        public string Collaborator3 { get; set; }

        [Required]
        public int Duration { get; set; }

        // These are the Foreign keys
        public virtual User User { get; set; }

        public virtual User User1 { get; set; }

        public virtual User User2 { get; set; }

        public virtual User User3 { get; set; }

    }
}
