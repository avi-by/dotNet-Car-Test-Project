using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    class Tools
    {
        static string[] seperators = { " ", ",", "/", ".", "/n" };
        public static string[] StringToStringArry(string st)
        {
            if (st == "")
                return null;
            foreach (string item in seperators)
            {
                st = st.Replace(item, " ");
            }
            while (st.Contains("  "))
            {
                st = st.Replace("  ", " ");
            }
            if (st.Last() == ' ')
            {
                st = st.Remove(st.Length - 1);
            }
            string[] tokens = st.Split(' ');
            return tokens;
        }
        /// <summary>
        /// make arrry of string from string sperated by seperatirs
        /// </summary>
        /// <param name="st"></param>
        /// <param name="senderSeperatrs"></param>
        /// <returns></returns>
        public static string[] StringToStringArry(string st ,params string[] senderSeperatrs)
        {
            if (st == "")
                return null;
            foreach (string item in senderSeperatrs)
            {
                st = st.Replace(item, " ");
            }
            while (st.Contains("  "))
            {
                st = st.Replace("  ", " ");
            }
            if (st.Last() == ' ')
            {
                st = st.Remove(st.Length - 1);
            }
            string[] tokens = st.Split(' ');
            return tokens;
        }

        public static int[] StringToIntArry(string st)
        {
            if (st == "")
                return null;
            foreach (string item in seperators)
            {
                st = st.Replace(item, " ");
            }
            while (st.Contains("  "))
            {
                st = st.Replace("  ", " ");
            }
            if (st.Last() == ' ')
            {
                st = st.Remove(st.Length - 1);
            }
            string[] tokens = st.Split(' ');
            try
            {
                int[] convertedItems = Array.ConvertAll(tokens, int.Parse);
                return convertedItems;
            }
            catch { return null; }
        }
    }
          
}
