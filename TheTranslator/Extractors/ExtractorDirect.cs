using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TheTranslator.DataManager;

namespace TheTranslator.Extractors
{
    class ExtractorDirect : Extractor
    {

        public override List<List<Sentence>> extractTransParts(string source)
        {
            List<List<Sentence>> ans = new List<List<Sentence>>();
            Regex rxSpace = new Regex(@"\s\s+");
            source = rxSpace.Replace(source, " ");
            string[] splitSource = source.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            HashEntry[] directResults =  m_sentences.GetSet(source);

            Sentence sen = new Sentence(source, -1);
            List<TargetSentence> tsList = new List<TargetSentence>();
            int totalCount = 0;
            foreach (var item in directResults)
            {
                TargetSentence ts = new TargetSentence(item.Name,(int)item.Value,1);
                totalCount += (int)item.Value;
                tsList.Add(ts);
            }
            sen.addTargetList(tsList,totalCount);
            List<Sentence> oneItemList = new List<Sentence>();
            oneItemList.Add(sen);
            ans.Add(oneItemList);
            return ans;
        }

        public override bool TranslationExists(string source)
        {
            Regex rxSpace = new Regex(@"\s\s+");
            source = rxSpace.Replace(source, " ");
            return DBManager.GetInstance().CheckGet("SourceSentences", source);
        }

        public override string[] ExtractExactTranslation(string source,int minCount, int maxRes)
        {
            Regex rxSpace = new Regex(@"\s\s+");
            source = rxSpace.Replace(source, " ");

            HashEntry[] directResults = DBManager.GetInstance().GetSet(source);

            string bestTranslation = "";
            double bestCounted = 0;
            int bestLength = int.MaxValue;
            
            Dictionary<string,double> bestCandidates = new Dictionary<string, double>();

            // Step 1: find best candidate

            int sumRepetitions = 0;

            foreach (var item in directResults)
            {
                string trans = item.Name.ToString().Substring(1);
                int timesRepeated = (int)item.Value;

                if (timesRepeated>=bestCounted && timesRepeated> minCount)
                {
                    bestTranslation = trans;
                    bestCounted = timesRepeated;

                }
                sumRepetitions += timesRepeated;

            }

            // Step 2: Get all the translations which are at almost the same length as the best candidate
            foreach (var item in directResults)
            {
                string trans = item.Name.ToString().Substring(1);
                int timesRepeated = (int)item.Value;
                bestCandidates.Add(trans,timesRepeated/(double)sumRepetitions);
            }

            var topN = (from entry in bestCandidates orderby entry.Value descending select entry.Key).Take(maxRes).ToArray();

            /*double topItemValue = 0;
            string topItemKey="";

            foreach (var item in topN)
            {
                if (topItemValue<item.Key.Length)
                {
                    topItemKey = item.Key;
                    topItemValue = item.Key.Length;
                }
            }*/
            return topN;

            /*
            double bestScore = 0;
            string bestCandidate = "";
            int isShortenVer = 0;
            foreach (var candidate in topN)
            {
                int popularity = 0;
                m_targetSentences.TryGetValue(candidate.Key, out popularity);
                if (candidate.Key.Contains("&apos;"))
                    isShortenVer = 1;

                double adjPop = popularity / (double)m_targetSentences.Count;
                //double candidateScore = candidate.Value;
                double candidateScore = (0.849 * candidate.Value +
                    0.001 * adjPop + 0.05 *isShortenVer);
                if (candidateScore> bestScore)
                {
                    bestScore = candidateScore;
                    bestCandidate = candidate.Key;
                }
            }
            return bestCandidate;
            */
            /*
            foreach (var item in directResults)
            {
                string normalLine = item.Name.ToString().Substring(1);
                int popularity = 0;
                m_targetSentences.TryGetValue(normalLine,out popularity);


                double normalizedLength = 0.8 * (int)item.Value + 0.2 * popularity;

                if (normalizedLength >= bestCounted)
                {
                    if (normalizedLength > bestCounted ||
                        normalizedLength == bestCounted && item.Name.ToString().Substring(1).Split(' ').Length < bestLength)
                    {
                        bestCounted = normalizedLength;
                        bestTranslation = item.Name.ToString().Substring(1);
                        bestLength = bestTranslation.Split(' ').Length;
                    }
                }
                /*
                if ((int)item.Value >= bestCounted)
                {
                    if ((int)item.Value > bestCounted ||
                        (int)item.Value == bestCounted && item.Name.ToString().Substring(1).Split(' ').Length < bestLength)
                    {
                        bestCounted = (int)item.Value;
                        bestTranslation = item.Name.ToString().Substring(1);
                        bestLength = bestTranslation.Split(' ').Length;
                    }
                }*/
            /*}
            if (bestCounted > minCount)
                return bestTranslation;
            else
                return "";*/
        }
    }
}
