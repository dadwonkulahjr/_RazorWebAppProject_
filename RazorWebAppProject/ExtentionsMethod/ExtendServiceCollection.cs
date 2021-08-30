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
            }).AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                //options.LoginPath = "/Account/Login";
                //options.LogoutPath = "/Account/Logout";
            });

            services.AddScoped<IDefaultRepository, EmployeeSQLRepository>();
            services.AddAuthorization();
        }
    }
}
