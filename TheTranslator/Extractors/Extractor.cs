using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using TheTranslator.DataManager;
using TheTranslator.Extractors;

namespace TheTranslator
{
    public abstract class Extractor
    { 
        private const int MIN_COMMON_MAKES_SKIP=2;

        protected DBManager m_sentences;
        //path of the files.
        protected string m_dataPath;

        public abstract List<List<Sentence>> extractTransParts(string source);

        private HashSet<string> m_sourceSentences = new HashSet<string>();

        protected Extractor(string path)
        {
            m_dataPath = path;
            m_sentences = new DBManager();
        }


        public virtual bool build()
        {
            m_sentences.Reset();
            // check if all needed files exists
            if (!File.Exists(m_dataPath + @"\DownloadedFullTrain.en-he.true.he"))
                return false;

            if (!File.Exists(m_dataPath + @"\DownloadedFullTrain.en-he.true.en"))
                return false;

            if (!File.Exists(m_dataPath + @"\GoogleTranslateWords.txt"))
                return false;
            //
            StreamReader soReader = new StreamReader(m_dataPath + @"/DownloadedFullTrain.en-he.true.he");
            StreamReader taReader = new StreamReader(m_dataPath + @"/DownloadedFullTrain.en-he.true.en");
            StreamReader dicReader = new StreamReader(m_dataPath + @"/GoogleTranslateWords.txt");

            int linesCounter = 0;

            //Regex rxRemovePsik = new Regex(",+");
            Regex rxRemoveSpace = new Regex(@"\s\s+");
            
            string sourceLine = null;
            string targetLine = null;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                // Read every line in each file
                while ((sourceLine = soReader.ReadLine()) != null & (targetLine = taReader.ReadLine()) != null)
                {
                    try
                    {
                        // write progress
                        if (linesCounter % 10000 == 0)
                        {
                            Console.WriteLine(linesCounter + " ,Time took: " + stopwatch.Elapsed);
                            Trace.WriteLine(linesCounter + " ,Time took: " + stopwatch.Elapsed);
                            stopwatch.Restart();
                        }
                        linesCounter++;
                        //if (linesCounter == 1367)
                        //    Console.Beep();
                        //if (linesCounter == 10000) break;

                        sourceLine = rxRemoveSpace.Replace(sourceLine, " ");
                        targetLine = rxRemoveSpace.Replace(targetLine, " ");

                        // Save all statistics here. (this is the english [[source]] language model)
                        //stats.Insert(targetLine);
                        //stats.Insert(sourceLine);
                        //m_sentences.WriteSet("TargetSentences",targetLine);
                        //m_sentences.WriteSet("SourceSentences",sourceLine);
                        if (!m_sourceSentences.Contains(sourceLine))
                            m_sourceSentences.Add(sourceLine);
                        //m_sentences.WriteSet(sourceLine, targetLine);

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("could not remember " + sourceLine + " -> " + targetLine);
                    }
                }
                return true;

                if ((sourceLine == null && targetLine != null) || (sourceLine != null && targetLine == null))
                {
                    Console.WriteLine("!!!!!the DB files have diff length!!!!!");
                }

                // Read auxilery dictionary
                string[] allWords = dicReader.ReadToEnd().Split(new string[] { "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);
                int counterDictionary = 0;
                foreach (var line in allWords)
                {
                    if (counterDictionary % 1000 == 0)
                    {
                        Console.WriteLine(counterDictionary + "/" + allWords.Length);
                    }
                    counterDictionary++;

                    string[] lineData = line.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    m_sentences.WriteSet(lineData[0], lineData[2]);
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

        internal void Enhance()
        {
            //SeperatingCombiner sc = new SeperatingCombiner(this);
            //IEnumerable<HashEntry> entries = m_sentences.GetAllValues("SourceSentences");

            int counter = 0;
            int counterAdded = 0;
            foreach (var source in m_sourceSentences)
            {
                Console.WriteLine("Enhance: " + counter++ + "/" + m_sourceSentences.Count + ",added: "+counterAdded);
                string key = source.ToString();//.Substring(1);
                string[] srcParts = key.Split(' ');
                IEnumerable<HashEntry> correlatedTranslations = m_sentences.GetAllValues(key);
                foreach (var target in correlatedTranslations)
                {
                    string value = target.Name.ToString().Substring(1);

                    string[] dstParts = value.Split(' ');
                    if (dstParts.Length != srcParts.Length || dstParts.Length<2)
                        continue;

                    List<Tuple<string, string>> srcCombinedParts = CombineStringParts(srcParts);
                    List<Tuple<string, string>> dstCombinedParts = CombineStringParts(dstParts);

                    for (int i = 0; i < srcCombinedParts.Count; i++)
                    {
                        var srcopt1Ex = m_sentences.GetSet(srcCombinedParts[i].Item1);
                        var srcopt2Ex = m_sentences.GetSet(srcCombinedParts[i].Item2);
                        var dstopt1Ex = m_sentences.GetSet(dstCombinedParts[i].Item1);
                        var dstopt2Ex = m_sentences.GetSet(dstCombinedParts[i].Item2);

                        if (m_sentences.CheckGet(srcCombinedParts[i].Item1,dstCombinedParts[i].Item1)) {
                            if ((int)m_sentences.GetSet(srcCombinedParts[i].Item1, dstCombinedParts[i].Item1) > 3)
                            {
                                m_sentences.WriteSet(srcCombinedParts[i].Item2, dstCombinedParts[i].Item2);
                                counterAdded++;
                            }
                        }
                        if (m_sentences.CheckGet(srcCombinedParts[i].Item2, dstCombinedParts[i].Item2))
                        {
                            if ((int)m_sentences.GetSet(srcCombinedParts[i].Item2, dstCombinedParts[i].Item2) > 3)
                            {
                                m_sentences.WriteSet(srcCombinedParts[i].Item1, dstCombinedParts[i].Item1);
                                counterAdded++;
                            }
                        }
                    }
                }
            }
            Console.WriteLine("Finished enhancing, added " + counterAdded + " new entries");
        }

        private List<Tuple<string, string>> CombineStringParts(string[] srcParts)
        {
            List<Tuple<string, string>> parts = new List<Tuple<string, string>>();
            StringBuilder Part1;
            StringBuilder Part2;

            for (int limit = 1; limit < srcParts.Length; limit++)
            {
                Part1 = new StringBuilder();
                Part2 = new StringBuilder();
                for (int i = 0; i < limit; i++)
                {
                    if (i != limit - 1)
                        Part1.Append(srcParts[i]).Append(' ');
                    else
                        Part1.Append(srcParts[i]);
                }
                for (int i = limit; i < srcParts.Length; i++)
                {
                    if (i != srcParts.Length - 1)
                        Part2.Append(srcParts[i]).Append(' ');
                    else
                        Part2.Append(srcParts[i]);
                }
                parts.Add(new Tuple<string, string>(Part1.ToString(), Part2.ToString()));
            }
            return parts;
        }

        private void playMario()
        {
            Console.Beep(659, 125);
            Console.Beep(659, 125);
            Thread.Sleep(125);
            Console.Beep(659, 125);
            Thread.Sleep(167);
            Console.Beep(523, 125);
            Console.Beep(659, 125);
            Thread.Sleep(125);
            Console.Beep(784, 125);
            Thread.Sleep(375);
            Console.Beep(392, 125);
            Thread.Sleep(375);
            Console.Beep(523, 125);
            Thread.Sleep(250);
            Console.Beep(392, 125);
            Thread.Sleep(250);
            Console.Beep(330, 125);
            Thread.Sleep(250);
            Console.Beep(440, 125);
            Thread.Sleep(125);
            Console.Beep(494, 125);
            Thread.Sleep(125);
            Console.Beep(466, 125);
            Thread.Sleep(42);
            Console.Beep(440, 125);
            Thread.Sleep(125);
            Console.Beep(392, 125);
            Thread.Sleep(125);
            Console.Beep(659, 125);
            Thread.Sleep(125);
            Console.Beep(784, 125);
            Thread.Sleep(125);
            Console.Beep(880, 125);
            Thread.Sleep(125);
            Console.Beep(698, 125);
            Console.Beep(784, 125);
            Thread.Sleep(125);
            Console.Beep(659, 125);
            Thread.Sleep(125);
            Console.Beep(523, 125);
            Thread.Sleep(125);
            Console.Beep(587, 125);
            Console.Beep(494, 125);
            Thread.Sleep(125);
            Console.Beep(523, 125);
            Thread.Sleep(250);
            Console.Beep(392, 125);
            Thread.Sleep(250);
            Console.Beep(330, 125);
            Thread.Sleep(250);
            Console.Beep(440, 125);
            Thread.Sleep(125);
            Console.Beep(494, 125);
            Thread.Sleep(125);
            Console.Beep(466, 125);
            Thread.Sleep(42);
            Console.Beep(440, 125);
            Thread.Sleep(125);
            Console.Beep(392, 125);
            Thread.Sleep(125);
            Console.Beep(659, 125);
            Thread.Sleep(125);
            Console.Beep(784, 125);
            Thread.Sleep(125);
            Console.Beep(880, 125);
            Thread.Sleep(125);
            Console.Beep(698, 125);
            Console.Beep(784, 125);
            Thread.Sleep(125);
            Console.Beep(659, 125);
            Thread.Sleep(125);
            Console.Beep(523, 125);
            Thread.Sleep(125);
            Console.Beep(587, 125);
            Console.Beep(494, 125);
            Thread.Sleep(375);
            Console.Beep(784, 125);
            Console.Beep(740, 125);
            Console.Beep(698, 125);
            Thread.Sleep(42);
            Console.Beep(622, 125);
            Thread.Sleep(125);
            Console.Beep(659, 125);
            Thread.Sleep(167);
            Console.Beep(415, 125);
            Console.Beep(440, 125);
            Console.Beep(523, 125);
            Thread.Sleep(125);
            Console.Beep(440, 125);
            Console.Beep(523, 125);
            Console.Beep(587, 125);
            Thread.Sleep(250);
            Console.Beep(784, 125);
            Console.Beep(740, 125);
            Console.Beep(698, 125);
            Thread.Sleep(42);
            Console.Beep(622, 125);
            Thread.Sleep(125);
            Console.Beep(659, 125);
            Thread.Sleep(167);
            Console.Beep(698, 125);
            Thread.Sleep(125);
            Console.Beep(698, 125);
            Console.Beep(698, 125);
            Thread.Sleep(625);
            Console.Beep(784, 125);
            Console.Beep(740, 125);
            Console.Beep(698, 125);
            Thread.Sleep(42);
            Console.Beep(622, 125);
            Thread.Sleep(125);
            Console.Beep(659, 125);
            Thread.Sleep(167);
            Console.Beep(415, 125);
            Console.Beep(440, 125);
            Console.Beep(523, 125);
            Thread.Sleep(125);
            Console.Beep(440, 125);
            Console.Beep(523, 125);
            Console.Beep(587, 125);
            Thread.Sleep(250);
            Console.Beep(622, 125);
            Thread.Sleep(250);
            Console.Beep(587, 125);
            Thread.Sleep(250);
            Console.Beep(523, 125);
            Thread.Sleep(1125);
            Console.Beep(784, 125);
            Console.Beep(740, 125);
            Console.Beep(698, 125);
            Thread.Sleep(42);
            Console.Beep(622, 125);
            Thread.Sleep(125);
            Console.Beep(659, 125);
            Thread.Sleep(167);
            Console.Beep(415, 125);
            Console.Beep(440, 125);
            Console.Beep(523, 125);
            Thread.Sleep(125);
            Console.Beep(440, 125);
            Console.Beep(523, 125);
            Console.Beep(587, 125);
            Thread.Sleep(250);
            Console.Beep(784, 125);
            Console.Beep(740, 125);
            Console.Beep(698, 125);
            Thread.Sleep(42);
            Console.Beep(622, 125);
            Thread.Sleep(125);
            Console.Beep(659, 125);
            Thread.Sleep(167);
            Console.Beep(698, 125);
            Thread.Sleep(125);
            Console.Beep(698, 125);
            Console.Beep(698, 125);
            Thread.Sleep(625);
            Console.Beep(784, 125);
            Console.Beep(740, 125);
            Console.Beep(698, 125);
            Thread.Sleep(42);
            Console.Beep(622, 125);
            Thread.Sleep(125);
            Console.Beep(659, 125);
            Thread.Sleep(167);
            Console.Beep(415, 125);
            Console.Beep(440, 125);
            Console.Beep(523, 125);
            Thread.Sleep(125);
            Console.Beep(440, 125);
            Console.Beep(523, 125);
            Console.Beep(587, 125);
            Thread.Sleep(250);
            Console.Beep(622, 125);
            Thread.Sleep(250);
            Console.Beep(587, 125);
            Thread.Sleep(250);
            Console.Beep(523, 125);
            Thread.Sleep(625);
        }
        public abstract bool TranslationExists(string source);
        public abstract string ExtractExactTranslation(string source, int minCount);

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
        /*public Sentence getWordTranslation(string word)
        {
            string isnb = m_sentences.GetSet(word);


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
            return ans;
        }*/
        /*public bool Enhance()
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
        }    */
        /*public void printTrans(List<List<Sentence>> trans)
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
        }*/
    }
}
