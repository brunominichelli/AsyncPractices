using System;
namespace PigLatinSentences
{
    partial class Program
    {
        static void Main(string[] args)
        {
            string sentence = "Bruno Minichelli";
            PigLatinManager.PigLatin(sentence);
            Console.Read();
        }
    }
}
