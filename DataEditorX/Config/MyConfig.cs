using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using DataEditorX.Common;

namespace DataEditorX.Config
{
    class MyConfig
    {
        public const int WM_OPEN = 0x0401;
        public const int MAX_HISTORY = 0x10;
        public const string TAG_DATA = "data";
        public const string TAG_LANGUAGE = "language";

        public const string FILE_LANGUAGE = "language.txt";
        public const string FILE_TEMP = "open.tmp";
        public const string FILE_HISTORY = "history.txt";
        
        public const string FILE_FUNCTION = "_functions.txt";
        public const string FILE_CONSTANT = "constant.lua";
        public const string FILE_STRINGS = "strings.conf";
        public const string TAG_SOURCE_URL = "sourceURL";
        public const string TAG_UPDATE_URL = "updateURL";

        public static string readString(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
        public static int readInteger(string key, int def)
        {
            int i;
            if (int.TryParse(readString(key), out i))
                return i;
            return def;
        }
        public static float readFloat(string key, float def)
        {
            float i;
            if (float.TryParse(readString(key), out i))
                return i;
            return def;
        }
        public static int[] readIntegers(string key, int length)
        {
            string temp = readString(key);
            int[] ints = new int[length];
            string[] ws = string.IsNullOrEmpty(temp) ? null : temp.Split(',');

            if (ws != null && ws.Length > 0 && ws.Length <= length)
            {
                for (int i = 0; i < ws.Length; i++)
                {
                    int.TryParse(ws[i], out ints[i]);
                }
            }
            return ints;
        }
        public static Area readArea(string key)
        {
            int[] ints = readIntegers(key, 4);
            Area a = new Area();
            if (ints != null)
            {
                a.left = ints[0];
                a.top = ints[1];
                a.width = ints[2];
                a.height = ints[3];
            }
            return a;
        }
        public static bool readBoolean(string key)
        {
            if (readString(key).ToLower() == "true")
                return true;
            else
                return false;
        }
    }

}
