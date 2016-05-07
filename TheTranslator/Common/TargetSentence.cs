using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TheTranslator
{
    public class TargetSentence : IEquatable<TargetSentence>, IComparable<TargetSentence>
    {
        Regex rxPsik = new Regex(",+");

        // Text in english, tranlation
        public string m_translation { get; set; }
        // how many time repeated as translation
        public int m_count { get; set; }
        // probablity of the current translation option, how many times the source translated into 
        // this text
        public double m_pr { get; set; }

        public bool m_isReal;

        public int[] m_psikLocations;

        public TargetSentence(string translation, int count, double pr)
        {
            string cleanedTrans = rxPsik.Replace(translation, "");
            m_translation = cleanedTrans;
            m_count = count;
            m_pr = pr;
            m_isReal = true;

            MatchCollection mc = rxPsik.Matches(translation);
            m_psikLocations = new int[mc.Count];
            for (int i = 0; i < mc.Count; i++)
            {
                m_psikLocations[i] = mc[i].Index;
            }
        }
        /*public TargetSentence(string translation, int count, double pr, bool isReal)
        {
            m_isReal = isReal;
            m_translation = translation;
            m_count = count;
            m_pr = pr;
        }*/

        public override string ToString()
        {
            return m_translation + " _ " + m_count;
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            TargetSentence objAsPart = obj as TargetSentence;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }
        public int SortByNameAscending(string name1, string name2)
        {
            return name1.CompareTo(name2);
        }
        // Default comparer for Part type.
        public int CompareTo(TargetSentence comparePart)
        {
            // A null value means that this object is greater.
            if (comparePart == null)
                return 1;

            else
                return comparePart.m_count.CompareTo(this.m_count);
        }
        public override int GetHashCode()
        {
            return m_count;
        }
        public bool Equals(TargetSentence other)
        {
            if (other == null) return false;
            return (this.m_count.Equals(other.m_count));
        }
        // Should also override == and != operators.
        
    }
}
