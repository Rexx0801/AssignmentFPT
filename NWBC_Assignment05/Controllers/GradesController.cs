using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NWBC_Assignment03.DbContext;
using NWBC_Assignment03.Models;
using NWBC_Assignment03.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace NWBC_Assignment03.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class GradesController : ControllerBase
    {
        private readonly SchoolContext _context;

        public GradesController(SchoolContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Authorize(Policy = "UserPolicy")]
        public async Task<ActionResult<IEnumerable<Grade>>> GetGrades()
        {
            try
            {
                return await _context.Grades.Include(g => g.Students).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new BadRequestException("An error occurred while retrieving grades: " + ex.Message);
            }
        }
        [HttpGet("{id}")]
        [Authorize(Policy = "UserPolicy")]
        public async Task<ActionResult<Grade>> GetGrade(int id)
        {
            try
            {
                var grade = await _context.Grades.Include(g => g.Students).FirstOrDefaultAsync(g => g.Id == id);

                if (grade == null)
                {
                    throw new NotFoundException("Grade not found.");
                }

                return grade;
            }
            catch (Exception ex)
            {
                throw new BadRequestException("An error occurred while retrieving the grade: " + ex.Message);
            }
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Grade>> PostGrade(Grade grade)
        {
            try
            {
                _context.Grades.Add(grade);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetGrade), new { id = grade.Id }, grade);
            }
            catch (Exception ex)
            {
                throw new BadRequestException("An error occurred while creating the grade: " + ex.Message);
            }
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutGrade(int id, Grade grade)
        {
            if (id != grade.Id)
            {
                throw new BadRequestException("Grade ID mismatch.");
            }

            _context.Entry(grade).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GradeExists(id))
                {
                    throw new NotFoundException("Grade not found.");
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                throw new BadRequestException("An error occurred while updating the grade: " + ex.Message);
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteGrade(int id)
        {
            try
            {
                var grade = await _context.Grades.FindAsync(id);
                if (grade == null)
                {
                    throw new NotFoundException("Grade not found.");
                }

                _context.Grades.Remove(grade);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                throw new BadRequestException("An error occurred while deleting the grade: " + ex.Message);
            }
        }
        private bool GradeExists(int id)
        {
            return _context.Grades.Any(e => e.Id == id);
        }
    }
}