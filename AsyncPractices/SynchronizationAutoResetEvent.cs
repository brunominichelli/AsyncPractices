using System;
using System.Threading;

namespace AsyncPractices
{
    partial class Program
    {
        public class SynchronizationAutoResetEvent 
        {
            public static int result = 0; //shared static problem

            const int m_repetitions = 100;

            private static object locker = new object();
            //Event to notify if ready to receive a value
            public static EventWaitHandle readyForResult = new AutoResetEvent(false);
            //Event to notify when the value has been modified
            public static EventWaitHandle setResult = new AutoResetEvent(false);

            static void DoRepetitions(int repetitions)
            {
                while (true)
                {

                    int i = result;

                    Thread.Sleep(1);

                    //Waiting the Main thread is ready to receive
                    readyForResult.WaitOne();

                    lock (locker) //Lock critical section, which modifies the shared variable
                    {
                        result = i + 1; //Accumulate
                    }
                    //Sends the main thread the written value
                    setResult.Set();
                }
            }

            public void Start() 
            {
                Thread t = new Thread(() => DoRepetitions(m_repetitions));
                t.Start();


                for (int i = 0; i < m_repetitions; i++)
                {
                    //This call tells the Dorepetitions thread that is ready to receive a value
                    readyForResult.Set();
                    //this call is waiting the dorepetitions thread to write the variable
                    setResult.WaitOne();
                    lock (locker)
                    {
                        Console.WriteLine(result);
                    }
                }
                //simulates other code calls
                Thread.Sleep(10);
                Console.Read();
            }
        }
    }
}
