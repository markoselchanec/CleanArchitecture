using CleanArch.Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.ViewModels
{
    public class UpdateStudentViewModel
    {
        public Student ExistingStudent { get; set; }
        public IEnumerable<SelectListItem> CoursesList { get; set; }
        public IEnumerable<int> CoursesListIds { get; set; }
    }
}
