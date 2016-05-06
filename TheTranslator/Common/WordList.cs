using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheTranslator
{
    public class WordList: Word
    {
        //sorted list of the sentence numbers in which the word appears

        // 
        List<Sentence> m_wordAppearance;

        public WordList(string word) : base(word)
        {
            m_wordAppearance = new List<Sentence>();
        }

        public WordList(string word, List<TargetSentence> trans) : base(word,trans)
        {
            m_wordAppearance = new List<Sentence>();
        }

        public override void addSentence(Sentence sen)
        {
            m_countInDB++;
            if (m_wordAppearance.Count== 0 || m_wordAppearance.Last().m_id!=sen.m_id)
                m_wordAppearance.Add(sen);
            

            //m_wordAppearance is sorted after all the training set is read

        }

        // Compare with other word, return the sentences in which they both appeared in
        public override  List<Sentence> getItemsInCommon(WordList other)
        {
            //init
            List<Sentence> inCommonList = new List<Sentence>();
            if (m_countInDB == 0 || other.m_countInDB == 0)
                return inCommonList;
            List<Sentence>.Enumerator thisListE = m_wordAppearance.GetEnumerator();
            thisListE.MoveNext();
            List<Sentence>.Enumerator otherListE = other.m_wordAppearance.GetEnumerator();
            otherListE.MoveNext();
            int thisCurr;
            int otherCurr;

            while (true)
            {
                thisCurr = thisListE.Current.m_id;
                otherCurr = otherListE.Current.m_id;
                if (thisCurr == otherCurr)
                {
                    inCommonList.Add(thisListE.Current);
                    if (!thisListE.MoveNext() || !otherListE.MoveNext())
                        return inCommonList;
                    continue;
                }
                if (thisCurr > otherCurr)
                {
                    if (!otherListE.MoveNext())
                        return inCommonList;
                    continue;
                }
                if (thisCurr < otherCurr)
                {
                    if (!thisListE.MoveNext())
                        return inCommonList;
                    continue;
                }
            }
        }
    }
}