using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESAlgorithm
{
    public class BinaryHelper
    {
        public static string GetBits(string input)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in Encoding.Unicode.GetBytes(input))
            {
                sb.Append(Convert.ToString(b, 2));
            }
            return sb.ToString();
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
