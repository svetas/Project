using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheTranslator
{
    public class TranslationOption
    {
        public List<TargetSentence> m_targetSenParts;
        public double m_prPi;
        public int m_chunkCount;
        public string m_translation;
        public string m_source;

        public TranslationOption(string source,string trans, List<TargetSentence> targetSenParts)
        {
            m_targetSenParts = targetSenParts;
            m_translation = trans;
            m_prPi = 1;
            foreach (var item in targetSenParts)
            {
                m_prPi = m_prPi * item.m_pr;
            }
            m_chunkCount = targetSenParts.Count;
            m_source = source;

        }


        public TranslationOption(TranslationOption other)
        {
            m_targetSenParts = new List<TargetSentence>();
            m_translation = other.m_translation; ;
            m_prPi = other.m_prPi;
            foreach (var item in other.m_targetSenParts)
            {
                m_targetSenParts.Add(item);
            }
            m_chunkCount = other.m_chunkCount;
            m_source = other.m_source;

        }

        public TranslationOption(string source)
        {
            m_targetSenParts = new List<TargetSentence>(); ;
            m_translation = "";
            m_prPi = 1;
            m_source = source;
            m_chunkCount = 0;

        }

        public void addPart(TargetSentence part)
        {
            m_targetSenParts.Add(part);
            m_prPi = m_prPi * part.m_pr;
            m_chunkCount++;
        }
        public string concatParts()
        {
            string ans = "";
            foreach (var item in m_targetSenParts)
            {
                ans = ans + " " + item.m_translation;
            }
            return ans;
        }



    }
}
