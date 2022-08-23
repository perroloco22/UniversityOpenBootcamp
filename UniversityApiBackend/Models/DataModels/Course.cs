using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.Models.DataModels
{
    public enum Level
    {
        Basic,
        Medium,
        Advanced,
        Expert
    }

    public class Course : BaseEntity
    {
        [StringLength (50)]
        [Required]
        public string? Name { get; set; }

        [Required]
        [StringLength (280)]
        public string? ShortDescription { get; set; }

        public Level Level { get; set; } = Level.Basic;

        [Required]
        public ICollection<Category> Categories { get; set; } = new List<Category> ();

        [Required]
        public Chapter Chapter { get; set; } = new Chapter();

        [Required]
        public ICollection<Student> Students { get; set; } = new List<Student> ();

    }
}
