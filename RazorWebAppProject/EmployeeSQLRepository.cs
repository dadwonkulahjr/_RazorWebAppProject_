using Microsoft.EntityFrameworkCore;
using RazorWebAppProject.Models;
using RazorWebAppProject.Repository.Data;
using RazorWebAppProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RazorWebAppProject
{
    public class EmployeeSQLRepository : IDefaultRepository
    {
        private readonly ApplicationDbContext _appDbContext;
        public EmployeeSQLRepository(ApplicationDbContext applicationDbContext)
        {
            _appDbContext = applicationDbContext;
        }

        public Employee Add(Employee newEmployee)
        {
            _appDbContext.Database.ExecuteSqlRaw("sp_add_new_employee {0}, {1}, {2}, {3}, {4}, {5}", newEmployee.FirstName, newEmployee.LastName, newEmployee.Email, newEmployee.Gender, newEmployee.Dept, newEmployee.Image);

            return newEmployee;
        }

        public Employee Delete(int id)
        {
            var findEmployee = _appDbContext.Employee.Find(id);
            if (findEmployee != null)
            {
                _appDbContext.Database.ExecuteSqlRaw("sp_delete_employee {0}", findEmployee.Id);
                return findEmployee;
            }
            return null;
        }

        public Employee Delete(Employee employeeToDelete)
        {
            return Delete(employeeToDelete.Id);
        }

        public IEnumerable<DepartmentHeadCount> DepartmentHeadCounts(Dept? department)
        {
            IEnumerable<Employee> query = _appDbContext.Employee;
            if (department.HasValue)
            {
                query = query.Where(e => e.Dept.Value == department.Value).ToList();
            }

            return query.GroupBy(e => e.Dept)
                        .Select(e => new DepartmentHeadCount()
                        {
                            Department = e.Key.Value,
                            Count = e.Count()
                        }).ToList();
        }

        public IEnumerable<Employee> Get(Func<Employee, bool> filter = null, Func<IQueryable<Employee>, IOrderedQueryable<Employee>> orderBy = null)
        {
            if (filter != null)
            {
                return _appDbContext.Employee.Where(filter);
            }
            if (orderBy != null)
            {
                return orderBy(_appDbContext.Employee.AsQueryable()).ToList();
            }
            return _appDbContext.Employee.ToList();
        }

        public Employee GetFirstOrDefaultEmployee(Func<Employee, bool> filter = null)
        {
            if (filter != null)
            {
                return _appDbContext.Employee.FirstOrDefault(filter);
            }

            return _appDbContext.Employee.FirstOrDefault();
        }

        public IEnumerable<Employee> SearchForEmployee(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                return _appDbContext.Employee.Where(e => e.FirstName.Contains(name)
                || e.LastName.Contains(name) || e.Email.Contains(name));
            }

            return _appDbContext.Employee.OrderBy(e => e.FirstName)
                        .ThenBy(e => e.LastName)
                        .ThenBy(e => e.Gender)
                        .ThenBy(e => e.Email)
                        .ToList();
        }

        public IEnumerable<Employee> SearchForEmployee(Employee employee)
        {
            return SearchForEmployee(employee.FirstName);
        }

        public Employee Update(Employee updateEmployee)
        {
            var findEmployee = _appDbContext.Employee.Find(updateEmployee.Id);
            if (findEmployee != null)
            {
                _appDbContext.Database.ExecuteSqlRaw("sp_update_employee {0}, {1}, {2}, {3}, {4}, {5}", updateEmployee.FirstName, updateEmployee.LastName, updateEmployee.Email, updateEmployee.Gender, updateEmployee.Dept, updateEmployee.Image);
                return updateEmployee;
            }

            return null;
        }
    }
}
