using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransltrProject.SyncAlgorithms
{
    public class SyncByLengthRatio : SyncMethod
    {
        private double CalculateStdDev(List<double> values)
        {
            double ret = 0;
            if (values.Count() > 0)
            {
                //Compute the Average      
                double avg = values.Average();
                //Perform the Sum of (value-avg)_2_2      
                double sum = values.Sum(d => Math.Pow(d - avg, 2));
                //Put it all together      
                ret = Math.Sqrt((sum) / (values.Count() - 1));
            }
            return ret;
        }

        public override AlignmentSetting GetBestDeviation(Subtitle s1, Subtitle s2, out double deviation)
        {
            List<Tuple<Sentence, int>> smallS1 = this.FindSentenceNextToGaps(s1, 30);
            List<Tuple<Sentence, int>> smallS2 = this.FindSentenceNextToGaps(s2, 30);

            List<Tuple<Sentence, List<Sentence>>> subHistoryS1 = new List<Tuple<Sentence, List<Sentence>>>();
            List<Tuple<Sentence, List<Sentence>>> subHistoryS2 = new List<Tuple<Sentence, List<Sentence>>>();

            foreach (var sen in smallS1)
            {
                if (sen.Item2 < 3)
                    continue;
                List<Sentence> historyPerSen = new List<Sentence>();
                historyPerSen.Add(s1.Sentences[sen.Item2 - 2]);
                historyPerSen.Add(s1.Sentences[sen.Item2 - 3]);
                historyPerSen.Add(s1.Sentences[sen.Item2 - 4]);
                subHistoryS1.Add(new Tuple<Sentence, List<Sentence>>(sen.Item1, historyPerSen));
            }

            foreach (var sen in smallS2)
            {
                if (sen.Item2 < 3)
                    continue;
                List<Sentence> historyPerSen = new List<Sentence>();
                historyPerSen.Add(s2.Sentences[sen.Item2 - 2]);
                historyPerSen.Add(s2.Sentences[sen.Item2 - 3]);
                historyPerSen.Add(s2.Sentences[sen.Item2 - 4]);
                subHistoryS2.Add(new Tuple<Sentence, List<Sentence>>(sen.Item1, historyPerSen));
            }

            double minScore = 0;
            AlignmentSetting bestAlignsetting = new AlignmentSetting(0,0,0,0);

            for (int i1 = 0; i1 < subHistoryS1.Count - 1; i1++)
            {
                for (int j1 = i1 + 1; j1 < subHistoryS1.Count; j1++)
                {
                    for (int i2 = 0; i2 < subHistoryS2.Count - 1; i2++)
                    {
                        for (int j2 = i2 + 1; j2 < subHistoryS2.Count; j2++)
                        {
                            double score = calcScore(subHistoryS1[i1].Item2, subHistoryS1[j1].Item2, subHistoryS2[i2].Item2, subHistoryS2[j2].Item2);
                            score = Math.Abs(score);
                            if (score < minScore || minScore == 0)
                            {
                                minScore = score;
                                bestAlignsetting = new AlignmentSetting(smallS1[i1].Item2, smallS1[j1].Item2, smallS2[i2].Item2, smallS2[j2].Item2);
                            }

                        }
                    }
                }

            }
            deviation = minScore;
            return bestAlignsetting;
        }

        private double calcScore(List<Sentence> src1, List<Sentence> src2, List<Sentence> trg1, List<Sentence> trg2)
        {
            List<double> ratioList = new List<double>();
            for (int i = 0; i < src1.Count; i++)
            {
                ratioList.Add(src1[i].TimeToNext / (double)trg1[i].TimeToNext);
            }
            double score = CalculateStdDev(ratioList);
            ratioList = new List<double>();
            for (int i = 0; i < src2.Count; i++)
            {
                ratioList.Add(src2[i].TimeToNext / (double)trg2[i].TimeToNext);
            }
            score += CalculateStdDev(ratioList);
            return score;
        }

    }
}
