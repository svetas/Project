using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransltrProject.ExtractingAlgorithms
{
    class ExectAlignmentExtractor : ExtractorMethod
    {
        public ExectAlignmentExtractor(Subtitle s1, Subtitle s2, int sycnThreshold) : base(s1, s2, sycnThreshold) { }

        public override TranSubtitle Extract()
        {
            TranSubtitle ts = new TranSubtitle();
            ts.Translations = new List<Tuple<string, string>>();

            Sentence[] src = s1.Sentences.ToArray();
            Sentence[] dst = s2.Sentences.ToArray();

            //double SyncRatio = countSync / ((double)s1.Sentences.Count);

            for (int i = 0; i < s1.Sentences.Count; i++)
            {
                string origin = "";
                string translation = "";
                //
                for (int j = 0; j < s2.Sentences.Count; j++)
                {
                    if (Math.Abs(src[i].Start - dst[j].Start) < SyncThreshold)
                    {
                        origin += src[i].Text;
                        translation += dst[j].Text;
                        Tuple<string, string> tup = new Tuple<string, string>(origin, translation);
                        ts.Translations.Add(tup);
                    }
                }
            }
            if (ts.Translations.Count == 0)
                ts.Translations.Add(new Tuple<string, string>("", ""));
            return ts;
        }
    }
}
