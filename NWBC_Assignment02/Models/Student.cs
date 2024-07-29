using System.ComponentModel.DataAnnotations;

namespace NWBC_Assignment02.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Major { get; set; }
    }
}
