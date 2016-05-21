using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TheTranslator.Extractors
{
    class ExtractorDirect : Extractor
    {


        public ExtractorDirect(string path) : base(path) { }

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
            return m_sentences.CheckGet("SourceSentences", source);
        }

        public override string ExtractExactTranslation(string source,int minCount)
        {
            Regex rxSpace = new Regex(@"\s\s+");
            source = rxSpace.Replace(source, " ");

            HashEntry[] directResults = m_sentences.GetSet(source);

            string bestTranslation = "";
            double bestCounted = 0;
            int bestLength = int.MaxValue;
            
            Dictionary<string,int> bestCandidates = new Dictionary<string, int>();

            // Step 1: find best candidate
            foreach (var item in directResults)
            {
                string trans = item.Name.ToString().Substring(1);
                int timesRepeated = (int)item.Value;

                if (timesRepeated>=bestCounted && timesRepeated> minCount)
                {
                    bestTranslation = trans;
                    bestCounted = timesRepeated;
                }
            }

            // Step 2: Get all the translations which are at almost the same length as the best candidate
            foreach (var item in directResults)
            {
                string trans = item.Name.ToString().Substring(1);
                int timesRepeated = (int)item.Value;

                if (Math.Abs(trans.Split(' ').Length - bestTranslation.Split(' ').Length) < 2)
                {
                    bestCandidates.Add(trans,timesRepeated);
                }
            }

            double bestScore = 0;
            string bestCandidate = "";

            foreach (var candidate in bestCandidates)
            {
                int popularity = 0;
                m_targetSentences.TryGetValue(candidate.Key, out popularity);

                //double candidateScore = candidate.Value;
                double candidateScore = 0.999 * candidate.Value + 0.001 * popularity;
                if (candidateScore> bestScore)
                {
                    bestScore = candidateScore;
                    bestCandidate = candidate.Key;
                }
            }
            return bestCandidate;

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
