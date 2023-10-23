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
        public static void RemoveStudent(Group group, int studentId)
        {         
            using var db = new Context();
            var student = db.Students.FirstOrDefault(s=>s.Id == studentId);         
            db.Students.Remove(student);
            db.SaveChanges();
        }
        public static Group GetGroupById(int id)
        {
            using var db = new Context();
            return db.Groups.Include(g=>g.Students).FirstOrDefault(s=>s.Id == id);
        }
        public static void UpdateStudent(Student student)
        {
            using var db = new Context();
            db.Students.Update(student);
            db.SaveChanges();
        }
    }
}
