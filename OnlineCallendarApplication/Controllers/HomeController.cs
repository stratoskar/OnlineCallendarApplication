﻿using Microsoft.AspNetCore.Mvc;
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
        NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;Database=Callendar_DB;Port=5432;User Id=postgres;Password=grepolis2001");
        NpgsqlCommand comm = new NpgsqlCommand();

        private static string USERNAME; // Current User
        private static int EVENT_ID; // Current Event

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
                        event_list.Event_ID = (int)sdr["Event_ID"];
                        event_list.Date_Hour = (DateTime)sdr["Date_Hour"];
                        event_list.Collaborators = (string[])sdr["Collaborators"];
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

        // Τhis action is called, in order to delete a record (event) that a specific user has
        public IActionResult Delete(int? id)
        {
            try
            {
                string query = string.Format("DELETE FROM public.\"Event\" WHERE \"Event_ID\"='{0}'", id);

                conn.Open();

                NpgsqlCommand comm = new NpgsqlCommand(query, conn);

                comm.Connection = conn;
                comm.CommandType = CommandType.Text;
                comm.ExecuteNonQuery();
                conn.Close();

               return RedirectToAction("Index");
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

        //Calls Event_Update Form

        public ActionResult Edit(int Id)
        {
            EVENT_ID = Id;
            return View("Edit");
        }

        //Function of Edit_Event 

        public IActionResult Update_Event()
        {
            // Take the username and password that user inserted
            DateTime GivenDateHour = DateTime.Parse(Request.Form["date-hour"].ToString());
            string GivenCollaborators = Request.Form["collaborators"].ToString();
            int GivenDuration = Int32.Parse(Request.Form["duration"].ToString());

            try
            {
                string query = string.Format("UPDATE public.\"Event\" SET \"Date_Hour\" = '{0}', \"Collaborators\" = '{1}', \"Duration\" = '{2}' WHERE \"Event_ID\"='{3}'", GivenDateHour, "{" + GivenCollaborators +"}", GivenDuration, EVENT_ID);
                
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
