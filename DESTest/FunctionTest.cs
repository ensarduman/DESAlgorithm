﻿using System;
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
            for(int i = 1; i <= 64; i++)
            {
                Assert.IsTrue(Data.InıtialPermutationTable.Contains(i));
            }

            var data = "0011111111100111000100111101100000101000010110001111111100101100";
            var output = AlgorithmFunctions.InitialPermutation(data);
            Assert.IsTrue(output == "0110101001101101110000110100011101001010110100111111100101000111");
        }

        [TestMethod]
        public void KeyTransformation()
        {
            string data = "ENSAR DUMAN";
            var res = AlgorithmFunctions.KeyTransformationEncryption(data, 0);
            res = AlgorithmFunctions.KeyTransformationDecryption(res, 0);
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
    }
}
