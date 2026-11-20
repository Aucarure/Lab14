using System.ComponentModel.DataAnnotations;

namespace aplicacion.Models
{
    public class Grade
    {
        public int GradeId { get; set; }

        [MaxLength(45)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(45)]
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
