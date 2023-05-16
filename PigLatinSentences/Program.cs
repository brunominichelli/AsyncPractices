using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
