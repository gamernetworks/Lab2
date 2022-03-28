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
            classrooms.Add(new Classroom(1, "PE"));
            classrooms[0].students.Add(new Student("Oscar", "Montalvo"));
            classrooms[0].students.Add(new Student("Manuel", "Montalvo"));

            classrooms.Add(new Classroom(2, "English"));
            classrooms[1].students.Add(new Student("Manuel", "Montalvo"));
            classrooms[1].students.Add(new Student("Byron", "Montalvo"));

            classrooms.Add(new Classroom(3, "Math"));
            classrooms[2].students.Add(new Student("Emmanuel", "Montalvo"));
            classrooms[2].students.Add(new Student("Byron", "Montalvo"));
            classrooms[2].students.Add(new Student("Jarianna", "Montalvo"));

            classrooms.Add(new Classroom(4, "Biology"));
            classrooms[3].students.Add(new Student("Emmanuel", "Montalvo"));
            classrooms[3].students[0].assignments.Add(new Assignment("Animals", 79));
            classrooms[3].students[0].assignments.Add(new Assignment("A&P", 0));
            classrooms[3].students.Add(new Student("Byron", "Montalvo"));
            classrooms[3].students[1].assignments.Add(new Assignment("Plants", 95));
            classrooms[3].students.Add(new Student("Jarianna", "Montalvo"));
            classrooms[3].students[2].assignments.Add(new Assignment("Animals", 100));
            classrooms[3].students[2].assignments.Add(new Assignment("Plants", 95));
        }
    }
}
