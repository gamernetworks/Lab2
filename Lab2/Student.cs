using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lab2.Tools;
using static Lab2.GlobalVariables;
using static Lab2.Headers;
using static Lab2.Classroom;

namespace Lab2
{
    internal class Student
    {
        public int studID;
        public string studFirstName;
        public string studLastName;
        public string studName;
        public List<Assignment> assignments = new List<Assignment>();
        public Student() { }
        public Student(string name) : this()
        {
            studName = name;
        }
        public Student(string firstName, string lastName) : this()
        {
            studFirstName = firstName;
            studLastName = lastName;
            studName = firstName + " " + lastName;
        }
        public Student(string firstName, string lastName, Assignment a) : this(firstName, lastName)
        {
            studName = firstName + " " + lastName;
        }
        public Student(int ID, string firstName, string lastName) : this(firstName, lastName)
        {
            studID = ID;
            studName = firstName + " " + lastName;
        }
        public Student(int ID, string firstName, string lastName, Assignment a) : this(ID, firstName, lastName)
        {
            studName = firstName + " " + lastName;
        }
        public static void OverallStdInfo()
        {
            Console.WriteLine("You can view the student detailed information here.");
            Console.ReadKey();
        }
        public static void DetailedStdInfo()
        {












            Console.WriteLine("You can view the student detailed information here.");
            Console.ReadKey();
        }
        public static int AddStd(int classIndex)
        {
            string className = classrooms[classIndex].className;

            Console.Clear();
            var a = SecondIndividualClassStats(classIndex);
            int stdIndex = 0;
            ClassSubMenuHeader(className);
            ClassSubHeader();
            foreach (var classroom in classrooms[classIndex].students)
            {
                Console.WriteLine(string.Format(" {0,-9}{1,-25}{2,-15}{3,-13}",
                    classroom.studID, classroom.studName, classroom.assignments.Count, StdGPACalc(classIndex, stdIndex)));
                stdIndex++;
            }
                        
            PrintBlue_("\n Enter the first name of the new student: ");
            string newFirstName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Console.ReadLine().ToLower());

            PrintBlue_(" Enter the last name of the new student: ");
            string newLastName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Console.ReadLine().ToLower());
            int newStdID = FindNextAvailableStdID(classIndex);
            classrooms[classIndex].students.Add(new Student(newStdID, newFirstName, newLastName));

            Console.Clear();
            ClassSubMenuHeader(className);
            ClassSubHeader();
            stdIndex = 0;
            foreach (var classroom in classrooms[classIndex].students)
            {
                if (classroom.studID != newStdID)
                {
                    Console.WriteLine(string.Format(" {0,-9}{1,-25}{2,-15}{3,-13}",
                    classroom.studID, classroom.studName, classroom.assignments.Count, StdGPACalc(classIndex, stdIndex)));
                    stdIndex++;
                }
                else
                {
                    PrintLineRed__(string.Format(" {0,-9}{1,-25}{2,-15}{3,-13}",
                    classroom.studID, classroom.studName, classroom.assignments.Count, StdGPACalc(classIndex, stdIndex)));
                    stdIndex++;
                }
            }

