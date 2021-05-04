using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebUniversity.Models.Interfaces;

namespace WebUniversity.Models
{
    public class Student : IHaveId
    {
        public int Id { get; set; }
        [MaxLength(50), Required]
        public string FirstName { get; set; }
        [MaxLength(50), Required]
        public string LastName { get; set; }

        public int? GroupId { get; set; }
        public Group Group { get; set; }
    }
}
