using System;
using System.Linq;
using static Lab2.GlobalVariables;
using static Lab2.Tools;
using static Lab2.Headers;
using static Lab2.Student;
using System.Globalization;

namespace Lab2
{
    internal class Assignment
    {
        public int assignmentID;
        public string assignmentName;
        public int assignmentGrade;
        public bool assignmentCompleteness;
        public string assignmentStatus;
        public Assignment() { }
        public Assignment(string name) : this()
        {
            assignmentName = name;
        }
        public Assignment(string name, int grade) : this(name)
        {
            assignmentGrade = grade;
            if (assignmentGrade <= 0)
                assignmentStatus = "Incomplete";
            else
                assignmentStatus = "Complete";
        }
        public Assignment(int ID, string name) : this(name)
        {
            assignmentID = ID;
            if (assignmentGrade <= 0)
            {
                assignmentStatus = "Incomplete";
                assignmentCompleteness = false;
            }
            else
            {
                assignmentStatus = "Complete";
                assignmentCompleteness = false;
            }
        }
        public Assignment(int ID, string name, int grade) : this(name, grade)
        {
            assignmentID = ID;
            if (assignmentGrade <= 0)
            {
                assignmentStatus = "Incomplete";
                assignmentCompleteness = false;
            } else
            {
                assignmentStatus = "Complete";
                assignmentCompleteness = false;
            }                
        }
        public Assignment(int ID, string name, int grade, bool completness) : this(name, grade)
        {
            assignmentID = ID;
            if (assignmentGrade <= 0)
            {
                assignmentStatus = "Incomplete";
                assignmentCompleteness = false;
            }
            else
            {
                assignmentStatus = "Complete";
                assignmentCompleteness = false;
            }
        }
        public static int ViewStdAssignments(int classIndex, int stdIndex, int elementUpdatingIndex)
        {
            // Menu containing the student's class name, her/his name and GPA
            StdMenuHeader(classIndex, stdIndex, CalcStdGPA(classIndex, stdIndex).ToString());            
            if (classrooms[classIndex].students[stdIndex].assignments.Count <= 0 && elementUpdatingIndex == -1)
            {   // Returns the message below if student has no assignments
                PrintLineRed__("\n There are no assignments for this student."); return 0;
            } else
            {   // Otherwise, print the stundets assignment ID, name, grade and status and highlight what's being updated
                foreach (var classroom in classrooms[classIndex].students[stdIndex].assignments)
                {
                    if (classroom.assignmentID == elementUpdatingIndex)
                        PrintLineRed__(string.Format(" {0,-11}{1,-22}{2,-12}{3,-13}",
                            classroom.assignmentID, classroom.assignmentName, classroom.assignmentGrade, classroom.assignmentStatus));
                    else
                        Console.WriteLine(string.Format(" {0,-11}{1,-22}{2,-12}{3,-13}",
                            classroom.assignmentID, classroom.assignmentName, classroom.assignmentGrade, classroom.assignmentStatus));
                }
            }
            return 0;
        }
        public static int AddAssignment(int classIndex, int stdIndex)
        {
            string addGrade;
            int newGrade = 0;
            int assignmentIndex = 0;
            int newAssignmentID = FindNextAvailableAssignmentID(classIndex, stdIndex);

            // Menu with all student's details
            ViewStdAssignments(classIndex, stdIndex, -1); // Use -1 when no highlights are necessary
            PrintBlue_("\n Enter the name of the new Assignment: ".ToUpper());
            string newAssignmentName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Console.ReadLine().ToLower());;
            // Adds the new assignment to the students list of assignments.
            classrooms[classIndex].students[stdIndex].assignments.Add(new Assignment(newAssignmentID, newAssignmentName));
            // Fetch the new assignment Index
            assignmentIndex = classrooms[classIndex].students[stdIndex].assignments.FindIndex(x => x.assignmentID == newAssignmentID);
            // Menu with all student's details after the addition. It will all show the new assignment highlighted red
            ViewStdAssignments(classIndex, stdIndex, newAssignmentID);
            PrintLineRed__("\n Assignment added successfully!");
            // Asks the user if he/she wants to add a grade to the new assignment
            Console.WriteLine("\n Would you like to add a grade to the new Assignment?");
            do
            {
                PrintBlue_(" Type \"Yes\" to proceed or \"No\" to Cancel: ");
                addGrade = Console.ReadLine().ToLower();
                if (addGrade == "yes") // User wants to add a grade to the new assignment
                {
                    do
                    {
                        try
                        {
                            PrintBlue_("\n Please enter a grade: ".ToUpper());
                            newGrade = int.Parse(Console.ReadLine());
                            // Checks if the grade provided is valid (0 to 100)
                            classrooms[classIndex].students[stdIndex].assignments[assignmentIndex].assignmentGrade = newGrade;
                            if (newGrade < 0 || newGrade > 100) // If grade is not within the paramenters...
                                PrintLineRed__(" You have entered an invalid entry.\n Please enter a number between 0 and 100.");
                            else // Otherwise...
                            {
                                // Marks the newly added assignment complete, even if the grade is 0.
                                classrooms[classIndex].students[stdIndex].assignments[assignmentIndex].assignmentStatus = "Complete";                                
                                ViewStdAssignments(classIndex, stdIndex, newAssignmentID) ;// Menu with all student's info. Shows the new assignment highlighted in red
                                PrintLineBlue_("\n\n Grade successfully added!");
                                Console.WriteLine(" Press any key to continue.");
                                Console.ReadKey();
                            }                            
                        }
                        catch
                        {
                            // Error if the user enters anything than the accepted parameters...to include characters
                            PrintLineRed__(" You have entered an invalid entry.\n Please enter a number between 0 and 100.");
                        }
                    } while (newGrade < 0 || newGrade > 100);
                    return 0;
                } else if (addGrade == "no")
                {
                    return 0;
                } else
                {
                    continue;
                }
            } while (addGrade != "yes" || addGrade != "no");
            return 0;
        }
        public static int AddEditAssignmentGrade(int classIndex, int stdIndex)
        {
            int newGrade = 0;
            int assignmentIndex = 0;
            int assignmentID;
                        
            ViewStdAssignments(classIndex, stdIndex, -1); // Menu with all student's details. Use -1 when highlights are not necessary
            if (classrooms[classIndex].students[stdIndex].assignments.Count <= 0)
            {   // Returns the user to the previous menu as there are no assignments to grade/edit
                PrintLineRed__(" Press any key to continue."); Console.ReadKey(); return 0;
            }
            PrintLineRed__("\n Type \"Q\" to cancel this operation. ");
            PrintBlue_(" Enter the Assignment's name or ID number: ");
            string assignmentName = Console.ReadLine().ToLower(); // Takes the user's input (the assignment's name or ID) to find the assignment to be graded            
            if (assignmentName == "q") { return 0; } // Quits this method if the user wants to by typing "q"             
            bool indexTest = assignmentName.All(char.IsDigit); // Tests if the user's input can be converted into a digit
            // This allows the use of either the assignment ID or its name
            if (indexTest == true)
            {
                assignmentIndex = classrooms[classIndex].students[stdIndex].assignments.FindIndex(x => x.assignmentID == int.Parse(assignmentName));
                if (assignmentIndex == -1)
                    assignmentIndex = classrooms[classIndex].students[stdIndex].assignments.FindIndex(x => x.assignmentName.ToLower() == assignmentName);
            } else
                assignmentIndex = classrooms[classIndex].students[stdIndex].assignments.FindIndex(x => x.assignmentName.ToLower() == assignmentName);            
            if (assignmentIndex == -1) // If the index of the assignment input from the user is incorrect, this method will restart
            {
                PrintLineRed__(" You have entered an invalid name or ID. \n Press any key and try again."); Console.ReadKey(); return 4;
            }
            else // If the assignment exists, then...
            {   // Get the assignment ID to highlight it on the page
                assignmentID = classrooms[classIndex].students[stdIndex].assignments[assignmentIndex].assignmentID;                
                ViewStdAssignments(classIndex, stdIndex, assignmentID); // Menu with all student's details. Shows the new assignment highlighted red
                do
                {
                    try
                    {
                        PrintBlue_("\n Please enter a grade: ");
                        newGrade = int.Parse(Console.ReadLine());                        
                        if (newGrade < 0 || newGrade > 100) // If the grade its outside the paremeters, repeat this block of code
                            PrintLineRed__(" You have entered an invalid entry.\n Please enter a number between 0 and 100.");
                        else
                        {                             
                            classrooms[classIndex].students[stdIndex].assignments[assignmentIndex].assignmentGrade = newGrade; // Adds new grade to the selected assignment                            
                            classrooms[classIndex].students[stdIndex].assignments[assignmentIndex].assignmentStatus = "Complete"; // Sets assignment status to complete
                            ViewStdAssignments(classIndex, stdIndex, assignmentID); // Menu with all student's details. Shows the new assignment highlighted red
                            PrintLineRed__("\n\n Grade successfully edited/added!");
                            Console.WriteLine(" Press any key to continue.");
                            Console.ReadKey();
                        }
                    }
                    catch
                    {
                        // Repeats the loop when the user input is incorrect
                        PrintLineRed__(" You have entered an invalid entry.\n Please enter a number between 0 and 100.");
                    }
                } while (newGrade < 0 || newGrade > 100);
                return 0;
            }            
        }
        public static int RemoveAssignment(int classIndex, int stdIndex)
        {
            string assignmentName, deleteAssignment;
            int assignmentIndex = 0, assignmentID;

            ViewStdAssignments(classIndex, stdIndex, -1); // Menu with all student's details. Use -1 when highlights are not necessary
            // Returns the user to the previous menu as there are no assignments to grade/edit. Otherwise, continue with adding/editing method
            if (classrooms[classIndex].students[stdIndex].assignments.Count <= 0)
            {
                PrintLineRed__(" Press any key to continue."); Console.ReadKey(); return 0;
            }            
            PrintLineRed__("\n Type \"Q\" to cancel this operation.");
            PrintBlue_(" Enter the Assignment name or ID number: ");
            // Takes the user's input (the assignment's name or ID) to find the assignment to be graded
            assignmentName = Console.ReadLine().ToLower();
            // Quits this method if the user wants to by typing "q"
            if (assignmentName == "q") { return 0; }
            // Tests if the user's input can be converted into a digit 
            bool indexTest = assignmentName.All(char.IsDigit);
            if (indexTest == true)
            {   // If user entered the assignment ID number then...
                assignmentIndex = classrooms[classIndex].students[stdIndex].assignments.FindIndex(x => x.assignmentID == int.Parse(assignmentName)); // ID
                if (assignmentIndex == -1) // If ID is not found then try by name
                    assignmentIndex = classrooms[classIndex].students[stdIndex].assignments.FindIndex(x => x.assignmentName.ToLower() == assignmentName);
            }
            else // If user entered the assignment name then...
                assignmentIndex = classrooms[classIndex].students[stdIndex].assignments.FindIndex(x => x.assignmentName.ToLower() == assignmentName);
            if (assignmentIndex == -1)
            {   // If the user input is incorrect, this method will restart
                PrintLineRed__(" You have entered an invalid name or ID. \n Press any key and try again."); Console.ReadKey(); return 4;
            }
            else
            {                
                assignmentID = classrooms[classIndex].students[stdIndex].assignments[assignmentIndex].assignmentID; // Get the assignment ID to highlight it on the page               
                ViewStdAssignments(classIndex, stdIndex, assignmentID); // Menu with all student's details after the addition. It will all show the new assignment highlighted red
                // Gets the assignment to be deleted name to present it to the user 
                assignmentName = classrooms[classIndex].students[stdIndex].assignments[assignmentIndex].assignmentName;
                PrintRed__("\n Are you sure you want to delete ");
                Console.Write(assignmentName);
                PrintRed__("?");
                do
                {
                    PrintRed__("\n Type \"Yes\" to proceed or \"No\" to Cancel. ");
                    ColorChangeToBlue();
                    deleteAssignment = Console.ReadLine().ToLower();
                    if (deleteAssignment == "yes")
                    {                        
                        classrooms[classIndex].students[stdIndex].assignments.RemoveAt(assignmentIndex); // Removes the assignment using it's index
                        PrintRed__($"\n {assignmentName}");
                        Console.WriteLine(" has been successfully deleted!");
                        Console.Write(" Press any key to continue.");
                        Console.ReadKey();
                        return 0;
                    }
                    else if (deleteAssignment == "no")
                    {
                        return 0;
                    }
                } while (deleteAssignment != "no" || deleteAssignment != "yes");
                return 0;
            }            
        }
    }
}
