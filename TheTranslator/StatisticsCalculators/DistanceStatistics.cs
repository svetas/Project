using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheTranslator.DataManager;

namespace TheTranslator
{
    public class DistanceStatistics : Statistics
    {
        //
        private const double DEFAULT_RANK = 0.01;
        //
        DBManager dmbPairCount;
        DBManager dmbTripleCount;
        DBManager dmbSingleCount;

        Dictionary<string, Dictionary<string, int>[]> m_Statistics = new Dictionary<string, Dictionary<string, int>[]>();
        Dictionary<string, int> m_TargetGlobalCount = new Dictionary<string, int>();

        public DistanceStatistics()
        {
            dmbPairCount = new DBManager();//@"Z:\tmp", "Pairs");
            dmbTripleCount = new DBManager();//@"Z:\tmp", "Triple");
            dmbSingleCount = new DBManager();//@"Z:\tmp", "Single");
        }

        //      
        //
        public double GetRank(string original, string target, int range) 
        {
            double answer = DEFAULT_RANK;
            if (m_Statistics.ContainsKey(original))
            {
                if (m_Statistics[original].Length>range)
                {
                    var y = m_Statistics[original];
                    var x = m_Statistics[original][range];
                    //var z = m_Statistics[original][range][target];
                    if (m_Statistics[original][range].ContainsKey(target))
                    {
                        int totalCount = 0;
                        foreach (var item in m_Statistics[original][range])
                        {
                            totalCount += item.Value;
                        }
                        int mone = m_Statistics[original][range][target];
                        answer = GetPopularity(target)* mone/(double)totalCount;

                    }
                }
            }
            double rangePanelty =  answer*Math.Pow(0.8, range+1);
            return rangePanelty;
        }

        internal double GetPopularity(string m_translation)
        {
            if (!m_TargetGlobalCount.ContainsKey(m_translation))
                return 1/(double)m_TargetGlobalCount.Count;
            return m_TargetGlobalCount[m_translation] / (double)m_TargetGlobalCount.Count;
        }

        private void GetPermutations(int ArrSize, int ElemCount, int CurrInd, ref int[] arr, ref List<int[]> ans)
        {
            if (ArrSize == ElemCount)
            {
                for (int i = 0; i < ElemCount; i++)
                {
                    arr[CurrInd + i] = 1;
                }

                int[] target = new int[arr.Length];
                Array.Copy(arr, target, arr.Length);
                ans.Add(target);
            }
            else if (ElemCount == 1)
            {
                arr[CurrInd] = ArrSize;
                int[] target = new int[arr.Length];
                Array.Copy(arr, target, arr.Length);
                ans.Add(target);
            }
            else
            {
                for (int i = ArrSize - ElemCount + 1; i > 0; i--)
                {
                    arr[CurrInd] = i;
                    GetPermutations(ArrSize - i, ElemCount - 1, CurrInd + 1, ref arr, ref ans);
                }
            }
        }

        private void AddArrayOfStrings(string[] linePartsTa)
        {
            if (linePartsTa.Length == 0)
                return;

            // count 
            string currentWord;
            string otherWord;
            for (int i = 0; i < linePartsTa.Length; i++)
            {
                currentWord = linePartsTa[i];
                if (!m_Statistics.ContainsKey(currentWord))
                {
                    Dictionary<string, int>[] followings = new Dictionary<string, int>[2];
                    m_Statistics.Add(currentWord, followings);
                    m_Statistics[currentWord][0] = new Dictionary<string, int>();
                    m_Statistics[currentWord][1] = new Dictionary<string, int>();
                }
                if (i + 1 < linePartsTa.Length)
                {
                    otherWord = linePartsTa[i + 1];

                    if (!m_Statistics[currentWord][0].ContainsKey(otherWord))
                    {
                        m_Statistics[currentWord][0].Add(otherWord, 1);
                    }
                    else
                    {
                        m_Statistics[currentWord][0][otherWord]++;
                    }
                }
                if (i + 2 < linePartsTa.Length)
                {
                    otherWord = linePartsTa[i + 2];
                    if (!m_Statistics[currentWord][1].ContainsKey(otherWord))
                    {
                        m_Statistics[currentWord][1].Add(otherWord, 1);
                    }
                    else
                    {
                        m_Statistics[currentWord][1][otherWord]++;
                    }
                }
            }
        }
        public override void Insert(string target)
        {
            string[] chunks = target.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);



            if ((chunks.Length>=1) && (chunks.Length<=3))
            {
                dmbSingleCount.WriteData(target);
            } else if (chunks.Length>3 && chunks.Length<=5)
            {
                dmbPairCount.WriteData(target);
            } else
            {
                dmbTripleCount.WriteData(target);
            }

            /*
            List<int[]> sizes = new List<int[]>();
            int groupsNum = Math.Min(3,chunks.Length);
            int[] arr = new int[groupsNum];
            GetPermutations(chunks.Length, groupsNum, 0, ref arr, ref sizes);
            List<string> chunksArr;

            for (int i = 0; i < sizes.Count; i++)
            {
                int currentIndex = 0;
                chunksArr = new List<string>();
                for (int j = 0; j < sizes[i].Length; j++)
                {
                    
                    string chunkStr = "";
                    for (int n = 0; n < sizes[i][j]; n++)
                    {
                        if (n != sizes[i][j] - 1)
                            chunkStr += chunks[currentIndex + n] + " ";
                        else
                            chunkStr += chunks[currentIndex + n];
                    }
                    currentIndex += sizes[i][j];
                    // add chunkStr to list of chunks
                    chunksArr.Add(chunkStr);

                    if (!m_TargetGlobalCount.ContainsKey(chunkStr))
                        m_TargetGlobalCount.Add(chunkStr, 0);
                    m_TargetGlobalCount[chunkStr]++;
                }
                // set all chunks into memory
                string[] parts = chunksArr.ToArray();
                AddArrayOfStrings(parts);
            }*/
        }
    }
}
