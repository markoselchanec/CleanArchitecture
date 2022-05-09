using CleanArch.Domain.Interfaces;
using CleanArch.Domain.Models;
using CleanArch.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Infra.Data.Repository
{
    public class StudentRepository : GenericRepository<Student,int> , IStudentRepository
    {
        private UniversityDBContext _ctx;
        public StudentRepository(UniversityDBContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }

        public void Update(Student student)
        {
            var objFromDb = _ctx.Students.FirstOrDefault(x => x.Id == student.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = student.Name;
                objFromDb.Courses = student.Courses;
            }
            _ctx.SaveChanges();
        }
    }
}
