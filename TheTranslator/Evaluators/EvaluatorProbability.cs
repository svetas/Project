using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheTranslator.Evaluators
{
    class EvaluatorProbability : Evaluator
    {
        public EvaluatorProbability(DistanceStatistics ex) : base(ex) { }

        public override string GetBestTranslation(List<TranslationOption> transO)
        {
            DistanceStatistics m_dsStats = ((DistanceStatistics)m_stats);
            double bestRank = 0;
            string answer = "fail";
            foreach (var transSen in transO)
            {
                double rank = 0;
                double wordRank = 1;
                TargetSentence[] parts = transSen.m_targetSenParts.ToArray();
                for (int i = 0; i < parts.Length; i++)
                {
                    for (int j = 1; j < 3; j++)
                    {
                        if (j + i >= parts.Length) break;                        
                        wordRank = wordRank * m_dsStats.GetRank(parts[i].m_translation, parts[i+j].m_translation, j);
                    }
                    rank += wordRank;
                }
                if (rank > bestRank)
                {
                    answer = transSen.concatParts();
                }
            }
            return answer;
        }
    }
}
