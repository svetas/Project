using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheTranslator.Common
{
    public class WekaRecord
    {
        public int SourceLength { get; set; }
        public int MosesLength { get; set; }
        public int OurLength { get; set; }
        public int Repitions { get; set; }
        public int SourceOurLenDiff { get { return OurLength - SourceLength; } }
        public double OurMosesJaccard { get; set; }
        public int OurMosesLevinstain { get; set; }
        public char ContainsApos { get; set; }
        public char WeHaveLonger { get { return OurLength > MosesLength ? 't' : 'f'; } }
        public char Replace { get; set; }
    }
}
