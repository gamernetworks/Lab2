using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lab2.Tools;
using static Lab2.GlobalVariables;

namespace Lab2
{
    internal class Headers
    {
        public static void MainMenuHeader()
        {
            ColorChangeToRed();
            Console.WriteLine("\n             CLASSROOM MANAGEMENT MAIN MENU");
            Console.ResetColor();
            Console.WriteLine("********************************************************");
        }
        public static void ClassMainMenuHeader()
        {
            ColorChangeToRed();
            Console.WriteLine("\n                       CLASSROOMS");
            Console.ResetColor();
            Console.WriteLine("********************************************************");
        }
        public static void ClassMainHeader()
        {
            ColorChangeToBlue();
            Console.WriteLine(" ID       Class Name      Total Students      Avg Grade");
            Console.ResetColor();
            Console.WriteLine("********************************************************");
        }
        public static void ClassSubHeader()
        {
            PrintLineBlue_(" ID       Student Name        Assignments       Avg GPA");
            Console.WriteLine("********************************************************");
        }
        public static void AssignmentHeader()
        {
            PrintLineBlue_(" ID       Assignment Name        Grade       Status");
            Console.WriteLine("********************************************************");
        }
        public static void ClassSubMenuHeader(string className, int stdAssigned, int numOfAssignments, string gpa)
        {
            ColorChangeToRed();
            PrintLineRed__($"\n === {className.ToUpper()} === ");
            PrintBlue_($"      Students: ");
            Console.Write(stdAssigned);
            PrintBlue_($"      Assignments: ");
            Console.Write(numOfAssignments);
            PrintBlue_($"      Avg GPA: ");
            Console.WriteLine(gpa);
            Console.WriteLine("********************************************************");
        }
        public static void ClassSubMenuHeader(string className)
        {
            PrintLineRed__($"\n === {className.ToUpper()} === ");
            Console.WriteLine("********************************************************");
        }
        public static void StdSubMenuHeader(int classIndex, int stdIndex, string gpa)
        {
            string className = classrooms[classIndex].className.ToUpper();
            string stdName = classrooms[classIndex].students[stdIndex].studName.ToUpper();
            className = "== " + className + " ==";
            stdName = "== " + stdName + " ==";
            PrintRed__($"{className}");
            Console.Write($"      GPA: ");
            Console.Write(gpa);
            PrintLineRed__($"     {stdName}");
            Console.WriteLine("********************************************************");
        }
        public static void StdMenuHeader()
        {
            PrintLineBlue_(" ID      Assignment Name         Grade        Status");
            Console.WriteLine("********************************************************");
        }
        public static void StudentHeaderWithoutID()
        {
            ColorChangeToBlue();
            Console.WriteLine(" **      First Name     Last Name       Grade ");
            Console.ResetColor();
            Console.WriteLine("********************************************************");
        }
        public static void StudentHeaderWithID()
        {
            ColorChangeToBlue();
            Console.WriteLine(" ID    First Name     Last Name       Grade ");
            Console.ResetColor();
            Console.WriteLine("********************************************************");
        }
        public static void StudentHeaderMenu()
        {
            ColorChangeToRed();
            Console.WriteLine("\n               STUDENT MENU");
            Console.ResetColor();
            Console.WriteLine("********************************************************");
        }
        public static void GradesHeader()
        {
            ColorChangeToRed();
            Console.WriteLine("\n                Grades MENU");
            Console.ResetColor();
            Console.WriteLine("********************************************************");
        }
        public static void AddGradesHeader()
        {
            ColorChangeToRed();
            Console.WriteLine("\n                Add Grades");
            Console.ResetColor();
            Console.WriteLine("********************************************************");
        }
        public static void AddStudentHeader()
        {
            ColorChangeToRed();
            Console.WriteLine("\n                ADD STUDENT");
            Console.ResetColor();
            Console.WriteLine("********************************************************");
        }
        public static void EditStudentHeader()
        {
            ColorChangeToRed();
            Console.WriteLine("\n               EDIT STUDENT");
            Console.ResetColor();
            Console.WriteLine("********************************************************");
        }
        public static void DeleteStudentHeader()
        {
            ColorChangeToRed();
            Console.WriteLine("\n              DELETE STUDENT");
            Console.ResetColor();
            Console.WriteLine("********************************************************");
        }
    }
}
