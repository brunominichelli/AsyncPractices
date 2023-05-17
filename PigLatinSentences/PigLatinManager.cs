using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace PigLatinSentences
{
    public class PigLatinManager
    {

        // TODO: complete this method - return the sentence as pig latin
        public static string PigLatin(string sentence)
        {
            if (string.IsNullOrEmpty(sentence))
            {
                return string.Empty;
            }
            //string mySentence = "Bruno Minichelli";
            var continuationTask = Task<string[]>.Factory.StartNew(() => Map(sentence))
            .ContinueWith<string[]>(t => Process(t.Result))
            .ContinueWith<string>(t => Reduce(t.Result));

            Console.WriteLine($"Result: {continuationTask.Result}");
            return continuationTask.Result;
        }

        static string[] Map(string sentence)
        {
            var result = sentence.Split();
            return result;
        }

        static string[] Process(string[] words)
        {
            for (int i = 0; i < words.Length; ++i)
            {
                var index = i;
                Task<string>.Factory.StartNew(() =>
                {
                    words[index] = MutateString(words[index], 1000);
                    return words[index];
                }, TaskCreationOptions.AttachedToParent | TaskCreationOptions.LongRunning);

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
            var result = sb.ToString();
            return result;
        }

        static string MutateString(string str, int ms)
        {
            if (string.IsNullOrEmpty(str))
            {
                Console.WriteLine("Can't work on null or empty strings, returning");
                return string.Empty;
            }
            Console.WriteLine($"Setting string {str} to lower");
            Thread.Sleep(ms);
            var result = str.ToLower();
            char c = result[0];

            StringBuilder sb = new StringBuilder(result);
            sb.Remove(0, 1);
            sb.Append($"{c}ay");

            Console.WriteLine($"Current string status: {sb.ToString()}");

            return sb.ToString();
        }
    }


}
