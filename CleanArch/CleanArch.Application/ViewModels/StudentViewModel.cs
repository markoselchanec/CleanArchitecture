﻿using CleanArch.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.ViewModels
{
    public class StudentViewModel
    {
        public string Name { get; set; }

        public List<Course> Courses { get; set; }
    }
}