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
            var hexKey = "8E1823607165E3D6";

            var encrypted = AlgorithmFunctions.Encrypt(data, hexKey);

            var decrypted = AlgorithmFunctions.Decrypt(encrypted, hexKey);

            Console.WriteLine(encrypted);
            Console.ReadLine();
        }
    }
}
