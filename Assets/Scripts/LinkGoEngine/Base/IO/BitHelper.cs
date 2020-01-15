using System;
using System.Text;

namespace LinkGo.Base
{
    public class BitHelper
    {
        /// <summary>
        /// 按位取反操作
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static byte Reverse(byte v)
        {
            byte c = v;
            for (int i = 0; i < sizeof(byte); i++)
            {
                c = (byte)(c ^ (byte)(1 << i));
            }
            return c;
        }

        public static UInt16 Reverse(UInt16 v)
        {
            UInt16 c = v;
            for (int i = 0; i < sizeof(UInt16) * 8; i++)
            {
                c = (UInt16)(c ^ (UInt16)(1 << i));
            }
            return c;
        }

        public static UInt32 Reverse(UInt32 v)
        {
            UInt32 c = v;
            for (int i = 0; i < sizeof(UInt32) * 8; i++)
            {
                c = (UInt32)(c ^ (UInt32)(1 << i));
            }
            return c;
        }

        public static UInt64 Reverse(UInt64 v)
        {
            UInt64 c = v;
            for (int i = 0; i < sizeof(UInt64) * 8; i++)
            {
                c = (UInt64)(c ^ (UInt64)(1 << i));
            }
            return c;
        }

        public static byte Reverse(byte v, int index)
        {
            return (byte)(v ^ (byte)(1 << index));
        }

        public static UInt16 Reverse(UInt16 v, int index)
        {
            return (UInt16)(v ^ (UInt16)(1 << index));
        }

        public static UInt32 Reverse(UInt32 v, int index)
        {
            return (UInt32)(v ^ (UInt32)(1 << index));
        }

        public static UInt64 Reverse(UInt64 v, int index)
        {
            return (UInt64)(v ^ (UInt64)(1 << index));
        }

        public static byte Reverse(byte v, int startIndex, int endIndex)
        {
            byte c = v;
            for (int i = startIndex; i <= endIndex; i++)
            {
                c = Reverse(c, i);
            }
            return c;
        }

        public static UInt16 Reverse(UInt16 v, int startIndex, int endIndex)
        {
            UInt16 c = v;
            for (int i = startIndex; i <= endIndex; i++)
            {
                c = Reverse(c, i);
            }
            return c;
        }

        public static UInt32 Reverse(UInt32 v, int startIndex, int endIndex)
        {
            UInt32 c = v;
            for (int i = startIndex; i <= endIndex; i++)
            {
                c = Reverse(c, i);
            }
            return c;
        }

        public static UInt64 Reverse(UInt64 v, int startIndex, int endIndex)
        {
            UInt64 c = v;
            for (int i = startIndex; i <= endIndex; i++)
            {
                c = Reverse(c, i);
            }
            return c;
        }


        /// <summary>
        /// 反码位操作,正数相同，负数为符号位不变，其余取反
        /// </summary>
        /// <param name="b1"></param>
        /// <param name="b2"></param>
        /// <returns></returns>
        public static byte RadixMinusOneComplement(byte v)
        {
            byte s = sizeof(byte) * 8 - 1;
            if ((v & (1 << s)) == 0) return v; //正数反码相同


            return Reverse(v, 0, s - 1);
        }

        public static UInt16 RadixMinusOneComplement(UInt16 v)
        {
            byte s = sizeof(UInt16) * 8 - 1;
            if ((v & (1 << s)) == 0) return v; //正数反码相同


            return Reverse(v, 0, s - 1);
        }

        public static UInt32 RadixMinusOneComplement(UInt32 v)
        {
            byte s = sizeof(UInt32) * 8 - 1;
            if ((v & (1 << s)) == 0) return v; //正数反码相同


            return Reverse(v, 0, s - 1);
        }

        public static UInt64 RadixMinusOneComplement(UInt64 v)
        {
            byte s = sizeof(UInt64) * 8 - 1;
            if ((v & (UInt64)(1 << s)) == 0) return v; //正数反码相同


            return Reverse(v, 0, s - 1);
        }


