namespace taadaa.api.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using taadaa.api.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<taadaa.api.Models.TaadaaContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(taadaa.api.Models.TaadaaContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            User user = new User { Id = 1, Name = "Marcus Lira", UserName = "marcus", Email = "marcus@molexos.com", Password = "123456"  };
            TodoList list = new TodoList { Id = 1, Name = "To-do list 1", Owner = user };

            context.Users.AddOrUpdate(
              u => u.Id,
              user
            );

            context.TodoLists.AddOrUpdate(
              t => t.Id,
              list
            );

            context.Todos.AddOrUpdate(
              t => t.Id,
              new Todo { Id = 1, Description = "To-do 1", Parent = list, IsDone = false, Notes = "Some notes" },
              new Todo { Id = 2, Description = "To-do 2", Parent = list, IsDone = true, Notes = "" }
            );
        }
    }
}
