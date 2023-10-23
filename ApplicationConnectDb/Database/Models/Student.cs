using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationConnectDb.Database.Models
{
    internal class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public Group? Group { get; set; }

        public Student(string firstName, string lastName, string patronymic)
        {         
            FirstName = firstName;
            LastName = lastName;
            Patronymic = patronymic;
        }
        public Student()
        { 

        }
    }
}
