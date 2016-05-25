using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransltrProject.ExtractingAlgorithms
{
    class FlowExtractor : ExtractorMethod
    {
        public FlowExtractor(Subtitle s1, Subtitle s2, int sycnThreshold) : base(s1, s2, sycnThreshold) { }

        public override TranSubtitle Extract()
        {
            List<Tuple<int, int>> syncedLines = GetSyncedLines();

            // Combine
            List<Tuple<List<int>, List<int>>> syncedGaps = new List<Tuple<List<int>, List<int>>>();
            //
            for (int i = 0; i < syncedLines.Count-1; i++)
            {
                try {
                    List<int> gapSub1 = new List<int>();
                    List<int> gapSub2 = new List<int>();
                    for (int j = syncedLines[i].Item1; j < syncedLines[i + 1].Item1; j++)
                        gapSub1.Add(j);
                    for (int j = syncedLines[i].Item2; j < syncedLines[i + 1].Item2; j++)
                        gapSub2.Add(j);
                    if (gapSub2.Count == 0 || gapSub1.Count == 0)
                    {
                        if (syncedGaps.Count!=0)
                        {

                            if (gapSub1.Count == 0)
                                syncedGaps.Last().Item2.AddRange(gapSub2);
                            else
                                syncedGaps.Last().Item1.AddRange(gapSub1);
                        }
                        else
                        {
                            syncedGaps.Add(new Tuple<List<int>, List<int>>(new List<int>(), new List<int>()));     
                            if (gapSub1.Count == 0)
                                syncedGaps.Last().Item2.AddRange(gapSub2);
                            else
                                syncedGaps.Last().Item1.AddRange(gapSub1);
                        }
                    }
                    else
                        syncedGaps.Add(new Tuple<List<int>, List<int>>(gapSub1, gapSub2));
                } catch (Exception e)
                {

                }
            }

            List<Tuple<List<string>, List<string>>> res = new List<Tuple<List<string>, List<string>>>();
            // Break lines


            foreach (var item in syncedGaps)
            {
                try {
                    List<string> srcText = new List<string>();
                    List<string> dstText = new List<string>();
                    foreach (int i in item.Item1)
                    {
                        srcText.AddRange(s1.GetBrokenSentence(i).ToList());
                    }
                    foreach (int i in item.Item2)
                    {
                        dstText.AddRange(s2.GetBrokenSentence(i).ToList());
                    }
                    // fix sentences cut

                    if (srcText.Count != dstText.Count)
                    {

                        if (Math.Abs(srcText.Count - dstText.Count) == 1)
                        {
                            List<string> longList = srcText.Count > dstText.Count ? srcText : dstText;
                            List<string> shortList = srcText.Count < dstText.Count ? srcText : dstText;

                            if (longList.Count < 1 || shortList.Count < 1)
                                continue;

                            int minValue = Math.Abs(longList[1].Length + longList[0].Length - shortList[0].Length);
                            int minIndex = 1;
                            for (int i = 1; i < longList.Count; i++)
                            {
                                int diff = Math.Abs(longList[i].Length + longList[i - 1].Length - shortList[i - 1].Length);
                                if (diff < minValue)
                                {
                                    minValue = diff;
                                    minIndex = i;
                                }
                            }
                            List<string> newLong = new List<string>();

                            for (int i = 0; i < longList.Count - 1; i++)
                            {
                                if (i == minIndex - 1)
                                {
                                    newLong.Add(longList[i] + " " + longList[i + 1]);
                                    i++;
                                }
                                else
                                    newLong.Add(longList[i]);
                            }
                            if (srcText.Count > dstText.Count)
                                res.Add(new Tuple<List<string>, List<string>>(newLong, shortList));
                            else
                                res.Add(new Tuple<List<string>, List<string>>(shortList, newLong));
                        }
                    }
                    else
                    {
                        res.Add(new Tuple<List<string>, List<string>>(srcText, dstText));
                    }
                } catch (Exception e)
                {

                }
   
            }

            TranSubtitle answer = new TranSubtitle();

            foreach (var item in res)
            {
                if (item.Item1.Count==item.Item2.Count)
                    for (int i = 0; i < item.Item1.Count; i++)
                    {
                        answer.Add(item.Item1[i], item.Item2[i]);
                    }
            }
            return answer;
        }



        private List<Tuple<int,int>> GetSyncedLines()
        {
            List<Tuple<int, int>> synced = new List<Tuple<int, int>>();

            for (int i = 0; i < s1.Sentences.Count; i++)
            {
                for (int j = 0; j < s2.Sentences.Count; j++)
                {
                    if (Math.Abs(s1.Sentences[i].Start - s2.Sentences[j].Start) < SyncThreshold)
                    {
                        synced.Add(new Tuple<int, int>(i, j));
                        break;
                    }
                }
            }
            return synced;
        }

    }
}