        /// <summary>
        /// 补码
        /// </summary>
        /// <returns></returns>
        public static byte Complement(byte v)
        {
            byte s = sizeof(byte) * 8 - 1;
            if ((v & (1 << s)) == 0) return v; //正数反码相同


            byte b = RadixMinusOneComplement(v);
            return (byte)(b + 1);
        }

        public static UInt16 Complement(UInt16 v)
        {
            byte s = sizeof(UInt16) * 8 - 1;
            if ((v & (1 << s)) == 0) return v; //正数反码相同


            UInt16 b = RadixMinusOneComplement(v);
            return (UInt16)(b + 1);
        }
        public static UInt32 Complement(UInt32 v)
        {
            byte s = sizeof(UInt32) * 8 - 1;
            if ((v & (1 << s)) == 0) return v; //正数反码相同


            UInt32 b = RadixMinusOneComplement(v);
            return (UInt32)(b + 1);
        }
        public static UInt64 Complement(UInt64 v)
        {
            byte s = sizeof(UInt64) * 8 - 1;
            if ((v & (UInt64)(1 << s)) == 0) return v; //正数反码相同


            UInt64 b = RadixMinusOneComplement(v);
            return (UInt64)(b + 1);
        }


        /// <summary>
        /// 原码，正数相同，最高位为1表示负数，其余位就是原码真值
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static sbyte TrueForm(byte v)
        {
            byte s = sizeof(byte) * 8 - 1;
            if ((v & (1 << s)) == 0) return (sbyte)v; //正数反码相同


            byte c = Reverse(v, s);


            return (sbyte)(c * (-1));
        }

        public static Int16 TrueForm(UInt16 v)
        {
            byte s = sizeof(UInt16) * 8 - 1;
            if ((v & (1 << s)) == 0) return (Int16)v; //正数反码相同


            UInt16 c = Reverse(v, s);


            return (Int16)(c * (-1));
        }

        public static Int32 TrueForm(UInt32 v)
        {
            byte s = sizeof(UInt32) * 8 - 1;
            if ((v & (1 << s)) == 0) return (int)v; //正数反码相同


            UInt32 c = Reverse(v, s);


            return (Int32)(c * (-1));
        }

        public static Int64 TrueForm(UInt64 v)
        {
            byte s = sizeof(UInt64) * 8 - 1;
            if ((v & (UInt64)(1 << s)) == 0) return (int)v; //正数反码相同


            UInt64 c = Reverse(v, s);


            Int64 cc = Convert.ToInt64(c);
            return cc * (-1);
        }

        /// <summary>
        /// 根据补码，计算原码
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static byte ComplementToTrueForm(byte v)
        {
            byte s = sizeof(byte) * 8 - 1;
            if ((v & (1 << s)) == 0) return v; //正数补码相同

            byte b = (byte)(v - (byte)1); //逆操作，先减1
            byte c = RadixMinusOneComplement(b); //再取反
            return c;
        }
        public static UInt16 ComplementToTrueForm(UInt16 v)
        {
            byte s = sizeof(UInt16) * 8 - 1;
            if ((v & (1 << s)) == 0) return v; //正数补码相同

            UInt16 b = (UInt16)(v - (UInt16)1); //逆操作，先减1
            UInt16 c = RadixMinusOneComplement(b); //再取反
            return c;
        }
        public static UInt32 ComplementToTrueForm(UInt32 v)
        {
            byte s = sizeof(UInt32) * 8 - 1;
            if ((v & (1 << s)) == 0) return v; //正数补码相同

            UInt32 b = (UInt32)(v - (UInt32)1); //逆操作，先减1
            UInt32 c = RadixMinusOneComplement(b); //再取反

            return c;
        }
        public static UInt64 ComplementToTrueForm(UInt64 v)
        {
            byte s = sizeof(UInt64) * 8 - 1;
            if ((v & (ulong)(1 << s)) == 0) return v; //正数补码相同

            UInt64 b = (UInt64)(v - (UInt64)1); //逆操作，先减1
            UInt64 c = RadixMinusOneComplement(b); //再取反
            return c;
        }

