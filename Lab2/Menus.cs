using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lab2.Assignment;
using static Lab2.Classroom;
using static Lab2.GlobalVariables;
using static Lab2.Headers;
using static Lab2.Student;
using static Lab2.Tools;


namespace Lab2
{
    internal class Menus
    {
        public static int mainMenuSelection = 0;
        public static int classMainMenuSelection = 0;
        public static int classSubMenuSelection = 0;
        public static int stdMenuSelection = 0;
        public static bool mainMenuLoop = true;
        public static bool MainMenu(bool mainMenuLoop)
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    MainMenuHeader();
                    Console.WriteLine("\n\n" +
                        "\n                 1. Go to Classroom Menu" +
                        "\n                 2. Quit Application");
                    if (mainMenuSelection < 1 || mainMenuSelection > 2)
                    {
                        mainMenuSelection = ReadKeyInput();
                    }
                    if (mainMenuSelection == 1)
                    {
                        mainMenuLoop = ClassMainMenu();
                    }
                    else if (mainMenuSelection == 2)
                    {
                        mainMenuLoop = false;
                        break;
                    }
                }
                catch
                {
                    continue;
                }
                break;
            }
            return mainMenuLoop;
        }
        public static bool ClassMainMenu()
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    ClassMainMenuHeader();
                    ViewAllClassroomsInfo();
                    Console.WriteLine("\n" +
                        "\n           1. Manage Classroom" +
                        "\n           2. Add New Classroom" +
                        "\n           3. Edit Classroom" +
                        "\n           4. Remove Classroom" +
                        "\n           5. Return to Previous Menu" +
                        "\n           6. Quit Application");
                    if (classMainMenuSelection < 1 || classMainMenuSelection > 6)
                    {
                        classMainMenuSelection = ReadKeyInput();
                    }
                    if (classMainMenuSelection == 1)
                    {
                        ColorChangerCaution();
                        Console.Write("\n Enter the name of the classroom: ");
                        Console.ResetColor();
                        string className = Console.ReadLine().ToLower();
                        int index = classrooms.FindIndex(x => x.className.ToLower() == className);
                        if (index < 0)
                        {
                            classMainMenuSelection = 1;
                            break;
                        }
                        else
                            ClassSubMenu(index, className);
                    }
                    else if (classMainMenuSelection == 2)
                    {
                        AddClass();
                        classMainMenuSelection = 0;
                    }
                    else if (classMainMenuSelection == 3)
                    {
                        EditClass();
                        classMainMenuSelection = 0;
                    }
                    else if (classMainMenuSelection == 4)
                    {
                        RemoveClass();
                        classMainMenuSelection = 0;
                    }
                    else if (classMainMenuSelection == 5)
                    {
                        classMainMenuSelection = 0;
                        mainMenuSelection = 0;
                        mainMenuLoop = true;
                        break;
                    }
                    else if (classMainMenuSelection == 6)
                    {
                        classMainMenuSelection = 0;
                        mainMenuSelection = 2;
                        return false;
                    }
                }
                catch
                {
                    continue;
                }
            }
            return true;
        }
        public static bool ClassSubMenu(int index, string className)
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    ClassSubMenuHeader(className.ToUpper());
                    IndividualClassStats(index);
                    Console.WriteLine("\n" +
                        "\n           1. Detailed Student View" +
                        "\n           2. Add New Student" +
                        "\n           3. Remove Student" +
                        "\n           4. Compare Students" +
                        "\n           5. Return to Previous Menu" +
                        "\n           6. Quit Application");
                    if (classSubMenuSelection < 1 || classSubMenuSelection > 6)
                    {
                        classSubMenuSelection = ReadKeyInput();
                    }
                    if (classSubMenuSelection == 1)
                    {
                        StdMenu();
                    }
                    else if (classSubMenuSelection == 2)
                    {
                        AddStd();
                        classSubMenuSelection = 0;
                    }
                    else if (classSubMenuSelection == 3)
                    {
                        RemoveStd();
                        classSubMenuSelection = 0;
                    }
                    else if (classSubMenuSelection == 4)
                    {
                        CompareClassStd();
                        classSubMenuSelection = 0;
                        //mainMenuLoop = true;
                    }
                    else if (classSubMenuSelection == 5)
                    {
                        classSubMenuSelection = 0;
                        classMainMenuSelection = 0;
                        //mainMenuLoop = true;
                        break;
                    }
                    else if (classSubMenuSelection == 6)
                    {
                        classSubMenuSelection = 0;
                        classMainMenuSelection = 6;
                        mainMenuSelection = 2;
                        return mainMenuLoop = false;
                    }
                }
                catch
                {
                    continue;
                }
            }
            return true;
        }
        public static bool StdMenu()
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    StdMenuHeader("test student".ToUpper());
                    ShowStdBestGrade();
                    ShowStdWorseGrade();
                    ViewAssignment();
                    Console.WriteLine("\n" +
                        "\n           1. Edit Student Information" +
                        "\n           2. Assign New Assignment" +
                        "\n           3. Remove Assignment" +
                        "\n           4. Add/Edit Grade" +
                        "\n           5. Return to Previous Menu" +
                        "\n           6. Quit Application");
                    if (stdMenuSelection < 1 || stdMenuSelection > 6)
                    {
                        stdMenuSelection = ReadKeyInput();
                    }
                    if (stdMenuSelection == 1)
                    {
                        EditStd();
                        stdMenuSelection = 0;
                    }
                    else if (stdMenuSelection == 2)
                    {
                        AddAssignment();
                        stdMenuSelection = 0;
                    }
                    else if (stdMenuSelection == 3)
                    {
                        RemoveAssignment();
                        stdMenuSelection = 0;
                    }
                    else if (stdMenuSelection == 4)
                    {
                        AddEditGrade();
                        stdMenuSelection = 0;
                    }
                    else if (stdMenuSelection == 5)
                    {
                        stdMenuSelection = 0;
                        classSubMenuSelection = 0;
                        mainMenuSelection = 0;
                        //mainMenuLoop = true;
                        break;
                    }
                    else if (stdMenuSelection == 6)
                    {
                        stdMenuSelection = 0;
                        classSubMenuSelection = 6;
                        classMainMenuSelection = 6;
                        mainMenuSelection = 2;
                        return mainMenuLoop = false;
                    }
                }
                catch
                {
                    continue;
                }
            }
            return true;
        }
    }
}
