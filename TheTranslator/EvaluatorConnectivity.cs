using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheTranslator
{
    class EvaluatorConnectivity : Evaluator
    {
        // i'm the best
        private static double alpha = 0.5;
        public EvaluatorConnectivity(Extractor ex) : base(ex) { }
        public override string GetBestTranslation(List<TranslationOption> transO)
        {
            double maxScor = -1;
            string bestTrans = "";
            double score;
            string trans = null;
            List<double> connectivity;
            string[] chunkPrevSplited;
            string[] chunkCurrSplited;
            foreach (var transSen in transO)
            {
                connectivity = new List<double>();
                //the first part is perfectly connected to nothing
                connectivity.Add(1.0);
                trans = transSen.concatParts();
                //find the connection of alll the other parts
                for (int i = 1; i < transSen.m_targetSenParts.Count; i++)
                {

                    chunkPrevSplited = transSen.m_targetSenParts[i - 1].m_translation.Split(new string[] { " ", "," }, StringSplitOptions.RemoveEmptyEntries);
                    chunkCurrSplited = transSen.m_targetSenParts[i].m_translation.Split(new string[] { " ", "," }, StringSplitOptions.RemoveEmptyEntries);
                    connectivity.Add(calconnectivityP(chunkPrevSplited, chunkCurrSplited));



                }
                score = calcScoreAvg(connectivity);
                if (score > maxScor)
                {
                    maxScor = score;
                    bestTrans = trans;
                }
            }
            return bestTrans;
        }

        //I(X, Y)=LOG2(P(X&Y) / P(Y))
        private double calconnectivityI(string[] chunkPrevSplited, string[] chunkCurrSplited)
        {
            string leftW = chunkPrevSplited.Last();
            string rightW = chunkCurrSplited.First();
            int countLR = m_extct.getCountTaPair(leftW, rightW);
            int countL = m_extct.getCountTaWord(leftW);
            int countR = m_extct.getCountTaWord(rightW);
            double PXY = countLR / (double)(countL + countR);
            double PY = countR / (double)m_extct.m_countWordsInDb;
            double ans = Math.Log((PXY / PY), 2);
            return ans;
        }

        //P(X, Y)=C(X&Y) / (C(Y)+C(X))
        private double calconnectivityP(string[] chunkPrevSplited, string[] chunkCurrSplited)
        {
            string leftW = chunkPrevSplited.Last();
            string rightW = chunkCurrSplited.First();
            int countLR = m_extct.getCountTaPair(leftW, rightW);
            int countL = m_extct.getCountTaWord(leftW);
            int countR = m_extct.getCountTaWord(rightW);
            double ans = countLR / (double)(countL+ countR);
            return ans;
        }




        private double calcScoreMean(List<double> connectivity)
        {
            throw new NotImplementedException();
        }

        
        private double calcScoreAvg(List<double> connectivity)
        {
            double sum = 0;
            foreach (var c in connectivity)
            {
                sum += c;
            }
            return sum / connectivity.Count;
        }
    }
}

