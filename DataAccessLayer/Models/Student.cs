using DataAccessLayer.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
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
