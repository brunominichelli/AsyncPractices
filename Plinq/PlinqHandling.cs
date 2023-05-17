using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace Plinq
{
    internal class PlinqHandling
    {
        internal void ReversingWordPlinq() 
        {

            ///////////////////////SINGLE THREAD EXECUTION///////////////////////////
            //var sentence = "Bruce is the best coder in the Mondongo Neighbourhood";

            //var words = sentence.Split().Select(word =>
            //{
            //    word.ToLower();
            //    word = HandleWord(word);
            //    return word;
            //}).ToArray();

            //var result = string.Join(" ", words);
            //Console.WriteLine(result);

            //words = sentence.Split().Select(word => new string(word.Reverse().ToArray())).ToArray();

            //result = string.Join(" ", words);
            //Console.WriteLine(result);
            ///////////////////////SINGLE THREAD EXECUTION///////////////////////////
            ///
            var sw = new Stopwatch();
            var sentence = "Bruce is the best coder in the Mondongo Neighbourhood";
            sw.Start();
            var words = sentence.Split()
                .AsParallel()//Parallel linq expresion, this will execute different threads for each process
                .AsOrdered()
                .WithExecutionMode(ParallelExecutionMode.ForceParallelism) //Can force parallelism instead of sequential operation
                .Select(word => new string(word.ToLower().Reverse().ToArray())).ToArray();
            sw.Stop();
            var result = string.Join(" ", words);
            Console.WriteLine($"Result: {result} / Elapsed time: {sw.ElapsedMilliseconds}");
        }


        string HandleWord(string word) 
        {
            if (string.IsNullOrEmpty(word))
            {
                Console.WriteLine("Can't work on null or empty strings, returning");
                return string.Empty;
            }
            //Thread.Sleep(1000);
            var result = word.ToLower();
            char c = result[0];

            StringBuilder sb = new StringBuilder(result);
            sb.Remove(0, 1);
            sb.Append($"{c}ay");

            return sb.ToString();
        }
        
    }
}
