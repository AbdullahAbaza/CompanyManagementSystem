using AutoMapper;
using Company.BLL.Interfaces;
using Company.DAL.Models;
using Company.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Company.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        //private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;

            //_departmentRepository = departmentRepo;
        }

        public IActionResult Index(string searchInput)
        {
            var departments = Enumerable.Empty<Department>();
            if (string.IsNullOrEmpty(searchInput))
            {
                departments = _unitOfWork.DepartmentRepository.GetAll();
            }
            else
            {
                departments = _unitOfWork.DepartmentRepository.SearchByName(searchInput.ToLower());
            }

            var mappedDepts = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(departments);
            return View(mappedDepts);
        }

        [HttpGet] // /Department/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DepartmentViewModel departmentVM)
        {
            //validate

            if (ModelState.IsValid) // Server-Side Validation
            {
                var mappedDept = _mapper.Map<DepartmentViewModel, Department>(departmentVM);

                _unitOfWork.DepartmentRepository.Add(mappedDept);
                int count = _unitOfWork.Complete();

                if (count > 0)
                    TempData["Message"] = "Department Created Successfully";
                else
                    TempData["Message"] = "An Error Has Occured, Department Not Created :(";

                return RedirectToAction(nameof(Index));
            }

            return View(departmentVM);
        }

        // /Department/Details/10
        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (!id.HasValue)
                return BadRequest(); //400

            var department = _unitOfWork.DepartmentRepository.Get(id.Value);

            if (department is null)
                return NotFound(); // 404

            var mappedDept = _mapper.Map<Department, DepartmentViewModel>(department);
            return View(ViewName, mappedDept);

        }

        // /Department/Edit/10
        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");

            ///if (!id.HasValue)
            ///    return BadRequest();
            ///var department = _departmentRepository.Get(id.Value);
            ///if (department is null)
            ///    return NotFound();
            ///return View(department);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, DepartmentViewModel departmentVM)
        {
            if (id != departmentVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var mappedDept = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
                    _unitOfWork.DepartmentRepository.Update(mappedDept);
                    _unitOfWork.Complete();

                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    // 1. Log Error
                    // 2. Friendly Message

                    ModelState.AddModelError(string.Empty, ex.Message);

                }

            }
            return View(departmentVM);
        }

        // /Department/Delete/1
        public IActionResult Delete(int? id)
        {
            return Details(id: id, ViewName: "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, DepartmentViewModel departmentVM)
        {
            if (id != departmentVM.Id)
                return BadRequest();

            try
            {
                var mappedDept = _mapper.Map<DepartmentViewModel, Department>(departmentVM);

                _unitOfWork.DepartmentRepository.Delete(mappedDept);
                _unitOfWork.Complete();

                return RedirectToAction(nameof(Index));

            }
            catch (System.Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
                return View(departmentVM);
            }

        }

    }
}
