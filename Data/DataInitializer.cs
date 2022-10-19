using DigitalPartnersTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalPartnersTask.Data
{
    public class DataInitializer
    {
        public static void Initialize(DataContext context)
        {
            if (context.Workers.Any())
            {
                return;   // DB has been seeded
            }

            var workers = new Worker[] {
                new Worker{Name="John Doe", Age=27},
                new Worker{Name="Jane Doe", Age=26}
            };
            foreach (Worker w in workers) {
                context.Workers.Add(w);
            }
            context.SaveChanges();

        }
    }
}
