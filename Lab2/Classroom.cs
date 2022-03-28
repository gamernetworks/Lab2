using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lab2.GlobalVariables;
using static Lab2.Headers;
using static Lab2.Tools;

namespace Lab2
{
    internal class Classroom
    {
        public int classID;
        public string className;
        public List<Student> students = new List<Student>();
        public Classroom() { }
        public Classroom(string name)
        {
            className = name;
        }
        public Classroom(string name, Student a) : this(name) { }
        public Classroom(int ID, string name) : this(name)
        {
            classID = ID;
        }
        public Classroom(int ID, string name, Student a) : this(ID, name) { }
        public static void ViewAllClassroomsInfo()
        {
            int classCount = classrooms.Count;
            int i = 0;

            ClassHeader();
            foreach (var classroom in classrooms)
            {
                Console.WriteLine(string.Format(" {0,-9}{1,-23}{2,-16}{3,-14}", classroom.classID, classroom.className, classroom.students.Count, ClassStats(i)));
                i++;
            }
        }
        public static void ManageClass()
        {
            Console.WriteLine("You can manage a classroom");
            Console.ReadKey();
        }
        public static void AddClass()
        {
            Console.WriteLine("You can add a classroom");
            Console.ReadKey();
        }
        public static void EditClass()
        {
            Console.WriteLine("You can edit a callroom");
            Console.ReadKey();
        }
        public static void RemoveClass()
        {
            Console.WriteLine("You can remove a class");
            Console.ReadKey();
        }
        public static void ShowClassAverage()
        {
            Console.WriteLine("You can view the class average grade");
            Console.ReadKey();
        }
        public static void ShowClassTopStudent()
        {
            Console.WriteLine("You can view the top student");
            Console.ReadKey();
        }
        public static void ShowClassBottomStudent()
        {
            Console.WriteLine("You can view the worse student");
            Console.ReadKey();
        }
        public static void CompareClassStd()
        {
            Console.WriteLine("You can compare students");
            Console.ReadKey();
        }
    }
}
