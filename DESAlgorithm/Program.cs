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
            string data = "ENSAR DUMAN";
            string binaryData = BinaryHelper.GetBits(data);
            Console.WriteLine(binaryData);
            Console.WriteLine(binaryData.Length);

            string result = "";
            for (int index = 0; index < ((decimal)binaryData.Length / (decimal)64); index++)
            {
                bool finished = false;

                string currentBinaryString = "";

                currentBinaryString = binaryData.Substring(index * 64);

                if (currentBinaryString.Length > 64)
                {
                    currentBinaryString = currentBinaryString.Substring(0, 64);
                }
                else
                {
                    finished = true;
                }

                string leftBinaryString = currentBinaryString.Substring(0, currentBinaryString.Length / 2);
                string rightBinaryString = currentBinaryString.Substring(currentBinaryString.Length / 2);

                result +=  Crypt(leftBinaryString, rightBinaryString, "101010101");

                if (finished)
                    break;
            }

            Console.WriteLine(result);
            Console.WriteLine(result.Length);
            Console.ReadLine();

        }

        private static string Crypt(string leftBinaryString, string rightBinaryString, string key)
        {
            for (int cryptIndex = 1; cryptIndex <= 16; cryptIndex++)
            {
                string cryptedRightBinaryString = CryptFunction(rightBinaryString, key);
                string prevLeftBinaryString = leftBinaryString;
                leftBinaryString = cryptedRightBinaryString;
                rightBinaryString = prevLeftBinaryString;
                key = GenerateNewKey(key);
            } 

            return leftBinaryString + rightBinaryString;
        }

        private static string CryptFunction(string rightBinaryString, string key)
        {
            var res = "";
            foreach(var currentChar in rightBinaryString)
            {
                if(currentChar == '1')
                {
                    res += "0";
                }
                else
                {
                    res += "1";
                }
            }


            return rightBinaryString;
        }

        private static string GenerateNewKey(string oldKey)
        {
            return oldKey;
        }
    }
}
