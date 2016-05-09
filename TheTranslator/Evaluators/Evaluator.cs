using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheTranslator
{
    public abstract class Evaluator
    {

        // test
        protected Statistics m_stats;

        protected Evaluator(Statistics stats)
        {
            m_stats = stats;
        }
        abstract public string GetBestTranslation(List<TranslationOption> transO);
     
    }
}
