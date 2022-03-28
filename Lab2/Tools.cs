using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lab2.GlobalVariables;

namespace Lab2
{
    internal class Tools
    {
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
        public static void ColorChangerCaution()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Blue;
        }
        public static void ColorChangerWarning()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
        }
        public static void IndividualStdStats(int classIndex, int stdIndex)
        {
            // classIndex - This is the class Index that would be passed to this function
            // stdIndex - This is the student Index that would be passed to this function

            string stdName = classrooms[classIndex].students[stdIndex].studName;
            int stdGradesTotal = 0;
            int assignmentsPerStd = 0;
            double stdAvgGrade;

            assignmentsPerStd += classrooms[classIndex].students[stdIndex].assignments.Count;

            for (int i = 0; i < assignmentsPerStd; i++)
            {
                stdGradesTotal += classrooms[classIndex].students[stdIndex].assignments[i].assignmentGrade;
            }
            stdAvgGrade = (double)stdGradesTotal / assignmentsPerStd;

            Console.WriteLine(" {0} has a total of {1} assignments in this class.", stdName, assignmentsPerStd);
            if (assignmentsPerStd == 0)
                Console.WriteLine(" The average grade cannot be computated at this time.");
            else
                Console.WriteLine(" {0} average grade is {1}", stdName, stdAvgGrade);
            Console.WriteLine();
        }
        public static void IndividualClassStats(int classIndex)
        {
            int classCount = classrooms.Count;
            string className = classrooms[classIndex].className;
            int stdCount = classrooms[classIndex].students.Count;

            int classGradesTotal = 0;
            int assignmentsPerClass = 0;
            double classAvgGrade = 0;

            for (int j = 0; j < stdCount; j++)
            {
                assignmentsPerClass += classrooms[classIndex].students[j].assignments.Count;
            }

            for (int j = 0; j < stdCount; j++)
            {
                for (int k = 0; k < classrooms[classIndex].students[j].assignments.Count; k++)
                {
                    classGradesTotal += classrooms[classIndex].students[j].assignments[k].assignmentGrade;
                }
            }
            classAvgGrade = (double)classGradesTotal / assignmentsPerClass;
                        
            Console.Write(" Classroom size: ");
            ColorChangerCaution();
            Console.WriteLine($"{stdCount} Students.");
            Console.ResetColor();    
            Console.Write(" Total number of assignments: ");
            ColorChangerCaution();
            Console.WriteLine(assignmentsPerClass);
            Console.ResetColor(); 
            if (assignmentsPerClass == 0)
            {                
                Console.Write(" Average Classroom grade: ");
                ColorChangerCaution();
                Console.WriteLine("Cannot be calculated.");
                Console.ResetColor();
            } else
            {
                Console.Write($" Average Classroom grade: ");
                ColorChangerCaution();
                Console.WriteLine(classAvgGrade);
                Console.ResetColor();
            }
            // Add code to show the top and lowest grades, including the students' names

        }
        public static string ClassStats(int classIndex)
        {
            int classCount = classrooms.Count;
            string className = classrooms[classIndex].className;
            int stdCount = classrooms[classIndex].students.Count;

            int classGradesTotal = 0;
            int assignmentsPerClass = 0;
            double classAvgGrade = 0;

            for (int j = 0; j < stdCount; j++)
            {
                assignmentsPerClass += classrooms[classIndex].students[j].assignments.Count;
            }

            for (int j = 0; j < stdCount; j++)
            {
                for (int k = 0; k < classrooms[classIndex].students[j].assignments.Count; k++)
                {
                    classGradesTotal += classrooms[classIndex].students[j].assignments[k].assignmentGrade;
                }
            }
            classAvgGrade = (double)classGradesTotal / assignmentsPerClass;


            if (assignmentsPerClass == 0)
            {

                return "N/A";
            }
            else
                return classAvgGrade.ToString();
        }
    }
}
