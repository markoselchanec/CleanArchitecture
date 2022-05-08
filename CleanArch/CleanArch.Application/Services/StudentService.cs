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
                Courses = student.Courses.ToList()
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

        public UpdateStudentViewModel getUpdateStudent(int id)
        {
            var student = _studentRepository.GetFirstOrDefault(x => x.Id == id, "Courses");


            UpdateStudentViewModel updateStudentModel = new()
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

        public void UpdateStudent(UpdateStudentViewModel updateStudentViewModel)
        {
            Student oldStudent = _studentRepository.GetFirstOrDefault(x => x.Id == updateStudentViewModel.ExistingStudent.Id, "Courses");


            //Remove course
            //oldStudent.Courses.Where(x => !updateStudentViewModel.CoursesListIds.Contains(x.Id)).ToList().ForEach(x=> oldStudent.Courses.Remove(x));

            var currentCourseIds = oldStudent.Courses.Select(x => x.Id).ToList();
            _courseRepository.GetAll()
                            .Where(x => updateStudentViewModel.CoursesListIds.Except(currentCourseIds).Contains(x.Id)).ToList().ForEach(x => oldStudent.Courses.Add(x));


            _studentRepository.Update(oldStudent);
        }
    }
}
