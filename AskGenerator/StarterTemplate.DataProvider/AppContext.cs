using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AskGenerator.Business.Entities;

namespace AskGenerator.DataProvider
{
    public class AppContext : DbContext
    {
        public AppContext() : base("ConnectionString")
        {

        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Question> Questions { get; set; }
    }
}
