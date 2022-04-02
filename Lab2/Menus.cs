using System;
using static Lab2.Assignment;
using static Lab2.Classroom;
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
                    MainMenuHeader();
                    Console.WriteLine("\n\n" +
                        "\n                 1. Go to Classroom Menu" +
                        "\n                 2. Quit Application");
                    if (mainMenuSelection < 1 || mainMenuSelection > 2)
                        mainMenuSelection = ReadKeyInput();
                    if (mainMenuSelection == 1)
                        mainMenuLoop = ClassMainMenu();
                    else if (mainMenuSelection == 2)
                    {
                        mainMenuLoop = false; break;
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
                {   // View every classroom with its ID, name, std count and GPA.
                    SchoolInfo(-1); // Use -1 in method to disable highlighting
                    Console.WriteLine("\n" +
                        "\n           1. Manage Classroom" +
                        "\n           2. Add New Classroom" +
                        "\n           3. Edit Classroom Name" +
                        "\n           4. Remove Classroom\n" +
                        "\n           5. Return to Previous Menu" +
                        "\n           6. Quit Application");
                    if (classMainMenuSelection < 1 || classMainMenuSelection > 6)
                        classMainMenuSelection = ReadKeyInput();
                    if (classMainMenuSelection == 1) // Goto Classroom SubMenu
                    {
                        classMainMenuSelection = ManageClass(); break;
                    }
                    else if (classMainMenuSelection == 2) // Add New Classroom
                    {                        
                        classMainMenuSelection = AddClass(); break;
                    }
                    else if (classMainMenuSelection == 3) // Edit Classroom Name
                    {
                        classMainMenuSelection = EditClassName(); break;
                    }
                    else if (classMainMenuSelection == 4) // Remove Classroom
                    {
                        classMainMenuSelection = RemoveClass(); break;
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
            int stdIndex;
            while (true)
            {
                try
                {
                    StdInfo(classIndex, -1, true); // -1 = Deactivate Highlights. true = show top/bottom stds
                    Console.Write("\n" +
                        "\n           1. Manage Student" +
                        "\n           2. Add New Student" +
                        "\n           3. Remove Student" +
                        "\n           4. Compare Students");
                    PrintRed__(" (Disabled: Comparison information on displayed.)");
                    Console.WriteLine("\n" + 
                        "\n           5. Return to Previous Menu" +
                        "\n           6. Quit Application");
                    if (classSubMenuSelection < 1 || classSubMenuSelection > 6)
                        classSubMenuSelection = ReadKeyInput();
                    if (classSubMenuSelection == 1)
                    {
                        //classSubMenuSelection = ManageStd(classIndex);
                        stdIndex = ManageStd(classIndex);
                        if (stdIndex == -1)
                            classSubMenuSelection = 0;
                        else
                            classSubMenuSelection = StdMenu(classIndex, stdIndex);
                    } else if (classSubMenuSelection == 2)
                        classSubMenuSelection = AddStd(classIndex);
                    else if (classSubMenuSelection == 3)
                        classSubMenuSelection = RemoveStd(classIndex);
                    else if (classSubMenuSelection == 4)
                        classSubMenuSelection = 0;
                    else if (classSubMenuSelection == 5)
                    {
                        classSubMenuSelection = 0;
                        classMainMenuSelection = 0;
                        break;
                    } else if (classSubMenuSelection == 6)
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
        public static int StdMenu(int classIndex, int stdIndex)
        {
            while (true)
            {
                try
                {
                    ViewStdAssignments(classIndex, stdIndex, -1); // Use -1 to deactivate highlights
                    ViewStdBestWorseGrades(classIndex, stdIndex);
                    Console.WriteLine("\n" +
                        "\n           1. Edit Student Name" +
                        "\n           2. Add New Assignment" +
                        "\n           3. Remove Assignment" +
                        "\n           4. Add/Edit Grade\n" +
                        "\n           5. Return to Previous Menu" +
                        "\n           6. Quit Application");
                    if (stdMenuSelection < 1 || stdMenuSelection > 6)
                        stdMenuSelection = ReadKeyInput();
                    if (stdMenuSelection == 1)
                        stdMenuSelection = EditStdName(classIndex, stdIndex);
                    else if (stdMenuSelection == 2)
                        stdMenuSelection = AddAssignment(classIndex, stdIndex);
                    else if (stdMenuSelection == 3)
                        stdMenuSelection = RemoveAssignment(classIndex, stdIndex);
                    else if (stdMenuSelection == 4)
                        stdMenuSelection = AddEditAssignmentGrade(classIndex, stdIndex);
                    else if (stdMenuSelection == 5)
                    {
                        stdMenuSelection = 0;
                        classSubMenuSelection = 0;
                        return 0;
                    }
                    else if (stdMenuSelection == 6)
                    {
                        stdMenuSelection = 0;
                        classSubMenuSelection = 6;
                        classMainMenuSelection = 6;
                        mainMenuSelection = 2;
                        mainMenuLoop = false;
                        return 6;
                    }
                }
                catch
                {
                    continue;
                }
            }
        }
    }
}
