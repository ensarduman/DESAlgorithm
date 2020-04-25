﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESAlgorithm
{
    public class Data
    {
        //InıtialPermutationTable
        public static int[] InıtialPermutationTable = new int[]
        {
                58,50,42,34,26,18,10,2,60,52,44,36,28,20,12,4,
                62,54,46,38,30,22,14,6,64,56,48,40,32,24,16,8,
                57,49,41,33,25,17,9,1,59,51,43,35,27,19,11,3,
                61,53,45,37,29,21,13,5,63,55,47,39,31,23,15,7
        };

        //Circular Left shift Table 
        public static int[] CircularLeftshiftTable = new int[]
        {
            1,1,2,2,2,2,2,2,1,2,2,2,2,2,2,1
        };

        /// <summary>
        /// Compression Permutation Table
        /// 56 bit to 48
        /// </summary>
        public static int[] CompressionPermutationTable = new int[]
        {
            14,17,11,24,1,5,3,28,15,6,21,10,
            23,19,12,4,26,8,16,7,27,20,13,2,
            41,52,31,37,47,55,30,40,51,45,33,48,
            44,49,39,56,34,53,46,42,50,36,29,32
        };

        /// <summary>
        /// Expansion Permutation Table
        /// genişletme
        /// </summary>
        public static int[] ExpansionPermutationTable = new int[]
        {
            32,1,2,3,4,5,6,7,8,9,
            8,9,10,11,12,13,14,15,16,17,
            16,17,18,19,20,21,22,23,24,25,
            24,25,26,27,28,29,30,31,32,1
        };
    }
}