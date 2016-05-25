using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TransltrProject.SyncAlgorithms
{
    public class SyncByDictionary: SyncMethod
    {
        public override AlignmentSetting GetBestDeviation(Subtitle s1, Subtitle s2, out double deviation)
        {

            throw new NotImplementedException();
            return null;
            /* TRANSLATION BY DICTIONARY */
            deviation = 0;

            List<Tuple<Sentence, int>> set1 = FindSentenceNextToGaps(s1, 50);
            List<Tuple<Sentence, int>> set2 = FindSentenceNextToGaps(s2, 50);

            List<Tuple<int, int, double>> ranks = new List<Tuple<int, int, double>>();
            int count1 = 0;
            foreach (var itemEng in set1)
            {
                int count2 = 0;
                string traslatedEng = itemEng.Item1.Translate();
                foreach (var itemHeb in set2)
                {
                    Regex r = new Regex("[^א-ת\\s]+");
                    string heb = r.Replace(itemHeb.Item1.Text, "");
                    int common = traslatedEng.Split(' ').Intersect(heb.Split(' ')).Count();
                    int union = traslatedEng.Split(' ').Union(itemHeb.Item1.Text.Split(' ')).Count();

                    double jaccard = common / (double)union;
                    //double jaccard = common;
                    int distance = Math.Abs(count1 - count2) + 1;
                    double adjastedJcrd = jaccard * (1 / (double)distance);

                    ranks.Add(new Tuple<int, int, double>(itemEng.Item2, itemHeb.Item2, adjastedJcrd));
                    count2++;
                }
                count1++;



                //take best of each
                Tuple<int, int, double> currentMaxValue = ranks.First();
                Dictionary<Tuple<int, int>, double> dicAns = new Dictionary<Tuple<int, int>, double>();
                foreach (var item in ranks)
                {
                    if (currentMaxValue.Item1 == item.Item1)
                    {
                        if (currentMaxValue.Item3 < item.Item3)
                        {
                            currentMaxValue = item;
                        }
                    }
                    else
                    {
                        dicAns.Add(new Tuple<int, int>(currentMaxValue.Item1, currentMaxValue.Item2), currentMaxValue.Item3);
                        currentMaxValue = item;
                    }
                }

                //take 2 best options:
                var sortedDict = from entry in dicAns orderby entry.Value ascending select entry;
                Tuple<int, int> a1 = sortedDict.ElementAt(0).Key;
                Tuple<int, int> a2 = sortedDict.ElementAt(1).Key;

                AlignmentSetting alignsetting = new AlignmentSetting(a1.Item1, a2.Item1, a1.Item2, a2.Item2);



                return alignsetting;


                //private const string URL = "https://sub.domain.com/objects.json";
                //private string urlParameters = "?api_key=123";

                //Dictionary<TranSubtitle, double> results = new Dictionary<TranSubtitle, double>();
                /*for (int i = 0; i < 50; i++)
                {
                    for (int j = 49; j >=0; j--)
                    {
                        for (int n = 0; n < 50; n++)
                        {
                            for (int m = 49; m >0; m--)
                            {
                                Tuple<int, int, int, int> settings = new Tuple<int, int, int, int>(i, j, n, m);

                                int src1 = partialS1[settings.Item1].m_Start;
                                int src2 = partialS1[settings.Item2].m_Start;
                                int trg1 = partialS2[settings.Item3].m_Start;
                                int trg2 = partialS2[settings.Item4].m_Start;

                                string text = s1.Path + '\n';
                                text += " 1: " + partialS1[settings.Item1].m_Text + " --> " + partialS2[settings.Item3].m_Text + '\n';
                                text += " 2: " + partialS1[settings.Item2].m_Text + " --> " + partialS2[settings.Item4].m_Text + '\n';
                                Logger.WriteDebug(text);

                                double ratio = (trg1 - trg2) / (double)(src1 - src2);

                                double offSet = trg2 - src2 * ratio;

                                Subtitle newSubtitle = GenerateArtificialSubtitle(ratio, offSet);

                                TranSubtitle tr = ExtractAlinedSentences(s1, newSubtitle, out precision);
                                settingValue.Add(settings, precision);
                                results.Add(tr, precision);
                            }
                        }
                    }
                }*/
                /*double[] sizes = results.Values.ToArray();
                double maxRes = sizes.Max();
                TranSubtitle item = results.First(kvp => kvp.Value == maxRes).Key;
                precision = results.First(kvp => kvp.Value == maxRes).Value;*/
                //return item;
                /*
                double prevDev=0, deviationGrade = 20;
                List<Sentence> partialS1 = null;
                List<Sentence> partialS2 = null;
                Tuple<int, int, int, int> settings = null;
                int range = 15;
                while (Math.Abs(prevDev - deviationGrade) > 10)
                {
                    prevDev = deviationGrade;
                    partialS1 = FindSentenceNextToGaps(new Subtitle(s1), range);
                    partialS2 = FindSentenceNextToGaps(new Subtitle(s2), range);
                    settings = GetBestDeviation(partialS1, partialS2, out deviationGrade);
                    range += 5;
                }

                int src1 = partialS1[settings.Item1].m_Start;
                int src2 = partialS1[settings.Item2].m_Start;
                int trg1 = partialS2[settings.Item3].m_Start;
                int trg2 = partialS2[settings.Item4].m_Start;

                string text = s1.Path + '\n';
                text += " 1: " + partialS1[settings.Item1].m_Text + " --> " + partialS2[settings.Item3].m_Text + '\n';
                text += " 2: " + partialS1[settings.Item2].m_Text + " --> " + partialS2[settings.Item4].m_Text + '\n';
                Logger.WriteDebug(text);

                double ratio = (trg1 - trg2) / (double)(src1 - src2);

                double offSet = trg2 - src2 * ratio;

                Subtitle newSubtitle = GenerateArtificialSubtitles(ratio, offSet);

                TranSubtitle tr = ExtractAlinedSentences(s1, newSubtitle, out precision);
                */
                // return tr;
            }
        }


    }
}
