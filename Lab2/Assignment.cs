using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lab2.GlobalVariables;
using static Lab2.Assignment;
using static Lab2.Tools;
using static Lab2.Headers;
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
        public static void ViewAssignment(int classIndex, int stdIndex)
        {
            int stdIndexHelper = 0;

            if (classrooms[classIndex].students[stdIndex].assignments.Count <= 0)
            {
                PrintLineRed__("\n There are no assignments for this student.");                
            } else
            {
                StdMenuHeader();
                foreach (var classroom in classrooms[classIndex].students[stdIndex].assignments)
                {
                    Console.WriteLine(string.Format(" {0,-11}{1,-22}{2,-12}{3,-13}",
                        classroom.assignmentID, classroom.assignmentName, classroom.assignmentGrade, classroom.assignmentStatus));
                    stdIndexHelper++;
                }
                stdIndexHelper = 0;
            }
        }
        public static int AddAssignment(int classIndex, int stdIndex)
        {
            string stdName = classrooms[classIndex].students[stdIndex].studName;
            int stdIndexHelper = 0;
            string addGrade;
            int newGrade =0;
            int assignmentIndex = 0;

            Console.Clear();
            var a = SecondIndividualClassStats(classIndex);
            StdSubMenuHeader(classIndex, stdIndex, StdGPACalc(classIndex, stdIndex).ToString());
            ClassSubHeader();
            foreach (var classroom in classrooms[classIndex].students[stdIndex].assignments)
            {
                Console.WriteLine(string.Format(" {0,-9}{1,-25}{2,-15}{3,-13}",
                    classroom.assignmentID, classroom.assignmentName, classroom.assignmentGrade, classroom.assignmentStatus,
                    StdGPACalc(classIndex, stdIndex)));
                stdIndexHelper++;
            }
            stdIndexHelper = 0;

            int newAssignmentID = FindNextAvailableAssignmentID(classIndex, stdIndex);

            PrintBlue_("\n Enter the name of the new Assignment: ");
            string newAssignmentName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Console.ReadLine().ToLower());
