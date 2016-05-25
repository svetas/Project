using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheTranslator.DataManager;

namespace TheTranslator.ImproveMethods
{
    class LongCombiner : ImprovementMethod
    {

        private Extractor m_extractor;

        public LongCombiner(Extractor extractor)
        {
            m_extractor = extractor;
        }

        public override string ChooseBetter(string ourTrans, string otherTrans, string source, out int selected)
        {
            selected = 0;
            if (ourTrans == null || ourTrans.Length == 0)
            {
                // try to split the source and translate it apart
                selected = -1;
                return otherTrans;
            }
            if (ourTrans.Split(' ').Length > 3 && (int)DBManager.GetInstance().GetSet(source, ourTrans) <2)
            {
                string[] Parts = source.Split(' ');

                List<Tuple<string, string>> CombinedParts = m_extractor.CombineStringParts(Parts);

                for (int i = 0; i < CombinedParts.Count; i++)
                {
                    // check if both parts exists
                    HashEntry[] entriesFirst = DBManager.GetInstance().GetSet(CombinedParts[i].Item1);
                    HashEntry[] entriesSecond = DBManager.GetInstance().GetSet(CombinedParts[i].Item2);

                    foreach (var item1 in entriesFirst)
                    {
                        string first = item1.Name.ToString().Substring(1);
                        foreach (var item2 in entriesSecond)
                        {
                            string second = item2.Name.ToString().Substring(1);

                            if ((ourTrans == first + " " + second) || (ourTrans == second + " " + first))
                            {
                                selected = 1;
                                return ourTrans;
                            }
                        }
                    }
                }
            }
            if (ourTrans.Split(' ').Length > 3 && (int)DBManager.GetInstance().GetSet(source, ourTrans) > 1)
            {
                selected = 1;
                return ourTrans;
            }

            selected = -1;
            return otherTrans;
        }
    }
}
