using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace RazorWebAppProject.ExtentionsMethod
{
    public static class ExtendApplicationBuilder
    {
        public static void MyBuilder(this IApplicationBuilder incomingBuilder, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                DeveloperExceptionPageOptions options = new()
                {
                    SourceCodeLineCount = 1
                };
                incomingBuilder.UseDeveloperExceptionPage(options);
            }
            else
            {
                incomingBuilder.UseExceptionHandler("/Error");
                incomingBuilder.UseHsts();
            }


            incomingBuilder.UseHttpsRedirection();
            incomingBuilder.UseStaticFiles();

            incomingBuilder.UseRouting();
            incomingBuilder.UseOpenApi();
            incomingBuilder.UseSwaggerUi3();


            incomingBuilder.UseAuthentication();
            incomingBuilder.UseAuthorization();
           

            incomingBuilder.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute
                (
                    name: "default",
                    pattern: "{controller=Employee}/{action=Get}/{id?}"    
                );
            });
        }
    }
}
