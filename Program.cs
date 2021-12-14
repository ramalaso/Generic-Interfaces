using System;
using System.Collections.Generic;
using System.Linq;

namespace Interfaces
{
    class Program
    {
        static void Main(string[] args)
        {
            var studentService = new StudentPrinterService(new StudentRepository());
            studentService.PrintStudents(5);

            System.Console.WriteLine($"Total Studentes Created: {Student.StudentCount}");
        }
    }

    public class Student:IComparable<Student> {
        public static int StudentCount = 0;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Student(string first, string last)
        {
            FirstName = first;
            LastName = last;
            StudentCount++;
        }

        public override string ToString()
        {
            return $"{FirstName}, {LastName}";
        }
        public int CompareTo(Student other)
        {
            if(other is null) return 1;
            if(other.LastName == this.LastName) {
                return this.FirstName.CompareTo(other.FirstName);
            }
           return this.LastName.CompareTo(other.LastName);
        }
    }

    public class StudentPrinterService {
        private readonly IRepository<Student> _studentRepository;

        public StudentPrinterService(IRepository<Student> studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public void PrintStudents(int max = 100) {
            var students = _studentRepository.List().Take(max).ToArray();
            // Array.Sort(students);
            // System.Console.WriteLine("Students");
            // for (int i = 0; i < students.Length && i < max; i++)
            // {
            //     System.Console.WriteLine(students[i]);
            // }
            PrintStudentsToConsole(students);
        }

        private void PrintStudentsToConsole(IEnumerable<Student> students) {
            System.Console.WriteLine("Students");
            foreach (var student in students)
            {
                System.Console.WriteLine($"{student.FirstName}, {student.LastName}");
            }
        }
    }

    public interface IRepository<T> {
        IEnumerable<T> List();
    }

    public record Name(string First, string Last);

    public class StudentRepository : IRepository<Student>
    {
        private Name[] _names = new Name[10];
        public StudentRepository()
        {
            _names[0] = new ("Steve", "Smith");
            _names[1] = new ("Chad", "Smith");
            _names[2] = new ("Ben", "Smith");
            _names[3] = new ("Erick", "Smith");
            _names[4] = new ("Julie", "Lerman");
            _names[5] = new ("David", "Startt");
            _names[6] = new ("Aaron", "Skonnard");
            _names[7] = new ("Aaron", "Stewart");
            _names[8] = new ("Aaron", "Powell");
            _names[9] = new ("Aaron", "Frost");
        }
        public IEnumerable<Student> List()
        {
            int index = 0;
            while(index < _names.Length) {
                yield return new Student(_names[index].First, _names[index].Last);
                index++;
            }
        }
    }
}
