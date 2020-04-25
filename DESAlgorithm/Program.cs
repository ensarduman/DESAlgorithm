using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            string data = "ENSARDUM";
            string key = "10111110101011100001110001001110000010000100100000100011";
            var crypted = AlgorithmFunctions.Crypt(data, key, EnumProcessType.ENCRYPTION);

            Console.WriteLine(crypted);
            Console.ReadLine();
        }
    }
}
