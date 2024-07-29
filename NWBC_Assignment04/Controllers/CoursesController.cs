using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NWBC_Assignment03.Db;
using NWBC_Assignment03.Models;
using NWBC_Assignment03.Exceptions;

namespace NWBC_Assignment03.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly SchoolContext _context;

        public CoursesController(SchoolContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            try
            {
                return await _context.Courses.Include(c => c.Students).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new BadRequestException("An error occurred while retrieving courses: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            try
            {
                var course = await _context.Courses.Include(c => c.Students).FirstOrDefaultAsync(c => c.Id == id);

                if (course == null)
                {
                    throw new NotFoundException("Course not found.");
                }

                return course;
            }
            catch (Exception ex)
            {
                throw new BadRequestException("An error occurred while retrieving the course: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
            try
            {
                _context.Courses.Add(course);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, course);
            }
            catch (Exception ex)
            {
                throw new BadRequestException("An error occurred while creating the course: " + ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            if (id != course.Id)
            {
                throw new BadRequestException("Course ID mismatch.");
            }
            _context.Entry(course).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                {
                    throw new NotFoundException("Course not found.");
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                throw new BadRequestException("An error occurred while updating the course: " + ex.Message);
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            try
            {
                var course = await _context.Courses.FindAsync(id);
                if (course == null)
                {
                    throw new NotFoundException("Course not found.");
                }

                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                throw new BadRequestException("An error occurred while deleting the course: " + ex.Message);
            }
        }
        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
    }
}