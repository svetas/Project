using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TheTranslator
{
    class ExtractorNaive: Extractor
    {
        // if less then this number is returned ,
        //then try to split into 3 parts or more
        private const int MINIMUM_LIMIT_FOR_DOUBLE_SPLIT = 3;
        private const int MINIMUM_LIMIT_FOR_EVERY_WORD_SPLIT = 3;
        private const int m_MAX_SPLIT_NUM_ALLOWED = 12;

        public ExtractorNaive(string path) : base(path) { }
        public override string[] ExtractExactTranslation(string source, int minCount, int maxRes)
        {
            return "";
        }
        // 
        public override List<List<Sentence>> extractTransParts(string source)
        {
            List<List<Sentence>> ans= new List<List<Sentence>>();
            Regex rxRemovePsik = new Regex(",+");
            source = rxRemovePsik.Replace(source, " ");
            Regex rxSpace = new Regex(@"\s\s+");
            source = rxSpace.Replace(source, " ");
            string[] splitSource = source.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            //translate a sentence which contains one word 
            if (splitSource.Length == 1)
            {
                ans = new List<List<Sentence>>();
                List<Sentence> transOption = new List<Sentence>();
                Sentence wordTrans = getWordTranslation(splitSource[0]);
                transOption.Add(wordTrans);
                ans.Add(transOption);
                return ans;

            }

            //the sentence is in the db or in 2 chunks
            ans = fullTransOrTwoChunks(source, splitSource);
            if (ans.Count > MINIMUM_LIMIT_FOR_DOUBLE_SPLIT || splitSource.Length<3)
                return ans;

            //the sentence is in the db in two parts
            ans.AddRange(chunksTrans(source, splitSource));
            if (ans.Count > 0)
                return ans;

            return ans;
        }
        public override bool TranslationExists(string source)
        {
            return true;
        }
        // Get the translation possibilies of the source
        private List<List<Sentence>> fullTransOrTwoChunks(string source, string[] splitSource)
        {
            List<List<Sentence>> ans = new List<List<Sentence>>();
            List<Sentence> transOption = new List<Sentence>();
            Sentence senPart;
            if (m_allSen.TryGetValue(source, out senPart))
            {
                transOption.Add(senPart);
                ans.Add(transOption);
                transOption = new List<Sentence>();
            }
            int countWords = splitSource.Length;
            string firstChunk = "";
            string secondChunk = source;
            Sentence firstHalf;
            Sentence secondHalf;

            for (int loc = 0; loc < splitSource.Length - 1; loc++)
            {
                if (loc == 0)
                    firstChunk = splitSource[loc];
                else
                    firstChunk = firstChunk + " " + splitSource[loc];
                secondChunk = secondChunk.Substring(splitSource[loc].Length + 1);
                if (!chunkTrans(firstChunk, loc + 1, out firstHalf) || !chunkTrans(secondChunk, splitSource.Length - 1 - loc, out secondHalf))
                    continue;
                transOption.Add(firstHalf);
                transOption.Add(secondHalf);
                ans.Add(transOption);
                transOption = new List<Sentence>();
            }
            return ans;

        }

        private bool chunkTrans(string source, int count, out Sentence target)
        {
            if (count == 1)
            {
                target = getWordTranslation(source);
                return true;
            }
            return m_allSen.TryGetValue(source, out target);
        }

        private List<List<Sentence>> chunksTrans(string source, string[] splitSource)
        {
            List<List<Sentence>> ans = new List<List<Sentence>>();
            //j-> number of parts
            for (int j = 3; j <= Math.Min(splitSource.Length, m_MAX_SPLIT_NUM_ALLOWED); j++)
            {
                List<Tuple<string, int, int>> senChunks = initStringParts(splitSource, j);
                do
                {
                    List<Sentence> chunksTranslation = null;
                    if (translateParts(senChunks, out chunksTranslation))
                        ans.Add(chunksTranslation);

                }
                while (advancePart(senChunks, splitSource));
               
            }
            
            if (splitSource.Length < m_MAX_SPLIT_NUM_ALLOWED)
                return ans;

            //do we need to add the option to split by every word
            if(ans.Count< MINIMUM_LIMIT_FOR_EVERY_WORD_SPLIT)
                return ans;

            List<Tuple<string, int, int>> senWords = initStringParts(splitSource, splitSource.Length);
            List<Sentence> wordTranslation = null;
            if (translateParts(senWords, out wordTranslation))
                ans.Add(wordTranslation);
            return ans;

        }

        //Tuple<string, int, int>= source part, start loc, number of words
        private bool translateParts(List<Tuple<string, int, int>> senChunks, out List<Sentence> chunksTrans)
        {
            chunksTrans = new List<Sentence>();
            Sentence part;
            foreach (var item in senChunks)
            {
                if (!chunkTrans(item.Item1, item.Item3, out part))
                    return false;
                chunksTrans.Add(part);               
            }
            return true;
        }

        private bool advancePart(List<Tuple<string, int, int>> senChunks, string[] splitSource)
        {
            int locOfLast = senChunks.Count - 1;
            for (int currPartNum = locOfLast; currPartNum > 0; currPartNum--)
            {
                if (senChunks[currPartNum].Item3 > 1)
                {
                    //addvance curr
                    int countFlowingParts = senChunks.Count - currPartNum - 1;
                    int currCStringBeginigLoc = senChunks[currPartNum].Item2;
                    string currChunkString = senChunks[currPartNum].Item1;
                    int currItemcount = 0;
                    if (locOfLast == currPartNum)
                    {
                        currChunkString = currChunkString.Substring(splitSource[currCStringBeginigLoc].Length + 1);
                        currItemcount = senChunks[currPartNum].Item3 - 1;
                    }
                    else
                    {
                        currChunkString = splitSource[currCStringBeginigLoc + 1];
                        currItemcount = 1;
                    }
                    senChunks[currPartNum] = new Tuple<string, int, int>(currChunkString, currCStringBeginigLoc + 1, currItemcount);

                    //update prev
                    int bCurrStringBeginingLoc = senChunks[currPartNum - 1].Item2;
                    string bCurrString = senChunks[currPartNum - 1].Item1 + " " + splitSource[currCStringBeginigLoc];
                    senChunks[currPartNum - 1] = new Tuple<string, int, int>(bCurrString, bCurrStringBeginingLoc, senChunks[currPartNum - 1].Item3 + 1);


                    if (locOfLast == currPartNum)
                        return true;
                    //updating following
                    int followCStringBeginigLoc = currCStringBeginigLoc + 1;
                    currPartNum++;
                    string followChunkString;
                    while (currPartNum < locOfLast)
                    {
                        followCStringBeginigLoc++;
                        followChunkString = splitSource[followCStringBeginigLoc];
                        senChunks[currPartNum++] = new Tuple<string, int, int>(followChunkString, followCStringBeginigLoc, 1);
                    }
                    //last follower takes the rest of the text
                    int countItem = 0;
                    followCStringBeginigLoc++;
                    followChunkString = splitSource[followCStringBeginigLoc];
                    countItem++;
                    followCStringBeginigLoc++;
                    for (int i = followCStringBeginigLoc; i < splitSource.Length; i++)
                    {
                        countItem++;
                        followChunkString = followChunkString + " " + splitSource[i];
                    }
                    senChunks[locOfLast] = new Tuple<string, int, int>(followChunkString, followCStringBeginigLoc - 1, countItem);
                    return true;
                }
            }
            return false;
        }

        private List<Tuple<string, int, int>> initStringParts(string[] splitedSorce, int j)
        {
            List<Tuple<string, int, int>> ans = new List<Tuple<string, int, int>>();
            //each part is a single word
            for (int i = 0; i < j - 1; i++)
            {
                ans.Add(new Tuple<string, int, int>(splitedSorce[i], i, 1));
            }
            //the last part contains the rest
            string theRest = splitedSorce[j - 1];
            for (int loc = j; loc < splitedSorce.Length; loc++)
            {
                theRest = theRest + " " + splitedSorce[loc];
            }
            ans.Add(new Tuple<string, int, int>(theRest, j - 1, splitedSorce.Length - j + 1));
            return ans;
        }
    }
}
