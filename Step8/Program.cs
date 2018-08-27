using Container;
using System;

namespace Step8
{
    class Program
    {
        public static void Main(string[] args)
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