using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace The_task_parallel_library
{
    //The task parallel library
    //The task parallel library can start tasks simultaniously with 3 different methods

    
    class Program
    {
        static void Task1()
        {
            Console.WriteLine("Task 1 starting");
            Thread.Sleep(2000);
            Console.WriteLine("Task 1 ending");
        }

        static void Task2()
        {
            Console.WriteLine("Task 2 starting");
            Thread.Sleep(1000);
            Console.WriteLine("Task 2 ending");
        }

        static void WorkOnItem(object item)
        {
            Console.WriteLine("Started working on item: " + item);
            Thread.Sleep(2000);
            Console.WriteLine("Finished working on item: " + item);
        }

        
        static void Main(string[] args)
        {
            // First method Parallel.Invoke()
            //2 ways of syntax to use the invoke method, regular or with lambda

            //Parallel.Invoke(Task1, Task2);
            Parallel.Invoke(() => Task1(), () => Task2());
            Console.WriteLine("Parallel.Invoke() Finished");
          
            // Second method Parallel.ForEach() -> Same result as Parallel.For()
            var items = Enumerable.Range(0, 20);
            Parallel.ForEach(items, item =>
            {
                WorkOnItem(item);
            });
            Console.WriteLine("Parallel.ForEach() Finished");
            

            var itemsArray = Enumerable.Range(0, 20).ToArray();
            //third method Parellel.For() -> Same result as Parallel.Foreach()
            Parallel.For(0, itemsArray.Length, i =>
            {
                WorkOnItem(itemsArray[i]);
            });
            Console.WriteLine("Parallel.For() Finished");


            //Extra feature Managing a parallel For/Foreach loop
            ParallelLoopResult result = Parallel.For(0, itemsArray.Count(), (int i, ParallelLoopState loopstate) =>
            {
                if(i == 10)
                {
                    //.Stop will prevent any new iterations with value greater then the current index (10 in this case) to be performed
                    // This gives no assurance that all iterations with a value lower then 10 will be performed
                    //loopstate.Stop();

                    //.Break will stop any iteriations with an index higher then the current iteration (10 in this case)
                    //and will also make sure that any iterations with an index lower then the current index will be performed
                    loopstate.Break();
                }

                WorkOnItem(itemsArray[i]);
            });

            Console.WriteLine("Completed: " + result.IsCompleted);
            Console.WriteLine("Items: " + result.LowestBreakIteration);

            Console.WriteLine("Finished processing. Press a key to end.");
            Console.ReadKey();




        }
    }


}
