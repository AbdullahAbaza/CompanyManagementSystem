using Company.DAL.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Company.PL.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Code is Required!!")] // dotnet5
        public string Code { get; set; }
        [Required(ErrorMessage = "Name is Required!!")]
        public string Name { get; set; }

        [Display(Name = "Date Of Creation")] // frontEnd
        public DateTime DateOfCreation { get; set; }

        // Navigational Property Many
        //[InverseProperty(nameof(Employee.Department))]
        //public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
