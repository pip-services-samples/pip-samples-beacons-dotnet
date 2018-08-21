using Step6.Container;
using System;

namespace Step8.Process
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var beacon = (new BeaconsProcess()).RunAsync(args);
                beacon.Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadLine();
            }
        }
    }
}
