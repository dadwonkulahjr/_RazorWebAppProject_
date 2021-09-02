using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RazorWebAppProject.Repository;
using RazorWebAppProject.Repository.Data;
using RazorWebAppProject.Services;

namespace RazorWebAppProject.ExtentionsMethod
{
    public static class ExtendServiceCollection
    {
        public static void MyServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddOpenApiDocument(options =>
            {
                options.Title = "RazorWebAppProject Open Api Test!";
            });
            services.AddRazorPages(options =>
            {
                options.Conventions.AuthorizePage("/Admin/SuperUser/Upsert");
                options.Conventions.AuthorizePage("/Account/Register");
                options.Conventions.AuthorizePage("/Admin/SuperUser/Delete");
                options.Conventions.AllowAnonymousToPage("/Account/Login");
                
            }).AddControllersAsServices();
            services.AddMvcCore()
                    .AddApiExplorer();
            services.Configure<RouteOptions>(options =>
            {
                options.AppendTrailingSlash = true;
                options.LowercaseQueryStrings = true;
                options.LowercaseUrls = true;
                //options.ConstraintMap.Add("test", typeof(TestConstraint));
            });
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }).AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
               
                options.LoginPath = new PathString( "/Account/Login");
                options.LogoutPath =new PathString("/Account/Logout");
            });

            services.AddScoped<IDefaultRepository, EmployeeSQLRepository>();
            services.AddAuthorization();
        }
    }
}
