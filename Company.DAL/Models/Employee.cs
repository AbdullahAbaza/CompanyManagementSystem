using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company.DAL.Models
{
    public class Employee : ModelBase
    {
        public string Name { get; set; }

        public int? Age { get; set; }

        public string Address { get; set; }

        public decimal Salary { get; set; }

        public bool IsActive { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime HireDate { get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public string ImageName { get; set; }






        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }

        // Navigational Property => [One] [Related Data ==> EagerLoading]

        [InverseProperty(nameof(Models.Department.Employees))] // when we have Ternary or more Relation
        // [InverseProperty("Employees")]
        public virtual Department Department { get; set; }
    }
}
