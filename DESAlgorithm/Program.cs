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
            string data = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam varius, ex sit amet mollis convallis";
            var hexKey = "BEAE1C4E084823";

            var crypted = AlgorithmFunctions.Crypt(data, hexKey, EnumProcessType.ENCRYPTION);

            Console.WriteLine(crypted);
            Console.ReadLine();
        }
    }
}
