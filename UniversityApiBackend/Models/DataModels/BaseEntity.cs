using System.ComponentModel.DataAnnotations;


namespace UniversityApiBackend.Models.DataModels
{
    public class BaseEntity
    {
        [Required]
        [Key]
        public int Id { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? UpdateBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? DeleteBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
