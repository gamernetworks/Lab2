using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    internal class Assignment
    {
        public int assignmentID;
        public string assignmentName;
        public int assignmentGrade;
        public bool assignmentStatus;
        public Assignment() { }
        public Assignment(string name) : this()
        {
            assignmentName = name;
        }
        public Assignment(string name, int grade) : this(name)
        {
            assignmentGrade = grade;
            if (assignmentGrade <= 0)
                assignmentStatus = false;
            else
                assignmentStatus = true;
        }
        public Assignment(int ID, string name, int grade) : this(name, grade)
        {
            assignmentID = ID;
            if (assignmentGrade <= 0)
                assignmentStatus = false;
            else
                assignmentStatus = true;
        }
        public static void ViewAssignment()
        {
            Console.WriteLine("You are in the View Assignment section. This will show the student's detailed information");
            Console.ReadKey();
        }
        public static void AddAssignment()
        {
            Console.WriteLine("You are in the Add New Assignment section");
            Console.ReadKey();
        }
        public static void AddEditGrade()
        {
            Console.WriteLine("You are in the Add or Edit Grade section");
            Console.ReadKey();
        }
        public static void RemoveAssignment()
        {
            Console.WriteLine("You are in the Remove Assignment section");
            Console.ReadKey();
        }
    }
}
