using RazorWebAppProject.Models;
using RazorWebAppProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RazorWebAppProject
{
    public class EmployeeRepository : IDefaultRepository
    {
        private readonly List<Employee> _list;
        public EmployeeRepository()
        {
            _list = new List<Employee>(InMemoryData);
        }
        private static List<Employee> InMemoryData
        {
            get
            {
                return new List<Employee>()
                {
                    new Employee(1, "iamtuse", "theProgrammer", "iamtuse@iamtuse.com", Gender.Male, Dept.IT, "heytuse.jpg"),
                    new Employee(2, "Tom", "Smith", "tom@iamtuse.com", Gender.Male, Dept.HR, image:"avatar-big-01.jpg"),
                    new Employee(3, "Sara", "Robert", "sara@iamtuse.com", Gender.Female, Dept.Manager, image: "test2.png"),
                    new Employee(4, "Alex", "Holder", "alex@iamtuse.com", Gender.Male, Dept.HR, "no-access-denied.jpeg"),
                    new Employee(5, "Alexa", "Carr", "alexa@iamtuse.com", Gender.Female, Dept.Agent, "focus.jpg"),
                    new Employee(6, "Test", "User", "text@iamtuse.com", Gender.Unknown, Dept.Manager, null),
                };
            }
        }
        public Employee Add(Employee newEmployee)
        {
            newEmployee.Id = _list.Max(e => e.Id) + 1;
            _list.Add(newEmployee);
            return newEmployee;
        }
        public Employee Delete(int id)
        {
            Employee employee = _list.FirstOrDefault(e => e.Id == id);
            if (employee != null)
            {
                _list.Remove(employee);
                return employee;
            }

            return null;
        }
        public Employee Delete(Employee employeeToDelete)
        {
            return Delete(employeeToDelete.Id);
        }
        public IEnumerable<Employee> Get(Func<Employee, bool> filter = null, Func<IQueryable<Employee>, IOrderedQueryable<Employee>> orderBy = null)
        {
            if (filter != null)
            {
                return _list.Where(filter);
            }
            if (orderBy != null)
            {
                return orderBy(_list.AsQueryable()).ToList();
            }
            return _list.ToList();
        }
        public Employee GetFirstOrDefaultEmployee(Func<Employee, bool> filter = null)
        {
            if (filter != null)
            {
                return _list.FirstOrDefault(filter);
            }

            return _list.FirstOrDefault();
        }
        public Employee Update(Employee updateEmployee)
        {
            var findObj = _list.FirstOrDefault(e => e.Id == updateEmployee.Id);
            if (findObj != null)
            {
                findObj.FirstName = updateEmployee.FirstName;
                findObj.LastName = updateEmployee.LastName;
                findObj.Email = updateEmployee.Email;
                findObj.Gender = updateEmployee.Gender;
                findObj.Dept = updateEmployee.Dept;

                return updateEmployee;
            }

            return null;
        }

        public IEnumerable<DepartmentHeadCount> DepartmentHeadCounts(Dept? department)
        {
            IEnumerable<Employee> query = _list;
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

        public IEnumerable<Employee> SearchForEmployee(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                return _list.Where(e => e.FirstName.Contains(name)
                || e.LastName.Contains(name) || e.Email.Contains(name));
            }

            return _list.OrderBy(e => e.FirstName)
                        .ThenBy(e => e.LastName)
                        .ThenBy(e => e.Gender)
                        .ThenBy(e => e.Email)
                        .ToList();
        }

        public IEnumerable<Employee> SearchForEmployee(Employee employee)
        {
            return SearchForEmployee(employee.FirstName);
        }
    }
}
