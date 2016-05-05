using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheTranslator
{
    public abstract class Word
    {
        // one word in Hebrew 
        public string m_word;

        // The selected translation  - the chosen one         
        public TargetSentence m_translation;

        // how many times the word appeared in the training set
        protected int m_countInDB;    
        public int countInDB { get { return m_countInDB; } }

        protected Word(string word)
        {
            m_word = word;
            m_countInDB = 0;
        }

        protected Word(string word, TargetSentence trans)
        {
            m_word = word;
            m_translation = trans;
            m_countInDB = 0;
        }

        public void addTranslaion(TargetSentence trans)
        {
            m_translation = trans;
        }
        abstract public void addSentence(Sentence sen);
        abstract public List<Sentence> getItemsInCommon(WordList other);
        
    }
}
