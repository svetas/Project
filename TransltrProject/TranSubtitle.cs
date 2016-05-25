using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
namespace TransltrProject
{
    public class TranSubtitle
    {
        public List<Tuple<string,string>> Translations { get; set; }
        public TranSubtitle()
        {
            Translations = new List<Tuple<string, string>>();
        }
        public List<Tuple<string[], string[]>> GetTranslations()
        {
            List<Tuple<string[], string[]>> ans = new List<Tuple<string[], string[]>>();
            foreach (var tran in Translations)
            {
                // get texts
                string tranSrc = tran.Item1;
                string tranDst = tran.Item2;
                // split
                string[] splitspace = { "?", "!", ".", "-", "," }; //{"?", "!", ".", "-" };
                //string[] splitspace = { };
                string[] tranSrcWords = tranSrc.Split(splitspace, StringSplitOptions.RemoveEmptyEntries);
                string[] tranDstWords = tranDst.Split(splitspace, StringSplitOptions.RemoveEmptyEntries);
                //string[] tranSrcWords = { tranSrc };
                //string[] tranDstWords = { tranDst };
                ans.Add(new Tuple<string[], string[]>(tranSrcWords, tranDstWords));                             
            }
            return ans;
        }
        public void WriteToFile(string p)
        {
            using (StreamWriter sw = new StreamWriter(p))
            {
                foreach (var item in Translations)
                {
                    sw.WriteLine(item.Item1 + " --> " + item.Item2);
                }
            }
        }
        internal void Add(string phrase, string v)
        {
            Translations.Add(new Tuple<string, string>(phrase, v));
        }
    }
}
