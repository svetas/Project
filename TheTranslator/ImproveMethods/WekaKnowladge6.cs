﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheTranslator.DataManager;

namespace TheTranslator.ImproveMethods
{
    class WekaKnowladge6 : ImprovementMethod
    {

        // 

        public override string ChooseBetter(string ourTrans, string otherTrans, string source, out int selected)
        {
            bool takeOur = false;

            if (ourTrans == null || ourTrans.Length == 0)
            {
                selected = -1;
                IncrementCounter("Picke Moses");
                return otherTrans;
            }
            else
            {
                int ourLength = ourTrans.Split(' ').Length;
                int mosesLength = otherTrans.Split(' ').Length;
                //int sourceLength = source.Split(' ').Length;
                //char containsShort = ourTrans.Contains("&apos;") ? 't' : 'f';
                int timesSeen = (int)DBManager.GetInstance().GetSet(source, ourTrans);
                int OurMosesLevinstain = LevenshteinDistance(otherTrans, ourTrans);
                //int ourSourceLenDiff = Math.Abs(ourLength - sourceLength);
                double ourMosesJaccard = CalcJaccard(ourTrans, otherTrans);
                ///char ContainsApos = ourTrans.Contains("&apos;") ? 't' : 'f';

                if (OurMosesLevinstain <= 0) takeOur = false;
                else
                {
                    if (ourLength < mosesLength)
                    {
                        if (mosesLength <= 1) takeOur = false;
                        else
                        {
                            if (ourLength <= 1)
                            {
                                if (OurMosesLevinstain <= 4) takeOur = true;
                                else
                                {
                                    if (mosesLength <= 2) takeOur = false;
                                    else takeOur = true;
                                }
                               
                            }
                            else
                            {
                                if (mosesLength <= 2) takeOur = false;
                                else
                                {
                                    if (ourLength <= 2) takeOur = true;
                                    else
                                    {
                                        if (timesSeen <= 5)
                                        {
                                            if (mosesLength <= 3) takeOur = false;
                                            else
                                            {
                                                if (ourLength <= 3) takeOur = true;
                                                else takeOur = false;
                                            }
                                        }
                                        else
                                        {
                                            if (OurMosesLevinstain <= 11)
                                            {
                                                if (ourMosesJaccard <= 0.6) takeOur = false;
                                                else takeOur = true;
                                            }
                                            else takeOur = true;
                                        }
                                        
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (ourLength <= 2)
                        {
                            if (timesSeen <= 753)
                            {
                                if (timesSeen <= 547)
                                {
                                    if (timesSeen <= 35)
                                    {
                                        if (timesSeen <= 33) takeOur = false;
                                        else takeOur = true;
                                    }
                                    else takeOur = false;
                                }
                                else takeOur = true;
                            }
                            else takeOur = false;
                        }
                        else takeOur = false;
                    }
                  
                }


                /*if (OurMosesLevinstain <= 0) takeOur = false;
                else
                {
                    if (ourLength < mosesLength)
                    {
                        if (mosesLength <= 1) takeOur = false;
                        else
                        {
                            if (timesSeen <= 3) takeOur = false;
                            else
                            {
                                if (timesSeen <= 491)
                                {
                                    if (mosesLength <= 2) takeOur = false;
                                    else takeOur = true;
                                }
                                else takeOur = true;
                            }
                        }
                    }
                    else takeOur = false;
                }*/


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
