using System;
using static Lab2.Tools;
using static Lab2.GlobalVariables;

namespace Lab2
{
    internal class Headers
    {
        public static void MainMenuHeader()
        {
            Console.Clear();
            PrintLineRed__("\n             CLASSROOM MANAGEMENT MAIN MENU");
            Console.WriteLine("********************************************************");
        }
        public static void ClassroomMenuHeader()
        {
            Console.Clear();
            PrintLineRed__("\n                       CLASSROOMS");
            Console.WriteLine("********************************************************");            
            PrintLineBlue_(" ID       Class Name      Total Students      Avg Grade");
            Console.WriteLine("********************************************************");
        }
        public static void ClassSubMenuHeader(string className, int stdAssigned, int numOfAssignments, string gpa)
        {
            Console.Clear();
            PrintLineRed__($"\n === {className.ToUpper()} === ");
            PrintBlue_($"      Students: ");
            Console.Write(stdAssigned);
            PrintBlue_($"      Assignments: ");
            Console.Write(numOfAssignments);
            PrintBlue_($"      Avg GPA: ");
            Console.WriteLine(gpa);
            Console.WriteLine("********************************************************");
            PrintLineBlue_(" ID       Student Name        Assignments       Avg GPA");
            Console.WriteLine("********************************************************");
        }
        public static void StdMenuHeader(int classIndex, int stdIndex, string gpa)
        {
            Console.Clear();
            string className = classrooms[classIndex].className.ToUpper();
            string stdName = classrooms[classIndex].students[stdIndex].studName.ToUpper();
            className = "== " + className + " ==";
            stdName = "== " + stdName + " ==";
            PrintRed__($"{className}");
            Console.Write($"      GPA: ");
            Console.Write(gpa);
            PrintLineRed__($"     {stdName}");
            Console.WriteLine("********************************************************");
            AssignmentHeader();
        }
        public static void AssignmentHeader()
        {
            PrintLineBlue_(" ID      Assignment Name         Grade        Status");
            Console.WriteLine("********************************************************");
        }
    }
}
