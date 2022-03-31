using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    internal class GlobalVariables
    {
        public static List<Classroom> classrooms = new List<Classroom>();
        public static void SampleInitialData()
        {
            /*classrooms.Add(new Classroom(1, "PE"));
            classrooms[0].students.Add(new Student(1, "Oscar", "Montalvo"));
            classrooms[0].students.Add(new Student(2, "Manuel", "Montalvo"));

            classrooms.Add(new Classroom(2, "English"));
            classrooms[1].students.Add(new Student(1, "Manuel", "Montalvo"));
            classrooms[1].students[0].assignments.Add(new Assignment(1, "Vocabulary", 74));
            classrooms[1].students[0].assignments.Add(new Assignment(2, "Verbs", 99));
            classrooms[1].students.Add(new Student(2, "Byron", "Montalvo"));
            
            classrooms.Add(new Classroom(3, "Math"));
            classrooms[2].students.Add(new Student(1, "Emmanuel", "Montalvo"));
            classrooms[2].students[0].assignments.Add(new Assignment(1, "Sum", 50));
            classrooms[2].students.Add(new Student(2, "Byron", "Montalvo"));
            classrooms[2].students[1].assignments.Add(new Assignment(1, "Sum", 0));
            classrooms[2].students.Add(new Student(3, "Jarianna", "Montalvo"));
            classrooms[2].students[2].assignments.Add(new Assignment(1, "Algebra", 88));

            classrooms.Add(new Classroom(4, "Biology"));
            classrooms[3].students.Add(new Student(1, "Emmanuel", "Montalvo"));
            classrooms[3].students[0].assignments.Add(new Assignment(1, "Animals", 79));
            classrooms[3].students[0].assignments.Add(new Assignment(2, "A&P", 0));
            classrooms[3].students.Add(new Student(2, "Byron", "Montalvo"));
            classrooms[3].students[1].assignments.Add(new Assignment(1, "Plants", 100));
            classrooms[3].students.Add(new Student(3, "Jarianna", "Montalvo"));
            classrooms[3].students[2].assignments.Add(new Assignment(1, "Animals", 100));
            classrooms[3].students[2].assignments.Add(new Assignment(2, "Plants", 95));*/

            // Syntax 4: (DOES NOT WORK. It tries to add the student before creating a new Classroom
            //classrooms.Add(new Classroom("Biology", new Student("Oscar")));            
        }
    }
}
