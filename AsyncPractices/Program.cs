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
            AutoResetEventTest();
        }


        static void AutoResetEventTest() 
        {
            var synchro = new SynchronizationAutoResetEvent();
            synchro.Start();
        }
    }
}
