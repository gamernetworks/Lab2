﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lab2.GlobalVariables;
using static Lab2.Headers;
using static Lab2.Tools;
using static Lab2.Menus;
using static Lab2.Student;

namespace Lab2
{
    internal class Classroom
    {



        public int classID;
        public string className;
        public List<Student> students = new List<Student>();
        public Classroom() { }
        public Classroom(string name)
        {
            className = name;
        }
        public Classroom(string name, Student a) : this(name) { }
        public Classroom(int ID, string name) : this(name)
        {
            classID = ID;
        }
        public Classroom(int ID, string name, Student a) : this(ID, name) { }
        public static int SchoolInfo(int updatingElementID) // -1 = Deactivate highlights
        {
            int i = 0; // ClassAvgGPACalc Helper

            ClassroomMenuHeader(); // View Classroom header            
            if (classrooms.Count() <= 0 && updatingElementID == -1)
            {   // Returns this message if there are no classrooms
                PrintLineRed__("\n There are no classrooms to display."); return 0;
            }
            else
            {
                foreach (var classroom in classrooms)
                {
                    if (classroom.classID == updatingElementID)
                    {   // Highlight the class info if the classID is passed to the method.
                        PrintLineRed__(string.Format(" {0,-9}{1,-23}{2,-16}{3,-14}", classroom.classID, classroom.className,
                            classroom.students.Count, ClassAvgGPACalc(i))); i++;
                    }
                    else
                    {   // Print normal color
                        Console.WriteLine(string.Format(" {0,-9}{1,-23}{2,-16}{3,-14}", classroom.classID, classroom.className,
                        classroom.students.Count, ClassAvgGPACalc(i))); i++;
                    }
                }
                return 1; // To continue the opetation in the origin caller
            }
        }      
        public static int ManageClass()
        {
            bool indexTest;
            int classIndex;
            string className;

            if (SchoolInfo(-1) == 0)
            {   // View Classroom header. If theare are no classrooms, quit module
                Console.WriteLine("\n Press any key to continue"); Console.ReadKey(); return 0;
            }
            PrintLineRed__("\n Type \"Q\" to cancel this operation. ");
            PrintBlue_(" Enter the classroom's name or ID number: ".ToUpper());
            className = Console.ReadLine().ToLower();
            if (className == "q") { return 0; } // Quits this method if the user wants to by typing "q"
            indexTest = className.All(char.IsDigit); // Tests if the user's input can be converted into a digit
            if (indexTest == true)
            {   // If the user entered the class number then...
                classIndex = classrooms.FindIndex(x => x.classID == int.Parse(className)); // Try the class ID
                if (classIndex == -1) // Try the class name if class ID is not found
                    classIndex = classrooms.FindIndex(x => x.className.ToLower() == className);
            } else // If the user entered the class name then...
                classIndex = classrooms.FindIndex(x => x.className.ToLower() == className);
            if (classIndex == -1)
            {   // If the input from the user is incorrect, this method will restart
                PrintLineRed__(" You have entered an invalid name or ID. \n Press any key and try again."); Console.ReadKey(); return 1;
            } else
                ClassSubMenu(classIndex); return 0;
        }
        public static int AddClass()
        {
            int newClassID = FindNextAvailableClassID();

            SchoolInfo(-1); // View Classroom header
            PrintLineRed__("\n Type \"Q\" to cancel this operation. ");
            PrintBlue_(" Enter the name of the new classroom: ".ToUpper());
            // Takes user's input and capitalizes the first letter of each word
            string newClassName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Console.ReadLine().ToLower());
            if (newClassName == "Q")
            {   // If the user types q or Q, then quit the module.
                return 0;
            }
            else
            {
                // Add new Classroom obj. Gets the next available ClassID using FindNextAvailableClassID()
                classrooms.Add(new Classroom(newClassID, newClassName));
                SchoolInfo(newClassID); // Passing new class ID for highlight
                PrintLineRed__("\n Class added successfully!");
                Console.WriteLine(" Press any key to continue."); Console.ReadKey(); return 0;
            }
        }
        public static int EditClassName()
        {
            string className, newClassName;
            int classIndex = 0, classID;
            bool indexTest;

            if (SchoolInfo(-1) == 0)
            {   // View Classroom header. If theare are no classrooms, quit module
                Console.WriteLine("\n Press any key to continue"); Console.ReadKey(); return 0;
            }
            PrintLineRed__("\n Type \"Q\" to cancel this operation. ");
            PrintBlue_(" Enter the classroom's name or ID number: ".ToUpper());
            className = Console.ReadLine().ToLower();
            if (className == "q") { return 0; } // Quits this method if the user wants to by typing "q"
            indexTest = className.All(char.IsDigit); // Tests if the user's input can be converted into a digit
            if (indexTest == true)
            {   // If the user entered the class number then...
                classIndex = classrooms.FindIndex(x => x.classID == int.Parse(className)); // Try the class ID
                if (classIndex == -1) // Try the class name if class ID is not found
                    classIndex = classrooms.FindIndex(x => x.className.ToLower() == className);
            }
            else
            {   // If the user entered the class name then...
                classIndex = classrooms.FindIndex(x => x.className.ToLower() == className);
            }
            if (classIndex == -1)
            {   // If the input from the user is incorrect, this method will restart
                PrintLineRed__(" You have entered an invalid name or ID. \n Press any key and try again."); Console.ReadKey(); return 4;
            }
            else
            {
                classID = classrooms[classIndex].classID; // Get the class ID to highlight it on the page
                className = classrooms[classIndex].className; // Get the class name to display the success msg
                SchoolInfo(classIndex); // View the class information with the class being edited highlighted in red                
                PrintBlue_("\n Enter a new name for this classroom: "); // Gets the class to be deleted name to present it to the user
                newClassName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Console.ReadLine().ToLower());
                classrooms[classIndex].className = newClassName; // Edits the class name
                SchoolInfo(classIndex);
                Console.Write($"\n Classroom ");
                PrintRed__(className);
                Console.Write(" has been successfully renamed ");
                PrintLineRed__($"{newClassName}.");
                Console.WriteLine(" Press any key to continue.");
                Console.ReadKey();
                return 0;
            }
        }
        public static int RemoveClass()
        {
            string className, deleteClass;
            int classIndex = 0, classID;
            bool indexTest;

            if (SchoolInfo(-1) == 0)
            {   // View Classroom header. If theare are no classrooms, quit module
                Console.WriteLine("\n Press any key to continue"); Console.ReadKey(); return 0;
            }
            PrintLineRed__("\n Type \"Q\" to cancel this operation. ");
            PrintBlue_(" Enter the classroom's name or ID number: ".ToUpper());
            className = Console.ReadLine().ToLower();
            if (className == "q") { return 0; } // Quits this method if the user wants to by typing "q"
            indexTest = className.All(char.IsDigit); // Tests if the user's input can be converted into a digit
            if (indexTest == true)
            {   // If the user entered the class number then...
                classIndex = classrooms.FindIndex(x => x.classID == int.Parse(className)); // Try the class ID
                if (classIndex == -1) // Try the class name if class ID is not found
                    classIndex = classrooms.FindIndex(x => x.className.ToLower() == className);
            } else
            {   // If the user entered the class name then...
                classIndex = classrooms.FindIndex(x => x.className.ToLower() == className);
            }
            if (classIndex == -1)
            {   // If the input from the user is incorrect, this method will restart
                PrintLineRed__(" You have entered an invalid name or ID. \n Press any key and try again."); Console.ReadKey(); return 4;
            } else
            {
                className = classrooms[classIndex].className; // Stores the class to be deleted
                classID = classrooms[classIndex].classID; // Gets the class ID to be passed below
                SchoolInfo(classID); // View Classroom header and highlights the class to be deleted
                PrintRed__("\n Are you sure you want to delete ");
                Console.Write(className);
                PrintRed__("?");
                do
                {   // Loops until the user enters yes, no or completes the deletion process
                    PrintRed__("\n Type \"Yes\" to proceed or \"No\" to Cancel. ");
                    ColorChangeToBlue();
                    deleteClass = Console.ReadLine().ToLower();
                    if (deleteClass == "yes")
                    {   // If yes then...
                        classrooms.RemoveAt(classIndex); // Removes the class
                        PrintRed__($"\n {className}");
                        Console.WriteLine(" has been successfully deleted! \n Press any key to continue.");
                        Console.ReadKey();
                        return 0; // mainMenuSelection to complete operation;
                    }
                    else if (deleteClass == "no")
                        return 0; // Operation is canceled
                } while (deleteClass != "no" || deleteClass != "yes");
                return 0;
            }
        }
    }
}
