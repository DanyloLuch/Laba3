using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba3
{
    public class Student
    {
        public bool IsSelected { get; set; }
        public string FullName { get; set; } 
        public string Faculty { get; set; }  
        public string Department { get; set; }
        public string EducationLevel { get; set; }
        public string Institution { get; set; }  
        public string Subject { get; set; }     

        public Student(string fullName, string faculty, string department, string educationLevel, string institution, string subject)
        {
            FullName = fullName;
            Faculty = faculty;
            Department = department;
            EducationLevel = educationLevel;
            Institution = institution;
            Subject = subject;
        }
    }

}