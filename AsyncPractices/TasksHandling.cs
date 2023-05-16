using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncPractices
{
    public class TasksHandling 
    {
        public void StartGenericTask()
        {
            // Factory = Access to FactoryMethods 
            //StartNew = starts and RUNS a task, takes a Func<T> as a parameter. T is the RETURN type (because its a func, which returns an actual value != Actions) 
            var task = Task<string>.Factory.StartNew(() =>
            {
                Thread.Sleep(2000);
                return "Bruno";
            });


            // This requires the scope method to be async and expects to return a Task, or Task<T> if a value is expected
            //await task;
            //Console.Write($"My name is {task.Result}");

            //This works without the need of the scope method to be async 
            task.Wait();
            Console.Write($"My name is {task.Result}");


            //Console.Write("My name is ");
            //Console.Write(task.Result);
        }

    }
}
