using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheTranslator
{
    public class Sentence
    {

        private const int N_FOR_TOPN = 4;
        private const int MINIMUM_TOPN_OPTION_COUNT = 2;
        public const double m_gradeForUnkown = 0.3;

        // Source, Hebrew sentence (contains 1-n words)
        public string m_source;

        // Possible translations of the source
        public List<TargetSentence> m_target;

        // Delete    
        public int m_id;

        // How many times the sentence appeared in the training set
        public int m_countInDB;


        public Sentence(string source, string target, int id, double pr)
        {
            m_source = source;
            m_target = new List<TargetSentence>();
            m_target.Add(new TargetSentence(target,1, pr));
            m_id = id;
            m_countInDB = 1;
            
        }

        public Sentence(string source, int id)
        {
            m_target = new List<TargetSentence>();
            m_source = source;
            m_id = id;
            m_countInDB = 0;
        }

        public List<TargetSentence> getTopN()
        {
            int n = N_FOR_TOPN;
            List<TargetSentence> top = new List<TargetSentence>();
            if(m_target.Count==1)
            {
                top.Add(m_target[0]);
                return top;
            }
            foreach (var item in m_target) // items sorted by count  
            {                
                if (top.Count == 0 || (n > 0 && item.m_count > MINIMUM_TOPN_OPTION_COUNT))
                    top.Add(item);
                else
                    return top;
                n--;
            }
            return top;
        }

        public void addTarget(string target)
        {
            m_countInDB++;
            for (int i = 0; i < m_target.Count; i++)
            {
                TargetSentence sen = m_target[i];
                if (sen.m_translation.Equals(target))
                {
                    int count = sen.m_count + 1;
                    sen.m_count++;
                    sen.m_pr = sen.m_count / (double)m_countInDB;
                    return;
                }   
            }
            m_target.Add(new TargetSentence(target, 1, (1/(double)m_countInDB)));
        }

        public void addTargetList(List<TargetSentence> target, int count)
        {
            m_target = target;
            m_countInDB = count;
        }



        public void sortAmdUpdatePr()
        {
            int countS = m_countInDB;
            int countT = 0;
            m_target.Sort();
            foreach (var t in m_target)
            {
                countT = t.m_count;
                t.m_pr = countT / (double)countS;
            }
        }
    }
}
