using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Callendar.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// Aυτό το αρχείο χρησιμοποιείται για να εισάγει δεδομένα στην βάση, σε περίπτωση που η τελευταία δεν περιέχει κανέναν χρήστη!

namespace OnlineCallendarApplication
{
    public static class DataSeeder
    {
        public static void Seed(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<CallendarDataContext>();
            context.Database.EnsureCreated();
            AddData(context);
        }

        private static void AddData(CallendarDataContext context)
        {
            var check_event = context.Event.FirstOrDefault();
            if (check_event != null) return; // if table Event has records, then return

            context.Event.Add(new Event
            {
                Date_Hour = DateTime.Now,
                Owner_Username = "stratoskar",
                Collaborator1 = "gbrisimis", 
                Collaborator2 = "gmissas",
                Duration = 2
            });

            context.Event.Add(new Event
            {
                Date_Hour = DateTime.Now,
                Owner_Username = "CharisChrist",
                Collaborator1 = "gbrisimis", 
                Collaborator2 = "gmissas", 
                Collaborator3 = "stratoskar",
                Duration = 3
            });

            context.Event.Add(new Event
            {
                Date_Hour = DateTime.Now,
                Owner_Username = "CharisChrist",
                Collaborator1 = "gmissas",
                Collaborator2 = "stratoskar",
                Duration = 5
            });

            var check = context.User.FirstOrDefault();
            if (check != null) return; // if table User has records, then return

            // if table User does not have records, then add 4 new Users to the database
            // add first user
            context.User.Add(new User
            {
                Username = "stratoskar",
                Fullname = "Efstratios Karkanis",
                Password = "sk123"
            });

            // add second user
            context.User.Add(new User
            {
                Username = "CharisChrist",
                Fullname = "Charis Christoforidis",
                Password = "cc123"
            });

            // add third user
            context.User.Add(new User
            {
                Username = "gmissas",
                Fullname = "John Missas",
                Password = "gm123"
            });

            // add fourth user
            context.User.Add(new User
            {
                Username = "gbrisimis",
                Fullname = "John Brisimis",
                Password = "gb123"
            });

            context.User.Add(new User
            {
                Username = "stratoskar",
                Fullname = "Efstratios Karkanis",
                Password = "sk123"
            });

            // Add dummy user
            context.User.Add(new User
            {
                Username = "null",
                Fullname = "system_user",
                Password = "123"
            });

            context.SaveChanges();
        }
    }
}
