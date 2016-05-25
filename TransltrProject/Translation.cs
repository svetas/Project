using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransltrProject
{
    public class Translation
    {
        public Dictionary<string, Dictionary<string, int>> Translations;

        public Translation()
        {
            Translations = new Dictionary<string, Dictionary<string, int>>();
        }

        public void AddToDictionary(TranSubtitle translations)
        {
            List<Tuple<string[], string[]>> Lines = translations.GetTranslations();
            // first, add all the single words definitions
            foreach (var item in Lines)
            {
                List<string> dstList = item.Item1.ToList();
                List<string> srcList = item.Item2.ToList();

                dstList.RemoveAll(str => string.IsNullOrWhiteSpace(str));
                srcList.RemoveAll(str => string.IsNullOrWhiteSpace(str));

                string[] src = dstList.ToArray();
                string[] dst = srcList.ToArray();

                /*string srcText = string.Join(" ", item.Item1);
                string dstText = string.Join(" ", item.Item2);

                var charsToRemove = new string[] { "@", ",", ".", ";", "'" ,"?"};
                foreach (var c in charsToRemove)
                {
                    srcText = srcText.Replace(c, string.Empty).TrimEnd(' ').TrimStart(' ');
                    dstText = dstText.Replace(c, string.Empty).TrimEnd(' ').TrimStart(' ');
                }
                if (!Translations.ContainsKey(srcText))
                    Translations.Add(srcText, new Dictionary<string, int>());

                if (Translations[srcText].ContainsKey(dstText))
                    Translations[srcText][dstText] += 1;
                else
                    Translations[srcText].Add(dstText, 1);*/

                if (src.Count() == dst.Count())
                {
                    for (int i = 0; i < dst.Count(); i++)
                    {
                        string srcText = src[i];
                        string dstText = dst[i];

                        var charsToRemove = new string[] { "@", ".", ";", "'", "?" };
                        foreach (var c in charsToRemove)
                        {
                            srcText = srcText.Replace(c, string.Empty).TrimEnd(' ').TrimStart(' ');
                            dstText = dstText.Replace(c, string.Empty).TrimEnd(' ').TrimStart(' ');
                        }
                        if (!Translations.ContainsKey(srcText))
                            Translations.Add(srcText, new Dictionary<string, int>());

                        if (Translations[srcText].ContainsKey(dstText))
                            Translations[srcText][dstText] += 1;
                        else
                            Translations[srcText].Add(dstText, 1);
                    }
                }
            }                            
        }

        internal void WriteToHTMLfile(string v)
        {
            StreamWriter sw = new StreamWriter(v);
            sw.Write("<html>");
            sw.Write("<body>");
            sw.Write("<table>");
            foreach (var item in Translations)
            {
                foreach (var values in item.Value)
                {
                    if (values.Value > 0)
                        sw.Write("<tr><td>" + item.Key + "</td><td>" + values.Key + "</td><td>" + values.Value + "</td>");
                }
            }
            sw.Write("</table>");
            sw.Write("</body>");
            sw.Write("</html>");
        }
    }
}
