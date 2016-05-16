using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheTranslator.Extractors
{
    class SeperatingCombiner
    {
        private Extractor m_extractor;

        public SeperatingCombiner(Extractor m_extractor)
        {
            this.m_extractor = m_extractor;
        }

        public string SeperateTranslate(string source, int splits)
        {
            List<List<string>> res = GetPermuationsToGroups(source.Split(' '), splits,true,true);
            string result = "";
            if (res == null || res.Count==0)
                return result;

            for (int i = 0; i < res[0].Count; i++)
            {
                if (i == res[0].Count - 1)
                    result = result + res[0][i];
                else
                    result = result + res[0][i]+ " ";
            }
            return result;
        }
        public List<List<string>> GetPermuationsToGroups(string[] chunks, int groups, bool checkExists,bool translate)
        {
            // calculate all possible options for sizes (for string of size 4: 1-1-2, 1-2-1, 2-1-1
            List<List<string>> PossibleChunks = new List<List<string>>();
            List<int[]> sizes = new List<int[]>();
            int[] arr = new int[groups];
            func(chunks.Length, groups, 0, ref arr, ref sizes);

            for (int i = 0; i < sizes.Count; i++)
            {
                int currentIndex = 0;
                List<string> currentConnectedChunks = new List<string>();
                bool dropAll = false;
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
                    if (!m_extractor.TranslationExists(chunkStr) && checkExists)
                    {
                        dropAll = true;
                        break;
                    }
                    else
                    {

                        string translation;
                        if (translate)
                            translation = m_extractor.ExtractExactTranslation(chunkStr);
                        else
                            translation = chunkStr;
                        currentConnectedChunks.Add(translation);
                    }
                }

                if (!dropAll)
                    PossibleChunks.Add(currentConnectedChunks);
            }
            if (PossibleChunks.Count == 0)
            {
                return null;
            }
            else
            {
                return PossibleChunks;
            }
        }

        private static void func(int ArrSize, int ElemCount, int CurrInd, ref int[] arr, ref List<int[]> ans)
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
                    func(ArrSize - i, ElemCount - 1, CurrInd + 1, ref arr, ref ans);
                }
            }
        }
    }
}
