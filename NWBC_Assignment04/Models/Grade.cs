using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace NWBC_Assignment03.Models
{
    public class Grade
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Student> Students { get; set; }
}
}
