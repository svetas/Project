using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheTranslator.ImproveMethods
{
    class SureAndLong : ImprovementMethod
    {

        // This returns 31.66 , moses is 31.51

        public override string ChooseBetter(string ourTrans, string otherTrans, out int selected)
        {
            if (ourTrans == null || ourTrans.Length == 0)
            {
                selected = -1;
                return otherTrans;
            }
            else if (ourTrans.Split(' ').Length > 3)
            {
                selected = 1;
                return ourTrans;
            }
            else
            {
                selected = -1;
                return otherTrans;
            }
        }
    }
}
