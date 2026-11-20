using System.ComponentModel.DataAnnotations;

namespace aplicacion.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        public int GradeId { get; set; }
        public Grade Grade { get; set; } = null!;

        [MaxLength(45)]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(45)]
        public string LastName { get; set; } = string.Empty;

        [MaxLength(45)]
        public string? Phone { get; set; }

        [MaxLength(45)]
        public string? Email { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
