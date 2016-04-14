namespace PolarisOS
{
    using Cosmos.HAL.SolarisGroup;

    public static class StringExtensions
    {
        /// <summary>
        /// Checks if the string starts with [string]
        /// </summary>
        public static bool _StartsWith(this string __str, string __expression)
        {
            string str = "";
            if (__expression.Length > __str.Length) return false;
            for (int i = 0; i < __expression.Length; i++)
            {
                str += __str[i];
                if (str == __expression) return true;
            }
            return false;
        }
        /// <summary>
        /// Checks if the string ends with [string]
        /// </summary>
        public static bool _EndsWith(this string __str, string __expression)
        {
            string str = "";
            if (__expression.Length > __str.Length) return false;
            for (int i = (__str.Length - 1) - (__expression.Length - 1); i == __str.Length - 1; i++)
            {
                str += __str[i];
                if (str == __expression) return true;
            }
            return false;
        }
        /// <summary>
        /// Returns the char at position [string[index]]
        /// </summary>
        public static char _GetCharAt(this string str, int null_based_index)
        {
            if (null_based_index >= 0 && null_based_index < str.Length)
                return str[null_based_index];
            Terminal.setFATAL().WrapGray("string._GetCharAt").White("null_based_index must be >= 0 and <= the length of the string");
            return char.MinValue;
        }
        /// <summary>
        /// Removes the char at position [string[index]]
        /// </summary>
        public static string _RemoveCharAt(this string str, int null_based_index)
        {
            if (null_based_index < str.Length)
            {
                string _str = "";
                for (int i = 0; i < null_based_index; i++) _str += str[i];
                for (int i = null_based_index + 1; i < str.Length; i++) _str += str[i];
                return _str;
            }
            Terminal.setFATAL().WrapGray("string._GetCharAt").White("null_based_index must be >= 0 and <= the length of the string");
            return str;
        }
    }
}