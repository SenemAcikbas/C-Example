using EntityFrameworkCore.ConfigurationManager;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace efders1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var ctx = new SchoolContext())
            {
                ctx.Database.EnsureCreated();
                var stud = new Student() { Name = "Serdar" };
                ctx.Students.Add(stud);
                ctx.SaveChanges();
                var sorgu = from ogrenci in ctx.Students
                            orderby ogrenci.Name
                            select ogrenci;
                Console.WriteLine("Veritabanındaki Ögrencilerimiz");
                foreach (var item in sorgu)
                {
                    Console.WriteLine(item.Name);
                }
                Console.ReadLine();
            }
        }
    }
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public virtual StudentAdress Adress { get; set; }
    }
    public class StudentAdress 
    {
        [ForeignKey("Student")]
        public int StudentAdressId { get; set; }
        public string Adress { get; set; }

        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public virtual Student Student { get; set; }
    }
    public class SchoolContext :DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-KJPR7U8;database=myDb;trusted_connection=true;Trust Server Certificate=True;");
        }
       
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentAdress> StudentAdresses { get; set; }
    }
}