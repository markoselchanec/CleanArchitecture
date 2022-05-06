using CleanArch.Application.Interfaces;
using CleanArch.Application.ViewModels;
using CleanArch.Domain.Interfaces;
using CleanArch.Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CleanArch.Application.Services
{
    public class StudentService : IStudentService
    {
        private IStudentRepository _studentRepository;
        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
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
                Courses = student.Courses
            };
            _studentRepository.Add(newStudent); 
        }

        public StudentViewModel GetStudent(int id)
        {
            Student student = _studentRepository.GetFirstOrDefault(x => x.Id == id, "Courses");
            StudentViewModel studentViewModel = new()
            {
                Name = student.Name,
                Courses = student.Courses
            };
            return studentViewModel;
        }
    }
}
