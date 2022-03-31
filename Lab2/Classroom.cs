using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lab2.GlobalVariables;
using static Lab2.Headers;
using static Lab2.Tools;
using static Lab2.Menus;

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
        public static void ViewAllClassroomsInfo()
        {
            //int classCount = classrooms.Count;
            int i = 0;

            ClassMainHeader();
            foreach (var classroom in classrooms)
            {
                Console.WriteLine(string.Format(" {0,-9}{1,-23}{2,-16}{3,-14}", classroom.classID, classroom.className, classroom.students.Count,
                    ClassAvgGPAStats(i)));
                i++;
            }
        }
        public static void ViewAllClassroomsInfo(string className)
        {
            //int classCount = classrooms.Count;
            int i = 0;

            ClassMainHeader();
            foreach (var classroom in classrooms)
            {
                if (classroom.className != className)
                {
                    Console.WriteLine(string.Format(" {0,-9}{1,-23}{2,-16}{3,-14}", classroom.classID, classroom.className, classroom.students.Count, ClassAvgGPAStats(i)));
                    i++;
                }else
                {
                    ColorChangeToRed();
                    Console.WriteLine(string.Format(" {0,-9}{1,-23}{2,-16}{3,-14}", classroom.classID, classroom.className, classroom.students.Count, ClassAvgGPAStats(i)));
                    Console.ResetColor();
                }
            }
        }
        public static int ManageClass()
        {
            Console.Clear();
            ClassMainMenuHeader();
            ViewAllClassroomsInfo();
            if (classrooms.Count <= 0)
            {
                PrintLineRed__("\n There are no classrooms in the program. \n Press any key to continue.");
                Console.ReadKey();
                return 0;
            }
            PrintLineRed__("\n Type \"Q\" to cancel this operation. ");
            PrintBlue_(" Enter the classroom's name or ID number: ");
            string className = Console.ReadLine().ToLower();

            if (className == "q")
            {
                return 0; // mainMenuSelection to cancel operation
            }
            bool indexTest = className.All(char.IsDigit);
            if (indexTest == false)
            {
                int classNameIndex = classrooms.FindIndex(x => x.className.ToLower() == className);
                if (classNameIndex == -1)
                {
                    PrintLineRed__(" You have entered an invalid name or ID. \n Press any key and try again.");
                    Console.ReadKey();
                    return 1; // classMainMenuSelection
                }
                else
                {
                    ClassSubMenu(classNameIndex);
                    return 0;
                }
            } else
            {
                int classIDIndex = classrooms.FindIndex(x => x.classID == int.Parse(className));
                if (classIDIndex == -1)
                {
                    PrintLineRed__(" You have entered an invalid name or ID. \n Press any key and try again.");
                    Console.ReadKey();
                    return 1; // classMainMenuSelection
                }
                else
                {
                    ClassSubMenu(classIDIndex);
                    return 0;
                }
            }
        }
        public static int ManageStd(int classIndex)
        {
            string className = classrooms[classIndex].className;

            Console.Clear();
            var a = SecondIndividualClassStats(classIndex);
            int stdIndex = 0;
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
            } else
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
                    StdMenu(classIndex, stdIDIndex);
                    return 0;
                }
            }
        }
        public static int AddClass()
        {
            Console.Clear();
            ClassMainMenuHeader();
            ViewAllClassroomsInfo();
            PrintLineRed__("\n Type \"Q\" to cancel this operation. ");
            PrintBlue_(" Enter the name of the new classroom: ");
            string newClassName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Console.ReadLine().ToLower());
            if (newClassName == "Q")
            {
                return 0; // mainMenuSelection to cancel operation
            } else
            {
                classrooms.Add(new Classroom(FindNextAvailableClassID(), newClassName));
                Console.Clear();
                ClassMainMenuHeader();
                ViewAllClassroomsInfo(newClassName);                
                PrintLineRed__("\n Class added successfully!");
                Console.WriteLine(" Press any key to continue.");
                Console.ReadKey();
                return 0;
            }            
        }
        public static int EditClassName()
        {
            Console.Clear();
            ClassMainMenuHeader();
            ViewAllClassroomsInfo();
            if (classrooms.Count <= 0)
            {
                PrintLineRed__("\n There are no classrooms in the program. \n Press any key to continue.");
                Console.ReadKey();
                return 0;
            }
            PrintLineRed__("\n Type \"Q\" to cancel this operation. ");
            PrintBlue_(" Enter the classroom's name or ID number: ");
            string oldClassName = Console.ReadLine().ToLower();
            int index = classrooms.FindIndex(x => x.className.ToLower() == oldClassName);

            if (oldClassName == "q")
            {
                return 0; // mainMenuSelection to cancel operation
            }

            bool indexTest = oldClassName.All(char.IsDigit);
            if (indexTest == false)
            {
                int classNameIndex = classrooms.FindIndex(x => x.className.ToLower() == oldClassName);
                if (classNameIndex == -1)
                {
                    PrintLineRed__(" You have entered an invalid name or ID. \n Press any key and try again.");
                    Console.ReadKey();
                    return 1; // classMainMenuSelection
                }
                else
                {
                    oldClassName = classrooms[classNameIndex].className;
                    Console.Clear();
                    ClassMainMenuHeader();
                    ViewAllClassroomsInfo(oldClassName);
                    PrintBlue_("\n Enter a new name for this classroom: ");
                    string newClassName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Console.ReadLine().ToLower());
                    oldClassName = classrooms[classNameIndex].className;
                    classrooms[classNameIndex].className = newClassName;
                    Console.Clear();
                    ClassMainMenuHeader();
                    ViewAllClassroomsInfo(newClassName);
                    Console.Write($"\n Classroom ");
                    PrintRed__(oldClassName);
                    Console.Write(" has been successfully renamed ");
                    PrintLineRed__($"{newClassName}.");
                    Console.WriteLine(" Press any key to continue.");
                    Console.ReadKey();
                    return 0; // mainMenuSelection to complete operation
                }
            }
            else
            {
                int classIDIndex = classrooms.FindIndex(x => x.classID == int.Parse(oldClassName));
                if (classIDIndex == -1)
                {
                    PrintLineRed__(" You have entered an invalid name or ID. \n Press any key and try again.");
                    Console.ReadKey();
                    return 1; // classMainMenuSelection
                }
                else
                {
                    oldClassName = classrooms[classIDIndex].className;
                    Console.Clear();
                    ClassMainMenuHeader();
                    ViewAllClassroomsInfo(oldClassName);
                    PrintBlue_("\n Enter the new name of the classroom: ");
                    string newClassName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Console.ReadLine().ToLower());
                    oldClassName = classrooms[classIDIndex].className;
                    classrooms[classIDIndex].className = newClassName;
                    Console.Clear();
                    ClassMainMenuHeader();
                    ViewAllClassroomsInfo(newClassName);
                    Console.Write($"\n Classroom ");
                    PrintRed__(oldClassName);
                    Console.Write(" has been successfully renamed ");
                    PrintLineRed__($"{newClassName}.");
                    Console.WriteLine(" Press any key to continue.");
                    Console.ReadKey();
                    return 0; // mainMenuSelection to complete operation
                }
            }
        }
        public static int RemoveClass()
        {
            string deleteClassroom;

            Console.Clear();
            ClassMainMenuHeader();
            ViewAllClassroomsInfo();
            if (classrooms.Count <= 0)
            {
                PrintLineRed__("\n There are no classrooms in the program. \n Press any key to continue.");
                Console.ReadKey();
                return 0;
            }
            PrintLineRed__("\n Type \"Q\" to cancel this operation.");
            PrintBlue_(" Enter the classroom name or ID number: ");
            string className = Console.ReadLine().Trim().ToLower();

            if (className == "q")
            {
                return 0; // mainMenuSelection to cancel operation
            }

            bool indexTest = className.All(char.IsDigit);
            if (indexTest == false)
            {
                int classNameIndex = classrooms.FindIndex(x => x.className.ToLower() == className);
                if (classNameIndex == -1)
                {
                    PrintLineRed__(" You have entered an invalid name or ID. \n Press any key and try again.");
                    Console.ReadKey();
                    return 4; // classMainMenuSelection
                }
                else
                {
                    className = classrooms[classNameIndex].className;
                    Console.Clear();
                    ClassMainMenuHeader();
                    ViewAllClassroomsInfo(className);
                    PrintRed__("\n Are you sure you want to delete ");
                    Console.Write(className);
                    PrintRed__("?");
                    do
                    {
                        PrintRed__("\n Type \"Yes\" to proceed or \"No\" to Cancel. ");
                        ColorChangeToBlue();
                        deleteClassroom = Console.ReadLine().ToLower();
                        if (deleteClassroom == "yes")
                        {
                            classrooms.RemoveAt(classNameIndex);
                            PrintRed__($"\n {className}");
                            Console.WriteLine(" has been successfully deleted!");
                            Console.Write(" Press any key to continue.");
                            Console.ReadKey();
                            return 0; // mainMenuSelection to complete operation;
                        }
                        else if (deleteClassroom == "no")
                        {
                            return 0; // mainMenuSelection to cancel operation
                        }
                    } while (deleteClassroom != "no" || deleteClassroom != "yes");
                    return 0; // mainMenuSelection to complete operation
                }
            }
            else
            {                
                int classIDIndex = classrooms.FindIndex(x => x.classID == int.Parse(className));
                if (classIDIndex == -1)
                {
                    PrintLineRed__(" You have entered an invalid name or ID. \n Press any key and try again.");
                    Console.ReadKey();
                    return 4; // classMainMenuSelection
                }
                else
                {
                    className = classrooms[classIDIndex].className;
                    Console.Clear();
                    ClassMainMenuHeader();
                    ViewAllClassroomsInfo(className);
                    PrintRed__("\n Are you sure you want to delete ");
                    Console.Write(className);
                    PrintRed__("?");
                    do
                    {
                        PrintRed__("\n Type \"Yes\" to proceed or \"No\" to Cancel. ");
                        ColorChangeToBlue();
                        deleteClassroom = Console.ReadLine().ToLower();
                        if (deleteClassroom == "yes")
                        {
                            classrooms.RemoveAt(classIDIndex);
                            PrintRed__($"\n {className}");
                            Console.WriteLine(" has been successfully deleted!");
                            Console.Write(" Press any key to continue.");
                            Console.ReadKey();
                            return 0; // mainMenuSelection to complete operation;
                        }
                        else if (deleteClassroom == "no")
                        {
                            return 0; // mainMenuSelection to cancel operation
                        }
                    } while (deleteClassroom != "no" || deleteClassroom != "yes");
                    return 0; // mainMenuSelection to complete operation
                }
            }
        }
    }
}
