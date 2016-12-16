namespace IvyOS.Core
{
    public class Math
    {
        public enum Base
        {
            Base02,
            Base10,
            Base16,
            Base26,
            Base36,
            Base52,
            Base62
        }
        public static string IntToBase(int value, Base Base)
        {
            string val = "";
            if (Base == Base.Base02)
                val = IntToBase(value, new char[] { '0', '1' });
            else if (Base == Base.Base10)
                val = IntToBase(value, new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' });
            else if (Base == Base.Base16)
                val = IntToBase(value, new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' });
            else if (Base == Base.Base26)
            {
                char[] t = "abcdefghijklnmopqrstuvwxyz".ToCharArray();
                val = IntToBase(value, t);
            }
            else if (Base == Base.Base36)
            {
                char[] t = "0123456789abcdefghijklnmopqrstuvwxyz".ToCharArray();
                val = IntToBase(value, t);
            }
            else if (Base == Base.Base52)
            {
                char[] t = "abcdefghijklnmopqrstuvwxyzABCDEFGHIJKLNMOPQRSTUVWXYZ".ToCharArray();
                val = IntToBase(value, t);
            }
            else if (Base == Base.Base62)
            {
                char[] t = "0123456789abcdefghijklnmopqrstuvwxyzABCDEFGHIJKLNMOPQRSTUVWXYZ".ToCharArray();
                val = IntToBase(value, t);
            }
            return val;
        }


        public static string IntToBase(int value, char[] baseChars)
        {
            string result = string.Empty;
            int targetBase = baseChars.Length;

            while (value > 0)
            {
                result = baseChars[value % targetBase] + result;
                value = value / targetBase;
            }

            return result;

        }

        public static int BaseToInt(string value, Base Base)
        {
            int val = 0;
            if (Base == Base.Base02)
            {
                val = BaseToInt(value, new char[] { '0', '1' });
            }
            else if (Base == Base.Base10)
            {
                val = BaseToInt(value, new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' });
            }
            else if (Base == Base.Base16)
            {
                val = BaseToInt(value, new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' });
            }
            else if (Base == Base.Base26)
            {
                char[] t = "abcdefghijklnmopqrstuvwxyz".ToCharArray();
                val = BaseToInt(value, t);
            }
            else if (Base == Base.Base36)
            {
                char[] t = "0123456789abcdefghijklnmopqrstuvwxyz".ToCharArray();
                val = BaseToInt(value, t);
            }
            else if (Base == Base.Base52)
            {
                char[] t = "abcdefghijklnmopqrstuvwxyzABCDEFGHIJKLNMOPQRSTUVWXYZ".ToCharArray();
                val = BaseToInt(value, t);
            }
            else if (Base == Base.Base62)
            {
                char[] t = "0123456789abcdefghijklnmopqrstuvwxyzABCDEFGHIJKLNMOPQRSTUVWXYZ".ToCharArray();
                val = BaseToInt(value, t);
            }

            return val;

        }

        public static int BaseToInt(string value, char[] baseChars)
        {
            int result = 0;
            foreach (char c in value)
            for (int i = 0; i < baseChars.Length; i++)
            if (c == baseChars[i])
            {
                result += i;
                break;
            }
            return result;
        }
    }
}