using Microsoft.EntityFrameworkCore;
using RazorWebAppProject.Models;
using System.Collections.Generic;

namespace RazorWebAppProject.Repository.Data
{
    public static class ExtendDbSeeding
    {
        public static void SeedDatabaseWithInitialData(this ModelBuilder incomingBuilder)
        {
            incomingBuilder.Entity<Employee>()
                            .HasData(Employees);
        }
        private static List<Employee> Employees
        {
            get
            {
                return new List<Employee>()
                {
                    new Employee(1, "tuse", "theProgrammer", "tuse@iamtuse.com", Gender.Male, Dept.IT, null),
                    new Employee(2, "Precious", "Wonkulah", "wonkulahp@iamtuse.com", Gender.Female, Dept.Finance, image:"focus.jpg"),
                    new Employee(3, "Test", "User", "test@iamtuse.com", Gender.Unknown, Dept.HR, null),
                };
            }
        }
    }
}
