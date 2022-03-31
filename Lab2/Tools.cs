﻿using System;
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
        public static void StdBestWorseGrades(int classIndex, int stdIndex)
        {
            string stdName = classrooms[classIndex].students[stdIndex].studName;
            int assignmentsPerStd = classrooms[classIndex].students[stdIndex].assignments.Count;
            int maxGrade;
            int minGrade;
                        
            try
            {
                maxGrade = classrooms[classIndex].students[stdIndex].assignments.Max(x => x.assignmentGrade);
                PrintLineRed__ ($"\n Best Grade is: {maxGrade}");
            }
            catch
            {
                PrintLineRed__(" Unable to calculate the best grade at this time.");
            }
            try
            {
                minGrade = classrooms[classIndex].students[stdIndex].assignments.Min(x => x.assignmentGrade);
                PrintLineRed__($" Worse Grade is: {minGrade}");
            }
            catch
            {
                PrintLineRed__(" Unable to calculate the worse grade at this time.");
            }
        }
        public static string StdGPACalc(int classIndex, int stdIndex)
        {
            int stdGradesTotal = 0;
            int assignmentsPerStd;
            double stdAvgGrade;

            assignmentsPerStd = classrooms[classIndex].students[stdIndex].assignments.Count;

            for (int i = 0; i < assignmentsPerStd; i++)
            {
                stdGradesTotal += classrooms[classIndex].students[stdIndex].assignments[i].assignmentGrade;
            }
            stdAvgGrade = (double)stdGradesTotal / assignmentsPerStd;
            stdAvgGrade = Math.Round(stdAvgGrade, 2);

            if (assignmentsPerStd == 0)
                return "N/A";
            else
                return stdAvgGrade.ToString();
        }
        public static void IndividualStdStats(int classIndex, int stdIndex)
        {
            string stdName = classrooms[classIndex].students[stdIndex].studName;
            int stdAssignmentCount = classrooms[classIndex].students[stdIndex].assignments.Count;            
            int bottomGrade = classrooms[classIndex].students[stdIndex].assignments.Min(x => x.assignmentGrade);
            string stdGPA = StdGPACalc(classIndex, stdIndex);
            int topGrade;

            try
            {
                topGrade = classrooms[classIndex].students[stdIndex].assignments.Max(x => x.assignmentGrade);

            }
            catch
            {
                topGrade = 0;
            }

            ClassSubHeader();                        
            foreach (var classroom in classrooms[classIndex].students[stdIndex].assignments)
            {
                Console.WriteLine(string.Format(" {0,-9}{1,-25}{2,-15}{3,-13}",
                    classroom.assignmentID, classroom.assignmentName, classroom.assignmentStatus, classroom.assignmentGrade));
            }            
        }
        public static void FirstIndividualClassStats(int classIndex)
        {
            int classCount = classrooms.Count;
            string className = classrooms[classIndex].className;
            int stdCount = classrooms[classIndex].students.Count;
            int assignmentsPerClass = 0;
            List<Tools> stdGradesList = new List<Tools>();
            List<string> filteredClassroomList = new List<string>();

            ClassSubHeader();
            int stdIndex = 0;
            foreach (var classroom in classrooms[classIndex].students)
            {
                Console.WriteLine(string.Format(" {0,-9}{1,-25}{2,-15}{3,-13}",
                    classroom.studID, classroom.studName, classroom.assignments.Count, StdGPACalc(classIndex, stdIndex)));
                stdIndex++;
            }            

            List<Tools> stdGPAList = new List<Tools>();

            for (int j = 0; j < stdCount; j++)
            {
                try
                {
                    stdGPAList.Add(new Tools(classrooms[classIndex].students[j].studName, double.Parse(StdGPACalc(classIndex, j))));
                }
                catch
                {
                    stdGPAList.Add(new Tools(classrooms[classIndex].students[j].studName, 0));
                }
            }

            double maxGPA;
            Console.WriteLine();

            for (int j = 0; j < stdCount; j++)
            {
                assignmentsPerClass += classrooms[classIndex].students[j].assignments.Count;
            }

            try
            {
                maxGPA = stdGPAList.Max(x => x.gpa);
                IEnumerable<Tools> queryMax = from stdName in stdGPAList
                                              where stdName.gpa.Equals(maxGPA)
                                              select stdName;
                if (maxGPA == 0 && assignmentsPerClass <= 0)
                {
                    PrintRed__(" There are no assignments for any student assigned to the class.");
                } else if (stdCount == 0)
                {
                    PrintRed__(" There are no students assigned to the class.");
                } else
                {
                    if (queryMax.Count() > 1)
                    {

                        PrintLineBlue_(" Top Students are: ");
                    } else
                    {
                        PrintLineBlue_(" Top Student is: ");
                        foreach (var item in queryMax)
                        {
                            Console.WriteLine($" GPA of {item.gpa}: {item.stdName}");
                        }
                        double minGPA;
                        try
                        {
                            minGPA = stdGPAList.Min(x => x.gpa);
                        }
                        catch
                        {
                            minGPA = 0;
                        }

                        IEnumerable<Tools> queryMin = stdGPAList.Where(x => x.gpa.Equals(minGPA));
                        if (queryMin.Count() > 1)
                            PrintLineBlue_(" Bottom Students are: ");
                        else
                            PrintLineBlue_(" Bottom Student is: ");
                        foreach (var item in queryMin)
                        {
                            Console.WriteLine($" GPA of {item.gpa}: {item.stdName}");
                        }
                    }                    
                }               
            }
            catch
            {
                PrintRed__(" There are no assignments for any student or there are no students assigned to the class.");
                maxGPA = 0;
            }                      
        }
        public static Tuple<string, int, int, string> SecondIndividualClassStats(int classIndex)
        {
            string className = classrooms[classIndex].className;
            int stdCount = classrooms[classIndex].students.Count;
            int classID = classrooms[classIndex].classID;
            int assignmentsPerClass = 0;
            string gpa = "";

            for (int j = 0; j < stdCount; j++)
            {
                assignmentsPerClass += classrooms[classIndex].students[j].assignments.Count;
            }            

            foreach (var classroom in classrooms.Where(x => x.classID.Equals(classIndex + 1)))
            {
                gpa = ClassAvgGPAStats(classIndex);
            }
            return new Tuple<string, int, int, string>(className, stdCount, assignmentsPerClass, gpa);
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
            } else
            {
                classAvgGrade = Math.Round(classAvgGrade ,2);
                return classAvgGrade.ToString();
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
