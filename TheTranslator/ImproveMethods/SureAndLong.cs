using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheTranslator.Common;
using TheTranslator.DataManager;

namespace TheTranslator.ImproveMethods
{
    class SureAndLong : ImprovementMethod
    {

        // This returns 31.66 , moses is 31.51

        public override string ChooseBetter(string ourTrans, string otherTrans, string source, out int selected)
        {
            bool takeOur = false;
            if (ourTrans == null || ourTrans.Length == 0)
            {
                takeOur = false;
            }
            else if (ourTrans.Split(' ').Length > 3 && (int)DBManager.GetInstance().GetSet(source,ourTrans)>1)// && ourTrans!=otherTrans)
            {
                takeOur = true;
            } 
            else if (ourTrans.Split(' ').Length > 5)
            {
                takeOur = true;
            }
            else if (ourTrans == otherTrans)
                takeOur = true;

            if (takeOur)
            {
                selected = 1;
                IncrementCounter("Our");
                return ourTrans;
            }
            else
            {
                selected = -1;
                IncrementCounter("Picked moses");
                return otherTrans;
            }
        }


    }
}
