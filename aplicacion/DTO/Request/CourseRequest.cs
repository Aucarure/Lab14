using System.ComponentModel.DataAnnotations;

namespace aplicacion.DTO.Request
{
    public class CourseRequest
    {
        [Required]
        [MaxLength(45)]
        public string Name { get; set; } = string.Empty;

        [Range(1, int.MaxValue)]
        public int Credit { get; set; }
    }
}
