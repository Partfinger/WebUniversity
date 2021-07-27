using DataAccessLayer.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class Group : IHaveId
    {
        public int Id { get; set; }
        [MaxLength(10), Required]
        public string Name { get; set; }

        public int? CourseId { get; set; }
        public Course Course { get; set; }

        public List<Student> Students { get; set; }
    }
}
