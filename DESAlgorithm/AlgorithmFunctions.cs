using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESAlgorithm
{
    public class AlgorithmFunctions
    {
        public static string Encrypt(string data, string hexKey)
        {
            //data karıştırılır
            string binaryData = BinaryHelper.GetBitsFromString(data);

            var result = Crypt(binaryData, hexKey, EnumProcessType.ENCRYPTION);

            return BinaryHelper.GetHexFromBits(result);
        }

        public static string Decrypt(string data, string hexKey)
        {
            //data karıştırılır
            string binaryData = BinaryHelper.GetBitsFromHex(data);

            var result = Crypt(binaryData, hexKey, EnumProcessType.DECRYPTION);

            return BinaryHelper.GetStringFromBits(result);
        }

        public static string Crypt(string binaryData, string hexKey, EnumProcessType processType)
        {
            //hexKey bit string'e dönüşütürülür
            string bitKey = BinaryHelper.GetBitsFromHex(hexKey);

            //Key 64 bit'ten 56 bit'e dönüştürülür
            bitKey = CompressionPermutationKeyInit(bitKey);

            string result = "";

            List<int> roundIndexes = new List<int>();

            for (int index = 0; index < ((decimal)binaryData.Length / (decimal)64); index++)
                roundIndexes.Add(index);

            //On decrpytion, rounds are reversed
            if (processType == EnumProcessType.DECRYPTION)
                roundIndexes.Reverse();


            foreach (int index in roundIndexes)
            {
                bool finished = false;

                string currentBinaryString = binaryData.Substring(index * 64);

                if (currentBinaryString.Length > 64)
                {
                    currentBinaryString = currentBinaryString.Substring(0, 64);

                    finished = processType == EnumProcessType.DECRYPTION && index == 0;
                }
                else
                {
                    finished = processType == EnumProcessType.ENCRYPTION || (processType == EnumProcessType.DECRYPTION && index == 0);
                }

                currentBinaryString = InitialPermutation(currentBinaryString);

                //32'şer bit olarak data ikiye ayrılır
                string leftBinaryString = currentBinaryString.Substring(0, currentBinaryString.Length / 2);
                string rightBinaryString = currentBinaryString.Substring(currentBinaryString.Length / 2);

                //cryp işlemleri 16 kez tekrarlanır. Her adımda data çaprazlanır
                for (int cryptIndex = 0; cryptIndex < 16; cryptIndex++)
                {
                    //Bu adım için 56 bitlik Key'den 48 bitlik yeni key üretilir
                    var transformedKey = KeyTransformation(bitKey, cryptIndex, processType);

                    //FUNCTION çalıştırılır
                    var expandedRightBinaryString = MainFunction(rightBinaryString, transformedKey);

                    //leftBinaryString (48bit) ve expandedRightBinaryString (48bit) XOR'lanır (her bit ikilik sistemde toplanıp mod2'si alınır)
                    leftBinaryString = XORArray(leftBinaryString, expandedRightBinaryString);

                    //left ve right'ın yerleri değiştirilerek yeni adıma hazırlanır. (çaprazlama)
                    string prevLeftBinaryString = leftBinaryString;
                    leftBinaryString = rightBinaryString;
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

        //Ana F fonksiyonu
        public static string MainFunction(string rightBinaryString, string transformedKey)
        {

            //rightBinaryString öncelikle 48 bit'e genişletilir
            var expandedRightBinaryString = ExpensionPermutation(rightBinaryString);

            //Key (48bit) ve expandedRightBinaryString (48bit) XOR'lanır (her bit ikilik sistemde toplanıp mod2'si alınır)
            expandedRightBinaryString = XORArray(expandedRightBinaryString, transformedKey);

            //leftBinaryString tekrar 48'den 32 bit'e küçültülür(SBoxSubstuation)
            expandedRightBinaryString = SBoxSubstitution(expandedRightBinaryString);

            //expandedRightBinaryString PBox tablosuna göre karıştırılıyor
            expandedRightBinaryString = PBoxPermutation(expandedRightBinaryString);

            return expandedRightBinaryString;
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

            for (int keyCharIndex = 0; keyCharIndex < key.Length; keyCharIndex++)
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
            foreach (var resultChar in resultChars)
            {
                result += resultChar;
            }

            return result;
        }

        /// <summary>
        /// 64 bit to 56 bit compression
        /// </summary>
        /// <param nameinputIndex></param>
        public static string CompressionPermutationKeyInit(string input)
        {
            string result = "";

            for(int inputIndex = 0; inputIndex < input.Length; inputIndex++)
            {
                if((inputIndex + 1) % 8 != 0)
                {
                    result += input[inputIndex];
                }
            }

            return result;
        }

        /// <summary>
        /// 56 bit to 48 bit compression
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 32 to 48 bit expension
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ExpensionPermutation(string input)
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

        /// <summary>
        /// 48 bitlik datayı 32 bit'e dönüştürür.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string SBoxSubstitution(string input)
        {
            //48 biti 6 bitlik bloklara bölüyoruz
            List<string> sixBitBlocks = new List<string>();
            for (int inputIndex = 0; inputIndex < input.Length; inputIndex += 6)
            {
                if (inputIndex + 6 < input.Length)
                {
                    sixBitBlocks.Add(input.Substring(inputIndex, 6));
                }
                else
                {
                    sixBitBlocks.Add(input.Substring(inputIndex));
                    break;
                }
            }

            string result = "";

            /*
             Her bir blok için ilk ve son bit yan yana eklenip 
             integer'a çevirilecek. elde edilen sayı satırı belirtecek.

             Ortada kalanlar ise yine ineteger'a çevirilecek. elde edilen
             sayı sütunu belirtecek.
             */
            for (int blockIndex = 0; blockIndex < sixBitBlocks.Count; blockIndex++)
            {
                //block tablosu index'e göre alınıyor
                int[,] sBoxTable = Data.SBoxSubstitutionTables[blockIndex];

                var block = sixBitBlocks[blockIndex];

                //satır numarası alınıyor
                string lineNumberBinary = "";

                //baştaki rakam direkt alınıp block'tan kaldırılıyor
                lineNumberBinary += block[0];
                block = block.Remove(0, 1);

                //ikinci rakam eğer var ise alınıp block'tan kaldırılıyor
                if(block.Length > 0)
                {
                    lineNumberBinary += block[block.Length - 1];
                    block = block.Remove(block.Length - 1, 1);
                }

                //satır sayısı alındı
                int lineNumber = BinaryHelper.BinaryToInteger(lineNumberBinary);

                //sütun numarası alınıyor
                int columnNumber = 0;
                if(block.Length > 0 )
                {
                    columnNumber = BinaryHelper.BinaryToInteger(block);
                }

                int intValue = sBoxTable[lineNumber, columnNumber];
                result += BinaryHelper.IntegerToBinary(intValue, 4);
            }

            return result;
        }

        /// <summary>
        /// 32 bitlik datayı karıştırarak yine 32 bit olarak verir
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string PBoxPermutation(string input)
        {
            string result = "";

            foreach (int pBoxTableItem in Data.PBoxPermutationTable)
            {
                if (input.Length >= pBoxTableItem)
                {
                    result += input[pBoxTableItem - 1];
                }
            }

            return result;
        }
    }
}
