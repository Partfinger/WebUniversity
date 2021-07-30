using DataAccessLayer.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class Course : IHaveId
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "CourseMustHaveName")]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(500), Required]
        public string Description { get; set; }

        public List<Group> Groups { get; set; }
    }
}
