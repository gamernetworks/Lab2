using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lab2.Tools;

namespace Lab2
{
    internal class Headers
    {
        public static void MainMenuHeader()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n             CLASSROOM MANAGEMENT MAIN MENU");
            Console.ResetColor();
            Console.WriteLine("********************************************************");
        }
        public static void ClassMainMenuHeader()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n                       CLASSROOMS");
            Console.ResetColor();
            Console.WriteLine("********************************************************");
        }
        public static void ClassHeader()
        {
            ColorChangerCaution();
            Console.WriteLine(" ID       Class Name      Total Students      Avg Grade");
            Console.ResetColor();
            Console.WriteLine("********************************************************");
        }
        public static void ClassSubMenuHeader(string className)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n {0} CLASS", className);
            Console.ResetColor();
            Console.WriteLine("********************************************************");
        }
        public static void StdMenuHeader(string stdName)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n                     {0}", stdName);
            Console.ResetColor();
            Console.WriteLine("********************************************************");
        }
        public static void StudentHeaderWithoutID()
        {
            ColorChangerCaution();
            Console.WriteLine(" **      First Name     Last Name       Grade ");
            Console.ResetColor();
            Console.WriteLine("********************************************************");
        }
        public static void StudentHeaderWithID()
        {
            ColorChangerCaution();
            Console.WriteLine(" ID    First Name     Last Name       Grade ");
            Console.ResetColor();
            Console.WriteLine("********************************************************");
        }
        public static void StudentHeaderMenu()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n               STUDENT MENU");
            Console.ResetColor();
            Console.WriteLine("********************************************************");
        }
        public static void GradesHeader()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n                Grades MENU");
            Console.ResetColor();
            Console.WriteLine("********************************************************");
        }
        public static void AddGradesHeader()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n                Add Grades");
            Console.ResetColor();
            Console.WriteLine("********************************************************");
        }
        public static void AddStudentHeader()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n                ADD STUDENT");
            Console.ResetColor();
            Console.WriteLine("********************************************************");
        }
        public static void EditStudentHeader()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n               EDIT STUDENT");
            Console.ResetColor();
            Console.WriteLine("********************************************************");
        }
        public static void DeleteStudentHeader()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n              DELETE STUDENT");
            Console.ResetColor();
            Console.WriteLine("********************************************************");
        }
    }
}
