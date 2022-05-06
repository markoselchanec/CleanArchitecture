using CleanArch.Application.Interfaces;
using CleanArch.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CleanArch.Mvc.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }
        public IActionResult Index()
        {
            GetAllStudentsViewModel students = _studentService.GetStudents();
            return View(students);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(StudentViewModel studentViewModel)
        {
            _studentService.AddStudent(studentViewModel);
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            StudentViewModel student = _studentService.GetStudent(id);


            return View(student);
        }
    }
}
