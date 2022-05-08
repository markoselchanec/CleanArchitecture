﻿using CleanArch.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.Interfaces
{
    public interface IStudentService
    {
        GetAllStudentsViewModel GetStudents();
        void AddStudent(StudentViewModel student);
        StudentViewModel GetStudent(int id);
        void UpdateStudent(UpdateStudentViewModel updateStudentViewModel);
        UpdateStudentViewModel getUpdateStudent(int id);
    }
}
