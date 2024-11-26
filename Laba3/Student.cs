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
        public string FullName { get; set; }  // Заміна "Discipline"
        public string Faculty { get; set; }   // Заміна "Faculty"
        public string Department { get; set; } // Заміна "Department"
        public string EducationLevel { get; set; } // Заміна "EducationLevel"
        public string Institution { get; set; }  // Заміна "Institution"
        public string Subject { get; set; }     // Заміна "Subject"

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