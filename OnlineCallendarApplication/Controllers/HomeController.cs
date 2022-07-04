using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineCallendarApplication.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;

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
