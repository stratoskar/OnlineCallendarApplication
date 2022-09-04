using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineCallendarApplication.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using System.Data;
using Callendar.Data;

namespace OnlineCallendarApplication.Controllers
{
    public class HomeController : Controller
    {
        // Create connection with PostgreSQL
        NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;Database=Callendar_DB;Port=5432;User Id=postgres;Password=sobadata2;");
        NpgsqlCommand comm = new NpgsqlCommand();

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Index controller
        public IActionResult Index()
        {
            return View();
        }

        // This method checks if the login credentials that user inserted
        // are valid (user exists in database)
        public IActionResult LoginValidation(string username, string password)
        {
            // Take the username and password that user inserted
            string GivenUsername = Request.Form["username"].ToString();
            string GivenPassword = Request.Form["password"].ToString();

            conn.Open();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "SELECT * FROM public.\"User\"";

            NpgsqlDataReader sdr = comm.ExecuteReader();

            while (sdr.Read())
            {
                if (sdr["Username"].Equals(GivenUsername) && sdr["Password"].Equals(GivenPassword))
                {
                    return View("Index"); // Go to the main page if user's input is valid
                }
                else
                {
                    continue;
                }
            }

            return View("Login"); // Go to the login page if user's input is not valid  
        }

        // This method is called in order to add a new user
        // to database
        public IActionResult RegisterInsertion(string username, string fullname, string password)
        {

            string query = string.Format("Insert into public.\"User\" values ('{0}','{1}','{2}')", username, fullname, password);

            conn.Open();

            NpgsqlCommand comm = new NpgsqlCommand(query, conn);

            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.ExecuteNonQuery();
            conn.Close();

            // after insert query, user will have access to his main page
            return View("Index");
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
