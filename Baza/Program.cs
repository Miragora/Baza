using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;



namespace Baza
{
    class Program
    {
        static void Main()
        {
            var first_names = new List<string>();
            var last_names = new List<string>();
            var patronimycs = new List<string>();


            const string scr_Stud = "Studenty.txt";

            using (var file = new StreamReader(scr_Stud))
            {
                while (!file.EndOfStream)
                {
                    var str = file.ReadLine();
                    var values = str.Split(' ');
                    first_names.Add(values[1]);
                    last_names.Add(values[0]);
                    patronimycs.Add(values[2]);
                    Console.WriteLine(str);
                }

            }

            var students = CreateStudent(first_names.ToArray(), last_names.ToArray(), patronimycs.ToArray(), 200); //число создаваемых студентов

            Console.ReadLine();

        }

        static Student[] CreateStudent(string []Names, string []LastNames, string []Patronimics, int Count)
        {
            var result = new Student[Count];
            var end = new Random(); //генератор случ чисел
            for (var i = 0; i < Count; i++)
            {
                var first_name = Names[end.Next(0, Names.Length)];//рандомное число от 0 до длины массива
                var last_name = LastNames[end.Next(0, LastNames.Length)];
                var patronimics = Patronimics[end.Next(0, Patronimics.Length)];

                var student = new Student();
                student.FirstNames = first_name;
                student.LastNames = last_name;
                student.Patronimyc = patronimics;
                result[i] = student;

            }

           
            return result;
        }


    }

    class Student
    {
        public string FirstNames;
        public string LastNames;
        public string Patronimyc;

    }


}
