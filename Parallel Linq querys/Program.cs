using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parallel_Linq_querys
{
    class Program
    {

        //We can also use the Parallel class on Linq query's to see if using paralelization would speed up our query
        //If it is decided (By the .asParallel() method) that paralelization would increase performance the query is split up in different pieces.
        //It then runs on different threads for each piece so we get simultanious querying on the different pieces of the query 

        class Person
        {
            public string Name { get; set; }
            public string City { get; set; }
        }

        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();

            Person[] people = new Person[]
            {
                new Person { Name = "Alan", City = "Brussels"},
                new Person { Name = "Maarten", City = "Zelzate" },
                new Person { Name = "Pieter", City = "Gent" },
                new Person { Name = "Pieter", City = "Gent" },
                new Person { Name = "Pieter", City = "Gent" },

                new Person { Name = "Pieter", City = "Gent" },
                new Person { Name = "Pieter", City = "Gent" },
                new Person { Name = "Pieter", City = "Gent" },
                new Person { Name = "Pieter", City = "Gent" },
                new Person { Name = "Pieter", City = "Gent" },
                new Person { Name = "Pieter", City = "Gent" },
                new Person { Name = "Pieter", City = "Gent" }
            };

            sw.Start();
            var result = from person in people.AsParallel() where person.City == "Zelzate" select person;
            sw.Stop();

            foreach(var person in result)
            {
                Console.WriteLine(person.Name);
            }

            Console.WriteLine(sw.Elapsed);

            Console.WriteLine("Finished processing. Press a key to end.");
            Console.ReadKey();
        }
    }
}
