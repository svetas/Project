using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheTranslator
{
    public abstract class Combiner
    {
        abstract public List<TranslationOption> combine(List<List<Sentence>> transOptions, string source);
    }
}
