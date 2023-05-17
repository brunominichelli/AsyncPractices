using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncPractices
{
    public class TasksHandling
    {
        static List<Task<string>> m_tasks = new List<Task<string>>();
        static int m_delay = 1000;
        public void StartGenericBasicTask()
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

        public void ParentAndChildTasks()
        {
            var sw = new Stopwatch();
            sw.Start();
            string sentence = $"the quick brown bruno jumped overe the lazy bruh";
            Task.Factory.StartNew(() => ProcessSentence(sentence)).Wait();
            sw.Stop();

            Console.WriteLine($"Elapsed time: {sw.ElapsedMilliseconds}");


            Console.WriteLine("Result:");
            foreach (var task in m_tasks)
            {
                Console.Write($"{task.Result}");
            }

        }
        public static void ProcessSentence(string sentence) 
        {
            foreach (var word in sentence.Split())
            {
                m_tasks.Add(Task<string>.Factory.StartNew(() => $"{ReverseString(word, m_delay)} ", TaskCreationOptions.AttachedToParent | TaskCreationOptions.LongRunning));
            }
        }
        static string ReverseString(string str, int ms) 
        {
            if (string.IsNullOrEmpty(str))
            {
                Console.WriteLine("Can't work on null or empty strings, returning");
                return string.Empty;
            }
            Console.WriteLine($"Reversing string {str}");
            Thread.Sleep(ms);
            StringBuilder sb = new StringBuilder();
            for (int i = str.Length -1 ; i >= 0; --i)
            {
                sb.Append(str[i]);
            }

            var result = sb.ToString();

            return result;
        }
    }
}

