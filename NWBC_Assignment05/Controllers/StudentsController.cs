using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NWBC_Assignment03.DbContext;
using NWBC_Assignment03.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace NWBC_Assignment03.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly SchoolContext _context;

        public StudentsController(SchoolContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Authorize(Policy = "UserPolicy")]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            try
            {
                return await _context.Students.Include(s => s.Grade).Include(s => s.Courses).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new BadRequestException("An error occurred while retrieving students: " + ex.Message);
            }
        }
        [HttpGet("{id}")]
        [Authorize(Policy = "UserPolicy")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            try
            {
                var student = await _context.Students.Include(s => s.Grade).Include(s => s.Courses).FirstOrDefaultAsync(s => s.Id == id);

                if (student == null)
                {
                    throw new NotFoundException("Student not found.");
                }

                return student;
            }
            catch (Exception ex)
            {
                throw new BadRequestException("An error occurred while retrieving the student: " + ex.Message);
            }
        }
        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            try
            {
                _context.Students.Add(student);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
            }
            catch (Exception ex)
            {
                throw new BadRequestException("An error occurred while creating the student: " + ex.Message);
            }
        }
        [HttpPut("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> PutStudent(int id, Student student)
        {
            if (id != student.Id)
            {
                throw new BadRequestException("Student ID mismatch.");
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    throw new NotFoundException("Student not found.");
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                throw new BadRequestException("An error occurred while updating the student: " + ex.Message);
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                var student = await _context.Students.FindAsync(id);
                if (student == null)
                {
                    throw new NotFoundException("Student not found.");
                }

                _context.Students.Remove(student);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                throw new BadRequestException("An error occurred while deleting the student: " + ex.Message);
            }
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}