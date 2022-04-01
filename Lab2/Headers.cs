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
        public static void ClassroomMenuHeader()
        {
            Console.Clear();
            PrintLineRed__("\n                       CLASSROOMS");
            Console.WriteLine("********************************************************");            
            PrintLineBlue_(" ID       Class Name      Total Students      Avg Grade");
            Console.WriteLine("********************************************************");
        }
        public static void ClassMainHeader()
        {

        }
        public static void ClassSubHeader()
        {
            
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
        public static void ClassSubMenuHeader(string className)
        {
            PrintLineRed__($"\n === {className.ToUpper()} === ");
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
