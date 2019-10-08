using System;
using System.Collections.Generic;
using System.Linq; //упроститель жизни =)
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
                   
                    //Console.WriteLine(str);
                }

            }

            var students = CreateStudent(first_names.ToArray(), last_names.ToArray(), patronimycs.ToArray(), 200); //число создаваемых студентов


            //  foreach (var stud in students)
            //    stud.Visualise();
            double min = double.PositiveInfinity;
            double max = double.NegativeInfinity;
            for (int i = 0; i< students.Length; i++)
            {
                var avg = students[i].AverageRating;
                if (avg < min) min = avg;
                if (avg > max) max = avg;
            }

            var delta = max - min;
            var max_treshold = max - delta * 0.1;
            var min_treshold = min + delta * 0.1;

            var best = new List<Student>();
            var last = new List<Student>();
            foreach (var stud in students)
            {
                var average_rating = stud.AverageRating;
                if (average_rating > max_treshold)
                    best.Add(stud);
                else if (average_rating < min_treshold)
                    last.Add(stud);
            }


            Console.ReadLine();

        }

        static Student[] CreateStudent(string[] Names, string[] LastNames, string[] Patronimics, int Count)
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
                student.Ratings = GetRandomRatings(end,10, 2, 3, 4, 5);
                result[i] = student;

            }


            return result;
        }

        //params int[]  - сумочка с параметрами 

        static int GetRandom(Random rnd, params int[] Variants)
        {
            int index = rnd.Next(0, Variants.Length);
            return Variants[index];
        }

        static int[] GetRandomRatings(Random rnd, int count, params int[] Variants)
        {

        var result = new int[count];
        for (int i=0; i<count; i++)
            result[i]=GetRandom(rnd, Variants);
        return result;
        }

    }

    class Student
    {
        public string FirstNames;
        public string LastNames;
        public string Patronimyc;
        public int[] Ratings;
        public void Visualise()
        {
            Console.Write("{0}, {1}, {2}", LastNames, FirstNames, Patronimyc);
            if (Ratings!=null)
            {
                for (int i = 0; i < Ratings.Length; i++)
                    Console.Write("{0},", Ratings[i]);
            }

            Console.WriteLine();
        }
        public double AverageRating //средний балл
        {
            get //для извлечения свойства
            {
                if (Ratings == null || Ratings.Length == 0) //обезопасываем себя
                    return double.NaN;
                /*
                double sum = 0;
                for (int i = 0; i < Ratings.Length; i++)
                    sum += Ratings[i];
                return sum/Ratings.Length;
                */

                //или

                return Ratings.Average(); //уже существующая функция с помощью директивы using System.Linq;

            }
        }

    }


}
