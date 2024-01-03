using Company.DAL.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company.PL.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is Required!")]
        [MaxLength(50, ErrorMessage = "Max Lenght of Name is 50 Chars")]
        [MinLength(5, ErrorMessage = "Min Length of Name is 5 Chars")]
        public string Name { get; set; }

        [Range(22, 30)]
        public int? Age { get; set; }

        [RegularExpression(
            @"^[0-9]{1,3}-[a-zA-Z\s\d]{5,10}-[a-zA-Z\s\d]{4,15}-[a-zA-Z\s\d]{5,15}$",
            ErrorMessage = "Address must be like 123-Street-City-Country")]
        public string Address { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }


        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone Number")]

        public string PhoneNumber { get; set; }

        [Display(Name = "Hire Date")]

        public DateTime HireDate { get; set; }


        public IFormFile Image { get; set; }

        public string ImageName { get; set; }




        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }

        // Navigational Property => [One] [Related Data ==> EagerLoading]

        //[InverseProperty(nameof(DAL.Models.Department.Employees))] // when we have Ternary or more Relation
        //[InverseProperty("Employees")]
        public virtual DepartmentViewModel Department { get; set; }
    }
}
