using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransltrProject.ExtractingAlgorithms
{
    abstract class ExtractorMethod
    {
        protected Subtitle s1 { get; set; }
        protected Subtitle s2 { get; set; }
        protected int SyncThreshold { get; set; }

        public ExtractorMethod(Subtitle s1, Subtitle s2, int syncThreshold)
        {
            this.s1 = s1;
            this.s2 = s2;
            this.SyncThreshold = syncThreshold;
        }

        public abstract TranSubtitle Extract();
    }
}
