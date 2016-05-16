using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TheTranslator
{
    public class Sentence
    {


        private const int N_FOR_TOPN = 6;
        private const int MINIMUM_TOPN_OPTION_COUNT = 2;
        public const double m_gradeForUnkown = 0.3;


        Regex rxPsik = new Regex(",+");


        // Source, Hebrew sentence (contains 1-n words)
        public string m_source;

        // Possible translations of the source
        public List<TargetSentence> m_target;

        // Delete    
        public int m_id;

        // How many times the sentence appeared in the training set
        public int m_countInDB;

        //
        public int[] m_psikLocations;

        /*public Sentence(string source, string target, int id, double pr)
        {
            m_source = source;
            m_target = new List<TargetSentence>();
            m_target.Add(new TargetSentence(target,1, pr));
            m_id = id;
            m_countInDB = 1;
            
        }*/

        public Sentence(string source, int id)
        {
            string cleanedSourceFromPsik = rxPsik.Replace(source, "");
            m_target = new List<TargetSentence>();
            m_source = cleanedSourceFromPsik;
            m_id = id;
            m_countInDB = 0;

            MatchCollection mc = rxPsik.Matches(source);
            m_psikLocations = new int[mc.Count];
            for (int i = 0; i < mc.Count; i++)
            {
                m_psikLocations[i] = mc[i].Index;
            }
        }

        public List<TargetSentence> getTopN()
        {
            // assamption, there is at least one translation.

            List<TargetSentence> top = new List<TargetSentence>();

            foreach (var item in m_target) // items sorted by count  
            {       
                if (top.Count > N_FOR_TOPN)
                    return top;

                if (item.m_count < MINIMUM_TOPN_OPTION_COUNT)
                    continue;

                top.Add(item);
            }

            // if no perfect translation were found, 
            // enter translations without the minimum count check, it might help.
            if (top.Count==0)
            {
                foreach (var item in m_target)
                {
                    if (top.Count > N_FOR_TOPN)
                        return top;
                    top.Add(item);
                }
            }
            return top;
        }
        public void addTarget(string target,int count)
        {
            m_countInDB += count;
            for (int i = 0; i < m_target.Count; i++)
            {
                TargetSentence sen = m_target[i];
                if (sen.m_translation == target)
                {
                    //int count = sen.m_count + 1;
                    sen.m_count+= count;
                    sen.m_pr = sen.m_count / (double)m_countInDB;
                    return;
                }
            }
            m_target.Add(new TargetSentence(target, count, (count / (double)m_countInDB)));
        }

        public void addTarget(string target)
        {
            addTarget(target, 1);
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
