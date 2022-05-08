using CleanArch.Application.Interfaces;
using CleanArch.Application.ViewModels;
using CleanArch.Domain.Interfaces;
using CleanArch.Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.Services
{
    public class CourseService : ICourseService
    {
        private ICourseRepository _courseRepository;
        private IWebHostEnvironment _webHostEnvironment;
        public CourseService(ICourseRepository courseRepository, IWebHostEnvironment webHostEnvironment)
        {
            _courseRepository = courseRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public GetAllCoursesViewModel GetCourses()
        {
            return new GetAllCoursesViewModel
            {
                Courses = _courseRepository.GetAll()
            };
        }

        public void AddCourse(CourseViewModel course, IFormFile? file)
        {

            string wwwRootPath = _webHostEnvironment.WebRootPath;

            string fileName = Guid.NewGuid().ToString(); 
            var uploads = Path.Combine(wwwRootPath, @"images");
            var extension = Path.GetExtension(file.FileName);

            using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
            {
               file.CopyTo(fileStream);
            };

            Course newCourse = new Course()
            {
                Name = course.Name,
                Description = course.Description,
                Students = course.Students,
                ImageUrl = @"\images\"+fileName+extension,
            };
            _courseRepository.Add(newCourse);
        }

    }
}
