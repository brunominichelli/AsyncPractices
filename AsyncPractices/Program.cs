using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncPractices
{
    partial class Program
    {
        static void Main(string[] args)
        {
            //AutoResetEventTest();
            //TaskTesting();
            //ReverseSentenceTasking();
            Console.Read();

        }


        static void AutoResetEventTest() 
        {
            var synchro = new SynchronizationAutoResetEvent();
            synchro.Start();
        }
        static void BasicTaskTesting() 
        {
            var tasking = new TasksHandling();
            tasking.StartGenericBasicTask();
        }

        static void ReverseSentenceTasking() 
        {
            var tasking = new TasksHandling();

            tasking.ParentAndChildTasks();
        }
    }
}
