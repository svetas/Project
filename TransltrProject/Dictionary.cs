using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TransltrProject
{
    class Dictionary
    {
        public static Dictionary<string, string> dic = new Dictionary<string, string>();
        public static string Translate(string input)
        {
            string ans = "";
            Regex r = new Regex("[^a-zA-Z\\s]+");
            input = r.Replace(input, "");
            string[] inputArray = input.Split(' ');
            
            
            foreach (string item in inputArray)
            {
                
                if (dic.ContainsKey(item))
                {
                    ans +=" " + dic[item];
                }
                else
                {
                    ans += " " + item;
                }
            }
            return ans;
        }
    }
}
