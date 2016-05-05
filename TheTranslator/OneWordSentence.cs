using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheTranslator
{
   
    public class OneWordSentence:Sentence
    {
        // The Grade for unknown word (not translatable)
        public const double m_gradeForUnkown = 0.3;

        public OneWordSentence(string source, string target) : base(source, target, -1, m_gradeForUnkown)
        {
        }

    }
}
