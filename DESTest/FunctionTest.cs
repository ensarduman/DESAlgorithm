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
            for(int i = 1; i <= 64; i++)
            {
                Assert.IsTrue(AlgorithmFunctions.permutationTable.Contains(i));
            }

            var data = "0011111111100111000100111101100000101000010110001111111100101100";
            AlgorithmFunctions algorithmFunctions = new AlgorithmFunctions();
            var output = algorithmFunctions.InitialPermutation(data);
            Assert.IsTrue(output == "0110101001101101110000110100011101001010110100111111100101000111");
        }
    }
}
