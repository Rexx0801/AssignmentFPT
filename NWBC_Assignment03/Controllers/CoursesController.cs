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
    public class CoursesController : ControllerBase
    {
        private readonly SchoolContext _context;

        public CoursesController(SchoolContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Course>> GetCourses()
        {
            return _context.Courses.Include(c => c.Students).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Course> GetCourse(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid course ID.");
            }

            var course = _context.Courses.Include(c => c.Students).FirstOrDefault(c => c.Id == id);

            if (course == null)
            {
                return NotFound($"Course with ID {id} not found.");
            }

            return course;
        }

        [HttpPost]
        public ActionResult<Course> PostCourse(Course course)
        {
            if (course == null)
            {
                return BadRequest("Course cannot be null.");
            }

            _context.Courses.Add(course);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, course);
        }

        [HttpPut("{id}")]
        public IActionResult PutCourse(int id, Course course)
        {
            if (id != course.Id)
            {
                return BadRequest("Course ID mismatch.");
            }

            _context.Entry(course).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCourse(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid course ID.");
            }

            var course = _context.Courses.Find(id);
            if (course == null)
            {
                return NotFound($"Course with ID {id} not found.");
            }

            _context.Courses.Remove(course);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
