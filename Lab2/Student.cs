using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    internal class Student
    {
        public int studID;
        public string studFirstName;
        public string studLastName;
        public string studName;
        public List<Assignment> assignments = new List<Assignment>();
        public Student() { }
        public Student(string firstName, string lastName) : this()
        {
            studFirstName = firstName;
            studLastName = lastName;
            studName = firstName + " " + lastName;
        }
        public Student(string firstName, string lastName, Assignment a) : this(firstName, lastName)
        {
            studName = firstName + " " + lastName;
        }
        public Student(int ID, string firstName, string lastName) : this(firstName, lastName)
        {
            studID = ID;
            studName = firstName + " " + lastName;
        }
        public Student(int ID, string firstName, string lastName, Assignment a) : this(ID, firstName, lastName)
        {
            studName = firstName + " " + lastName;
        }
        public static void OverallStdInfo()
        {
            Console.WriteLine("You can view the student detailed information here.");
            Console.ReadKey();
        }
        public static void DetailedStdInfo()
        {
            Console.WriteLine("You can view the student detailed information here.");
            Console.ReadKey();
        }
        public static void AddStd()
        {
            Console.WriteLine("You can add a student");
            Console.ReadKey();
        }
        public static void EditStd()
        {
            Console.WriteLine("You can edit a student");
            Console.ReadKey();
        }
        public static void RemoveStd()
        {
            Console.WriteLine("You can remove a student");
            Console.ReadKey();
        }
        public static void ShowStdBestGrade()
        {
            Console.WriteLine("You can see the student best grade");
            Console.ReadKey();
        }
        public static void ShowStdWorseGrade()
        {
            Console.WriteLine("You can see the student worse grade");
            Console.ReadKey();
        }
    }
}
