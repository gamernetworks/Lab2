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
using static Lab2.Assignment;
using static Lab2.Student;
using static Lab2.Menus;

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
        public static int ClassStdInfo(int classIndex, int updatingElementID, bool viewTopBottomStd) // -1 = Deactivate highlights. True/false = View Top/Bottom Std
        {
            string className = classrooms[classIndex].className;
            int stdCount = classrooms[classIndex].students.Count;
            int classID = classrooms[classIndex].classID;
            int assignmentsPerClass = 0;
            int i = 0; // Std index helper
            string gpa = "";

            for (int j = 0; j < stdCount; j++) // Iterate to the list of students to find the total amount of assignments in the class
                assignmentsPerClass += classrooms[classIndex].students[j].assignments.Count;
            // Iterate to the list of students to find the class GPA
            foreach (var classroom in classrooms.Where(x => x.classID.Equals(classIndex + 1)))
                gpa = ClassAvgGPACalc(classIndex);
            ClassSubMenuHeader(className, stdCount, assignmentsPerClass, gpa); // Print classroom top info: std count, number of assignments and class GPA

            if (classrooms[classIndex].students.Count <= 0 && viewTopBottomStd == false)
            {
                PrintLineRed__("\n There are no students assigned to this classroom."); Console.ReadKey(); return 0;
            } else
            {
                foreach (var classroom in classrooms[classIndex].students)
                {
                    if (classroom.studID == updatingElementID)
                    {   // Displays the student info being highlighted in red
                        PrintLineRed__(string.Format(" {0,-9}{1,-25}{2,-15}{3,-13}",
                            classroom.studID, classroom.studName, classroom.assignments.Count, StdGPACalc(classIndex, i)));
                        i++;
                    }
                    else
                    {   
                        Console.WriteLine(string.Format(" {0,-9}{1,-25}{2,-15}{3,-13}",
                            classroom.studID, classroom.studName, classroom.assignments.Count, StdGPACalc(classIndex, i)));
                        i++;
                    }
                }
                ViewTopAndBottomStudent(classIndex); return 0; // Displays the class' top and bottom student
            }
        }
        public static int ManageStd(int classIndex)
        {
            string className = classrooms[classIndex].className;

            ClassStdInfo(classIndex, -1, false);
            int stdIndex = 0;
            ClassSubMenuHeader(className);
            if (classrooms[classIndex].students.Count <= 0)
            {
                PrintLineRed__("\n There are no students in this classroom. \n Press any key to continue.");
                Console.ReadKey();
                return 0;
            }

            foreach (var classroom in classrooms[classIndex].students)
            {
                Console.WriteLine(string.Format(" {0,-9}{1,-25}{2,-15}{3,-13}",
                    classroom.studID, classroom.studName, classroom.assignments.Count, StdGPACalc(classIndex, stdIndex)));
                stdIndex++;
            }
            PrintLineRed__("\n Type \"Q\" to cancel this operation. ");
            PrintBlue_(" Enter the student's name or ID number: ");
            string index = Console.ReadLine().ToLower();
            if (index == "q")
            {
                return 0; // mainMenuSelection to cancel operation
            }
            bool indexTest = index.All(char.IsDigit);
            if (indexTest == false)
            {
                int stdNameIndex = classrooms[classIndex].students.FindIndex(x => x.studName.ToLower() == index);
                if (stdNameIndex == -1)
                {
                    PrintLineRed__(" You have entered an invalid name or ID. \n Press any key and try again.");
                    Console.ReadKey();
                    return 1; // classMainMenuSelection
                }
                else
                {
                    StdMenu(classIndex, stdNameIndex);
                    return 0;
                }
            }
            else
            {
                int stdIDIndex = classrooms[classIndex].students.FindIndex(x => x.studID == int.Parse(index));
                if (stdIDIndex == -1)
                {
                    PrintLineRed__(" You have entered an invalid name or ID. \n Press any key and try again.");
                    Console.ReadKey();
                    return 1; // classMainMenuSelection
                }
                else
                {
                    return StdMenu(classIndex, stdIDIndex);
                }
            }
        }
        public static int AddStd(int classIndex)
        {
            string className = classrooms[classIndex].className;

            Console.Clear();
            ClassStdInfo(classIndex, -1, false);
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
        public static int EditStdName(int classIndex, int stdIndex)
        {
            string oldStdName;
            string newStdName;
            string newFirstName;
            string newLastName;
            int stdIndexHelper = 0;

            Console.Clear();
            StdMenuHeader(classIndex, stdIndex, StdGPACalc(classIndex, stdIndex).ToString());
            ViewStdAssignments(classIndex, stdIndex, -1); // Use - 1 to deactivate highlights
            // Edit Code Below...create a method
            /*foreach (var classroom in classrooms[classIndex].students)
            {
                Console.WriteLine(string.Format(" {0,-9}{1,-25}{2,-15}{3,-13}",
                    classroom.studID, classroom.studName, classroom.assignments.Count, StdGPACalc(classIndex, stdIndex)));
                stdIndex++;
            }*/
            if (classrooms[classIndex].students.Count <= 0)
            {
                PrintLineRed__("\n There are no students in this classroom. \n Press any key to continue.");
                Console.ReadKey();
                return 0;
            }

            PrintBlue_("\n Enter the new first name: ");
            newFirstName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Console.ReadLine().ToLower());
            PrintBlue_(" Enter the new last name: ");
            newLastName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Console.ReadLine().ToLower());
            oldStdName = classrooms[classIndex].students[stdIndex].studName;
            classrooms[classIndex].students[stdIndex].studFirstName = newFirstName;
            classrooms[classIndex].students[stdIndex].studLastName = newLastName;
            classrooms[classIndex].students[stdIndex].studName = newFirstName + " " + newLastName;
            newStdName = classrooms[classIndex].students[stdIndex].studName;

            Console.Clear();
            StdMenuHeader(classIndex, stdIndex, StdGPACalc(classIndex, stdIndex).ToString());
            AssignmentHeader();
            foreach (var classroom in classrooms[classIndex].students[stdIndex].assignments)
            {
                Console.WriteLine(string.Format(" {0,-11}{1,-22}{2,-12}{3,-13}",
                    classroom.assignmentID, classroom.assignmentName, classroom.assignmentGrade, classroom.assignmentStatus));
                stdIndexHelper++;
            }

            stdIndexHelper = 0;
            Console.Write($"\n Student ");
            PrintRed__(oldStdName);
            Console.Write(" has been successfully renamed ");
            PrintLineRed__($"{newStdName}.");
            Console.WriteLine(" Press any key to continue.");
            Console.ReadKey();
            return 0; // mainMenuSelection to complete operation              
        }
        public static int RemoveStd(int classIndex)
        {
            string className = classrooms[classIndex].className;

            Console.Clear();
            ClassStdInfo(classIndex, -1, false);
            int stdIndexHelper = 0;
            ClassSubMenuHeader(className);
            ClassSubHeader();
            if (classrooms[classIndex].students.Count <= 0)
            {
                PrintLineRed__("\n There are no students in this classroom. \n Press any key to continue.");
                Console.ReadKey();
                return 0;
            }
            foreach (var classroom in classrooms[classIndex].students)
            {
                Console.WriteLine(string.Format(" {0,-9}{1,-25}{2,-15}{3,-13}",
                    classroom.studID, classroom.studName, classroom.assignments.Count, StdGPACalc(classIndex, stdIndexHelper)));
                stdIndexHelper++;
            }
            PrintLineRed__("\n Type \"Q\" to cancel this operation.");
            PrintBlue_(" Enter the student's full name: ");
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
        public static void ViewStdBestWorseGrades(int classIndex, int stdIndex)
        {
            string stdName = classrooms[classIndex].students[stdIndex].studName;
            int assignmentsPerStd = classrooms[classIndex].students[stdIndex].assignments.Count;
            int maxGrade;
            int minGrade;

            try
            {
                maxGrade = classrooms[classIndex].students[stdIndex].assignments.Max(x => x.assignmentGrade);
                PrintLineRed__($"\n Best Grade is: {maxGrade}");
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
        public static void ViewTopAndBottomStudent(int classIndex)
        {
            int stdCount = classrooms[classIndex].students.Count;
            int assignmentsPerClass = 0;
            double maxGPA;
            double minGPA = 0;

            // Create a list of Tools to help filter the class student's GPAs
            List<Tools> stdGPAList = new List<Tools>();
            // Iterate through all students in the class and try to obtain their GPAs using the StdGPACalc method
            // Use of try and catch to avoid Max exceptions 
            for (int j = 0; j < stdCount; j++)
            {
                try
                {
                    // Adds the student's name and GPA when able
                    stdGPAList.Add(new Tools(classrooms[classIndex].students[j].studName, double.Parse(StdGPACalc(classIndex, j))));
                    // Keeps count of the number of assignments assigned to each student in the class
                    assignmentsPerClass += classrooms[classIndex].students[j].assignments.Count;
                }
                catch
                {
                    // Otherwise, set the GPA to zero becuase the student does not have any assignments (dividing by zero exception)
                    stdGPAList.Add(new Tools(classrooms[classIndex].students[j].studName, 0));
                    // Keeps count of the number of assignments assigned to each student in the class
                    assignmentsPerClass += classrooms[classIndex].students[j].assignments.Count;
                }
            }

            // Attempt to obtain the best GPA in the class. Try/Catch used to catch .Max ex
            try { maxGPA = stdGPAList.Max(x => x.gpa); } catch { maxGPA = 0; }
            // Attempt to obtain the worse GPA in the class. Try/Catch used to catch .Mix ex
            try { minGPA = stdGPAList.Min(x => x.gpa); } catch { minGPA = 0; }
            // Query the students with the best and worse GPAs from the filtered list maxGPA and minGPA
            IEnumerable<Tools> queryMax = stdGPAList.Where(x => x.gpa.Equals(maxGPA));
            IEnumerable<Tools> queryMin = stdGPAList.Where(x => x.gpa.Equals(minGPA));
            // Prints the message below if classroom's GPA is zero and there are zero assignments in the class
            if (maxGPA == 0 && minGPA == 0 && assignmentsPerClass <= 0 && stdCount > 0)
                PrintRed__("\n There are no assignments for any student assigned to the class.");
            else if (stdCount == 0) // Prints msg below if there are no students assigned to the class
                PrintRed__("\n There are no students assigned to this classroom.");
            else // Print the best and worse student(s)
            {
                // If there are more than one std with the same high GPA, then...
                if (queryMax.Count() > 1)
                    PrintLineBlue_("\n Top Students are: ");
                else // Single student with highest classroom GPA                
                    PrintLineBlue_("\n Top Student is: ");
                // Print and student(s) with the highest GPA (including the student name
                foreach (var item in queryMax)
                    Console.WriteLine($" GPA of {item.gpa}: {item.stdName}");
                // If there are more than one std with the same low GPA, then...
                if (queryMin.Count() > 1)
                    PrintLineBlue_(" Bottom Students are: ");
                else // Single student with lowest classroom GPA
                    PrintLineBlue_(" Bottom Student is: ");
                foreach (var item in queryMin)
                    Console.WriteLine($" GPA of {item.gpa}: {item.stdName}");
            }
        }
    }
}
