using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESAlgorithm
{
    public class BinaryHelper
    {
        public static string GetBitsFromString(string input)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in Encoding.Unicode.GetBytes(input))
            {
                sb.Append(Convert.ToString(b, 2));
            }
            return sb.ToString();
        }

        public static string GetStringFromBits(string input)
        {
            List<byte> bytes = new List<byte>();
            for (int inputIndex = 0; inputIndex < input.Length; inputIndex += 8)
            {
                if (inputIndex + 6 < input.Length)
                {
                    bytes.Add(Convert.ToByte(input.Substring(inputIndex, 8), 2));
                }
                else
                {
                    bytes.Add(Convert.ToByte(input.Substring(inputIndex), 2));
                    break;
                }
            }

            return Encoding.UTF8.GetString(bytes.ToArray());
        }

        public static string GetHexFromBits(string input)
        {
            string hexadecimal = "";
            for (int inputIndex = 0; inputIndex < input.Length; inputIndex += 4)
            {
                bool isFinished = false;
                string word;
                if (inputIndex + 4 < input.Length)
                {
                    word = input.Substring(inputIndex, 4);
                }
                else
                {
                    word = input.Substring(inputIndex);
                    isFinished = true; ;
                }

                switch (word)
                {
                    case "0000": hexadecimal += '0'; break;
                    case "0001": hexadecimal += '1'; break;
                    case "0010": hexadecimal += '2'; break;
                    case "0011": hexadecimal += '3'; break;
                    case "0100": hexadecimal += '4'; break;
                    case "0101": hexadecimal += '5'; break;
                    case "0110": hexadecimal += '6'; break;
                    case "0111": hexadecimal += '7'; break;
                    case "1000": hexadecimal += '8'; break;
                    case "1001": hexadecimal += '9'; break;
                    case "1010": hexadecimal += 'A'; break;
                    case "1011": hexadecimal += 'B'; break;
                    case "1100": hexadecimal += 'C'; break;
                    case "1101": hexadecimal += 'D'; break;
                    case "1110": hexadecimal += 'E'; break;
                    case "1111": hexadecimal += 'F'; break;
                    default:
                        return "Invalid number";
                }

                if (isFinished)
                    break;
            }

            return hexadecimal;
        }

        public static string GetBitsFromHex(string input)
        {
            string binary = "";
            foreach (var inputChar in input)
            {
                switch (inputChar)
                {
                    case '0': binary += "0000"; break;
                    case '1': binary += "0001"; break;
                    case '2': binary += "0010"; break;
                    case '3': binary += "0011"; break;
                    case '4': binary += "0100"; break;
                    case '5': binary += "0101"; break;
                    case '6': binary += "0110"; break;
                    case '7': binary += "0111"; break;
                    case '8': binary += "1000"; break;
                    case '9': binary += "1001"; break;
                    case 'A': binary += "1010"; break;
                    case 'B': binary += "1011"; break;
                    case 'C': binary += "1100"; break;
                    case 'D': binary += "1101"; break;
                    case 'E': binary += "1110"; break;
                    case 'F': binary += "1111"; break;
                    default:
                        return "Invalid number";
                }
            }

            return binary;
        }

        public static int BinaryToInteger(string binary)
        {
            return Convert.ToInt32(binary, 2);
        }

        public static string IntegerToBinary(int intValue)
        {
            return IntegerToBinary(intValue, false, 0);
        }

        public static string IntegerToBinary(int intValue, int width)
        {
            return IntegerToBinary(intValue, true, 4);
        }

        private static string IntegerToBinary(int intValue, bool paddingLeft, int width)
        {
            var strValue = Convert.ToString(intValue, 2);

            if (paddingLeft)
            {
                strValue = strValue.PadLeft(width, '0');
            }

            return strValue;
        }
    }

    public enum EnumProcessType
    {
        ENCRYPTION,
        DECRYPTION
    }

}
