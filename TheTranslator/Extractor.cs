using System;
using System.Collections.Generic;
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

        //number of words in english in the the db files 
        public int m_countWordsInDb;

        // map of a word to its' sentence, 
        protected Dictionary<string, Word> m_wordToSenMap;

        // Every line in the training set is inserted here.
        protected Dictionary<string, Sentence> m_allSen;

        // ALL extended dictionary
        protected Dictionary<string, string> m_dic;

        //path of the files.
        protected string m_dataPath;

        //the count of all words in DB
        protected Dictionary<string, int> m_singleWordsCount;
        //the count of all word pairs in DB
        protected Dictionary<string, int> m_pairWordsCount;
        //the cont of all word triple in db
        protected Dictionary<string, int> m_trippelWordsCount;

        protected Extractor(string path)
        {
            m_wordToSenMap = new Dictionary<string, Word>();
            m_allSen = new Dictionary<string, Sentence>();
            m_dataPath = path;
            m_dic = new Dictionary<string, string>();
            m_pairWordsCount = new Dictionary<string, int>();
            m_trippelWordsCount = new Dictionary<string, int>();
            m_singleWordsCount = new Dictionary<string, int>();
            m_countWordsInDb = 0;
        }


        public int getCountTaPair(string w1, string w2)
        {
            int ans=1;
            if(m_pairWordsCount.TryGetValue(w1 + " " + w2 , out ans))
                return ans;
            return 1;
        }
        public int getCountTaWord(string w)
        {
            int ans = 1;
            if (m_singleWordsCount.TryGetValue(w, out ans))
                return ans;
            return 1;

        }
        public Sentence getWordTranslation(string word)
        {
            if (m_wordToSenMap.ContainsKey(word) && m_wordToSenMap[word].m_translation!=null)
                return new OneWordSentence(word, m_wordToSenMap[word].m_translation.m_translation);
            else if (m_dic.ContainsKey(word))
                return new OneWordSentence(word, m_dic[word]);
            else return new OneWordSentence(word, word);
        }

        public bool build()
        {
            StreamReader soReader = new StreamReader(m_dataPath + @"/DownloadedFullTrain.en-he.low.he");
            StreamReader taReader = new StreamReader(m_dataPath + @"/DownloadedFullTrain.en-he.low.en");
            StreamReader dicReader = new StreamReader(m_dataPath + @"/GoogleTranslateWords.txt");
      
            int id = 0;
            Regex rxRemovePsik = new Regex(",+");
            Regex rxRemoveSpace = new Regex(@"\s\s+");
            string lineText = null;
            string lineTextTa = null;
            Sentence sen;
            bool isNew;
            string[] linePartsSo;
            string[] linePartsTa;

            try
            {
                string[] allWords = dicReader.ReadToEnd().Split(new string[] { "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);
                while ((lineText = soReader.ReadLine()) != null & (lineTextTa = taReader.ReadLine()) != null)
                {
                    lineText = rxRemovePsik.Replace(lineText, " ");
                    lineText = rxRemoveSpace.Replace(lineText, " ");

                    lineTextTa = rxRemovePsik.Replace(lineTextTa, " ");
                    lineTextTa = rxRemoveSpace.Replace(lineTextTa, " ");

                    if (id % 100000 == 0)
                        Console.WriteLine(id);
                    isNew = !m_allSen.TryGetValue(lineText, out sen);
                    if (isNew)
                    {
                        sen = new Sentence(lineText, id);
                        m_allSen.Add(lineText, sen);
                    }

                    sen.addTarget(lineTextTa);
                    linePartsSo = lineText.Split(new string[] { " ", "," }, StringSplitOptions.RemoveEmptyEntries);
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

                    id++;


                    linePartsTa = lineTextTa.Split(new string[] { " ", "," }, StringSplitOptions.RemoveEmptyEntries);
                    if (linePartsTa.Length == 0)
                        continue;

                    m_countWordsInDb += linePartsTa.Length;
                    string prevWord = linePartsTa[0];
                    string prevPrevWord = linePartsTa[0];
                    for (int i = 0; i < linePartsTa.Length; i++)
                    {
                        string currWord = linePartsTa[i];
                        if (!m_singleWordsCount.ContainsKey(currWord))
                            m_singleWordsCount.Add(currWord, 1);
                        else m_singleWordsCount[currWord] += 1;
                        
                        if (i > 0)
                        {
                            string pair = prevWord + " " + currWord;
                            if (!m_pairWordsCount.ContainsKey(pair))
                                m_pairWordsCount.Add(pair, 1);
                            else m_pairWordsCount[pair] += 1;
                        }
                        if (i > 1)
                        {
                            string pair = prevPrevWord + " " + prevWord + " " + currWord;
                            if (!m_trippelWordsCount.ContainsKey(pair))
                                m_trippelWordsCount.Add(pair, 1);
                            else m_trippelWordsCount[pair] += 1;
                            prevPrevWord = prevWord;
                        }

                        prevWord = currWord;
                    }

                }

                if ((lineText == null && lineTextTa != null) || (lineText != null && lineTextTa == null))
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
                Console.WriteLine("failed to load the DB: " + e.InnerException + "id=" + id);
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


        public Boolean Enhance()
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

        public abstract List<List<Sentence>> extractTransParts(string source);

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
