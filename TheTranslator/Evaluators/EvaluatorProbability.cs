using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheTranslator.Evaluators
{
    class EvaluatorProbability : Evaluator
    {
        private const double ALPHA = 0.2;

        public EvaluatorProbability(DistanceStatistics ex) : base(ex) { }

        public override string GetBestTranslation(List<TranslationOption> transO)
        {
            DistanceStatistics m_dsStats = ((DistanceStatistics)m_stats);
            double bestRank = 0;
            string answer = "fail";
            foreach (var transSen in transO)
            {

                double partsPopularity = 0;
                TargetSentence[] parts = transSen.m_targetSenParts.ToArray();

                // Part 1: calculate the probablity of the sentence with connected parts.
                foreach (var item in parts)
                {
                    partsPopularity += m_dsStats.GetPopularity(item.m_translation);//item.m_pr;
                }
                partsPopularity = partsPopularity / (double)parts.Length;
                // Part 2: calculate the probability that the words would be in that order.
                // Part 2.1: Marge all the translation into one string

                string wholeSentence = "";
                for (int i = 0; i < parts.Length; i++)
                {
                    if (i + 1 < parts.Length)
                        wholeSentence += parts[i].m_translation + " ";
                    else
                        wholeSentence += parts[i].m_translation;
                }

                // Part 2.2: Split the translation into parts
                string[] splittedSen = wholeSentence.Split(' ');

                // Part 2.3: Calcualte parts connectivity
                double conScore = 0;
                for (int i = 0; i < splittedSen.Length; i++)
                {
                    double wordRank = 1;
                    for (int j = 0; j < 2; j++)
                    {
                        if (j + i + 1 >= splittedSen.Length) break;
                        double grade = m_dsStats.GetRank(splittedSen[i], splittedSen[i + j + 1], j);
                        wordRank = wordRank * grade;
                    }
                    conScore += wordRank;
                }
                conScore = conScore / splittedSen.Length;
                // part 3: Calculate average
                //double rank = (conScore * ALPHA) + (partsPopularity * (1 - ALPHA));
                double rank = ALPHA * Math.Sqrt(conScore*partsPopularity) + ((1 - ALPHA) * (1/(double) transSen.m_targetSenParts.Count));

                //Trace.WriteLine(wholeSentence.PadRight(40) + "| Parts Connectivity: " + conScore + "| Parts Popularity: " + partsPopularity + " | Total: "+rank);
                //Console.WriteLine(wholeSentence.PadRight(40) + "| Parts Connectivity: " + conScore + "| Parts Popularity: " + partsPopularity + " | Total: " + rank);

                if (rank > bestRank)
                {
                    bestRank = rank;
                    answer = transSen.concatParts();
                }
            }
            return answer.TrimStart(' ');
        }
    }
}
