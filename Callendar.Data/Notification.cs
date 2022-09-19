using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Callendar.Data
{
    // attributes of table Notifications
    public class Notification
    {
        [Key] // Notification_ID is the primary key of the table Notification
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int Notification_ID { get; set; }

        [Required]
        [ForeignKey("User")]
        public string Owner_Username { get; set; }

        [Required]
        public string invited_person { get; set; }

        [Required]
        public DateTime time { get; set; }
        
        public bool attend_event {get; set;}

        // This is the Foreign key
        public virtual User User { get; set; }
    }
}
