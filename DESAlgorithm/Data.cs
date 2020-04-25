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
        public static int[] InitialPermutationTable = new int[]
        {
                58,50,42,34,26,18,10,2,60,52,44,36,28,20,12,4,
                62,54,46,38,30,22,14,6,64,56,48,40,32,24,16,8,
                57,49,41,33,25,17,9,1,59,51,43,35,27,19,11,3,
                61,53,45,37,29,21,13,5,63,55,47,39,31,23,15,7
        };

        //InıtialPermutationTable
        public static int[] FinalPermutationTable = new int[]
        {
            40,8,48,16,56,24,64,32,39,7,47,15,55,23,63,31,
            38,6,46,14,54,22,62,30,37,5,45,13,53,21,61,29,
            36,4,44,12,52,20,60,28,35,3,43,11,51,19,59,27,
            34,2,42,10,50,18,58,26,33,1,41,9,49,17,57,25
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
            32,1,2,3,4,5,4,5,6,7,8,9,
            8,9,10,11,12,13,12,13,14,15,16,17,
            16,17,18,19,20,21,20,21,22,23,24,25,
            24,25,26,27,28,29,28,29,30,31,32,1
        };

        /// <summary>
        /// XOR function data
        /// </summary>
        public static Dictionary<char, Dictionary<char, char>> XORData = new Dictionary<char, Dictionary<char, char>>
        {
            {'0', new Dictionary<char, char>{ { '0', '0'}, { '1', '1' } } },
            {'1', new Dictionary<char, char>{ { '0', '1'}, { '1', '0' } } },
        };
    }
}
