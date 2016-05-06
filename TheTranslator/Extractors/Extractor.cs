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
        protected Dictionary<string, Word> m_wordToSenMap;

        // Every line in the training set is inserted here.
        protected Dictionary<string, Sentence> m_allSen;

        // ALL extended dictionary
        protected Dictionary<string, string> m_dic;

        //path of the files.
        protected string m_dataPath;


        public abstract List<List<Sentence>> extractTransParts(string source);

        protected Extractor(string path)
        {
            m_wordToSenMap = new Dictionary<string, Word>();
            m_allSen = new Dictionary<string, Sentence>();
            m_dataPath = path;
            m_dic = new Dictionary<string, string>();
        }

       

        public bool build(ref Statistics stats)
        {
            // check if all needed files exists
            if (!File.Exists(m_dataPath + @"/DownloadedFullTrain.en-he.low.he") ||
                !File.Exists(m_dataPath + @"/DownloadedFullTrain.en-he.low.en") ||
                !File.Exists(m_dataPath + @"/GoogleTranslateWords.txt"))
                return false;
            //
            StreamReader soReader = new StreamReader(m_dataPath + @"/DownloadedFullTrain.en-he.low.he");
            StreamReader taReader = new StreamReader(m_dataPath + @"/DownloadedFullTrain.en-he.low.en");
            StreamReader dicReader = new StreamReader(m_dataPath + @"/GoogleTranslateWords.txt");

            int linesCounter = 0;

            Regex rxRemovePsik = new Regex(",+");
            Regex rxRemoveSpace = new Regex(@"\s\s+");

            string sourceLine = null;
            string targetLine = null;

            Sentence sen;
            bool isNew;
            string[] linePartsSo;

            try
            {
                string[] allWords = dicReader.ReadToEnd().Split(new string[] { "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);

                // Read every line in each file
                while ((sourceLine = soReader.ReadLine()) != null & (targetLine = taReader.ReadLine()) != null)
                {
                    // write progress
                    if (linesCounter++ % 10000 == 0) { 
                        Console.WriteLine(linesCounter);
                        Trace.WriteLine(linesCounter);
                    }
                    if (linesCounter == 200000) break;

                    //train language model (statistics for later use)
                    stats.Insert(targetLine);


                    sourceLine = rxRemovePsik.Replace(sourceLine, " ");
                    sourceLine = rxRemoveSpace.Replace(sourceLine, " ");

                    targetLine = rxRemovePsik.Replace(targetLine, " ");
                    targetLine = rxRemoveSpace.Replace(targetLine, " ");




                    isNew = !m_allSen.TryGetValue(sourceLine, out sen);
                    if (isNew)
                    {
                        sen = new Sentence(sourceLine, linesCounter);
                        m_allSen.Add(sourceLine, sen);
                    }

                    sen.addTarget(targetLine);
                    linePartsSo = sourceLine.Split(new string[] { " ", "," }, StringSplitOptions.RemoveEmptyEntries);
                    if (linePartsSo.Length == 0)
                        continue;

                    for (int i = 0; i < linePartsSo.Length; i++)
                    {
                        string linePart = linePartsSo[i];
                        Word word;
                        bool isNewWord = !m_wordToSenMap.TryGetValue(linePart, out word);
                        if (isNewWord)
                        {
                            word = new WordList(linePart);
                            m_wordToSenMap.Add(linePart, word);
                        }
                        word.addSentence(sen);
                    }
                    // Save all statistics here. (this is the english [[source]] language model)
                    stats.Insert(targetLine);

                }

                if ((sourceLine == null && targetLine != null) || (sourceLine != null && targetLine == null))
                {
                    Console.WriteLine("!!!!!the DB files have diff length!!!!!");
                }

                foreach (var item in m_allSen)
                {
                    item.Value.m_target.Sort();

                }
                foreach (var line in allWords)
                {
                    string[] lineData = line.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    string sWord = lineData[0];
                    string tWord = lineData[2];
                    tWord = tWord.ToLower();
                    m_dic.Add(sWord, tWord);
                    if (!m_wordToSenMap.ContainsKey(sWord))
                        continue;
                    else //WORD IS IN THE DB
                    {
                        TargetSentence ts = new TargetSentence(tWord, 1, OneWordSentence.m_gradeForUnkown);
                        if (m_allSen.ContainsKey(sWord)) //word apears as a sentence
                        {
                            Sentence oneWordSen = m_allSen[sWord];
                            int countWordAp = oneWordSen.m_target[0].m_count;
                            if (countWordAp > MIN_COMMON_MAKES_SKIP)
                            {
                                ts = new TargetSentence(oneWordSen.m_target[0].m_translation, countWordAp, OneWordSentence.m_gradeForUnkown);
                            }

                        }
                        m_wordToSenMap[sWord].m_translation = ts;
                    }
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
                Console.WriteLine("Load done!");
            }

            return true;
        }

        public Sentence getWordTranslation(string word)
        {
            if (m_wordToSenMap.ContainsKey(word) && m_wordToSenMap[word].m_translation!=null)
                return new OneWordSentence(word, m_wordToSenMap[word].m_translation.m_translation);
            else if (m_dic.ContainsKey(word))
                return new OneWordSentence(word, m_dic[word]);
            else return new OneWordSentence(word, word);
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
