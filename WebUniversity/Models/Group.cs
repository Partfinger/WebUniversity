﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUniversity.Models
{
    public class Group
    {
        public int Id { get; set; }
        [MaxLength(10)]
        public string Name { get; set; }

        public int? CourseId { get; set; }
        public Course Course { get; set; }

        public List<Student> Students { get; set; }
    }
}
