using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lab2.GlobalVariables;

namespace Lab2
{
    internal class TestingClass
    {
        public static void Testing()
        {
            //// Create the List ////
            /*
            //List<Classroom> classrooms = new List<Classroom>(); // This list is created in the GlobalVariables
            */

            //// Entering the Data ////
            /*
            // Syntax 1: (DOES NOT WORK. It tries to add the student before creating a new Classroom. ASK INSTRUCTOR
            //List<Classroom> classrooms = new List<Classroom>()
            //{
            //    new Classroom("Math", new Student("Byron"))
            //};

            // Syntax 2: Works            
            classrooms.Add(new Classroom());
            classrooms[0].classID = 1;
            classrooms[0].className = "PE";
            classrooms[0].students.Add(new Student("Oscar", "Montalvo"));
            classrooms[0].students.Add(new Student("Manuel", "Montalvo"));

            // Syntax 3: Works
            classrooms.Add(new Classroom(2, "English"));
            classrooms[1].students.Add(new Student("Manuel", "Montalvo"));
            classrooms[1].students.Add(new Student("Byron", "Montalvo"));

            classrooms.Add(new Classroom(3, "Math"));
            classrooms[2].students.Add(new Student("Emmanuel", "Montalvo"));
            classrooms[2].students.Add(new Student("Byron", "Montalvo"));
            classrooms[2].students.Add(new Student("Jarianna", "Montalvo"));

            classrooms.Add(new Classroom(4, "Biology"));
            classrooms[3].students.Add(new Student("Emmanuel", "Montalvo"));
            classrooms[3].students[0].assignments.Add(new Assignment("Animals"));
            classrooms[3].students.Add(new Student("Byron", "Montalvo"));
            classrooms[3].students[1].assignments.Add(new Assignment("Plants"));
            classrooms[3].students.Add(new Student("Jarianna", "Montalvo"));
            classrooms[3].students[2].assignments.Add(new Assignment("Animals"));            

            // Syntax 4: (DOES NOT WORK. It tries to add the student before creating a new Classroom
            // classrooms.Add(new Classroom("Biology", new Student("Oscar")));
            */

            //// Printing the data ////

            // Syntax 1:
            Console.WriteLine(classrooms[0].classID);
            Console.WriteLine(classrooms[0].className);
            Console.WriteLine(classrooms[0].students[0].studName);
            Console.WriteLine(classrooms[0].students[1].studName);

            Console.WriteLine(classrooms[1].classID);
            Console.WriteLine(classrooms[1].className);
            Console.WriteLine(classrooms[1].students[0].studName);
            Console.WriteLine(classrooms[1].students[1].studName);

            Console.WriteLine(classrooms[2].classID);
            Console.WriteLine(classrooms[2].className);
            Console.WriteLine(classrooms[2].students[0].studName);
            Console.WriteLine(classrooms[2].students[1].studName);
            Console.WriteLine(classrooms[2].students[2].studName);

            Console.WriteLine(classrooms[3].classID);
            Console.WriteLine(classrooms[3].className);
            Console.WriteLine(classrooms[3].students[0].studName);
            Console.WriteLine(classrooms[3].students[0].assignments[0].assignmentName);
            Console.WriteLine(classrooms[3].students[1].studName);
            Console.WriteLine(classrooms[3].students[1].assignments[0].assignmentName);
            Console.WriteLine(classrooms[3].students[2].studName);
            Console.WriteLine(classrooms[3].students[2].assignments[0].assignmentName);

            Console.WriteLine();

            // Syntax 2:
            Console.WriteLine(classrooms[0].className + " " + classrooms[0].students[0].studName);
            Console.WriteLine(classrooms[0].className + " " + classrooms[0].students[1].studName);
            Console.WriteLine(classrooms[1].className + " " + classrooms[1].students[0].studName);
            Console.WriteLine(classrooms[1].className + " " + classrooms[1].students[1].studName);
            Console.WriteLine(classrooms[2].className + " " + classrooms[2].students[0].studName);
            Console.WriteLine(classrooms[2].className + " " + classrooms[2].students[1].studName);
            Console.WriteLine(classrooms[2].className + " " + classrooms[2].students[2].studName);
            Console.WriteLine();

            // Syntax 3:
            Console.WriteLine($"{classrooms[0].className} {classrooms[0].students[0].studName}");
            Console.WriteLine($"{classrooms[0].className} {classrooms[0].students[1].studName}");
            Console.WriteLine($"{classrooms[1].className} {classrooms[1].students[0].studName}");
            Console.WriteLine($"{classrooms[1].className} {classrooms[1].students[1].studName}");
            Console.WriteLine($"{classrooms[2].className} {classrooms[2].students[0].studName}");
            Console.WriteLine($"{classrooms[2].className} {classrooms[2].students[1].studName}");
            Console.WriteLine($"{classrooms[2].className} {classrooms[2].students[2].studName}");
            Console.WriteLine();

            // Syntax 4: I can print the classrom name once followed by the students in horizontal order. Hardcoded Index
            // DO NOT USE as it does not work dynamically
            foreach (var classrm in classrooms)
            {
                Console.WriteLine($"{classrm.className} {classrm.students[0].studName} {classrm.students[1].studName}");
            }
            Console.WriteLine();

            // Syntax 5: I can print the classrom name once followed by the students in it vertical order. Hardcoded Index
            // DO NOT USE as it does not work dynamically
            foreach (var classrm in classrooms)
            {
                Console.WriteLine($"{classrm.className}");
                for (int i = 0; i < 2; i++) // the i < 2 i hardcoded...I need to find a solution to find the length of the List
                {
                    Console.WriteLine($"{classrm.students[i].studName}");
                }
            }
            Console.WriteLine();




            // Syntax 6: I can print the classrom name once followed by the students in it vertical order.
            for (int i = 0; i < classrooms.Count; i++) // classrooms.Count used to count the amount of classrooms in the list
            {
                int stundentCountInClass = classrooms[i].students.Count; // Counts the number of students per classroom
                Console.WriteLine("Class {0} with ID#: {1} has {2} students. ", classrooms[i].className, classrooms[i].classID,
                    stundentCountInClass);
                for (int j = 0; j < stundentCountInClass; j++)
                {
                    int assignmentsPerStudent = classrooms[i].students[j].assignments.Count; // Counts the number of assignments per student
                    Console.Write($"Student {classrooms[i].students[j].studName} has assignment: ");
                    for (int k = 0; k < assignmentsPerStudent; k++)
                    {
                        Console.WriteLine($"{classrooms[i].students[j].assignments[k].assignmentName}");
                    }
                }
            }
            Console.WriteLine();







            // Syntax 7: I can print the classrom name once followed by the students in it horizontal order.
            for (int i = 0; i < classrooms.Count; i++) // classrooms.Count used to count the amount of classrooms in the list
            {
                int stundentCountInClass = classrooms[i].students.Count(); // Counts the number of students per classroom
                Console.Write("Class {0} with ID#: {1} has: ", classrooms[i].className, classrooms[i].classID);
                for (int j = 0; j < stundentCountInClass; j++)
                {
                    Console.Write($"{classrooms[i].students[j].studName} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();



            Console.WriteLine();


        }
    }
}
