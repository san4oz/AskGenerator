namespace AskGenerator.DataProvider.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AskGenerator.DataProvider.AppContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "AskGenerator.DataProvider.AppContext";
        }

        protected override void Seed(AskGenerator.DataProvider.AppContext context)
        {
            //  This method will be called after migrating to the latest version.
        }
    }
}
