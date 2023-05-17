using System;
namespace Plinq
{
    class Program
    {
        static void Main(string[] args)
        {
            ReverseLinq();
            Console.WriteLine();
            Console.Read();
        }

        static void ReverseLinq()
        {
            var plinqHandler = new PlinqHandling();
            plinqHandler.ReversingWordPlinq();
        }
    }
}