            PrintLineRed__("\n Student added successfully!");
            Console.WriteLine(" Press any key to continue.");
            Console.ReadKey();
            return 0;
        }
        public static void EditStd()
        {
            Console.WriteLine("You can edit a student");
            Console.ReadKey();
        }
        public static int RemoveStd(int classIndex)
        {
            string className = classrooms[classIndex].className;

            Console.Clear();
            var a = SecondIndividualClassStats(classIndex);
            int stdIndexHelper = 0;
            ClassSubMenuHeader(className);
            ClassSubHeader();
            foreach (var classroom in classrooms[classIndex].students)
            {
                Console.WriteLine(string.Format(" {0,-9}{1,-25}{2,-15}{3,-13}",
                    classroom.studID, classroom.studName, classroom.assignments.Count, StdGPACalc(classIndex, stdIndexHelper)));
                stdIndexHelper++;
            }
            PrintLineRed__("\n Type \"Q\" to cancel this operation.");
            PrintBlue_("\n Enter the student's full name: ");
            string stdName = Console.ReadLine().Trim().ToLower();

            if (stdName == "q")
            {
                return 0; // mainMenuSelection to cancel operation
            }
            int stdID;
            bool indexTest = stdName.All(char.IsDigit);
            if (indexTest == false)
            {
                int stdNameIndex = classrooms[classIndex].students.FindIndex(x => x.studName.ToLower() == stdName);
                if (stdNameIndex == -1)
                {
                    PrintLineRed__(" You have entered an invalid name or ID. \n Press any key and try again.");
                    Console.ReadKey();
                    return 3; // classMainMenuSelection
                }
                else
                {
                    stdIndexHelper = 0;
                    stdID = classrooms[classIndex].students[stdNameIndex].studID;
                    Console.Clear();
                    ClassSubMenuHeader(className);
                    ClassSubHeader();
                    foreach (var classroom in classrooms[classIndex].students)
                    {
                        if (classroom.studID == stdID)
                        {
                            PrintLineRed__(string.Format(" {0,-9}{1,-25}{2,-15}{3,-13}",
                            classroom.studID, classroom.studName, classroom.assignments.Count, StdGPACalc(classIndex, stdIndexHelper)));
                            stdIndexHelper++;
                        } else
                        {
                            Console.WriteLine(string.Format(" {0,-9}{1,-25}{2,-15}{3,-13}",
                            classroom.studID, classroom.studName, classroom.assignments.Count, StdGPACalc(classIndex, stdIndexHelper)));
                            stdIndexHelper++;
                        }                        
                    }
                    stdName = classrooms[classIndex].students[stdNameIndex].studName;
                    PrintRed__("\n Are you sure you want to delete ");
                    Console.Write(stdName);
                    PrintLineRed__("?");
                    string deleteStd;
                    do
                    {
                        PrintRed__(" Type \"Yes\" to proceed or \"No\" to Cancel. ");
                        ColorChangeToBlue();
                        deleteStd = Console.ReadLine().ToLower();
                        if (deleteStd == "yes")
                        {
                            stdIndexHelper = 0;                            
                            classrooms[classIndex].students.RemoveAt(stdNameIndex);
                            Console.Clear();
                            ClassSubMenuHeader(className);
                            ClassSubHeader();
                            foreach (var classroom in classrooms[classIndex].students)
                            {
                                Console.WriteLine(string.Format(" {0,-9}{1,-25}{2,-15}{3,-13}",
                                    classroom.studID, classroom.studName, classroom.assignments.Count, StdGPACalc(classIndex, stdIndexHelper)));
                                stdIndexHelper++;
                            }
                            PrintRed__($"\n {stdName}");
                            Console.WriteLine(" has been successfully deleted!");
                            Console.Write(" Press any key to continue.");
                            Console.ReadKey();
                            return 0; // mainMenuSelection to complete operation;
                        }
                        else if (deleteStd == "no")
                        {
                            return 0; // mainMenuSelection to cancel operation
                        }
                    } while (deleteStd != "no" || deleteStd != "yes");
                    return 0; // mainMenuSelection to complete operation
                }
            }
            else
            {
                int stdIDIndex = classrooms[classIndex].students.FindIndex(x => x.studID == int.Parse(stdName));
                if (stdIDIndex == -1)
                {
                    PrintLineRed__(" You have entered an invalid name or ID. \n Press any key and try again.");
                    Console.ReadKey();
                    return 3; // classMainMenuSelection
                }
                else
                {
                    stdIndexHelper = 0;
                    stdID = classrooms[classIndex].students[stdIDIndex].studID;
                    Console.Clear();
                    ClassSubMenuHeader(className);
                    ClassSubHeader();
                    foreach (var classroom in classrooms[classIndex].students)
                    {
                        if (classroom.studID == stdID)
                        {
                            PrintLineRed__(string.Format(" {0,-9}{1,-25}{2,-15}{3,-13}",
                            classroom.studID, classroom.studName, classroom.assignments.Count, StdGPACalc(classIndex, stdIndexHelper)));
                            stdIndexHelper++;
                        }
                        else
                        {
                            Console.WriteLine(string.Format(" {0,-9}{1,-25}{2,-15}{3,-13}",
                            classroom.studID, classroom.studName, classroom.assignments.Count, StdGPACalc(classIndex, stdIndexHelper)));
                            stdIndexHelper++;
                        }
                    }
                    stdName = classrooms[classIndex].students[stdIDIndex].studName;
                    PrintRed__("\n Are you sure you want to delete ");
                    Console.Write(stdName);
                    PrintLineRed__("?");
                    string deleteStd;
                    do
                    {
                        PrintRed__(" Type \"Yes\" to proceed or \"No\" to Cancel. ");
                        ColorChangeToBlue();
                        deleteStd = Console.ReadLine().ToLower();
                        if (deleteStd == "yes")
                        {
                            stdIndexHelper = 0;
                            classrooms[classIndex].students.RemoveAt(stdIDIndex);
                            Console.Clear();
                            ClassSubMenuHeader(className);
                            ClassSubHeader();
                            foreach (var classroom in classrooms[classIndex].students)
                            {
                                Console.WriteLine(string.Format(" {0,-9}{1,-25}{2,-15}{3,-13}",
                                    classroom.studID, classroom.studName, classroom.assignments.Count, StdGPACalc(classIndex, stdIndexHelper)));
                                stdIndexHelper++;
                            }
                            PrintRed__($"\n {stdName}");
                            Console.WriteLine(" has been successfully deleted!");
                            Console.Write(" Press any key to continue.");
                            Console.ReadKey();
                            return 0; // mainMenuSelection to complete operation;
                        }
                        else if (deleteStd == "no")
                        {
                            return 0; // mainMenuSelection to cancel operation
                        }
                    } while (deleteStd != "no" || deleteStd != "yes");
                    return 0; // mainMenuSelection to complete operation
                }
            }
        }
        public static void ShowStdBestGrade()
        {
            Console.WriteLine("You can see the student best grade");
            Console.ReadKey();
        }
        public static void ShowStdWorseGrade()
        {
            Console.WriteLine("You can see the student worse grade");
            Console.ReadKey();
        }
    }
}
