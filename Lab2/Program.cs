using static Lab2.GlobalVariables;
using static Lab2.Menus;

namespace Lab2
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            bool mainMenuLoop = true;
            SampleInitialData(); // Loads sample data
            while (mainMenuLoop == true)
            {
                mainMenuLoop = MainMenu(mainMenuLoop);
            }            
        }
    }
}
