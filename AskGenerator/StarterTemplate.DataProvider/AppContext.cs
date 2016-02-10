using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AskGenerator.Business.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AskGenerator.DataProvider
{
    public class AppContext : IdentityDbContext<User>
    {
        public const string Connection = "ConnectionString";
        public AppContext()
            : base(Connection)
        {
        }

        public static AppContext Create()
        {
            return new AppContext();
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Vote> Votes { get; set; }
    }
}
