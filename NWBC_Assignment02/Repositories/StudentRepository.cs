using System.Collections.Generic;
using System.Linq;
using NWBC_Assignment02.Data;
using NWBC_Assignment02.Models;

namespace NWBC_Assignment02.Repositories
{
    public class StudentRepository
    {
        private readonly DataContext _context;
        public StudentRepository(DataContext context)
        {
            _context = context;
        }
        public List<Student> GetAllStudents() => _context.Students.ToList();
        public Student GetStudent(int id) => _context.Students.Find(id);
        public void AddStudent(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
        }
        public void UpdateStudent(int id, Student updatedStudent)
        {
            var student = GetStudent(id);
            if (student != null)
            {
                student.Name = updatedStudent.Name;
                student.Age = updatedStudent.Age;
                student.Major = updatedStudent.Major;
                _context.SaveChanges();
            }
        }
        public void DeleteStudent(int id)
        {
            var student = GetStudent(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }
        }
    }
}