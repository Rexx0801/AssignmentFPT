using Microsoft.AspNetCore.Mvc;
using NWBC_Assignment03.Db;
using Microsoft.AspNetCore.Mvc;
using NWBC_Assignment03.Db;
using NWBC_Assignment03.Models;
using Microsoft.EntityFrameworkCore;

namespace NWBC_Assignment03.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradesController : ControllerBase
    {
        private readonly SchoolContext _context;

        public GradesController(SchoolContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Grade>> GetGrades()
        {
            return _context.Grades.Include(g => g.Students).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Grade> GetGrade(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid grade ID.");
            }

            var grade = _context.Grades.Include(g => g.Students).FirstOrDefault(g => g.Id == id);

            if (grade == null)
            {
                return NotFound($"Grade with ID {id} not found.");
            }

            return grade;
        }

        [HttpPost]
        public ActionResult<Grade> PostGrade(Grade grade)
        {
            if (grade == null)
            {
                return BadRequest("Grade cannot be null.");
            }

            grade.Students = new List<Student>(); // Initialize the Students collection
            _context.Grades.Add(grade);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetGrade), new { id = grade.Id }, grade);
        }

        [HttpPut("{id}")]
        public IActionResult PutGrade(int id, Grade grade)
        {
            if (id != grade.Id)
            {
                return BadRequest("Grade ID mismatch.");
            }

            _context.Entry(grade).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGrade(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid grade ID.");
            }

            var grade = _context.Grades.Find(id);
            if (grade == null)
            {
                return NotFound($"Grade with ID {id} not found.");
            }

            _context.Grades.Remove(grade);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
