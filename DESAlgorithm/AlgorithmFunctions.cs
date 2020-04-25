using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESAlgorithm
{
    public class AlgorithmFunctions
    {
        public static string Crypt(string data, string key, EnumProcessType processType)
        {
            string binaryData = BinaryHelper.GetBits(data);

            string result = "";
            for (int index = 0; index < ((decimal)binaryData.Length / (decimal)64); index++)
            {
                bool finished = false;

                string currentBinaryString = binaryData.Substring(index * 64);

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

                for (int cryptIndex = 0; cryptIndex < 16; cryptIndex++)
                {
                    string cryptedRightBinaryString = CryptFunction(rightBinaryString, key);
                    string prevLeftBinaryString = leftBinaryString;
                    leftBinaryString = cryptedRightBinaryString;
                    rightBinaryString = prevLeftBinaryString;
                    key = KeyTransformation(key, cryptIndex, processType);
                }

                result += leftBinaryString + rightBinaryString;

                if (finished)
                    break;
            }

            return result;
        }

        public static string CryptFunction(string rightBinaryString, string key)
        {
            var res = "";
            foreach (var currentChar in rightBinaryString)
            {
                if (currentChar == '1')
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

        public static string KeyTransformation(string baseKey, int index, EnumProcessType processType)
        {
            //56 bit'lik Key 28 er iki diziye ayrılır
            string leftBinaryString = baseKey.Substring(0, baseKey.Length / 2);
            string rightBinaryString = baseKey.Substring(baseKey.Length / 2);

            leftBinaryString = CircularLeftShift(leftBinaryString, index, processType);
            rightBinaryString = CircularLeftShift(rightBinaryString, index, processType);

            /*Bu diziler birleştirilip sonrasında compression işlemi uygulanarak
             * 56 bitten 48 bit elde edilir
            */

            var transformedKey = CompressionPermutation(leftBinaryString + rightBinaryString);


            return transformedKey;
        }

        public static string InitialPermutation(string input)
        {
            string result = "";

            foreach(int permutationTableItem in Data.InıtialPermutationTable)
            {
                if (input.Length >= permutationTableItem)
                {
                    result += input[permutationTableItem - 1];
                }
            }

            return result;
        }

        /// <summary>
        /// anahtar oluşturma için laydırma işlemi
        /// Circular Left shift
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string CircularLeftShift(string key, int index, EnumProcessType processType)
        {
            char[] resultChars = new char[key.Length];

            int shift;
            if (processType == EnumProcessType.ENCRYPTION)
            {
                shift = Data.CircularLeftshiftTable[index];
            }
            else
            {
                shift = Data.CircularLeftshiftTable.Reverse().ToArray()[index];
            }

            for(int keyCharIndex = 0; keyCharIndex < key.Length; keyCharIndex++)
            {
                if (processType == EnumProcessType.ENCRYPTION)
                {
                    resultChars[(keyCharIndex + shift) % (key.Length)] = key[keyCharIndex];
                }
                else
                {
                    resultChars[(keyCharIndex - shift + key.Length) % (key.Length)] = key[keyCharIndex];
                }
            }

            string result = "";
            foreach(var resultChar in resultChars)
            {
                result += resultChar;
            }

            return result;
        }

        public static string CompressionPermutation(string input)
        {
            string result = "";

            foreach (int permutationTableItem in Data.CompressionPermutationTable)
            {
                if (input.Length >= permutationTableItem)
                {
                    result += input[permutationTableItem - 1];
                }
            }

            return result;
        }

        public static string ExpentsonPermutation(string input)
        {
            string result = "";

            foreach (int permutationTableItem in Data.ExpansionPermutationTable)
            {
                if (input.Length >= permutationTableItem)
                {
                    result += input[permutationTableItem - 1];
                }
            }

            return result;
        }

        public static char XOR(char one, char two)
        {
            return Data.XORData[one][two];
        }
    }
}
