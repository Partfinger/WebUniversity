using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUniversity.Models
{
    static class DataGenerator
    {
        static string fnamesPath = "Models//DataGenerator//fnames.xml";
        static string snamesPath = "Models//DataGenerator//snames.xml";

        public static void InitializeDB(ModelBuilder modelBuilder)
        {
            List<Course> courses = GenerateCourses();
            List<Group> groups = GenerateGroups(courses);
            List<Student> students = GenerateStudent(groups, 100);

            modelBuilder.Entity<Group>().HasData(groups);
            modelBuilder.Entity<Student>().HasData(students);
            modelBuilder.Entity<Course>().HasData(courses);
        }

        static List<Course> GenerateCourses()
        {
            List<Course> courses = new List<Course>()
            {
                new Course { Id = 1, Name = "C# Start", Description = "Основы C#" },
                new Course { Id = 2, Name = "C++ Start", Description = "Основы C++" },
                new Course { Id = 3, Name = "Java Start", Description = "Основы Java" },
                new Course { Id = 4, Name = "Python Start", Description = "Основы Python" },
                new Course { Id = 5, Name = "C# Pro", Description = "C# для профессионалов" },
                new Course { Id = 6, Name = "C++ Pro", Description = "C++ для профессионалов" },
                new Course { Id = 7, Name = "Java Pro", Description = "Java для профессионалов" },
                new Course { Id = 8, Name = "Python Pro", Description = "Python для профессионалов" }
                };
            return courses;
        }

        static List<Group> GenerateGroups(List<Course> courses)
        {
            List<Group> groups = new List<Group>()
            {
                new Group { Id = 1, Name = "CS-01", CourseId = 1 },
                new Group { Id = 2, Name = "CP-01", CourseId = 2 },
                new Group { Id = 3, Name = "JA-01", CourseId = 3 },
                new Group { Id = 4, Name = "PH-01", CourseId = 4 },
                new Group { Id = 5, Name = "CS-11", CourseId = 5 },
                new Group { Id = 6, Name = "CP-11", CourseId = 6 },
                new Group { Id = 7, Name = "JP-11", CourseId = 7 },
                new Group { Id = 8, Name = "PH-11", CourseId = 8 }
            };
            return groups;
        }

        static List<Student> GenerateStudent(List<Group> groups, int numberOfStudents)
        {
            int numberOfGroups = groups.Count;
            Random rand = new Random();
            string[] fnames = XMLReader.GetNamesArray(fnamesPath);
            string[] snames = XMLReader.GetNamesArray(snamesPath);

            List<Student> students = new List<Student>();
            for (int index = 0, userId = 1; index < numberOfStudents; index++, userId++)
            {
                 students.Add(
                    new Student()
                    {
                        Id = userId,
                        FirstName = fnames[(int)(rand.NextDouble() * fnames.Length)],
                        LastName = snames[(int)(rand.NextDouble() * snames.Length)],
                        GroupId = 1 + (int)(rand.NextDouble() * groups.Count)
                    }
                    );
            }
            return students;
        }
    }
}
