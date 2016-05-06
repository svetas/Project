using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheTranslator
{
    public class TargetSentence : IEquatable<TargetSentence>, IComparable<TargetSentence>
    {
        // Text in english, tranlation
        public string m_translation { get; set; }
        // how many time repeated as translation
        public int m_count { get; set; }
        // probablity of the current translation option, how many times the source translated into 
        // this text
        public double m_pr { get; set; }

        public TargetSentence(string translation, int count, double pr)
        {
            m_translation = translation;
            m_count = count;
            m_pr = pr;
        }
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
