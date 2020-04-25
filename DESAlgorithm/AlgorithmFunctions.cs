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
            //data karıştırılır

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

                currentBinaryString = InitialPermutation(currentBinaryString);

                //32'şer bit olarak data ikiye ayrılır
                string leftBinaryString = currentBinaryString.Substring(0, currentBinaryString.Length / 2);
                string rightBinaryString = currentBinaryString.Substring(currentBinaryString.Length / 2);

                //cryp işlemleri 16 kez tekrarlanır. Her adımda data çaprazlanır
                for (int cryptIndex = 0; cryptIndex < 16; cryptIndex++)
                {
                    //Bu adım için 56 bitlik Key'den 48 bitlik yeni key üretilir
                    var transformedKey = KeyTransformation(key, cryptIndex, processType);

                    //leftBinaryString öncelikle 48 bit'e genişletilir
                    leftBinaryString = ExpentsonPermutation(leftBinaryString);

                    //Key (48bit) ve leftBinaryString (48bit) XOR'lanır (her bit ikilik sistemde toplanıp mod2'si alınır)
                    string cryptedRightBinaryString = XORArray(leftBinaryString, transformedKey);

                    //leftBinaryString tekrar 48'den 32 bit'e küçültülür(SBoxSubstuation)

                    //left ve right'ın yerleri değiştirilerek yeni adıma hazırlanır. (çaprazlama)
                    string prevLeftBinaryString = leftBinaryString;
                    leftBinaryString = cryptedRightBinaryString;
                    rightBinaryString = prevLeftBinaryString;
                }

                //sonuçta elde edilen left ve right birbirine eklenerek 64 bitlik data elde edilir
                var resultBinary = leftBinaryString + rightBinaryString;
                resultBinary = FinalPermutation(resultBinary);
                result += resultBinary;

                if (finished)
                    break;
            }

            return result;
        }

        public static string XORArray(string input, string key)
        {
            var res = "";

            for (int inputIndex = 0; inputIndex < input.Length; inputIndex++)
            {
                res += XOR(input[inputIndex], key[inputIndex]);
            }

            return input;
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

            foreach (int permutationTableItem in Data.InitialPermutationTable)
            {
                if (input.Length >= permutationTableItem)
                {
                    result += input[permutationTableItem - 1];
                }
            }

            return result;
        }

        public static string FinalPermutation(string input)
        {
            string result = "";

            foreach (int permutationTableItem in Data.FinalPermutationTable)
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

        /// <summary>
        /// ikilik sistemde bitler toplanarak mod2'si alınır(XOR işlemi)
        /// </summary>
        /// <param name="one"></param>
        /// <param name="two"></param>
        /// <returns></returns>
        public static char XOR(char one, char two)
        {
            return Data.XORData[one][two];
        }
    }
}
