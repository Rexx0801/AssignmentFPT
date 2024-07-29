using Microsoft.AspNetCore.Mvc;
using NWBC_Assignment03.Db;
using NWBC_Assignment03.Models;
using Microsoft.EntityFrameworkCore;

namespace NWBC_Assignment03.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly SchoolContext _context;

        public StudentsController(SchoolContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetStudents()
        {
            return _context.Students.Include(s => s.Grade).Include(s => s.Courses).ToList();
        }
        [HttpGet("{id}")]
        public ActionResult<Student> GetStudent(int id)
        {
            var student = _context.Students.Include(s => s.Grade).Include(s => s.Courses).FirstOrDefault(s => s.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }
        [HttpPost]
        public ActionResult<Student> PostStudent(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
        }
        [HttpPut("{id}")]
        public IActionResult PutStudent(int id, Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
