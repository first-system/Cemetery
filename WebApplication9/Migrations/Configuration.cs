namespace Cemetery.Migrations
{
    using Cemetery.Models;
    using System.Data.Entity.Migrations;


    internal sealed class Configuration : DbMigrationsConfiguration<Cemetery.Models.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Cemetery.Models.DataContext DB)
        {
            DB.Roles.Add(new Role { Id = 1, RoleName = "admin" });
            DB.Roles.Add(new Role { Id = 2, RoleName = "user" });
            DB.SaveChanges();
            DB.Users.Add(new User { Id = 1, Login = "Admin", Password = EncoderGuid.Encoder.GetHashString("2033"), RoleId = 1 });
            DB.SaveChanges();
            base.Seed(DB);
        }
    }
}
