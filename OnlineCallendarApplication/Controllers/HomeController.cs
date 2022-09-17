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
        // Create connection with PostgreSQL database
        NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;Database=Callendar_DB;Port=5432;User Id=postgres;Password=Postgrejo01;");
        NpgsqlCommand comm = new NpgsqlCommand();

        private static string USERNAME; // Current User that uses the session
        private static int EVENT_ID; // Current Event

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        // Index controller (is executed when Index view is loading)
        public IActionResult Index()
        {
            try
            {
                List<Event> display_event = new List<Event>();

                conn.Open();
                comm.Connection = conn;
                comm.CommandType = CommandType.Text;
                comm.CommandText = "SELECT * FROM public.\"Event\""; // select query

                NpgsqlDataReader sdr = comm.ExecuteReader();

                while (sdr.Read()) // read information from "Event" table
                {
                    // show every event of current user
                    if (sdr["Owner_Username"].ToString().Equals(USERNAME))
                    {
                        var event_list = new Event();
                        event_list.Event_ID = (int)sdr["Event_ID"];
                        event_list.Date_Hour = (DateTime)sdr["Date_Hour"];
                        event_list.Collaborators = (string[])sdr["Collaborators"];
                        event_list.Duration = (int)sdr["Duration"];
                        display_event.Add(event_list);
                    }
                }

                conn.Close();


                return View(display_event); // show every event of the Loged-In user
            }
            catch(Exception e) // Error with the database occured
            {
                conn.Close();

                ErrorViewModel error = new ErrorViewModel();
                {
                    error.Explain = "Problem with the Database!";
                };

                ViewBag.Message = error; // show error to user

                return View("Error");
            }
        }

        // This method checks if the login credentials that user inserted
        // are valid (if valid then user exists in database)
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
                comm.CommandText = "SELECT * FROM public.\"User\""; // execute SELECT query

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

                // If user not in database, then show an error to user
                ErrorViewModel error = new ErrorViewModel();
                {
                    error.Explain = "Invalid Credentials!";
                };

                ViewBag.Message = error;
                return View("Error"); // show error to user
            }
            catch(Exception e) // problem with connection occured
            {
                conn.Close();

                ErrorViewModel error = new ErrorViewModel();
                {
                    error.Explain = "Problem with the Database!"; // show error to user
                };

                ViewBag.Message = error;
                return View("Error");
            }
        }

        // This method is executed when Edit view is loading
        public IActionResult Edit(int Id)
        {
            try
            {
                // find the event that needs to be Edited from database
                string query = string.Format("SELECT * FROM public.\"Event\" WHERE \"Event_ID\"='{0}'", Id);

                conn.Open();

                NpgsqlCommand comm = new NpgsqlCommand(query, conn);

                comm.Connection = conn;
                comm.CommandType = CommandType.Text;
                comm.ExecuteNonQuery();

                NpgsqlDataReader sdr = comm.ExecuteReader();
                var event_list = new Event();

                while (sdr.Read())
                {
                    event_list.Event_ID = (int)sdr["Event_ID"];
                    event_list.Date_Hour = (DateTime)sdr["Date_Hour"];
                    event_list.Collaborators = (string[])sdr["Collaborators"];
                    event_list.Duration = (int)sdr["Duration"];
                }

                conn.Close();

                EVENT_ID = Id; // save current event

                return View(event_list);
            }
            catch (Exception e) // problem with DB connection
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
                // Execute INSERT query of neww user to database
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
            catch (Exception e) // db connection exception
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

        // Controller of Create view
        public IActionResult Create()
        {
            return View();
        }

        // This function is executed, when a new event is going to be appended to the database
        public IActionResult Add_Event()
        {
            // Take information of the Event that needs to be inserted
            DateTime GivenDateHour = DateTime.Parse(Request.Form["date-hour"]);
            string GivenCollaborators = Request.Form["collaborators"];
            int GivenDuration = Int32.Parse(Request.Form["duration"].ToString());

            try
            {
                // Insert Query
                string query = string.Format("INSERT INTO public.\"Event\" VALUES (DEFAULT,'{0}','{1}','{2}','{3}')", String.Format("{0:d/M/yyyy HH:mm:ss}",GivenDateHour), USERNAME,"{"+GivenCollaborators+"}",GivenDuration);

                conn.Open();

                NpgsqlCommand comm = new NpgsqlCommand(query, conn);

                comm.Connection = conn;
                comm.CommandType = CommandType.Text;
                comm.ExecuteNonQuery();
                conn.Close();

                return RedirectToAction("Index"); // after INSERT query, user will have access to his DASHBOARD
            }
            catch (Exception e)
            {
                conn.Close();

                ErrorViewModel error = new ErrorViewModel();
                {
                    error.Explain = "Problem with the Database!";
                    error.Explain = e.ToString();
                };

                ViewBag.Message = error;
                return View("Error");
            }
        }

        // Τhis action is called, in order to delete a record (event) that a specific user has
        public IActionResult Delete(int? id)
        {
            try
            {
                // Execute DELETE query
                string query = string.Format("DELETE FROM public.\"Event\" WHERE \"Event_ID\"='{0}'", id);

                conn.Open();

                NpgsqlCommand comm = new NpgsqlCommand(query, conn);

                comm.Connection = conn;
                comm.CommandType = CommandType.Text;
                comm.ExecuteNonQuery();
                conn.Close();

               return RedirectToAction("Index");
            }
            catch (Exception e) // DB connection exception
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

       
        // Update Event method
        // This method is executed as user clicks SUBMIT button in Edit form (this form is located in Edit.cshtml view)
        public IActionResult Update_Event()
        {
            // Take informtion of Current Event that needs to be updated
            DateTime GivenDateHour = DateTime.Parse(Request.Form["date-hour"]);
            string GivenCollaborators = Request.Form["collaborators"];
            int GivenDuration = Int32.Parse(Request.Form["duration"].ToString());

            try
            {
                // Update Query
                string query = string.Format("UPDATE public.\"Event\" SET \"Date_Hour\" = '{0}', \"Collaborators\" = '{1}', \"Duration\" = '{2}' WHERE \"Event_ID\"='{3}'", String.Format("{0:d/M/yyyy HH:mm:ss}", GivenDateHour), "{"+GivenCollaborators+"}", GivenDuration, EVENT_ID);
               
                conn.Open();

                NpgsqlCommand comm = new NpgsqlCommand(query, conn);

                comm.Connection = conn;
                comm.CommandType = CommandType.Text;
                comm.ExecuteNonQuery();
                conn.Close();

                return RedirectToAction("Index"); // after UPDATE query, user will have access to his DASHBOARD
            }
            catch (Exception e)
            {
                conn.Close();

                ErrorViewModel error = new ErrorViewModel();
                {
                    error.Explain = "Problem with the Database!";
                    error.Explain = e.ToString();
                };

                ViewBag.Message = error;
                return View("Error");
            }
        }
    }
}