        /// <summary>
        /// 保留某些位，其余位设置为0
        /// 如： ReserveBit(0xDE, 0, 3) = 0x0E
        /// </summary>
        /// <param name="b"></param>
        /// <param name="startBit"></param>
        /// <param name="endBit"></param>
        /// <returns></returns>
        public static byte Reserve(byte b, int startBit, int endBit)
        {
            byte s = sizeof(byte) * 8 - 1;
            if (endBit > s) return b;

            byte c = b;
            if (startBit > 0)
            {
                for (int i = 0; i < startBit; i++)
                {
                    c = (byte)(c & (byte)(byte.MaxValue - (1 << i)));
                }
            }

            for (int i = endBit + 1; i < s + 1; i++)
            {
                c = (byte)(c & (byte)(byte.MaxValue - (1 << i)));
            }
            return c;
        }
        public static UInt16 Reserve(UInt16 b, int startBit, int endBit)
        {
            byte s = sizeof(UInt16) * 8 - 1;

            if (endBit > s) return b;

            UInt16 c = b;
            if (startBit > 0)
            {
                for (int i = 0; i < startBit; i++)
                {
                    c = (UInt16)(c & (UInt16)(UInt16.MaxValue - (1 << i)));
                }
            }

            for (int i = endBit + 1; i < s + 1; i++)
            {
                c = (UInt16)(c & (UInt16)(UInt16.MaxValue - (1 << i)));
            }
            return c;
        }
        public static UInt32 Reserve(UInt32 b, int startBit, int endBit)
        {
            byte s = sizeof(UInt32) * 8 - 1;
            if (endBit > s) return b;
            UInt32 c = b;
            if (startBit > 0)
            {
                for (int i = 0; i < startBit; i++)
                {
                    c = (UInt32)(c & (UInt32)(UInt32.MaxValue - (1 << i)));
                }
            }

            for (int i = endBit + 1; i < s + 1; i++)
            {
                c = (UInt32)(c & (UInt32)(UInt32.MaxValue - (1 << i)));
            }
            return c;
        }

        public static UInt64 Reserve(UInt64 b, int startBit, int endBit)
        {
            byte s = sizeof(UInt64) * 8 - 1;
            if (endBit > s) return b;

            UInt64 c = b;
            if (startBit > 0)
            {
                for (int i = 0; i < startBit; i++)
                {
                    c = (UInt64)(c & (UInt64)(UInt64.MaxValue - (ulong)(1 << i)));
                }
            }

            for (int i = endBit + 1; i < s + 1; i++)
            {
                c = (UInt64)(c & (UInt64)(UInt64.MaxValue - (ulong)(1 << i)));
            }

            return c;
        }

        /// <summary>
        /// 合并多个字节，第一个参数是最高字节，其余类推
        /// 如：Combine2Byte(0x0f, 0x13) = 0x0F13
        /// </summary>
        /// <param name="b1"></param>
        /// <param name="b2"></param>
        /// <returns></returns>
        public static UInt16 Combine(byte b1, byte b2)
        {
            byte[] a = { b2, b1 };
            UInt16 b = BitConverter.ToUInt16(a, 0);
            return b;
        }

        public static UInt32 Combine(byte b1, byte b2, byte b3)
        {
            byte[] a = { b3, b2, b1 };
            UInt32 b = BitConverter.ToUInt32(a, 0);
            return b;
        }

        public static UInt64 Combine(byte b1, byte b2, byte b3, byte b4)
        {
            byte[] a = { b4, b3, b2, b1 };
            UInt64 b = BitConverter.ToUInt64(a, 0);
            return b;
        }

