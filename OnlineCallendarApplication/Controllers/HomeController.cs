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
        NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;Database=Callendar_DB;Port=5432;User Id=postgres;Password=soobadata2");
        NpgsqlCommand comm = new NpgsqlCommand();

        private static string USERNAME; // Current User

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Index controller
        public IActionResult Index()
        {
            try
            {
                List<Event> display_event = new List<Event>();

                conn.Open();
                comm.Connection = conn;
                comm.CommandType = CommandType.Text;
                comm.CommandText = "SELECT * FROM public.\"Event\"";

                NpgsqlDataReader sdr = comm.ExecuteReader();

                while (sdr.Read())
                {
                    // show every event of current user
                    if (sdr["Owner_Username"].ToString().Equals(USERNAME))
                    {
                        var event_list = new Event();
                        event_list.Date_Hour = (DateTime)sdr["Date_Hour"];
                        event_list.Collaborators = sdr["Collaborators"].ToString();
                        event_list.Duration = (int)sdr["Duration"];
                        display_event.Add(event_list);
                    }
                }

                conn.Close();
                return View(display_event);
            }
            catch(Exception e)
            {
                conn.Close();

                ErrorViewModel error = new ErrorViewModel();
                {
                    error.Explain = "Problem with the Database!";
                };

                ViewBag.Message = error;

                return View("Error");
            }
        }

        // This method checks if the login credentials that user inserted
        // are valid (user exists in database)
        public IActionResult LoginValidation()
        {
            // Take the username and password that user inserted
            string GivenUsername = Request.Form["username"].ToString();
            string GivenPassword = Request.Form["password"].ToString();

            try
            {
                conn.Open();
                comm.Connection = conn;
                comm.CommandType = CommandType.Text;
                comm.CommandText = "SELECT * FROM public.\"User\"";

                NpgsqlDataReader sdr = comm.ExecuteReader();

                while (sdr.Read())
                {
                    if (sdr["Username"].Equals(GivenUsername) && sdr["Password"].Equals(GivenPassword))
                    {
                        USERNAME = sdr["Username"].ToString(); // save current user
                        conn.Close();
                        return RedirectToAction("Index"); // Go to the main page if user's input is valid
                    }
                    else
                    {
                        continue;
                    }
                }

                conn.Close();

                ErrorViewModel error = new ErrorViewModel();
                {
                    error.Explain = "Invalid Credentials!";
                };

                ViewBag.Message = error;
                return View("Error"); // Go to the login page if user's input is not valid
            }
            catch(Exception e)
            {
                conn.Close();

                ErrorViewModel error = new ErrorViewModel();
                {
                    error.Explain = "Problem with the Database!";
                };

                ViewBag.Message = error;
                return View("Error");
            }
        }

        // This method is called in order to add a new user
        // to database
        public IActionResult RegisterInsertion(string username, string fullname, string password)
        {
            try
            {
                string query = string.Format("Insert into public.\"User\" values ('{0}','{1}','{2}')", username, fullname, password);

                conn.Open();

                NpgsqlCommand comm = new NpgsqlCommand(query, conn);

                comm.Connection = conn;
                comm.CommandType = CommandType.Text;
                comm.ExecuteNonQuery();
                conn.Close();

                USERNAME = username; // save current user

                return RedirectToAction("Index"); // after insert query, user will have access to his main page
            }
            catch (Exception e)
            {
                conn.Close();

                ErrorViewModel error = new ErrorViewModel();
                {
                    error.Explain = "Problem with the Database!";
                };

                ViewBag.Message = error;
                return View("Error");
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

    }
}
