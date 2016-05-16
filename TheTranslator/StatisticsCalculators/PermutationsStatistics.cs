using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheTranslator.DataManager;

namespace TheTranslator
{
    class PermutationsStatistics : Statistics
    {
        //number of words in english in the the db files 
        public int m_countWordsInDb;
        //the count of all words in DB
        protected Dictionary<string, int> m_singleWordsCount;
        //the count of all word pairs in DB
        protected Dictionary<string, int> m_pairWordsCount;
        //the cont of all word triple in db
        protected Dictionary<string, int> m_trippelWordsCount;

        public PermutationsStatistics()
        {
            m_pairWordsCount = new Dictionary<string, int>();
            m_trippelWordsCount = new Dictionary<string, int>();
            m_singleWordsCount = new Dictionary<string, int>();
            m_countWordsInDb = 0;
        }

        public override void Insert(string target)
        {

            string[] linePartsTa = target.Split(new string[] { " ", "," }, StringSplitOptions.RemoveEmptyEntries);

            if (linePartsTa.Length == 0)
                return;

            m_countWordsInDb += linePartsTa.Length;
            string prevWord = linePartsTa[0];
            string prevPrevWord = linePartsTa[0];
            for (int i = 0; i < linePartsTa.Length; i++)
            {
                string currWord = linePartsTa[i];
                if (!m_singleWordsCount.ContainsKey(currWord))
                    m_singleWordsCount.Add(currWord, 1);
                else m_singleWordsCount[currWord] += 1;

                if (i > 0)
                {
                    string pair = prevWord + " " + currWord;
                    if (!m_pairWordsCount.ContainsKey(pair))
                        m_pairWordsCount.Add(pair, 1);
                    else m_pairWordsCount[pair] += 1;
                }
                if (i > 1)
                {
                    string pair = prevPrevWord + " " + prevWord + " " + currWord;
                    if (!m_trippelWordsCount.ContainsKey(pair))
                        m_trippelWordsCount.Add(pair, 1);
                    else m_trippelWordsCount[pair] += 1;
                    prevPrevWord = prevWord;
                }

                prevWord = currWord;
            }
        }
        public int getCountTaPair(string w1, string w2)
        {
            int ans = 1;
            if (m_pairWordsCount.TryGetValue(w1 + " " + w2, out ans))
                return ans;
            return 1;
        }
        public int getCountTaWord(string w)
        {
            int ans = 1;
            if (m_singleWordsCount.TryGetValue(w, out ans))
                return ans;
            return 1;

        }
    }
}
