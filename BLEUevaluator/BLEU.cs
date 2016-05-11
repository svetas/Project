using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLEUevaluator
{
    public class BLEU
    {
        private static int N = 4;

        public int verbosity = 0;

        public BLEU() { }

        public BLEU(int verbosity)
        {
            this.verbosity = verbosity;
        }

        public static int pickReference(List<string> hyp, List<List<string>> refs, int verbosity)
        {
            int hypLen = hyp.Count;
            int selectedRefLen = int.MaxValue;
            int selectedRef = -1;
            // TODO: "Closest" or "least harsh"? 
            // TODO: Use "least harsh" to break ties betweeen references of equal closeness... 
            int curDist = int.MaxValue;
            int i = 0;
            foreach (List<string> refe in refs)
            {
                // for now, always use closest ref 
                int myDist = Math.Abs(hypLen - refe.Count);
                if (myDist < curDist)
                {
                    selectedRefLen = refe.Count;
                    selectedRef = i;
                    curDist = myDist;
                }
                else if (myDist == curDist)
                {
                    // break ties based on having a more optimistic brevity penalty (shorter reference) 
                    if (refe.Count < selectedRefLen) {
                        selectedRefLen = refe.Count;
                        selectedRef = i;
                        curDist = myDist;
                        if (verbosity >= 2)
                        {
                            Console.WriteLine("BLEU: Picking more optimistic reference for brevity penalty: hyp_len = " + hypLen + "; ref_len = " + refe.Count
                                + " distance =" + myDist);
                        }
                    }
                }
                i++;
            }
            return selectedRef;
        }

        public void stats(List<string> hyp, List<List<string>> refs, int[] result)
        {
            if (result.Length != 9) throw new Exception();
            if (!(refs.Count > 0)) throw new Exception();

            // 1) choose reference length 
            int selectedRef = pickReference(hyp, refs, verbosity);
            int selectedRefLen = refs[selectedRef].Count;

            // TODO: Integer-ify everything inside Ngram? Or is there too much 
            // overhead there? 

            // 2) determine the bag of n-grams we can score against 
            // build a simple tries 
            MultiSet<Ngram> clippedRefNgrams = new MultiSet<Ngram>();
            foreach (List<string> refe in refs)
            {
                MultiSet<Ngram> refNgrams = new MultiSet<Ngram>();
                for (int order = 1; order <= N; order++)
                {
                    for (int i = 0; i <= refe.Count - order; i++)
                    {
                        List<string> toks = refe.Skip(i).Take(order).ToList();
                        Ngram ngram = new Ngram(toks);
                        refNgrams.Add(ngram);
                    }
                }


                // clip n-grams by taking the maximum number of counts for any given reference 
                foreach (Ngram ngram in refNgrams)
                {
                    int clippedCount = Math.Max(refNgrams[ngram], clippedRefNgrams[ngram]);
                    clippedRefNgrams[ngram] = clippedCount;
                }
            }

            // 3) now match n-grams 
            int[] attempts = new int[N];
            int[] matches = new int[N];
            for (int order = 1; order <= N; order++)
            {
                for (int i = 0; i <= hyp.Count - order; i++)
                {
                    List<string> toks = hyp.Skip(i).Take(order).ToList();
                    Ngram ngram = new Ngram(toks);
                    bool found = clippedRefNgrams.Remove(ngram);
                    ++attempts[order - 1];
                    if (found)
                    {
                        ++matches[order - 1];
                    }
                }
            }

            // 4) assign sufficient stats 
            Array.Copy(attempts, 0, result, 0, N);
            Array.Copy(matches, 0, result, N, N);
            result[N * 2] = selectedRefLen;
        }

        private static double getAttemptedNgrams(int[] suffStats, int j)
        {
            return suffStats[j];
        }

        private static double getMatchingNgrams(int[] suffStats, int j)
        {
            return suffStats[j + N];
        }

        private static double getRefWords(int[] suffStats)
        {
            return suffStats[N * 2];
        }

        public double score(int[] suffStats)
        {
            return score(suffStats, null);
        }

        // ############################################################################################################################### 
        // # Default method used to compute the BLEU score, using smoothing. 
        // # Note that the method used can be overridden using the '--no-smoothing' 
        // command-line argument 
        // # The smoothing is computed by taking 1 / ( 2^k ), instead of 0, for each 
        // precision score whose matching n-gram count is null 
        // # k is 1 for the first 'n' value for which the n-gram match count is null 
        // # For example, if the text contains: 
        // # - one 2-gram match 
        // # - and (consequently) two 1-gram matches 
        // # the n-gram count for each individual precision score would be: 
        // # - n=1 => prec_count = 2 (two unigrams) 
        // # - n=2 => prec_count = 1 (one bigram) 
        // # - n=3 => prec_count = 1/2 (no trigram, taking 'smoothed' value of 1 / ( 
        // 2^k ), with k=1) 
        // # - n=4 => prec_count = 1/4 (no fourgram, taking 'smoothed' value of 1 / 
        // ( 2^k ), with k=2) 
        // ############################################################################################################################### 
        // segment-level bleu smoothing is done by default and is similar to that of 
        // bleu-1.04.pl (IBM) 
        // 
        // if allResults is non-null it must be of length N+1 and it 
        // will contain bleu1, bleu2, bleu3, bleu4, brevity penalty 
        public double score(int[] suffStats, double[] allResults)
        {
            if (!(suffStats.Length == N * 2 + 1)) {
                Console.WriteLine("BLEU sufficient stats must be of length N*2+1");
                throw new Exception();
            }
            double brevityPenalty;
            double refWords = getRefWords(suffStats);
            double hypWords = getAttemptedNgrams(suffStats, 0);
            if (hypWords < refWords)
            {
                brevityPenalty = Math.Exp(1.0 - refWords / hypWords);
            }
            else
            {
                brevityPenalty = 1.0;
            }
            if (brevityPenalty < 0.0     || brevityPenalty > 1) 
                throw new Exception();

            if (verbosity >= 1)
            {
                Console.WriteLine("BLEU: Brevity penalty = " + brevityPenalty + " (ref_words = " + refWords + ", hyp_words = " + hypWords + ")");
            }

            if (allResults != null)
            {
                if (!(allResults.Length == N + 1))
                    throw new Exception();
                allResults[N] = brevityPenalty;
            }

            double score = 0.0;
            double smooth = 1.0;

            for (int j = 0; j < N; j++)
            {
                double attemptedNgramsJ = getAttemptedNgrams(suffStats, j);
                double matchingNgramsJ = getMatchingNgrams(suffStats, j);
                double iscore;
                if (attemptedNgramsJ == 0)
                {
                    iscore = 0.0;
                    if (verbosity >= 1)
                        Console.WriteLine("jBLEU: "+ (j+1) +"-grams: raw 0/0 = 0 %%");
                }
                else if (matchingNgramsJ == 0)
                {
                    smooth *= 2;
                    double smoothedPrecision = 1.0 / (smooth * attemptedNgramsJ);
                    iscore = Math.Log(smoothedPrecision);
                    if (verbosity >= 1) Console.WriteLine("jBLEU: "+ j + 1 + "-grams: "+ matchingNgramsJ + "/"+ attemptedNgramsJ + " = "+ smoothedPrecision * 100 + " %% (smoothed) :: raw = "+ matchingNgramsJ / (double)attemptedNgramsJ * 100 + " %%");
                }
                else
                {
                    double precisionAtJ = matchingNgramsJ / attemptedNgramsJ;
                    iscore = Math.Log(precisionAtJ);
                    if (verbosity >= 1) Console.WriteLine("jBLEU: "+ j + 1 + "-grams: "+ matchingNgramsJ + "/"+ attemptedNgramsJ + " = " + precisionAtJ * 100 + " %% (unsmoothed)");
                }
                // TODO: Allow non-uniform weights instead of just the "baseline" 
                // 1/4 from Papenini 
                double ngramOrderWeight = 0.25;
                score += iscore * ngramOrderWeight;

                if (allResults != null)
                {
                    if (!(allResults.Length == N + 1))
                        throw new Exception();
                    allResults[j] = brevityPenalty * Math.Exp(score);
                }

                // assert Math.exp(iscore * ngramOrderWeight) <= 1.0 : 
                // String.format("ERROR for order %d-grams iscore: %f -> %f :: %s", 
                // j+1, iscore, Math.exp(iscore * ngramOrderWeight), 
                // Arrays.toString(suffStats)); 
                // assert Math.exp(score * ngramOrderWeight) <= 1.0; 
            }

            double totalScore = brevityPenalty * Math.Exp(score);

            if (totalScore > 1.0)
            {
                Console.WriteLine("BLEU: Thresholding out of range score: " + totalScore + "; stats: "
                    + string.Join(",", suffStats));
                totalScore = 1.0;
            }
            else if (totalScore < 0.0)
            {
                Console.WriteLine("BLEU: Thresholding out of range score: " + totalScore);
                totalScore = 0.0;
            }

            return totalScore;
        }

        public static int getSuffStatCount()
        {
            // attempted 1-4 gram counts 
            // matching 1-4 gram counts 
            // length of selected reference for brevity penalty 
            return N * 2 + 1;
        }
    }
}
