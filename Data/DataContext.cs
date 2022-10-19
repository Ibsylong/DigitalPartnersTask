using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigitalPartnersTask.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalPartnersTask.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Worker> Workers { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<TodoThings> Tasks { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Worker>().ToTable("Worker");
            modelBuilder.Entity<Shift>().ToTable("Shift");
            modelBuilder.Entity<TodoThings>().ToTable("Task");
        }
    }
}
