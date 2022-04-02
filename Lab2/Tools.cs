using System;
using System.Linq;
using static Lab2.GlobalVariables;

namespace Lab2
{
    internal class Tools
    {
        public string StdName { get; set; }
        public string AssignmentName { get; set; }
        public int AssignmentGrade { get; set; }
        public double gpa { get; set; }
        public Tools(string sName, double sGpa)
        {
            StdName = sName;
            gpa = sGpa;
        }
        public static int ReadKeyInput()
        {
            int selectionResult;
            var selectionInput = Console.ReadKey(); // Takes a Key Input from User

            if (char.IsDigit(selectionInput.KeyChar))
            {   // If the KeyInput can be converted into an int, then:
                selectionResult = int.Parse(selectionInput.KeyChar.ToString());
                return selectionResult;
            } else // If input cannot be converted into int then return 0
                return 0;
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
                return classrooms[classIndex].students[stdIndex].assignments.Max(x => x.asgmtID) + 1;
            }
            catch
            {
                return 1;
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
    }
}
