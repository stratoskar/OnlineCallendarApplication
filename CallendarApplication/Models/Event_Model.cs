using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CallendarApplication.Models
{
    public class Event_Model
    {
        public int event_id { get; set; }
        public DateTime start_time { get; set; }
        public string owner_username { get; set; }
        public DateTime Date { get; set; }
        public string collaborators { get; set; }
        public DateTime duration { get; set; }

        //public int SaveDetails()
        // {
        //NpgsqlConnection con = new NpgsqlConnection(GetConString.ConString());
        //string query = "INSERT INTO Profile(Name, Age, City) values ('" + Name + "','" + Age + "','" + City + "')";
        //NpgCommand cmd = new SqlCommand(query, con);
        //con.Open();
        //int i = cmd.ExecuteNonQuery();
        //con.Close();
        //return i;
        // }
    }
}
