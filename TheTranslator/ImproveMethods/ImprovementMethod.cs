using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheTranslator.ImproveMethods
{
    public abstract class ImprovementMethod
    {
        private Dictionary<string, int> m_ImprovementTypesCount = new Dictionary<string, int>();

        public abstract string ChooseBetter(string ourTrans, string otherTrans, string source, out int selected);

        public override string ToString()
        {
            if (m_ImprovementTypesCount.Count>0)
            {
                string output = "";
                foreach (var item in m_ImprovementTypesCount)
                {
                    output += item.Key + "-" + item.Value+"\r\n";
                }
                return output;
            }
            return base.ToString();
        }

        protected void IncrementCounter(string v)
        {
            if (!m_ImprovementTypesCount.ContainsKey(v))
                m_ImprovementTypesCount.Add(v,0);
            m_ImprovementTypesCount[v]++;
        }
    }
}
