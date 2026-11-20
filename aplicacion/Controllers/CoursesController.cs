using aplicacion.DTO.Request;
using aplicacion.DTO.Response;
using aplicacion.Models;
using Microsoft.AspNetCore.Mvc;

namespace aplicacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Courses
        [HttpGet]
        public ActionResult<List<CourseResponse>> GetAll()
        {
            return _context.Courses
                .Where(x => x.IsActive)
                .Select(x => ToResponse(x))
                .ToList();
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public ActionResult<CourseResponse> GetById(int id)
        {
            var course = FindActive(id);
            if (course == null)
            {
                return NotFound();
            }

            return ToResponse(course);
        }

        // POST: api/Courses
        [HttpPost]
        public ActionResult<CourseResponse> Create(CourseRequest request)
        {
            var course = new Course
            {
                Name = request.Name,
                Credit = request.Credit,
                IsActive = true
            };

            _context.Courses.Add(course);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = course.CourseId }, ToResponse(course));
        }

        // PUT: api/Courses/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, CourseRequest request)
        {
            var course = FindActive(id);
            if (course == null)
            {
                return NotFound();
            }

            course.Name = request.Name;
            course.Credit = request.Credit;
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/Courses/5  (eliminación lógica)
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var course = FindActive(id);
            if (course == null)
            {
                return NotFound();
            }

            course.IsActive = false;
            _context.SaveChanges();

            return NoContent();
        }

        private Course? FindActive(int id)
        {
            return _context.Courses.FirstOrDefault(x => x.CourseId == id && x.IsActive);
        }

        private static CourseResponse ToResponse(Course course)
        {
            return new CourseResponse
            {
                Name = course.Name,
                Credit = course.Credit
            };
        }
    }
}
