using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TheTranslator
{
    public abstract class Extractor
    {
        // if a dictionary word already appears more times then this number,
        // do not take the dictionary translations. - sometimes the dictionary is wrong.
        private const int MIN_COMMON_MAKES_SKIP=2;



        // map of a word to its' sentence, 
        //protected Dictionary<string, Word> m_wordToSenMap;


        // Every line in the training set is inserted here.
        protected Dictionary<string, Sentence> m_allSen;

        // ALL extended dictionary
        protected Dictionary<string, Sentence> m_dic;

        //path of the files.
        protected string m_dataPath;

        //protected Dictionary<string, HashSet<string>> m_similarities;

        public abstract List<List<Sentence>> extractTransParts(string source);

        protected Extractor(string path)
        {
            //m_wordToSenMap = new Dictionary<string, Word>();
            m_allSen = new Dictionary<string, Sentence>();
            m_dataPath = path;
            m_dic = new Dictionary<string, Sentence>();
            //m_similarities = new Dictionary<string, HashSet<string>>();
        }

       

        public virtual bool build(ref Statistics stats)
        {
            // check if all needed files exists
            if (!File.Exists(m_dataPath + @"\DownloadedFullTrain.en-he.low.he"))
                return false;

            if (!File.Exists(m_dataPath + @"\DownloadedFullTrain.en-he.low.en"))
                return false;

            if (!File.Exists(m_dataPath + @"\GoogleTranslateWords.txt"))
                return false;
            //
            StreamReader soReader = new StreamReader(m_dataPath + @"/DownloadedFullTrain.en-he.low.he");
            StreamReader taReader = new StreamReader(m_dataPath + @"/DownloadedFullTrain.en-he.low.en");
            StreamReader dicReader = new StreamReader(m_dataPath + @"/GoogleTranslateWords.txt");

            int linesCounter = 0;

            //Regex rxRemovePsik = new Regex(",+");
            Regex rxRemoveSpace = new Regex(@"\s\s+");
            
            string sourceLine = null;
            string targetLine = null;

            Sentence sen;
            bool isNew;
            //string[] linePartsSo;

            try
            {
                

                // Read every line in each file
                while ((sourceLine = soReader.ReadLine()) != null & (targetLine = taReader.ReadLine()) != null)
                {
                    // write progress
                    if (linesCounter % 10000 == 0) { 
                        Console.WriteLine(linesCounter);
                        Trace.WriteLine(linesCounter);
                    }
                    linesCounter++;
                    if (linesCounter >= 600000) break;

                    //sourceLine = rxRemovePsik.Replace(sourceLine, " ");
                    sourceLine = rxRemoveSpace.Replace(sourceLine, " ");

                    //targetLine = rxRemovePsik.Replace(targetLine, " ");
                    targetLine = rxRemoveSpace.Replace(targetLine, " ");

                    // Save all statistics here. (this is the english [[source]] language model)
                    stats.Insert(targetLine);

                    // Add all source sentences into a dictionary

                    isNew = !m_allSen.TryGetValue(sourceLine, out sen);
                    if (isNew)
                    {
                        sen = new Sentence(sourceLine, linesCounter);
                        m_allSen.Add(sourceLine, sen);
                    }

                    // Add the target to the source that is in the dictionary
                    sen.addTarget(targetLine);


                    //-------------------------------------------- we want to avoid using Word --------------------
                    /*linePartsSo = sourceLine.Split(new string[] { " ", "," }, StringSplitOptions.RemoveEmptyEntries);
                    if (linePartsSo.Length == 0)
                        continue;

                    for (int i = 0; i < linePartsSo.Length; i++)
                    {
                        string linePart = linePartsSo[i];
                        Word word;
                        bool isNewWord = !m_wordToSenMap.TryGetValue(linePart, out word);
                        if (isNewWord)
                        {
                            word = new Word(linePart);
                            m_wordToSenMap.Add(linePart, word);
                        }
                        word.addSentence(sen);
                    }*/
                    //--------------------------------------------
                }

                if ((sourceLine == null && targetLine != null) || (sourceLine != null && targetLine == null))
                {
                    Console.WriteLine("!!!!!the DB files have diff length!!!!!");
                }

                foreach (var item in m_allSen)
                {
                    item.Value.sortAmdUpdatePr();
                }

                // Read auxilery dictionary
                string[] allWords = dicReader.ReadToEnd().Split(new string[] { "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var line in allWords)
                {
                    string[] lineData = line.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    string sWord = lineData[0];
                    string tWord = lineData[2];

                    tWord = tWord.ToLower();
                    if (!m_dic.ContainsKey(sWord))
                        m_dic.Add(sWord, new Sentence(sWord, 0));
                    m_dic[sWord].addTarget(tWord);

                    /*if (!m_wordToSenMap.ContainsKey(sWord))
                        continue;
                    else //WORD IS IN THE DB
                    {
                        List<TargetSentence> lts = new List<TargetSentence>();
                        TargetSentence ts;
                        if (m_allSen.ContainsKey(sWord)) //word apears as a sentence
                        {
                            Sentence oneWordSen = m_allSen[sWord];
                            lts = oneWordSen.getTopN();
                            int countWordAp = lts[0].m_count;
                            if (countWordAp > MIN_COMMON_MAKES_SKIP)
                            {
                                m_wordToSenMap[sWord].m_translation = lts;
                                continue;
                            }

                        }
                        ts = new TargetSentence(tWord, 1, Sentence.m_gradeForUnkown, false);
                        lts.Add(ts);

                        m_wordToSenMap[sWord].m_translation = lts;
                    }*/

                    //UpdateLevenshteinSimilarities();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("failed to load the DB: " + e.InnerException + "id=" + linesCounter);
                return false;
            }
            finally
            {
                soReader.Close();
                taReader.Close();
                dicReader.Close();
                Console.WriteLine("Load done!");
            }

            return true;
        }

        /*private void UpdateLevenshteinSimilarities()
        {
            int counter = 0;
            int total = m_allSen.Keys.Count;

            // we want to do levenstain distance on all the db, we have to do it fast!

            List<Tuple<string, string>> temp = new List<Tuple<string, string>>();
            foreach (var item1 in m_allSen.Keys)
            {
                counter++;
                foreach (var item2 in m_allSen.Keys)
                {
                    if (item1 != item2 && ((item1.Length - item2.Length) < 3 || (item2.Length - item1.Length) < 3))
                        temp.Add(new Tuple<string, string>(item1, item2));
                }
                if (counter % 50 == 0)
                {
                    Console.WriteLine("Computing Levenshtein similarities Part 1: " + counter + "/" + total);
                }
            }

            counter = 0;
            total = temp.Count;

            foreach (var item in temp)
            {
                counter++;
                if (counter % 50 == 0)
                {
                    if (ComputeLevenshteinDistance(item.Item1,item.Item2)<3)
                    {
                        if (!m_similarities.ContainsKey(item.Item1))
                            m_similarities.Add(item.Item1, new HashSet<string>());
                        if (!m_similarities[item.Item1].Contains(item.Item2))
                            m_similarities[item.Item1].Add(item.Item2);
                        if (!m_similarities.ContainsKey(item.Item2))
                            m_similarities.Add(item.Item2, new HashSet<string>());
                        if (!m_similarities[item.Item2].Contains(item.Item1))
                            m_similarities[item.Item2].Add(item.Item1);
                    }
                    Console.WriteLine("Computing Levenshtein similarities Part 1: " + counter + "/" + total);
                }
            }
        }*/
        /*private static int ComputeLevenshteinDistance(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            // Step 1
            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            // Step 2
            for (int i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (int j = 0; j <= m; d[0, j] = j++)
            {
            }

            // Step 3
            for (int i = 1; i <= n; i++)
            {
                //Step 4
                for (int j = 1; j <= m; j++)
                {
                    // Step 5
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    // Step 6
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            // Step 7
            return d[n, m];
        }*/


        public Sentence getWordTranslation(string word)
        {
            if (m_allSen.ContainsKey(word))
            {
                return m_allSen[word];
            } else if (m_dic.ContainsKey(word))
            {
                return m_dic[word];
            } else
            {
                return new Sentence(word, -1);
            }
            
            
            /*
            Sentence ans = new Sentence(word, -1);
            List<TargetSentence> lts;
            if (m_wordToSenMap.ContainsKey(word))
            {
                Word w = m_wordToSenMap[word];
                if (w.m_translation != null)
                {
                    ans.addTargetList(w.m_translation, w.countInDB);
                    return ans;
                }
            }
            if (m_dic.ContainsKey(word))
            {
                string tWord = m_dic[word];
                lts = new List<TargetSentence>();
                lts.Add(new TargetSentence(tWord, 1, Sentence.m_gradeForUnkown, true));
                ans.addTargetList(lts, 1);
                return ans;
            }
            lts = new List<TargetSentence>();
            lts.Add(new TargetSentence(word, 1, Sentence.m_gradeForUnkown, false));
            ans.addTargetList(lts, 1);
            return ans;*/
        }

        public bool Enhance()
        {
            StreamReader soReader = new StreamReader(m_dataPath + @"\enhancmentSet.he.txt");
            StreamReader taReader = new StreamReader(m_dataPath + @"\enhancmentSet.en.txt");
            Regex rxRemovePsik = new Regex(",+");
            Regex rxRemoveSpace = new Regex(@"\s\s+");
            Sentence sen;
            int id = m_allSen.Count;
            string lineText = null;
            string lineTextTa = null;
            try
            {

                while ((lineText = soReader.ReadLine()) != null & (lineTextTa = taReader.ReadLine()) != null)
                {
                    lineText = rxRemovePsik.Replace(lineText, " ");
                    lineText = rxRemoveSpace.Replace(lineText, " ");

                    lineTextTa = rxRemovePsik.Replace(lineTextTa, " ");
                    lineTextTa = rxRemoveSpace.Replace(lineTextTa, " ");

                    if (id % 100000 == 0)
                        Console.WriteLine(id);
                    bool isNew = !m_allSen.TryGetValue(lineText, out sen);
                    if (isNew)
                    {
                        sen = new Sentence(lineText, id);
                        m_allSen.Add(lineText, sen);
                    }

                    sen.addTarget(lineTextTa);
                    string[] linePartsSo = lineText.Split(new string[] { " ", "," }, StringSplitOptions.RemoveEmptyEntries);
                    if (linePartsSo.Length == 0)
                        continue;

                    id++;

                }

                if ((lineText == null && lineTextTa != null) || (lineText != null && lineTextTa == null))
                {
                    Console.WriteLine("!!!!!the DB enhance files have diff length!!!!!");
                }

                foreach (var item in m_allSen)
                {
                    item.Value.m_target.Sort();
                    item.Value.m_target.Reverse();
                }


            }
            catch (Exception e)
            {
                Console.WriteLine("failed to load the DB enhace: " + e.InnerException + "id=" + id);
                return false;
            }
            finally
            {
                soReader.Close();
                taReader.Close();
                Console.WriteLine("Load done!");
            }
            return true;
        }     
        public void printTrans(List<List<Sentence>> trans)
        {
            Console.WriteLine("");
            Console.WriteLine("START");
            foreach (var transOption in trans)
            {
                Console.WriteLine("");
                Console.Write("option from " + transOption.Count + " parts " );
                Console.WriteLine("");
                foreach (var senPart in transOption)
                {
                    Console.WriteLine(" || ");
                    int orCount = 0;
                    foreach (var senPartTrans in senPart.m_target)
                    {
                        orCount++;
                        if (orCount > 10)
                            break;
                        Console.Write(" " + senPartTrans.ToString()+ " ");
                        Console.Write(" OR ");
                    }
                    Console.WriteLine("  ");
                }
                Console.WriteLine("  ");
            }
            Console.Write("End");
            Console.Write(" ");
        }
    }
}
