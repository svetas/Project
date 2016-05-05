using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheTranslator
{
    class CombinerNaive: Combiner
    {
        private const int N_FOR_TOPN = 4;
        private const int MINIMUM_TOPN_OPTION_COUNT = 2;
        

        public override List<TranslationOption> combine(List<List<Sentence>> transOptions, string source)
        {
            List<TranslationOption> ans = new List<TranslationOption>();
            if (transOptions.Count == 0)
                return ans;
            //List<sentence>
            foreach (var transOption in transOptions)
            {
                List<TranslationOption> partialAnsOld = new List<TranslationOption>();
                List<TranslationOption> partialAnsNew = new List<TranslationOption>();
                //sentence
                foreach (var transPart in transOption)
                {
                    partialAnsNew = new List<TranslationOption>();
                    List<TargetSentence> topN = transPart.getTopN(N_FOR_TOPN, MINIMUM_TOPN_OPTION_COUNT);
                    //no beginings of sentences
                    if (partialAnsOld.Count == 0)
                    {
                        foreach (var item in topN)
                        {
                            TranslationOption trO = new TranslationOption(source);
                            trO.addPart(item);
                            partialAnsOld.Add(trO);
                        }
                        partialAnsNew = partialAnsOld;
                        continue;

                    }

                    foreach (var item in topN)
                    {
                        foreach (var partAns in partialAnsOld)
                        {
                            TranslationOption tr = new TranslationOption(partAns);
                            tr.addPart(item);
                            partialAnsNew.Add(tr);
                        }

                    }

                    partialAnsOld = partialAnsNew;
                }
                ans.AddRange(partialAnsNew);
                
            }
            return ans;
        }
     
    }
}
