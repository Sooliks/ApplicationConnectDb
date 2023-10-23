using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationConnectDb.Database.Models
{
    internal class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Student> Students { get; set; } = new List<Student>();

        public Group()
        {

        }
        public Group(string name)
        {            
            Name = name;
        }
    }
}
