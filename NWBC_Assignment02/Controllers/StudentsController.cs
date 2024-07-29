using Microsoft.AspNetCore.Mvc;
using NWBC_Assignment02.Models;
using NWBC_Assignment02.Repositories;

namespace NWBC_Assignment02.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly StudentRepository _repository;
        public StudentsController(StudentRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            var students = _repository.GetAllStudents();
            return Ok(students);
        }
        [HttpGet("{id}")]
        public IActionResult GetStudent(int id)
        {
            var student = _repository.GetStudent(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }
        [HttpPost]
        public IActionResult AddStudent(Student student)
        {
            _repository.AddStudent(student);
            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, Student student)
        {
            var existingStudent = _repository.GetStudent(id);
            if (existingStudent == null)
            {
                return NotFound();
            }
            _repository.UpdateStudent(id, student);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            var student = _repository.GetStudent(id);
            if (student == null)
            {
                return NotFound();
            }
            _repository.DeleteStudent(id);
            return NoContent();
        }
    }
}
