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

        public override string ExtractExactTranslation(string source)
        {
            Regex rxSpace = new Regex(@"\s\s+");
            source = rxSpace.Replace(source, " ");

            HashEntry[] directResults = m_sentences.GetSet(source);

            string bestTranslation = "";
            int bestCounted = 0;

            foreach (var item in directResults)
            {
                if ((int)item.Value > bestCounted)
                {
                    bestCounted = (int)item.Value;
                    bestTranslation = item.Name.ToString().Substring(1);
                }
            }
            
            return bestTranslation;
        }
    }
}
