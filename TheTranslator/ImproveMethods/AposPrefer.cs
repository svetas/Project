using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheTranslator.DataManager;

namespace TheTranslator.ImproveMethods
{
    class AposPrefer : ImprovementMethod
    {
        public override string ChooseBetter(string ourTrans, string otherTrans, string source, out int selected)
        {
            if (ourTrans == null || ourTrans.Length == 0)
            {
                selected = -1;
                return otherTrans;
            }
            else if (ourTrans.Split(' ').Length > 3 && (int)DBManager.GetInstance().GetSet(source, ourTrans) > 1)
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
