using System.ComponentModel.DataAnnotations;

namespace RazorWebAppProject.Models
{
    public class Employee
    {
        public Employee()
        {
            Id = 0;
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            Gender = null;
            Dept = null;
            Image = string.Empty;
        }
        public Employee(int id, string firstName, string lastName, string email, Dept? dept, string image)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Dept = dept;
            Image = image;
        }
        public Employee(int id, string firstName, string lastName, string email, Gender? gender, string image)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Gender = gender;
            Image = image;
        }
        public Employee(int id, string firstName,string lastName, string email, string image)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Image = image;
        }
        public Employee(int id, string firstName, string lastName, string email, Gender? gender,
            Dept? dept, string image)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Gender = gender;
            Dept = dept;
            Image = image;
        }
        public int Id { get; set; }
        [Display(Name ="First name"), Required]
        public string FirstName { get; set; }
        [Display(Name = "Last name"), Required]
        public string LastName { get; set; }
        [Display(Name = "Office Email"), Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public Gender? Gender { get; set; }
        [Display(Name = "Department"), Required]
        public Dept? Dept { get; set; }
        [Display(Name ="Upload Image")]
        public string Image { get; set; }
        public string Fullname
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }
}
