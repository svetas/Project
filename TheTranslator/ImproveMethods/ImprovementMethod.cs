using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheTranslator.ImproveMethods
{
    public abstract class ImprovementMethod
    {
        public abstract string ChooseBetter(string ourTrans, string otherTrans, out int selected);
    }
}
