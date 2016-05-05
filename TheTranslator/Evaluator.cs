using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheTranslator
{
    public abstract class Evaluator
    {
        protected Extractor m_extct;

        protected Evaluator(Extractor eval)
        {
            m_extct = eval;
        }
        abstract public string GetBestTranslation(List<TranslationOption> transO);
     
    }
}
