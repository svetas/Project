using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheTranslator.DataManager;

namespace TheTranslator.ImproveMethods
{
    class WekaKnowladgeGoogle2 : ImprovementMethod
    {

        // 

        public override string ChooseBetter(string ourTrans, string otherTrans, string source, out int selected)
        {
            bool takeOur = false;

            if (ourTrans == null || ourTrans.Length == 0)
            {
                selected = -1;
                IncrementCounter("Picked Moses");
                return otherTrans;
            }
            else
            {
                int ourLength = ourTrans.Split(' ').Length;
                int mosesLength = otherTrans.Split(' ').Length;
                int sourceLength = source.Split(' ').Length;
                char containsShort = ourTrans.Contains("&apos;") ? 't' : 'f';
                int timesSeen = (int)DBManager.GetInstance().GetSet(source, ourTrans);
                int OurMosesLevinstain = LevenshteinDistance(otherTrans, ourTrans);
                int ourSourceLenDiff = Math.Abs(ourLength - sourceLength);
                double ourMosesJaccard = CalcJaccard(ourTrans, otherTrans);

                /*OurLonger = f
                | mosesLength <= 1
                |   | timesSeen <= 2118: t(892.0 / 255.0)
                |   | timesSeen > 2118
                |   |   | OurMosesLevin <= 4: t(589.0 / 216.0)
                |   |   | OurMosesLevin > 4: f(291.0 / 35.0)
                | mosesLength > 1
                |   | contains apos = f: f(11583.0 / 466.0)
                |   | contains apos = t
                |   |   | mosesLength <= 4
                |   |   |   | timesSeen <= 33: t(211.0 / 77.0)
                |   |   |   | timesSeen > 33: f(227.0 / 95.0)
                |   |   | mosesLength > 4: f(1311.0 / 125.0)
                OurLonger = t: t(3765.0 / 641.0)*/

                if (ourLength < mosesLength)
                {
                    if (mosesLength <= 1)
                    {
                        if (timesSeen <= 2118) takeOur = true;
                        else
                        {
                            if (OurMosesLevinstain <= 4) takeOur = true;
                            else takeOur = false;
                        }
                    }
                    else
                    {
                        if (containsShort == 'f') takeOur = false;
                        else
                        {
                            if (mosesLength <= 4)
                            {
                                if (timesSeen <= 33) takeOur = true;
                                else takeOur = false;
                            }
                            else takeOur = false;
                        }
                    }
                }
                else takeOur = true;


                if (takeOur)
                {
                    selected = 1;
                    IncrementCounter("weka");
                    
                    return ourTrans;
                }
                else
                {
                    selected = -1;
                    IncrementCounter("Picked moses");
                    return otherTrans;
                }
            }

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
    }
}
