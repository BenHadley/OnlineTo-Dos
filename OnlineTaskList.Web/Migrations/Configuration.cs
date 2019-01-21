namespace OnlineTaskList.Web.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using OnlineTaskList.Web.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            // Auto create some users when the code builds the DB
            AddUsers(context);
        }
        
        private void AddUsers(ApplicationDbContext context)
        {
            var user1 = new ApplicationUser() { UserName = "Ben" };
            var user2 = new ApplicationUser() { UserName = "test" };
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            userManager.Create(user1, "wasd123");
            userManager.Create(user2, "pwd123");
        }
    }
}
