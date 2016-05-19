using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLEUevaluator
{
    public class BLEUEvaluation
    {
        // @Option(shortName = "c", longName = "bleu.closestRefLength", usage =
        // "Use closest reference length when determining brevity penalty? (true behaves like IBM BLEU, false behaves like old NIST BLEU)",
        // defaultValue="true")
        // boolean closestRefLength;
        public int verbosity;

        private BLEU bleu = new BLEU();

        public static string[] SUBMETRIC_NAMES = { "bleu1p", "bleu2p", "bleu3p", "bleu4p", "brevity" };

        public int[] stats(string hyp, List<string> refs)
        {

            List<string> tokHyp = hyp.Split(' ').ToList();
            List<List<string>> tokRefs = tokenizeRefs(refs);

            int[] result = new int[BLEU.getSuffStatCount()];
            bleu.stats(tokHyp, tokRefs, result);
            return result;
        }

        private static List<List<string>> tokenizeRefs(List<string> refs)
        {
            List<List<string>> tokRefs = new List<List<string>>();
            foreach (string refe in refs)
            {
                tokRefs.Add(refe.Split(' ').ToList());
            }
            return tokRefs;
        }
      
        public double score(int[] suffStats)
        {
            return bleu.score(suffStats) * 100;
        }

        public double[] scoreSubmetrics(int[] suffStats)
        {
            int N = 4;
            double[] result = new double[N + 1];
            bleu.score(suffStats, result);
            return result;
        }

        public string[] getSubmetricNames()
        {
            return SUBMETRIC_NAMES;
        }

        public override string ToString()
        {
            return "BLEU";
        }
    }
}
