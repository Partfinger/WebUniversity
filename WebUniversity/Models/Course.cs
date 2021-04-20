﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUniversity.Models
{
    public class Course
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }

        public List<Group> Groups { get; set; }
    }
}