        /// <summary>
        /// 获取某位的值，1=true；0=false；
        /// </summary>
        /// <param name="v"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static bool GetBit(byte v, byte index)
        {
            byte c = Convert.ToByte(((v & (1 << index)) > 0) ? 1 : 0);
            bool b = c == 0x01 ? true : false;
            return b;
        }
        public static bool GetBit(UInt16 v, byte index)
        {
            byte c = Convert.ToByte(((v & (1 << index)) > 0) ? 1 : 0);
            bool b = c == 0x01 ? true : false;
            return b;
        }
        public static bool GetBit(UInt32 v, byte index)
        {
            byte c = Convert.ToByte(((v & (1 << index)) > 0) ? 1 : 0);
            bool b = c == 0x01 ? true : false;
            return b;
        }

        public static bool GetBit(UInt64 v, byte index)
        {
            byte c = Convert.ToByte(((v & (ulong)(1 << index)) > 0) ? 1 : 0);
            bool b = c == 0x01 ? true : false;
            return b;
        }

        /// <summary>
        /// 设置某一位的值
        /// </summary>
        /// <param name="data"></param>
        /// <param name="v"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static byte SetBit(byte data, bool v, byte index)
        {
            if (v) return (byte)(data | (1 << index));

            return (byte)(data & (byte.MaxValue - (1 << index)));
        }

        public static UInt16 SetBit(UInt16 data, bool v, byte index)
        {
            if (v) return (UInt16)(data | (1 << index));

            return (UInt16)(data & (UInt16.MaxValue - (1 << index)));
        }

        public static UInt32 SetBit(UInt32 data, bool v, byte index)
        {
            if (v) return (UInt32)(data | (UInt32)(1 << index));

            return (UInt32)(data & (UInt32.MaxValue - (1 << index)));
        }

        /// <summary>
        /// 取原码二进制格式，如：输入63，输出00111111
        /// </summary>
        /// <param name="originalValue">63</param>
        /// <returns>00111111</returns>
        public static string MachineCodeFormat(int originalValue)
        {
            StringBuilder buffer = new StringBuilder();

            int quotient = 0;
            int remainder = 0;

            int tmp = Math.Abs(originalValue);

            do
            {
                quotient = tmp / 2;
                remainder = tmp % 2;
                buffer.Insert(0, Convert.ToString(remainder));
                tmp = quotient;
            } while (tmp != 0);

            string result = buffer.ToString().TrimStart('0').PadLeft(7, '0');
            return result;
        }


        /// <summary>
        /// 将二进制转为十六进制
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string C2To16(string data)
        {
            return string.Format("{0:X}", System.Convert.ToInt32(data, 2));
        }

        public static Int16 C2To10(string data)
        {
            if (data.Length > 16) return 0;


            string i = string.Format("{0:D}", System.Convert.ToInt16(data, 2));
            Int16 r = Convert.ToInt16(i);
            return r;
        }


        /// <summary>
        /// 将十六进制转为二进制
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string C16To2(string data)
        {
            int d = Convert.ToInt16(data, 16);
            string a = MachineCodeFormat(d);
            return a;
        }

        public static string C16To2(UInt16 data)
        {
            string a = MachineCodeFormat(data);
            return a;
        }

        public static string C16To2(UInt32 data)
        {
            string a = MachineCodeFormat((int)data);
            return a;
        }


        /// <summary>
        /// 十六进制转二进制
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Int16 C16To10(string data)
        {
            return Convert.ToInt16(data, 16);
        }

        /// <summary>
        /// 十进制转十六进制
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte C10To16(Int16 data)
        {
            string c = string.Format("{0:X}", data);
            byte d = Convert.ToByte(c, 16);
            return d;
        }


        /// <summary>
        /// 十进制转二进制
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string C10To2(Int16 data)
        {
            string a = Convert.ToString(data, 2);
            string r = a.ToString().TrimStart('0').PadLeft(7, '0');
            return r;
        }
    }
}


