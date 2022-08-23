using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.Models.DataModels
{
    public class Category : BaseEntity
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
