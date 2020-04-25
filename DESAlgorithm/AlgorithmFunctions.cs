using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESAlgorithm
{
    public class AlgorithmFunctions
    {

        public static string Crypt(string leftBinaryString, string rightBinaryString, string key)
        {
            for (int cryptIndex = 0; cryptIndex < 16; cryptIndex++)
            {
                string cryptedRightBinaryString = CryptFunction(rightBinaryString, key);
                string prevLeftBinaryString = leftBinaryString;
                leftBinaryString = cryptedRightBinaryString;
                rightBinaryString = prevLeftBinaryString;
                key = GenerateNewKey(key);
            }

            return leftBinaryString + rightBinaryString;
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

        public static string GenerateNewKey(string oldKey)
        {
            return oldKey;
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
        /// anahtar oluşturma
        /// Circular Left shift
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string KeyTransformationEncryption(string key, int index)
        {
            char[] resultChars = new char[key.Length];

            int shift = Data.CircularLeftshiftTable[index];

            for(int keyCharIndex = 0; keyCharIndex < key.Length; keyCharIndex++)
            {
                resultChars[(keyCharIndex + shift) % (key.Length)] = key[keyCharIndex];
            }

            string result = "";
            foreach(var resultChar in resultChars)
            {
                result += resultChar;
            }

            return result;
        }

        /// <summary>
        /// anahtar oluşturma
        /// Circular Left shift
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string KeyTransformationDecryption(string key, int index)
        {
            char[] resultChars = new char[key.Length];

            int shift = Data.CircularLeftshiftTable.Reverse().ToArray()[index];

            for (int keyCharIndex = 0; keyCharIndex < key.Length; keyCharIndex++)
            {
                resultChars[(keyCharIndex - shift + key.Length) % (key.Length)] = key[keyCharIndex];
            }

            string result = "";
            foreach (var resultChar in resultChars)
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
    }
}
