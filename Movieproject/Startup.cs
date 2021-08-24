using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Movieproject.Models;
using Owin;

[assembly: OwinStartupAttribute(typeof(Movieproject.Startup))]
namespace Movieproject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesandUsers();
        }

        private void CreateRolesandUsers()
        {

            ApplicationDbContext db = new ApplicationDbContext();
            var rolemanager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var usermanager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            if (!rolemanager.RoleExists("Admin"))
            {
                //First we create Admin role
                var role = new IdentityRole();
                role.Name = "Admin";
                rolemanager.Create(role);
            }
            if (usermanager.FindByEmail("Oskar@movieshop.com") == null){

                //create SuperUser/Admin for site
                var user = new ApplicationUser();
                user.UserName = "Oskar";
                user.Email = "Oskar@movieshop.com";
                string pswrd = "Hej123!";
                var chkres = usermanager.Create(user, pswrd);

                //On success assign admin role to above user
                if (chkres.Succeeded)
                {
                    usermanager.AddToRole(user.Id, "Admin");
                }

            }
            // Adding Manager Role
            if (!rolemanager.RoleExists("Manager"))
            {
                //First we create Manager role
                var role = new IdentityRole();
                role.Name = "Manager";
                rolemanager.Create(role);
            }


        }
    }
}
