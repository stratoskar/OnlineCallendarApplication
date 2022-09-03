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
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // open Index page
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult F()
        {
            
            
            List<User> display_User = new List<User>();
            NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;Database=Callendar_DB;Port=5432;User Id=postgres;Password=sobadata2;");
            conn.Open();
            NpgsqlCommand comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "SELECT * FROM public.\"User\"";

            NpgsqlDataReader sdr = comm.ExecuteReader();
            while (sdr.Read())
            {
                var user_list = new User();
                user_list.Username = sdr["Username"].ToString();
                user_list.Fullname = sdr["Fullname"].ToString();
                user_list.Password = sdr["Password"].ToString();
                display_User.Add(user_list);
            }
            conn.Close();
            return View(display_User);

            /* Write
             * string query = string.Format("Insert into public.\"User\" values ('{0}','{1}','{2}')", "Kokou", "Full", "Pass");
              NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;Database=Callendar_DB;Port=5432;User Id=postgres;Password=sobadata2;");
              conn.Open();
              NpgsqlCommand comm = new NpgsqlCommand(query,conn);
              comm.Connection = conn;
              comm.CommandType = CommandType.Text;
              comm.ExecuteNonQuery();
              conn.Close();
            */


          }

          // open Register page
          public IActionResult Register()
          {
              return View();
          }

          // open Login page
          public IActionResult Login()
          {
              return View();
          }

          [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
          public IActionResult Error()
          {
              return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
          }
          /*
             List<String> usernames = new List<String>();
             List<String> password = new List<String>();
             var cs = "Host=localhost;Username=postgres;Password=grepolis2001;Database=Callendar_DB";
             using var con = new NpgsqlConnection(cs);
             con.Open();
             SqlCommand cmd = new SqlCommand("SELECT * FROM user_table;");
             SqlDataReader reader = cmd1.ExecuteReader();
             while(reader.Read() == true){

                    usernames.Add(reader.GetOrdinal("username"));
                    password.Add(reader.GetOrdinal("password"));
          }
           */
        }
    }
