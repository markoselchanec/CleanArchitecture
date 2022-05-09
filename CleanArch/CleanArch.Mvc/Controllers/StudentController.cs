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
        public IActionResult Update(int id)
        {
            UpdateStudentViewModel studentToUpdate = _studentService.GetUpdateStudentViewModel(id);
            return View(studentToUpdate);
        }        
        [HttpPost]
        public IActionResult Update(UpdateStudentViewModel updatedStudent) 
        {
            _studentService.UpdateStudent(updatedStudent);
            return RedirectToAction("Details", new { id = updatedStudent.Id });
        }
        public IActionResult Delete(int id)
        {
            _studentService.RemoveStudent(id);
            return RedirectToAction("Index");
        }
        public IActionResult EnrollCourse(int id)
        {
            EnrollCourseStudentViewModel student = _studentService.getCourseList(id);
            return View(student);
        }       
        [HttpPost]
        public IActionResult EnrollCourse(EnrollCourseStudentViewModel updateStudentViewModel)
        {
            _studentService.AddCourse(updateStudentViewModel);
            int studentId = updateStudentViewModel.ExistingStudent.Id;
            return RedirectToAction("Details", new {id = studentId});
        }
        public IActionResult RemoveCourse(int courseId, int modelId)
        {
            _studentService.RemoveCourse(courseId, modelId);
            return RedirectToAction("Details", new { id = modelId });
        }
    }
}
