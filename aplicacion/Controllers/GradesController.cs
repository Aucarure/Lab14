using aplicacion.DTO.Request;
using aplicacion.DTO.Response;
using aplicacion.Models;
using Microsoft.AspNetCore.Mvc;

namespace aplicacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GradesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Grades
        [HttpGet]
        public ActionResult<List<GradeResponse>> GetAll()
        {
            return _context.Grades
                .Where(x => x.IsActive)
                .Select(x => ToResponse(x))
                .ToList();
        }

        // GET: api/Grades/5
        [HttpGet("{id}")]
        public ActionResult<GradeResponse> GetById(int id)
        {
            var grade = FindActive(id);
            if (grade == null)
            {
                return NotFound();
            }

            return ToResponse(grade);
        }

        // POST: api/Grades
        [HttpPost]
        public ActionResult<GradeResponse> Create(GradeRequest request)
        {
            var grade = new Grade
            {
                Name = request.Name,
                Description = request.Description,
                IsActive = true
            };

            _context.Grades.Add(grade);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = grade.GradeId }, ToResponse(grade));
        }

        // PUT: api/Grades/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, GradeRequest request)
        {
            var grade = FindActive(id);
            if (grade == null)
            {
                return NotFound();
            }

            grade.Name = request.Name;
            grade.Description = request.Description;
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/Grades/5  (eliminación lógica)
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var grade = FindActive(id);
            if (grade == null)
            {
                return NotFound();
            }

            grade.IsActive = false;
            _context.SaveChanges();

            return NoContent();
        }

        private Grade? FindActive(int id)
        {
            return _context.Grades.FirstOrDefault(x => x.GradeId == id && x.IsActive);
        }

        private static GradeResponse ToResponse(Grade grade)
        {
            return new GradeResponse
            {
                Name = grade.Name,
                Description = grade.Description
            };
        }
    }
}
