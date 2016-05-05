using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheTranslator
{
    class EvaluatorNaive:Evaluator
    {
        private static double alpha = 0.5;
        public EvaluatorNaive(Extractor ex) : base(ex) { }
        public override string GetBestTranslation(List<TranslationOption> transO)
        {
            double maxScor = -1;
            string bestTrans = "";
            foreach (var item in transO)
            {
                string trans = item.concatParts();
                double score = item.m_prPi * Math.Pow(0.8, item.m_chunkCount-1);
                if (score > maxScor)
                {
                    maxScor = score;
                    bestTrans = trans;
                }

            }
            return bestTrans;
        }
    }
}
