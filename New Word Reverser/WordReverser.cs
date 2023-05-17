using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace New_Word_Reverser
{
    public static class WordReverser
    {
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
            for (int i = str.Length - 1; i >= 0; --i)
            {
                sb.Append(str[i]);
            }

            var result = sb.ToString();

            return result;
        }

        static string[] Map(string sentence)
        {
            if (string.IsNullOrEmpty(sentence))
            {
                Console.WriteLine($"Sentence parameter is either null or empty, returning null.");
                return null;
            }
            var result = sentence.Split();
            Console.WriteLine($"Sentence has {result.Length} words.");
            return result;
            //foreach (var word in sentence.Split())
            //{
            //    m_tasks.Add(Task<string>.Factory.StartNew(() => $"{ReverseString(word, m_delay)} ", TaskCreationOptions.AttachedToParent | TaskCreationOptions.LongRunning));

        }

        static string[] Process(string[] words)
        {
            for (int i = 0; i < words.Length; i++)
            {
                //In this case, it is mandatory to snapshot the index because as this loop creates child threads, the index will iterate again, without waiting for the previous one, and will cause an out of range exception
                var index = i;
                Task<string>.Factory.StartNew(() => words[index] = ReverseString(words[index], 1000), TaskCreationOptions.AttachedToParent | TaskCreationOptions.LongRunning);
            }
            return words;
        }

        static string Reduce(string[] words)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < words.Length; i++)
            {
                if (i >= words.Length - 1)
                {
                    sb.Append($"{words[i]}");
                }
                else
                {
                    sb.Append($"{words[i]} ");
                }
            }
            return sb.ToString();
        }

        public static void ConcatTasks()
        {
            string sentence = $"the quick brown bruno jumped over the lazy bruh";

            var sw = new Stopwatch();
            sw.Start();
            Console.WriteLine("Starting process..");
            var contatenateTask = Task<string[]>.Factory.StartNew(() => Map(sentence))
                                  .ContinueWith<string[]>(t => Process(t.Result))
                                  .ContinueWith<string>(t => Reduce(t.Result));
            sw.Stop();

            Console.WriteLine($"Process ended. Elapsed time: {sw.ElapsedMilliseconds}");
            Console.WriteLine($"Result: {contatenateTask.Result}");
        }
    }

}

