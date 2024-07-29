using NWBC_Assignment03.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Student
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }

    [ForeignKey("Grade")]
    public int GradeId { get; set; }
    public Grade Grade { get; set; } 

    public ICollection<Course> Courses { get; set; }
}