;
            classrooms[classIndex].students[stdIndex].assignments.Add(new Assignment(newAssignmentID, newAssignmentName));
            assignmentIndex = classrooms[classIndex].students[stdIndex].assignments.FindIndex(x => x.assignmentID == newAssignmentID);

            Console.Clear();
            StdSubMenuHeader(classIndex, stdIndex, StdGPACalc(classIndex, stdIndex).ToString());///
            ClassSubHeader();
            
            foreach (var classroom in classrooms[classIndex].students[stdIndex].assignments)
            {
                if (classroom.assignmentID != newAssignmentID)
                {
                    Console.WriteLine(string.Format(" {0,-9}{1,-25}{2,-15}{3,-13}",
                    classroom.assignmentID, classroom.assignmentName, classroom.assignmentGrade, classroom.assignmentStatus,
                    StdGPACalc(classIndex, stdIndex)));
                    stdIndexHelper++;
                }
                else
                {
                    PrintLineRed__(string.Format(" {0,-9}{1,-25}{2,-15}{3,-13}",
                    classroom.assignmentID, classroom.assignmentName, classroom.assignmentGrade, classroom.assignmentStatus,
                    StdGPACalc(classIndex, stdIndex)));
                    stdIndexHelper++;
                }
            }
            stdIndexHelper = 0;
            PrintLineRed__("\n Assignment added successfully!");
            Console.WriteLine("\n Would you like to add a grade to the new Assignment?");
            do
            {
                PrintBlue_(" Type \"Yes\" to proceed or \"No\" to Cancel: ");
                addGrade = Console.ReadLine().ToLower();
                if (addGrade == "yes")
                {
                    do
                    {
                        try
                        {
                            PrintBlue_("\n Please enter a grade: ");
                            newGrade = int.Parse(Console.ReadLine());
                            classrooms[classIndex].students[stdIndex].assignments[assignmentIndex].assignmentGrade = newGrade;
                            if (newGrade < 0 || newGrade > 100)
                            {
                                PrintLineRed__(" You have entered an invalid entry.\n Please enter a number between 0 and 100.");
                            } else                            
                            {
                                classrooms[classIndex].students[stdIndex].assignments[assignmentIndex].assignmentStatus = "Complete";
                                Console.Clear();
                                stdIndexHelper = 0;
                                StdSubMenuHeader(classIndex, stdIndex, StdGPACalc(classIndex, stdIndex).ToString());///
                                ClassSubHeader();
                                foreach (var classroom in classrooms[classIndex].students[stdIndex].assignments)
                                {
                                    if (classroom.assignmentID != newAssignmentID)
                                    {
                                        Console.WriteLine(string.Format(" {0,-9}{1,-25}{2,-15}{3,-13}",
                                        classroom.assignmentID, classroom.assignmentName, classroom.assignmentGrade, classroom.assignmentStatus,
                                        StdGPACalc(classIndex, stdIndex)));
                                        stdIndexHelper++;
                                    }
                                    else
                                    {
                                        PrintRed__(string.Format(" {0,-9}{1,-25}{2,-15}{3,-13}",
                                        classroom.assignmentID, classroom.assignmentName, classroom.assignmentGrade, classroom.assignmentStatus,
                                        StdGPACalc(classIndex, stdIndex)));
                                        stdIndexHelper++;
                                    }
                                }                                
                                PrintLineRed__("\n\n Grade successfully added!");
                                Console.WriteLine(" Press any key to continue.");
                                Console.ReadKey();
                            }                            
                        }
                        catch
                        {
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
        public static int AddEditGrade(int classIndex, int stdIndex)
        {
            string stdName = classrooms[classIndex].students[stdIndex].studName;
            int stdIndexHelper = 0;
            int newGrade = 0;

            Console.Clear();
            var a = SecondIndividualClassStats(classIndex);
            StdSubMenuHeader(classIndex, stdIndex, StdGPACalc(classIndex, stdIndex).ToString());
            ViewAssignment(classIndex, stdIndex);

            if (classrooms[classIndex].students[stdIndex].assignments.Count <= 0)
            {
                PrintLineRed__(" Press any key to continue.");
                Console.ReadKey();
                return 0;
            }

            Console.Clear();
            StdSubMenuHeader(classIndex, stdIndex, StdGPACalc(classIndex, stdIndex).ToString());
            ViewAssignment(classIndex, stdIndex);
            PrintLineRed__("\n Type \"Q\" to cancel this operation. ");
            PrintBlue_(" Enter the Assignment's name or ID number: ");
            string assignmentName = Console.ReadLine().ToLower();
            int index = classrooms[classIndex].students[stdIndex].assignments.FindIndex(x => x.assignmentName.ToLower() == assignmentName);

            if (assignmentName == "q")
            {
                return 0; // mainMenuSelection to cancel operation
            }

            bool indexTest = assignmentName.All(char.IsDigit);
            if (indexTest == false)
            {
                int assignmentNameIndex = classrooms[classIndex].students[stdIndex].assignments.FindIndex(x => x.assignmentName.ToLower() == assignmentName);
                if (assignmentNameIndex == -1)
                {
                    PrintLineRed__(" You have entered an invalid name or ID. \n Press any key and try again.");
                    Console.ReadKey();
                    return 4; // classMainMenuSelection
                }
                else
                {
                    assignmentName = classrooms[classIndex].students[stdIndex].assignments[assignmentNameIndex].assignmentName;
                    Console.Clear();
                    Console.Clear();
                    StdSubMenuHeader(classIndex, stdIndex, StdGPACalc(classIndex, stdIndex).ToString());
                    ViewAssignment(classIndex, stdIndex);
                    do
                    {
                        try
                        {
                            PrintBlue_("\n Please enter a grade: ");
                            newGrade = int.Parse(Console.ReadLine());
                            classrooms[classIndex].students[stdIndex].assignments[assignmentNameIndex].assignmentGrade = newGrade;
                            if (newGrade < 0 || newGrade > 100)
                            {
                                PrintLineRed__(" You have entered an invalid entry.\n Please enter a number between 0 and 100.");
                            }
                            else
                            {
                                classrooms[classIndex].students[stdIndex].assignments[assignmentNameIndex].assignmentStatus = "Complete";
                                Console.Clear();
                                stdIndexHelper = 0;
                                StdSubMenuHeader(classIndex, stdIndex, StdGPACalc(classIndex, stdIndex).ToString());///
                                ClassSubHeader();
                                foreach (var classroom in classrooms[classIndex].students[stdIndex].assignments)
                                {
                                    if (classroom.assignmentGrade != newGrade)
                                    {
                                        Console.WriteLine(string.Format(" {0,-9}{1,-25}{2,-15}{3,-13}",
                                        classroom.assignmentID, classroom.assignmentName, classroom.assignmentGrade, classroom.assignmentStatus,
                                        StdGPACalc(classIndex, stdIndex)));
                                        stdIndexHelper++;
                                    }
                                    else
                                    {
                                        PrintRed__(string.Format(" {0,-9}{1,-25}{2,-15}{3,-13}",
                                        classroom.assignmentID, classroom.assignmentName, classroom.assignmentGrade, classroom.assignmentStatus,
                                        StdGPACalc(classIndex, stdIndex)));
                                        stdIndexHelper++;
                                    }
                                }
                                PrintLineRed__("\n\n Grade successfully edited/added!");
                                Console.WriteLine(" Press any key to continue.");
                                Console.ReadKey();
                            }
                        }
                        catch
                        {
                            PrintLineRed__(" You have entered an invalid entry.\n Please enter a number between 0 and 100.");
                        }
                    } while (newGrade < 0 || newGrade > 100);
                    return 0;
                }
            }
            else
            {
                int assignmentIDIndex = classrooms[classIndex].students[stdIndex].assignments.FindIndex(x => x.assignmentID == int.Parse(assignmentName));
                if (assignmentIDIndex == -1)
                {
                    PrintLineRed__(" You have entered an invalid name or ID. \n Press any key and try again.");
                    Console.ReadKey();
                    return 4; // classMainMenuSelection
                }
                else
                {
                    assignmentName = classrooms[classIndex].students[stdIndex].assignments[assignmentIDIndex].assignmentName;
                    Console.Clear();
                    Console.Clear();
                    StdSubMenuHeader(classIndex, stdIndex, StdGPACalc(classIndex, stdIndex).ToString());
                    ViewAssignment(classIndex, stdIndex);
                    do
                    {
                        try
                        {
                            PrintBlue_("\n Please enter a grade: ");
                            newGrade = int.Parse(Console.ReadLine());
                            classrooms[classIndex].students[stdIndex].assignments[assignmentIDIndex].assignmentGrade = newGrade;
                            if (newGrade < 0 || newGrade > 100)
                            {
                                PrintLineRed__(" You have entered an invalid entry.\n Please enter a number between 0 and 100.");
                            }
                            else
                            {
                                classrooms[classIndex].students[stdIndex].assignments[assignmentIDIndex].assignmentStatus = "Complete";
                                Console.Clear();
                                stdIndexHelper = 0;
                                StdSubMenuHeader(classIndex, stdIndex, StdGPACalc(classIndex, stdIndex).ToString());///
                                ClassSubHeader();
                                foreach (var classroom in classrooms[classIndex].students[stdIndex].assignments)
                                {
                                    if (classroom.assignmentGrade != newGrade)
                                    {
                                        Console.WriteLine(string.Format(" {0,-9}{1,-25}{2,-15}{3,-13}",
                                        classroom.assignmentID, classroom.assignmentName, classroom.assignmentGrade, classroom.assignmentStatus,
                                        StdGPACalc(classIndex, stdIndex)));
                                        stdIndexHelper++;
                                    }
                                    else
                                    {
                                        PrintRed__(string.Format(" {0,-9}{1,-25}{2,-15}{3,-13}",
                                        classroom.assignmentID, classroom.assignmentName, classroom.assignmentGrade, classroom.assignmentStatus,
                                        StdGPACalc(classIndex, stdIndex)));
                                        stdIndexHelper++;
                                    }
                                }
                                PrintLineRed__("\n\n Grade successfully edited/added!");
                                Console.WriteLine(" Press any key to continue.");
                                Console.ReadKey();
                            }
                        }
                        catch
                        {
                            PrintLineRed__(" You have entered an invalid entry.\n Please enter a number between 0 and 100.");
                        }
                    } while (newGrade < 0 || newGrade > 100);
                    return 0;
                }
            }
        }
        public static int RemoveAssignment(int classIndex, int stdIndex)
        {
            string stdName = classrooms[classIndex].students[stdIndex].studName;
            int stdIndexHelper = 0;
            string deleteAssignment;

            Console.Clear();
            var a = SecondIndividualClassStats(classIndex);
            StdSubMenuHeader(classIndex, stdIndex, StdGPACalc(classIndex, stdIndex).ToString());            

            ClassSubHeader();
            foreach (var classroom in classrooms[classIndex].students[stdIndex].assignments)
            {
                Console.WriteLine(string.Format(" {0,-9}{1,-25}{2,-15}{3,-13}",
                    classroom.assignmentID, classroom.assignmentName, classroom.assignmentGrade, classroom.assignmentStatus,
                    StdGPACalc(classIndex, stdIndex)));
                stdIndexHelper++;
            }
            stdIndexHelper = 0;
            if (classrooms[classIndex].students[stdIndex].assignments.Count <= 0)
            {
                PrintLineRed__("\n There are no assignment for this student. \n Press any key to continue.");
                Console.ReadKey();
                return 0;
            }

            int newAssignmentID = FindNextAvailableAssignmentID(classIndex, stdIndex);

            PrintLineRed__("\n Type \"Q\" to cancel this operation.");
            PrintBlue_(" Enter the Assignment name or ID number: ");
            string assignmentName = Console.ReadLine().Trim().ToLower();

            if (assignmentName == "q")
            {
                return 0; // mainMenuSelection to cancel operation
            }

            bool indexTest = assignmentName.All(char.IsDigit);
            if (indexTest == false)
            {
                int assignmentNameIndex = classrooms[classIndex].students[stdIndex].assignments.FindIndex(x => x.assignmentName.ToLower() == assignmentName);
                if (assignmentNameIndex == -1)
                {
                    PrintLineRed__(" You have entered an invalid name or ID. \n Press any key and try again.");
                    Console.ReadKey();
                    return 3; // classMainMenuSelection
                }
                else
                {
                    assignmentName = classrooms[classIndex].students[stdIndex].assignments[assignmentNameIndex].assignmentName;
                    Console.Clear();
                    StdSubMenuHeader(classIndex, stdIndex, StdGPACalc(classIndex, stdIndex).ToString());
                    ClassSubHeader();
                    foreach (var classroom in classrooms[classIndex].students[stdIndex].assignments)
                    {
                        Console.WriteLine(string.Format(" {0,-9}{1,-25}{2,-15}{3,-13}",
                            classroom.assignmentID, classroom.assignmentName, classroom.assignmentGrade, classroom.assignmentStatus,
                            StdGPACalc(classIndex, stdIndex)));
                        stdIndexHelper++;
                    }
                    stdIndexHelper = 0;
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
                            classrooms[classIndex].students[stdIndex].assignments.RemoveAt(assignmentNameIndex);
                            PrintRed__($"\n {assignmentName}");
                            Console.WriteLine(" has been successfully deleted!");
                            Console.Write(" Press any key to continue.");
                            Console.ReadKey();
                            return 0; // mainMenuSelection to complete operation;
                        }
                        else if (deleteAssignment == "no")
                        {
                            return 0; // mainMenuSelection to cancel operation
                        }
                    } while (deleteAssignment != "no" || deleteAssignment != "yes");
                    return 0; // mainMenuSelection to complete operation
                }
            }
            else
            {
                int assignmentIDIndex = classrooms[classIndex].students[stdIndex].assignments.FindIndex(x => x.assignmentID == int.Parse(assignmentName));
                if (assignmentIDIndex == -1)
                {
                    PrintLineRed__(" You have entered an invalid name or ID. \n Press any key and try again.");
                    Console.ReadKey();
                    return 3; // classMainMenuSelection
                }
                else
                {
                    assignmentName = classrooms[classIndex].students[stdIndex].assignments[assignmentIDIndex].assignmentName;
                    Console.Clear();
                    Console.Clear();
                    StdSubMenuHeader(classIndex, stdIndex, StdGPACalc(classIndex, stdIndex).ToString());
                    ClassSubHeader();
                    foreach (var classroom in classrooms[classIndex].students[stdIndex].assignments)
                    {
                        Console.WriteLine(string.Format(" {0,-9}{1,-25}{2,-15}{3,-13}",
                            classroom.assignmentID, classroom.assignmentName, classroom.assignmentGrade, classroom.assignmentStatus,
                            StdGPACalc(classIndex, stdIndex)));
                        stdIndexHelper++;
                    }
                    stdIndexHelper = 0;
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
                            classrooms[classIndex].students[stdIndex].assignments.RemoveAt(assignmentIDIndex);
                            PrintRed__($"\n {assignmentName}");
                            Console.WriteLine(" has been successfully deleted!");
                            Console.Write(" Press any key to continue.");
                            Console.ReadKey();
                            return 0; // mainMenuSelection to complete operation;
                        }
                        else if (deleteAssignment == "no")
                        {
                            return 0; // mainMenuSelection to cancel operation
                        }
                    } while (deleteAssignment != "no" || deleteAssignment != "yes");
                    return 0; // mainMenuSelection to complete operation
                }
            }
        }
    }
}
