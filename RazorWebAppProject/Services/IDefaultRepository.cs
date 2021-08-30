using RazorWebAppProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RazorWebAppProject.Services
{
    public interface IDefaultRepository
    {
        IEnumerable<Employee> Get(Func<Employee, bool> filter = null, Func<IQueryable<Employee>, IOrderedQueryable<Employee>> orderBy = null);

        Employee GetFirstOrDefaultEmployee(Func<Employee, bool> filter = null);

        Employee Add(Employee newEmployee);
        Employee Update(Employee updateEmployee);
        Employee Delete(int id);
        Employee Delete(Employee employeeToDelete);

        IEnumerable<DepartmentHeadCount> DepartmentHeadCounts(Dept? department);

        IEnumerable<Employee> SearchForEmployee(string name);
        IEnumerable<Employee> SearchForEmployee(Employee employee);

    }
}
