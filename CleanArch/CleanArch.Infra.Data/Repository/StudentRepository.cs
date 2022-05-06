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
        public StudentRepository(UniversityDBContext ctx) : base(ctx)
        {
        }
    }
}
