using CleanArch.Application.Interfaces;
using CleanArch.Application.ViewModels;
using CleanArch.Domain.Interfaces;
using CleanArch.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CleanArch.Application.Services
{
    [BindProperties]
    public class StudentService : IStudentService
    {
        private IStudentRepository _studentRepository;
        private ICourseRepository _courseRepository;
        public StudentService(IStudentRepository studentRepository, ICourseRepository courseRepository)
        {
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
        }
        public GetAllStudentsViewModel GetStudents()
        {
            var result = new GetAllStudentsViewModel
            {
                Students = _studentRepository.GetAll("Courses")
            };
            return result;
        }
        public void AddStudent(StudentViewModel student)
        {
            Student newStudent = new()
            {
                Name = student.Name,
                Courses = new List<Course>()
            };
            _studentRepository.Add(newStudent); 
        }

        public StudentViewModel GetStudent(int id)
        {
            Student student = _studentRepository.GetFirstOrDefault(x => x.Id == id, "Courses");
            StudentViewModel studentViewModel = new()
            {
                Id = student.Id,
                Name = student.Name,
                Courses = student.Courses,
            };
            return studentViewModel;
        }

        public EnrollCourseStudentViewModel getCourseList(int id)
        {
            var student = _studentRepository.GetFirstOrDefault(x => x.Id == id, "Courses");
            EnrollCourseStudentViewModel updateStudentModel = new()
            {
                ExistingStudent = student,
                CoursesList = _courseRepository.GetAll().Where(x=> !student.Courses.Contains(x)).Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                CoursesListIds = student.Courses.Select(x => x.Id)
            };
            return updateStudentModel;
        }

        public void AddCourse(EnrollCourseStudentViewModel updateStudentViewModel)
        {
            Student oldStudent = _studentRepository.GetFirstOrDefault(x => x.Id == updateStudentViewModel.ExistingStudent.Id, "Courses");
            var currentCourseIds = oldStudent.Courses.Select(x => x.Id).ToList();
            _courseRepository.GetAll()
                            .Where(x => updateStudentViewModel.CoursesListIds.Except(currentCourseIds).Contains(x.Id)).ToList().ForEach(x => oldStudent.Courses.Add(x));
            _studentRepository.Update(oldStudent);
        }

        public void RemoveCourse(int courseId, int studentId)
        {
            var student = _studentRepository.GetFirstOrDefault(x=> x.Id == studentId, "Courses");
            student.Courses.Remove(_courseRepository.GetFirstOrDefault(x => x.Id == courseId));
            _studentRepository.Update(student);

        }

        public void RemoveStudent(int id)
        {
            _studentRepository.Delete(id);
        }

        public UpdateStudentViewModel GetUpdateStudentViewModel(int id)
        {
            var student = _studentRepository.GetFirstOrDefault(x => x.Id == id);
            UpdateStudentViewModel updateStudent = new()
            {
                Id = student.Id,
                Name = student.Name,
            };
            return updateStudent;
        }

        public void UpdateStudent(UpdateStudentViewModel student)
        {
            Student updatedStudent = _studentRepository.GetFirstOrDefault(x=>x.Id==student.Id);
            updatedStudent.Name = student.Name;
            _studentRepository.Update(updatedStudent);
        }
    }
}
