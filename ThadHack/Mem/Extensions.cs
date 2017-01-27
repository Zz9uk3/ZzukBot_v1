using System;
using System.CodeDom;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using ZzukBot.Constants;
using ZzukBot.Settings;

namespace ZzukBot.Mem
{
    /// <summary>
    /// Extension methods for different kind of things
    /// </summary>
    public static class Extensions
    {
        static Random _rnd = new Random();
        internal static string GenLuaVarName(this string name)
        {
            var shuffled = shuffle(name);
            var shuffled2 = new String(shuffled.Where(c => c != '-' && (c < '0' || c > '9')).ToArray());

            return shuffled2 + _rnd.Next(1, 9);
        }

        static string shuffle(string input)
        {
            var q = from c in input.ToCharArray()
                    orderby Guid.NewGuid()
                    select c;
            string s = string.Empty;
            foreach (var r in q)
                s += r;
            return s;
        }

        internal static void ExtInvoke(this Form value, Action parAction)
        {
            if (value.InvokeRequired)
            {
                value.Invoke(parAction);
            }
            else
            {
                parAction.Invoke();
            }
        }

        internal static void ExtBeginInvoke(this Form value, Action parAction)
        {
            if (value.InvokeRequired)
            {
                value.BeginInvoke(parAction);
            }
            else
            {
                parAction.Invoke();
            }
        }

        internal static string ExtJumpUp(this Assembly value, int parLevels)
        {
            var tmp = value.Location;
            for (var i = 0; i < parLevels; i++)
            {
                tmp = Path.GetDirectoryName(tmp);
            }
            return tmp;
        }

        internal static IntPtr Add(this IntPtr value, IntPtr toAdd)
        {
            return IntPtr.Add(value, (int)toAdd);
        }

        internal static IntPtr Add(this IntPtr value, int toAdd)
        {
            return IntPtr.Add(value, toAdd);
        }

        internal static IntPtr PointsTo(this IntPtr value)
        {
            return value == IntPtr.Zero ? IntPtr.Zero : Mem.Memory.Reader.Read<IntPtr>(value);
        }

        internal static IntPtr PointsTo(this int value)
        {
            return value == 0 ? IntPtr.Zero : Mem.Memory.Reader.Read<IntPtr>((IntPtr)value);
        }

        internal static IntPtr PointsTo(this uint value)
        {
            return value == 0 ? IntPtr.Zero : Mem.Memory.Reader.Read<IntPtr>((IntPtr)value);
        }

        internal static bool IsFlag(this Enum keys, Enum flag)
        {
            try
            {
                ulong keysVal = Convert.ToUInt64(keys);
                ulong flagVal = Convert.ToUInt64(flag);

                return (keysVal | flagVal) == flagVal;
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal static T ReadAs<T>(this IntPtr value) where T : struct
        {
            return value == IntPtr.Zero ? default(T) : Mem.Memory.Reader.Read<T>(value);
        }

        internal static T ReadAs<T>(this int value) where T : struct
        {
            return value == 0 ? default(T) : Mem.Memory.Reader.Read<T>((IntPtr)value);
        }

        internal static T ReadAs<T>(this uint value) where T : struct
        {
            return value == 0 ? default(T) : Mem.Memory.Reader.Read<T>((IntPtr)value);
        }

        internal static string ReadString(this IntPtr value, int length = 512, Encoding encoding = null)
        {
            if (value == IntPtr.Zero) return "";
            if ((int)value < 00401000) return "";
            if (encoding == null)
                encoding = Encoding.ASCII;
            try
            {
                return Mem.Memory.Reader.ReadString(value, encoding, length);
            }
            catch (Exception)
            {
                return "";
            }
        }

        internal static string ReadString(this int value, int length = 512, Encoding encoding = null)
        {
            if (value == 0) return "";
            if ((int)value < 00401000) return "";
            if (encoding == null)
                encoding = Encoding.ASCII;
            try
            {
                return Mem.Memory.Reader.ReadString((IntPtr)value, encoding, length);
            }
            catch (Exception)
            {
                return "";
            }
        }

        internal static string ReadString(this uint value, int length = 512, Encoding encoding = null)
        {
            if (value == 0) return "";
            if ((int)value < 00401000) return "";
            if (encoding == null)
                encoding = Encoding.ASCII;
            try
            {
                return Mem.Memory.Reader.ReadString((IntPtr)value, encoding, length);
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// Extension method to write a string to the bots root folder
        /// </summary>
        /// <param name="value">this</param>
        /// <param name="parFile">The files name</param>
        public static void LogTo(this string value, string parFile)
        {
            File.AppendAllText(Paths.Root + "\\" + parFile, value);
        }

        internal static void ExtUpdate(this Label value, string parFormat, params object[] parObjects)
        {
            value.Text = string.Format(parFormat, parObjects);
        }

        internal static void FileCreate(this string value, byte[] parArr)
        {
            if (File.Exists(value))
            {
                var bytes = File.ReadAllBytes(value);
                if (bytes.SequenceEqual(parArr)) return;
                File.WriteAllBytes(value, parArr);
            }
            else
            {
                File.WriteAllBytes(value, parArr);
            }
        }

        internal static bool FileEqualTo(this string value, byte[] parArr)
        {
            if (File.Exists(value))
            {
                var bytes = File.ReadAllBytes(value);
                if (bytes.SequenceEqual(parArr)) return true;
            }
            return false;
        }

        internal static void CreateFolderStructure(this string value)
        {
            if (!Directory.Exists(value))
            {
                Directory.CreateDirectory(value);
            }
        }
    }
}