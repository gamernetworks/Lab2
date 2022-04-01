using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lab2.GlobalVariables;
using static Lab2.Headers;
using static Lab2.Student;

namespace Lab2
{
    internal class Tools
    {
        public string stdName { get; set; }
        public string assignmentName { get; set; }
        public int assignmentGrade { get; set; }
        public double gpa { get; set; }
        public Tools(string stdName, string assignName, int assignGrade )
        {
            this.stdName = stdName;
            this.assignmentName = assignName;
            this.assignmentGrade = assignGrade;
        }
        public Tools(string stdName, double stdgpa)
        {
            this.stdName = stdName;
            this.gpa = stdgpa;
        }
        public static int ReadKeyInput()
        {
            int selectionResult;
            var selectionInput = Console.ReadKey(); // Takes a Key Input from User
            if (char.IsDigit(selectionInput.KeyChar))
            {
                // If the KeyInput can be converted into an int, then:
                selectionResult = int.Parse(selectionInput.KeyChar.ToString());
                return selectionResult;
            }
            else
            {
                // If input cannot be converted into int then return 0
                return 0;
            }
        }
        public static int FindNextAvailableClassID()
        {
            try
            {
                return classrooms.Max(x => x.classID) + 1;
            }
            catch
            {
                return 1;
            }            
        }
        public static int FindNextAvailableStdID(int classIndex)
        {
            try
            {
                return classrooms[classIndex].students.Max(x => x.studID) + 1;
            }
            catch
            {
                return 1;
            }
        }
        public static int FindNextAvailableAssignmentID(int classIndex, int stdIndex)
        {
            try
            {
                return classrooms[classIndex].students[stdIndex].assignments.Max(x => x.assignmentID) + 1;
            }
            catch
            {
                return 1;
            }
        }
        public static string ClassAvgGPACalc(int classIndex)
        {
            int stdCount = classrooms[classIndex].students.Count;

            int classGradesTotal = 0;
            int assignmentsPerClass = 0;
            double classAvgGrade;

            for (int j = 0; j < stdCount; j++) // Iterate through calass students to seek the assignment count and grades.
            {
                assignmentsPerClass += classrooms[classIndex].students[j].assignments.Count;
                for (int k = 0; k < classrooms[classIndex].students[j].assignments.Count; k++)
                    classGradesTotal += classrooms[classIndex].students[j].assignments[k].assignmentGrade;
            }
            classAvgGrade = (double)classGradesTotal / assignmentsPerClass;
            if (assignmentsPerClass == 0)
                return "N/A";
            else
            {
                classAvgGrade = Math.Round(classAvgGrade, 2);
                return classAvgGrade.ToString();
            }
        }
        public static void ColorChangeToBlue()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Blue;
        }
        public static void ColorChangeToRed()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
        }
        public static void PrintBlue_(string a)
        {
            ColorChangeToBlue();
            Console.Write(a);
            Console.ResetColor();
        }
        public static void PrintRed__(string a)
        {
            ColorChangeToRed();
            Console.Write(a);
            Console.ResetColor();
        }
        public static void PrintBlue_(double a)
        {
            ColorChangeToBlue();
            Console.Write(a);
            Console.ResetColor();
        }
        public static void PrintRed__(double a)
        {
            ColorChangeToRed();
            Console.Write(a);
            Console.ResetColor();
        }
        public static void PrintLineBlue_(string a)
        {
            ColorChangeToBlue();
            Console.WriteLine(a);
            Console.ResetColor();
        }
        public static void PrintLineRed__(string a)
        {
            ColorChangeToRed();
            Console.WriteLine(a);
            Console.ResetColor();
        }
        public static void PrintLineBlue_(int a)
        {
            ColorChangeToBlue();
            Console.WriteLine(a);
            Console.ResetColor();
        }
        public static void PrintLineRed__(int a)
        {
            ColorChangeToRed();
            Console.WriteLine(a);
            Console.ResetColor();
        }
        public static void PrintLineBlue_(double a)
        {
            ColorChangeToBlue();
            Console.WriteLine(a);
            Console.ResetColor();
        }
        public static void PrintLineRed__(double a)
        {
            ColorChangeToRed();
            Console.WriteLine(a);
            Console.ResetColor();
        }
    }
}
