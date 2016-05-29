﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AskGenerator.Business.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using AskGenerator.Business.Entities.Settings;
using AskGenerator.Business.Entities.Base;

namespace AskGenerator.DataProvider
{
    public class AppContext : IdentityDbContext<User>
    {
        public const string Connection = "ConnectionString";
        public AppContext()
            : base(Connection)
        { }

        public static AppContext Create()
        {
            return new AppContext();
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Team> Teames { get; set; }

        public DbSet<Faculty> Faculties { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Vote> Votes { get; set; }

        public DbSet<History> History { get; set; }

        public DbSet<TeacherQuestion> TeacherQuestion { get; set; }

        public DbSet<Subscriber> Subscribers { get; set; }

        public DbSet<Settings> Settings { get; set; }
    }
}
