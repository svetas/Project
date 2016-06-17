using BLEUevaluator;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheTranslator.DataManager;

namespace TheTranslator.Common
{
    class WekaOutput
    {
        // source length, moses length, our length, reference length, repetitions, contains apos, better bleu our
        public static List<WekaRecord> m_instances = new List<WekaRecord>();

        public static void Add(string lineSource, string lineReference, string lineMoses, string ourline)
        {
            if (ourline == "") return;

            WekaRecord wr = new WekaRecord();

            wr.Repitions =  (int)DBManager.GetInstance().GetSet(lineSource, ourline);
            wr.SourceLength = lineSource.Split(' ').Length;
            wr.MosesLength = lineMoses.Split(' ').Length;
            wr.OurLength= ourline.Split(' ').Length;
            wr.ContainsApos = ourline.Contains("&apos;") ? 't' : 'f';
            wr.OurMosesLevinstain = LevenshteinDistance(lineMoses, ourline);
            wr.OurMosesJaccard = CalcJaccard(lineMoses, ourline);

            double MosesScore = BLEU.Evaluate(lineMoses, new List<string>() { lineReference });
            double currentBleu = BLEU.Evaluate(ourline, new List<string>() { lineReference });
            char betterSystem = MosesScore >currentBleu ? 'f' : 't';
            if (ourline.Length < lineMoses.Length) betterSystem = 'f';

            wr.Replace = betterSystem;

            m_instances.Add(wr);
        }

        public static int LevenshteinDistance(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            // Step 1
            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            // Step 2
            for (int i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (int j = 0; j <= m; d[0, j] = j++)
            {
            }

            // Step 3
            for (int i = 1; i <= n; i++)
            {
                //Step 4
                for (int j = 1; j <= m; j++)
                {
                    // Step 5
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    // Step 6
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            // Step 7
            return d[n, m];
        }

        private static double CalcJaccard(string trans, string lineM)
        {
            List<string> l1 = trans.Split(' ').ToList();
            List<string> l2 = lineM.Split(' ').ToList();

            int mone = l1.Intersect(l2).Count();
            int mechane = l1.Union(l2).Count();

            return mone / (double)mechane;
        }

        public static void Print(string path)
        {
            int amountTotal = Math.Min(50000, m_instances.Count);
            int limit = (int)(amountTotal * 0.95);
            StreamWriter sw = new StreamWriter(path+"Train.arff");
            sw.WriteLine("@relation 'translationPreference'");
            sw.WriteLine("@attribute 'sourceLength' integer");
            sw.WriteLine("@attribute 'mosesLength' integer");
            sw.WriteLine("@attribute 'ourLength' integer");
            sw.WriteLine("@attribute 'timesSeen' integer");
            sw.WriteLine("@attribute 'OurSourceLenDiff' integer");
            sw.WriteLine("@attribute 'OurMosesJaccard' real");
            sw.WriteLine("@attribute 'OurMosesLevin' integer");
            sw.WriteLine("@attribute 'contains apos' {f,t}");
            sw.WriteLine("@attribute 'OurLonger' {f,t}");
            sw.WriteLine("@attribute 'class' {f,t}");
            sw.WriteLine("@data");

            for (int i = 0; i < limit; i++)
            {
                var item = m_instances[i];

                sw.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}",item.SourceLength,item.MosesLength,item.OurLength,item.Repitions,
                    item.SourceOurLenDiff,item.OurMosesJaccard,item.OurMosesLevinstain,item.ContainsApos,item.WeHaveLonger,item.Replace);
            }

            sw.Close();

            sw = new StreamWriter(path + "Test.arff");
            sw.WriteLine("@relation 'translationPreference'");
            sw.WriteLine("@attribute 'sourceLength' integer");
            sw.WriteLine("@attribute 'mosesLength' integer");
            sw.WriteLine("@attribute 'ourLength' integer");
            sw.WriteLine("@attribute 'timesSeen' integer");
            sw.WriteLine("@attribute 'OurSourceLenDiff' integer");
            sw.WriteLine("@attribute 'OurMosesJaccard' real");
            sw.WriteLine("@attribute 'OurMosesLevin' integer");
            sw.WriteLine("@attribute 'contains apos' {f,t}");
            sw.WriteLine("@attribute 'OurLonger' {f,t}");      
            sw.WriteLine("@attribute 'class' {f,t}");
            sw.WriteLine("@data");

            for (int i = limit; i < amountTotal; i++)
            {
                var item = m_instances[i];
                sw.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", item.SourceLength, item.MosesLength, item.OurLength, item.Repitions,
                    item.SourceOurLenDiff, item.OurMosesJaccard, item.OurMosesLevinstain, item.ContainsApos, item.WeHaveLonger, item.Replace);
            }

            sw.Close();
        }
    }
}
