using AutoMapper;
using Company.BLL.Interfaces;
using Company.DAL.Models;
using Company.PL.Helpers;
using Company.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace Company.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        //private readonly IEmployeeRepository _employeeRepo;
        //private readonly IDepartmentRepository _departmentRepo;

        public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;

            //this._employeeRepo = employeeRepo;
            //this._departmentRepo = departmentRepo;
        }

        // /Employee/Index
        public IActionResult Index(string searchInput)
        {
            ///Binding Through View's Dictionary : Transfere Data from Action to View => [One Way]
            /// 1. ViewData is a Dictionary Type Property( ASP.NET Framework 3.5)
            ///      => It helps us to transfer data from controller[Action] to View
            ///ViewData["Message"] = "Hello ViewData";
            /// 2. ViewBag is a Dynamic Type property (ASP.NET 4.0).However, both store data in the same dictionary internally.
            ///ViewBag.Message = "Hello ViewBag";

            var employees = Enumerable.Empty<Employee>();
            if (string.IsNullOrEmpty(searchInput))
                employees = _unitOfWork.EmployeeRepository.GetAll();
            else
                employees = _unitOfWork.EmployeeRepository.SearchByName(searchInput.ToLower());

            var mappedEmps = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            return View(mappedEmps);
        }

        public IActionResult Create()
        {
            //ViewData["Departments"] = _departmentRepo.GetAll();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeViewModel employeeVM)
        {

            if (ModelState.IsValid)
            {
                /// Manual Mapping
                ///var employee = new Employee()
                ///{
                ///    Name = employeeVM.Name,
                ///    Age = employeeVM.Age,
                ///    Address = employeeVM.Address,
                ///    Salary = employeeVM.Salary,
                ///    IsActive = employeeVM.IsActive,
                ///    Email = employeeVM.Email,
                ///    PhoneNumber = employeeVM.PhoneNumber,
                ///    HireDate = employeeVM.HireDate,
                ///    DepartmentId = employeeVM.DepartmentId,
                ///};
                ///
                ///employee = (Employee)employeeVM;

                employeeVM.ImageName = DocumentSettings.UploadFile(employeeVM.Image, "images");

                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);

                _unitOfWork.EmployeeRepository.Add(mappedEmp);

                int count = _unitOfWork.Complete();
                // 3. TempData

                if (count > 0)
                {
                    TempData["Message"] = "Employee Created Successfully";

                }
                else
                {
                    TempData["Message"] = "An Error Has Occured, Employee Not Created :(";
                }
                return RedirectToAction(nameof(Index));

            }
            return View(employeeVM);
        }

        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (!id.HasValue)
                return BadRequest();
            var employee = _unitOfWork.EmployeeRepository.Get(id.Value);

            if (employee is null)
                return NotFound();

            var mappedEmp = _mapper.Map<Employee, EmployeeViewModel>(employee);
            return View(ViewName, mappedEmp);
        }


        public IActionResult Edit(int? id)
        {
            //ViewData["Departments"] = _departmentRepo.GetAll();

            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    

                    //if (employeeVM.Image is not null)
                    //{
                    //    //delete old file if exists
                    //    var oldEmployee = _unitOfWork.EmployeeRepository.Get(id);
                    //    DocumentSettings.DeleteFile(oldEmployee.ImageName, "images");
                        
                    //}
                    if (employeeVM.Image is not null)
                    {
                        employeeVM.ImageName = DocumentSettings.UploadFile(employeeVM.Image, "images");

                    }


                    var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                    
                    _unitOfWork.EmployeeRepository.Update(mappedEmp);
                    _unitOfWork.Complete();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // 1. Log Error
                    // 2. Friendly Message

                    ModelState.AddModelError(string.Empty, ex.Message);

                }

            }
            return View(employeeVM);
        }

        // /employee/Delete/1
        public IActionResult Delete(int? id)
        {
            return Details(id: id, ViewName: "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id)
                return BadRequest();
            try
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);

                _unitOfWork.EmployeeRepository.Delete(mappedEmp);
                _unitOfWork.Complete();
                DocumentSettings.DeleteFile(employeeVM.ImageName, "images");
                return RedirectToAction(nameof(Index));

            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(employeeVM);
        }


    }
}
