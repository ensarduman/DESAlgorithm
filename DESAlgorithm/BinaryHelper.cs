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
    }

    public enum EnumProcessType
    {
        ENCRYPTION,
        DECRYPTION
    }

}
