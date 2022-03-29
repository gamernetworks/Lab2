using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lab2.GlobalVariables;
using static Lab2.Headers;

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
        public static void IndividualStdStatsCalc(int classIndex, int stdIndex)
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
        public static double IndividualStdStats(int classIndex, int stdIndex)
        {
            int stdGradesTotal = 0;
            int assignmentsPerStd = 0;
            double stdAvgGrade;

            assignmentsPerStd += classrooms[classIndex].students[stdIndex].assignments.Count;

            for (int i = 0; i < assignmentsPerStd; i++)
            {
                stdGradesTotal += classrooms[classIndex].students[stdIndex].assignments[i].assignmentGrade;
            }
            stdAvgGrade = (double)stdGradesTotal / assignmentsPerStd;

            if (assignmentsPerStd == 0)
                return 0;
            else
                return stdAvgGrade;
        }
        public static void IndividualClassStats(int classIndex)
        {
            int classCount = classrooms.Count;
            string className = classrooms[classIndex].className;
            int stdCount = classrooms[classIndex].students.Count;
            List<Tools> stdGradesList = new List<Tools>();
            List<string> filteredClassroomList = new List<string>();

            //int classGradesTotal = 0;
            int assignmentsPerClass = 0;
            //double classAvgGrade = 0;
            //int key = 0;

            // Collects class's ID, name, Std count and GPA: Syntax 1
            for (int j = 0; j < stdCount; j++)
            {
                assignmentsPerClass += classrooms[classIndex].students[j].assignments.Count;
            }
            
            /// Working
            foreach (var classroom in classrooms.Where(x => x.classID.Equals(classIndex + 1))) // Prints class's ID, name, Std count and GPA: Syntax 2
            {                
                Console.WriteLine(string.Format(" {0,-5}{1,-18}{2,-13}{3,-13}{4,-10}", classroom.classID, classroom.className, classroom.students.Count,
                    assignmentsPerClass, ClassAvgGPAStats(classIndex)));
            }
            /// Working
            List<Tools> stdGPAList = new List<Tools>();
            for (int j = 0; j < stdCount; j++)
            {
                stdGPAList.Add(new Tools(classrooms[classIndex].students[j].studName, IndividualStdStats(classIndex, j)));
            }

            Console.WriteLine();
            double maxGPA = stdGPAList.Max(x => x.gpa);  // Filter the best Student and their Assignment's Names and Grades. Syntax 1:
            IEnumerable<Tools> queryMax = from stdName in stdGPAList
                                          where stdName.gpa.Equals(maxGPA)
                                          select stdName;
            Console.WriteLine("\n Best Student(s): ");
            ColorChangerCaution();
            foreach (var item in queryMax)
            {
                Console.WriteLine($" GPA of {item.gpa}: {item.stdName}");
            }
            Console.ResetColor();

            double minGPA = stdGPAList.Min(x => x.gpa); // Filter the worse Student and their Assignment's Names and Grades. Syntax 2:
            IEnumerable<Tools> queryMin = stdGPAList.Where(x => x.gpa.Equals(minGPA));
            Console.WriteLine("\n Worse Student(s): ");
            ColorChangerCaution();
            foreach (var item in queryMin)
            {
                Console.WriteLine($" GPA {item.gpa}: {item.stdName}");
            }
            Console.ResetColor();


            /*
            for (int j = 0; j < stdCount; j++)
            {
                key = 0;
                for (int k = 0; k < classrooms[classIndex].students[j].assignments.Count; k++)
                {
                    classGradesTotal += classrooms[classIndex].students[j].assignments[k].assignmentGrade;
                    stdGradesList.Add(new Tools(classrooms[classIndex].students[j].studName,
                    classrooms[classIndex].students[j].assignments[k].assignmentName,
                    classrooms[classIndex].students[j].assignments[k].assignmentGrade));
                    key++;
                }
            }
            
            int maxGrade = stdGradesList.Max(x => x.assignmentGrade);  // Filter the best Student and their Assignment's Names and Grades. Syntax 1:
            IEnumerable<Tools> queryMax = from stdName in stdGradesList
                                                        where stdName.assignmentGrade.Equals(maxGrade)
                                                        select stdName;            
            List<string> stdKey = new List<string>();

            foreach (Tools item in queryMax)
            {
                stdKey.Add(item.stdName);
            }
            Console.ResetColor();

            int stdIndex;
            foreach (var a in stdKey)
            {
                stdIndex = classrooms[classIndex].students.FindIndex(x => x.studName == a);
                IndividualStdStats(classIndex, stdIndex);
            }
            */



            // Information already provided above. This is just another way to do it
            /*
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
            }*/
        }
        public static string ClassAvgGPAStats(int classIndex)
        {
            int stdCount = classrooms[classIndex].students.Count;

            int classGradesTotal = 0;
            int assignmentsPerClass = 0;
            double classAvgGrade;

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
