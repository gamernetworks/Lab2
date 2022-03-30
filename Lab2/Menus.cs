using System;
using System.Collections.Generic;
using System.Globalization;
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
                        "\n           3. Edit Classroom Name" +
                        "\n           4. Remove Classroom\n" +
                        "\n           5. Return to Previous Menu" +
                        "\n           6. Quit Application");
                    if (classMainMenuSelection < 1 || classMainMenuSelection > 6)
                    {
                        classMainMenuSelection = ReadKeyInput();
                    }
                    if (classMainMenuSelection == 1) // Goto Classroom SubMenu
                    {
                        classMainMenuSelection = ManageClass();
                        break;
                    }
                    else if (classMainMenuSelection == 2) // Add New Classroom
                    {                        
                        classMainMenuSelection = AddClass();
                        break;
                    }
                    else if (classMainMenuSelection == 3) // Edit Classroom Name
                    {
                        classMainMenuSelection = EditClassName();
                        break;
                    }
                    else if (classMainMenuSelection == 4) // Remove Classroom
                    {
                        classMainMenuSelection = RemoveClass();
                        break;
                    }
                    else if (classMainMenuSelection == 5) // Return to Previous Menu
                    {
                        classMainMenuSelection = 0;
                        mainMenuSelection = 0;
                        mainMenuLoop = true;
                        break;
                    }
                    else if (classMainMenuSelection == 6) // Quit Application
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
        public static bool ClassSubMenu(int classIndex)
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    var a = SecondIndividualClassStats(classIndex);                    
                    ClassSubMenuHeader(a.Item1, a.Item2, a.Item3, a.Item4);
                    FirstIndividualClassStats(classIndex);
                    Console.WriteLine("\n" +
                        "\n           1. View Student Detailed Information" +
                        "\n           2. Add New Student" +
                        "\n           3. Remove Student" +
                        "\n           4. Compare Students\n" +
                        "\n           5. Return to Previous Menu" +
                        "\n           6. Quit Application");
                    if (classSubMenuSelection < 1 || classSubMenuSelection > 6)
                    {
                        classSubMenuSelection = ReadKeyInput();
                    }
                    if (classSubMenuSelection == 1)
                    {
                        classSubMenuSelection = ManageStd(classIndex);
                    }
                    else if (classSubMenuSelection == 2)
                    {
                        classSubMenuSelection = AddStd(classIndex);                            
                    }
                    else if (classSubMenuSelection == 3)
                    {
                        classSubMenuSelection = RemoveStd(classIndex);
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
        public static bool StdMenu(int classIndex, int stdIndex)
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    StdSubMenuHeader(classIndex, stdIndex);
                    ShowStdBestGrade();
                    ShowStdWorseGrade();
                    ViewAssignment();
                    Console.WriteLine("\n" +
                        "\n           1. Edit Student Information" +
                        "\n           2. Assign New Assignment" +
                        "\n           3. Remove Assignment" +
                        "\n           4. Add/Edit Grade\n" +
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
