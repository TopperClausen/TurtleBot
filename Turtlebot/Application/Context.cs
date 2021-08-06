using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Application.Models;

namespace Application {
    public class Context : DbContext {

        public DbSet<JoinableRole> JoinableRoles { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(@"Server=176.20.223.225;Database=TurtleBot;User Id=TurtleBot;Password=Turtle123");
        }
    }
}