using System;
using System.Collections.Generic;
using System.Linq;
using DESAlgorithm;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DESTest
{
    [TestClass]
    public class FunctionTest
    {
        [TestMethod]
        public void InitialPermutation()
        {
            for (int i = 1; i <= 64; i++)
            {
                Assert.IsTrue(Data.InitialPermutationTable.Contains(i));
            }

            var data = "0011111111100111000100111101100000101000010110001111111100101100";
            var output = AlgorithmFunctions.InitialPermutation(data);
            Assert.IsTrue(output == "0110101001101101110000110100011101001010110100111111100101000111");
        }

        [TestMethod]
        public void FinalPermutation()
        {
            for (int i = 1; i <= 64; i++)
            {
                Assert.IsTrue(Data.InitialPermutationTable.Contains(i));
            }

            var data = "0011111111100111000100111101100000101000010110001111111100101100";
            var output = AlgorithmFunctions.FinalPermutation(data);
            Assert.IsTrue(output == "0101110001011100010110101110101101101101110110100011100100011001");
        }

        [TestMethod]
        public void CircularLeftShift()
        {
            string data = "ENSAR DUMAN";
            var res = AlgorithmFunctions.CircularLeftShift(data, 0, EnumProcessType.ENCRYPTION);
            res = AlgorithmFunctions.CircularLeftShift(res, 0, EnumProcessType.DECRYPTION);
            Assert.IsTrue(res == data);
        }

        [TestMethod]
        public void CompressionPermutation()
        {
            Assert.IsTrue(Data.CompressionPermutationTable.Length == 48);

            string data = "ENSAR DUMAN";
            //56 to 48
            var res = AlgorithmFunctions.CompressionPermutation(data);
        }

        [TestMethod]
        public void ExpensionPermutation()
        {
            Assert.IsTrue(Data.ExpansionPermutationTable.Length == 48);

            string data = "ENSAR DUMAN";

            //32 to 48
            var res2 = AlgorithmFunctions.ExpentsonPermutation(data);
        }

        [TestMethod]
        public void XORFunction()
        {
            Assert.IsTrue(AlgorithmFunctions.XOR('1', '1') == '0');
            Assert.IsTrue(AlgorithmFunctions.XOR('1', '0') == '1');
            Assert.IsTrue(AlgorithmFunctions.XOR('0', '1') == '1');
            Assert.IsTrue(AlgorithmFunctions.XOR('0', '0') == '0');
        }

        [TestMethod]
        public void KeyTransformantion()
        {
            string key = "10111110101011100001110001001110000010000100100000100011";
            var transformedKey = AlgorithmFunctions.KeyTransformation(key, 0, EnumProcessType.ENCRYPTION);
            Assert.IsTrue(transformedKey.Length == 48);
        }
    }
}
