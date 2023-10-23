using ApplicationConnectDb.Database.Models;
using Microsoft.EntityFrameworkCore;


namespace ApplicationConnectDb.Database.Handlers
{
    internal class GroupHandler
    {
        public static List<Group> GetGroups()
        {
            using var db = new Context();
            return db.Groups.Include(g=>g.Students).ToList();
        }
        public static void AddStudent(Group group,Student student)
        {
            using var db = new Context();
            group.Students.Add(student);
            db.Groups.Update(group);
            db.SaveChanges();
        }
    }
}
