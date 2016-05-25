using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransltrProject.SyncAlgorithms
{
    public abstract class SyncMethod
    {
        public abstract AlignmentSetting GetBestDeviation(Subtitle s1, Subtitle s2, out double deviation);
        public List<Tuple<Sentence, int>> FindSentenceNextToGaps(Subtitle sub, int sentencesAmount)
        {
            Dictionary<Tuple<Sentence, int>, int> minHeap = new Dictionary<Tuple<Sentence, int>, int>();
            int i = 0;
            foreach (var item in sub.Sentences)
            {
                i++;
                if (i < sub.Sentences.Count * 0.1 || i > sub.Sentences.Count * 0.9)
                    continue;
                if (item.Text.Contains("♪"))
                    continue;
                /*if (item.Text.Split(' ').Count() < 7)
                    continue;*/
                minHeap.Add(new Tuple<Sentence, int>(item, i), item.TimeToNext);
            }

            while (minHeap.Count > sentencesAmount)
            {
                int[] sizes = minHeap.Values.ToArray();
                int minvalue = sizes.Min();
                var item = minHeap.First(kvp => kvp.Value == minvalue);
                minHeap.Remove(item.Key);
            }

            return minHeap.Keys.ToList();
        }
    }
}
