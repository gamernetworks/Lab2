using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
        public Student(string firstName, string lastName) : this()
        {
            studFirstName = firstName;
            studLastName = lastName;
            studName = firstName + " " + lastName;
        }
        public Student(int ID, string firstName, string lastName) : this(firstName, lastName)
        {
            studID = ID;
            studName = firstName + " " + lastName;
        }
        public static int StdInfo(int classIndex, int updatingElementID, bool viewTopBottomStd) // -1 = Deactivate highlights. True/false = View Top/Bottom Std
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
                gpa = CalcClassAvgGPA(classIndex);
            ClassSubMenuHeader(className, stdCount, assignmentsPerClass, gpa); // Print classroom top info: std count, number of assignments and class GPA

            if (classrooms[classIndex].students.Count <= 0 && viewTopBottomStd == false)
            {
                PrintLineRed__("\n There are no students assigned to this classroom."); return 1;
            } else
            {
                foreach (var classroom in classrooms[classIndex].students)
                {
                    if (classroom.studID == updatingElementID)
                    {   // Displays the student info being highlighted in red
                        PrintLineRed__(string.Format(" {0,-9}{1,-25}{2,-15}{3,-13}",
                            classroom.studID, classroom.studName, classroom.assignments.Count, CalcStdGPA(classIndex, i)));
                        i++;
                    }
                    else
                    {   
                        Console.WriteLine(string.Format(" {0,-9}{1,-25}{2,-15}{3,-13}",
                            classroom.studID, classroom.studName, classroom.assignments.Count, CalcStdGPA(classIndex, i)));
                        i++;
                    }
                }
                if (viewTopBottomStd == true)
                    ViewTopAndBottomStudent(classIndex);
                return 0; // Displays the class' top and bottom student
            }
        }
        public static int ManageStd(int classIndex)
        {
            string stdName;
            int stdIndex;
            bool indexTest;

            if (StdInfo(classIndex, -1, false) == 1)
            {   // View Std header. If theare are no Std, quit module
                Console.WriteLine("\n Press any key to continue"); Console.ReadKey(); return -1;
            }
            PrintLineRed__("\n Type \"Q\" to cancel this operation. ");
            PrintBlue_(" Enter the student's name or ID number: ".ToUpper());
            stdName = Console.ReadLine().ToLower();
            if (stdName == "q") { return 0; } // Quits this method if the user enters "q"
            indexTest = stdName.All(char.IsDigit); // Tests if the user's input can be converted into a digit
            if (indexTest == true)
            {   // If the user entered the std ID number then...
                stdIndex = classrooms[classIndex].students.FindIndex(x => x.studID == int.Parse(stdName)); // Try the Std ID
                if (stdIndex == -1) // Try the Std name if Std ID is not found
                    stdIndex = classrooms[classIndex].students.FindIndex(x => x.studName.ToLower() == stdName);
            }
            else // If the user entered the Std name then...
                stdIndex = classrooms[classIndex].students.FindIndex(x => x.studName.ToLower() == stdName);
            if (stdIndex == -1) // If the input from the user is incorrect, this method will restart
            {
                PrintLineRed__(" You have entered an invalid name or ID. \n Press any key and try again."); Console.ReadKey(); return 4;
            }
            else
                return stdIndex;
        }
        public static int AddStd(int classIndex)
        {
            int newStdID = FindNextAvailableStdID(classIndex); // Finds the next available Std ID
            string newFirstName, newLastName;

            StdInfo(classIndex, -1, false); // Displays the students info without the top/bottom section.
            PrintBlue_("\n Enter the first name of the new student: ".ToUpper());
            newFirstName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Console.ReadLine().ToLower());
            PrintBlue_(" Enter the last name of the new student: ".ToUpper());
            newLastName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Console.ReadLine().ToLower());
            classrooms[classIndex].students.Add(new Student(newStdID, newFirstName, newLastName));
            StdInfo(classIndex, newStdID, false); // Displays the students info without the top/bottom section + highlighting the new std in red
            PrintLineRed__("\n Student added successfully!");
            Console.WriteLine(" Press any key to continue."); Console.ReadKey(); return 0;
        }        
        public static int RemoveStd(int classIndex)
        {
            string stdName, deleteStd;
            int stdID = 0, stdIndex;
            bool indexTest;

            if (StdInfo(classIndex, -1, false) == 1)
            {   // View Std header. If theare are no Std, quit module
                Console.WriteLine("\n Press any key to continue"); Console.ReadKey(); return 0;
            }
            PrintLineRed__("\n Type \"Q\" to cancel this operation. ");
            PrintBlue_(" Enter the student's name or ID number: ".ToUpper());
            stdName = Console.ReadLine().ToLower();
            if (stdName == "q") { return 0; } // Quits this method if the user enters "q"
            indexTest = stdName.All(char.IsDigit); // Tests if the user's input can be converted into a digit
            if (indexTest == true)
            {   // If the user entered the std ID number then...
                stdIndex = classrooms[classIndex].students.FindIndex(x => x.studID == int.Parse(stdName)); // Try the Std ID
                if (stdIndex == -1) // Try the Std name if Std ID is not found
                    stdIndex = classrooms[classIndex].students.FindIndex(x => x.studName.ToLower() == stdName);
            }
            else
            {   // If the user entered the Std name then...
                stdIndex = classrooms[classIndex].students.FindIndex(x => x.studName.ToLower() == stdName);
            }
            if (stdIndex == -1)
            {   // If the input from the user is incorrect, this method will restart
                PrintLineRed__(" You have entered an invalid name or ID. \n Press any key and try again."); Console.ReadKey(); return 4;
            }
            else
            {
                stdID = classrooms[classIndex].students[stdIndex].studID; // Gets the Std ID to highlight it on the page
                stdName = classrooms[classIndex].students[stdIndex].studName; // Get the Std name to be deleted
                StdInfo(classIndex, stdIndex, false); // View the Std information with the Std being edited highlighted in red
                PrintRed__("\n Are you sure you want to delete ");
                Console.Write(stdName);
                PrintLineRed__("?");                
                do
                {
                    PrintRed__(" Type \"Yes\" to proceed or \"No\" to Cancel. ");
                    ColorChangeToBlue();
                    deleteStd = Console.ReadLine().ToLower();
                    if (deleteStd == "yes")
                    {
                        classrooms[classIndex].students.RemoveAt(stdIndex);
                        Console.Clear();
                        StdInfo(classIndex, stdIndex, false); // View the Std information with the Std being edited highlighted in red
                        PrintRed__($"\n {stdName}");
                        Console.WriteLine(" has been successfully deleted!\n Press any key to continue"); Console.ReadKey(); return 0;
                    } else if (deleteStd == "no")
                        return 0; // mainMenuSelection to cancel operation
                } while (deleteStd != "no" || deleteStd != "yes");
                return 0; // mainMenuSelection to complete operation                
            }           
        }
        public static int EditStdName(int classIndex, int stdIndex)
        {
            string stdName, newStdName, newFirstName, newLastName;
            int stdID = 0;
            bool indexTest;

            if (StdInfo(classIndex, -1, false) == 1)
            {   // View Std header. If theare are no Std, quit module
                Console.WriteLine("\n Press any key to continue"); Console.ReadKey(); return 0;
            }
            PrintLineRed__("\n Type \"Q\" to cancel this operation. ");
            PrintBlue_(" Enter the student's name or ID number: ".ToUpper());
            stdName = Console.ReadLine().ToLower();
            if (stdName == "q") { return 0; } // Quits this method if the user enters "q"
            indexTest = stdName.All(char.IsDigit); // Tests if the user's input can be converted into a digit
            if (indexTest == true)
            {   // If the user entered the std ID number then...
                stdIndex = classrooms[classIndex].students.FindIndex(x => x.studID == int.Parse(stdName)); // Try the Std ID
                if (stdIndex == -1) // Try the Std name if Std ID is not found
                    stdIndex = classrooms[classIndex].students.FindIndex(x => x.studName.ToLower() == stdName);
            }
            else // If the user entered the Std name then...
                stdIndex = classrooms[classIndex].students.FindIndex(x => x.studName.ToLower() == stdName);
            if (stdIndex == -1)
            {   // If the input from the user is incorrect, this method will restart
                PrintLineRed__(" You have entered an invalid name or ID. \n Press any key and try again."); Console.ReadKey(); return 4;
            } else
            {
                stdID = classrooms[classIndex].students[stdIndex].studID; // Gets the Std ID to highlight it on the page
                stdName = classrooms[classIndex].students[stdIndex].studName; // Get the Std name to be deleted
                StdInfo(classIndex, stdIndex, false); // View the Std information with the Std being edited highlighted in red
                PrintBlue_("\n Enter a new First Name: ".ToUpper()); // Gets the new First Name
                newFirstName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Console.ReadLine().ToLower());
                PrintBlue_("\n Enter a new Last Name: ".ToUpper()); // Gets the new Last Name
                newLastName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Console.ReadLine().ToLower());
                classrooms[classIndex].students[stdIndex].studFirstName = newFirstName; // Edits the std first name
                classrooms[classIndex].students[stdIndex].studLastName = newLastName; // Edits the std last name
                newStdName = newFirstName + " " + newLastName;
                classrooms[classIndex].students[stdIndex].studName = newStdName; // Edits the std full name
                StdInfo(classIndex, stdIndex, false); // View the Std information with the Std being edited highlighted in red
                Console.Write($"\n Student ");
                PrintRed__(stdName);
                Console.Write(" has been successfully renamed ");
                PrintLineRed__($"{newStdName}.");
                Console.WriteLine(" Press any key to continue."); Console.ReadKey(); return 0;
            }
        }
        public static string CalcStdGPA(int classIndex, int stdIndex)
        {
            int stdGradesTotal = 0;
            int assignmentsPerStd;
            double stdAvgGrade;

            assignmentsPerStd = classrooms[classIndex].students[stdIndex].assignments.Count;
            for (int i = 0; i < assignmentsPerStd; i++) // Iterates through all std's assignments in class and adds the grades
                stdGradesTotal += classrooms[classIndex].students[stdIndex].assignments[i].asgmtGrade;
            stdAvgGrade = (double)stdGradesTotal / assignmentsPerStd; // Calculates the Std GPA
            stdAvgGrade = Math.Round(stdAvgGrade, 2); // Rounds the GPA to 2 decimals
            if (assignmentsPerStd == 0) // If there are no assignments for student....
                return "N/A";
            else // Otherwise, return Std GPA in string format
                return stdAvgGrade.ToString();
        }
        public static void ViewStdBestWorseGrades(int classIndex, int stdIndex)
        {
            string stdName = classrooms[classIndex].students[stdIndex].studName;
            int assignmentsPerStd = classrooms[classIndex].students[stdIndex].assignments.Count;
            int maxGrade, minGrade;

            try
            {   // Tries to obtain the Std best grade. If it fails the catch the ex
                maxGrade = classrooms[classIndex].students[stdIndex].assignments.Max(x => x.asgmtGrade);
                PrintLineRed__($"\n Best Grade is: {maxGrade}");
            }
            catch
            {   // Prints the below msg
                PrintLineRed__("\n Unable to calculate the best grade at this time.");
            }
            try
            {   // Tries to obtain the Std worse grade. If it fails the catch the ex
                minGrade = classrooms[classIndex].students[stdIndex].assignments.Min(x => x.asgmtGrade);
                PrintLineRed__($" Worse Grade is: {minGrade}");
            }
            catch
            {   // Prints the below msg
                PrintLineRed__(" Unable to calculate the worse grade at this time.");
            }
        }
        
    }
}
