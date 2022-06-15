using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace CallendarApplication.Controllers
{
    public class Event_Table_Controller : Controller
    {
        public ActionResult Index()
        {
           // List<StudentModel> displaystudentmodel = new List<StudentModel>();
            NpgsqlConnection connection = new NpgsqlConnection("Server=localhost;Port=5432;Database=Callendar_DB;User Id=postgres;Password=sobadata2;");
            connection.Open();
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "select * from event_table";
            return View();

            // NpgsqlDataReader reader = command.ExecuteReader();
            //while (reader.Read())
            //{
             //   var stlist = 
           // }
        }
    
    
    }
}
