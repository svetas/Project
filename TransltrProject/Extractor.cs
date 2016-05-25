using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TransltrProject.SyncAlgorithms;
using static System.Diagnostics.Stopwatch;

namespace TransltrProject
{
    public class Extractor
    {
        private string[] InputDirectory { get; set; }
        private string OutputDirectory { get; set; }
        private double ExtractionAmount { get; set; }
        public Translation translator { get; private set; }

        private const int MAX_VOCAB_LANG = 5000;

        public Extractor(string[] pathRaw,string pathOut, double amount)
        {
            InputDirectory = pathRaw;
            OutputDirectory = pathOut;
            ExtractionAmount = amount;
        }



        /*public void SyncFilesInInputPath()
        {
            if (!Directory.Exists(InputDirectory)) return;
            if (!Directory.Exists(OutputDirectory)) Directory.CreateDirectory(OutputDirectory);
            int totalMovies = 0;
            foreach (var movieID in Directory.GetDirectories(InputDirectory))
            {
                try
                {
                    int movieNumber = int.Parse(Path.GetFileName(movieID));
                    if (Directory.Exists(OutputDirectory + "\\" + movieNumber)) continue;



                    string engLoc = movieID + "\\en0.srt";
                    string hebLoc = movieID + "\\he0.srt";

                    if (!File.Exists(hebLoc) || !File.Exists(engLoc))
                    {
                        if (!File.Exists(engLoc))
                        {
                            Logger.WriteError(movieID + "\\en0.srt", "Missing");
                        }

                        if (!File.Exists(hebLoc))
                        {
                            Logger.WriteError(movieID + "\\he0.srt", "Missing");
                        }
                        continue;
                    }

                    Subtitle subtitleOne = new Subtitle();
                    Subtitle subtitleTwo = new Subtitle();

                    bool subOneStatus = subtitleOne.ExtractAsSrt("eng", engLoc);
                    bool subTwoStatus = subtitleTwo.ExtractAsSrt("heb", hebLoc);

                    if (!subOneStatus || !subTwoStatus)
                    {
                        continue;
                    }

                    Synchronizer sync = new Synchronizer(subtitleOne, subtitleTwo, new SyncByLengthRatio());

                    double precision = sync.Align(500);
                    if (precision == 0)
                        continue;


                    Logger.WriteError(subtitleOne.Path, "Synced and extracted phrases");

                    Directory.CreateDirectory(OutputDirectory + "\\" + movieNumber);
                    sync.GetFirstSyncedSub().writeToFile(OutputDirectory + "\\" + movieNumber + "\\en.srt");
                    sync.GetSecondSyncedSub().writeToFile(OutputDirectory + "\\" + movieNumber + "\\he.srt");

                    if (totalMovies++ % 100 == 0)
                        Console.WriteLine("Completed: " + totalMovies + "/" + Directory.GetDirectories(direc).Length);

                    /*s1.writeToFile("artificial.srt");
                    subtitleOne.writeToFile("subtitleOne.srt");
                    subtitleTwo.writeToFile("subtitleTwo.srt");*//*


                    //sc = new SubtitlesComparer(subtitleOne, s1);

                    //TranSubtitle tr = sc.Compare();
                    //tr.WriteToFile("output.txt");

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }*/
        private void AddToDictionaryReference(IEnumerable<int> length1, ref Dictionary<string,Dictionary<string,int>> savedTranslations,
            ref List<string> savedHebSentences, ref List<string> savedEngSentences,int i)
        {
            if (!savedTranslations.ContainsKey(savedHebSentences[length1.ElementAt(i)]))
                savedTranslations.Add(savedHebSentences[length1.ElementAt(i)], new Dictionary<string, int>());
            if (!savedTranslations[savedHebSentences[length1.ElementAt(i)]].ContainsKey(savedEngSentences[length1.ElementAt(i)]))
                savedTranslations[savedHebSentences[length1.ElementAt(i)]].Add(savedEngSentences[length1.ElementAt(i)], 0);
            savedTranslations[savedHebSentences[length1.ElementAt(i)]][savedEngSentences[length1.ElementAt(i)]]++;
        }


        public void ExtractAsSrt()
        {
            translator = new Translation();

            Stopwatch st = new Stopwatch();
            
            //SubtitlesComparer sc;

            double sumPrecision = 0;
            int syncedCount = 0;
            int totalMovies = 0;


            List<string> savedHebSentences = new List<string>();
            List<string> savedEngSentences = new List<string>();
            Dictionary<string,int> savedHebWords = new Dictionary<string, int>();
            Dictionary<string, int> savedEngWords = new Dictionary<string, int>();


            string[] splitby = { " ", ",", ".", "-", "!" };//, "?" };
            foreach (string directory in InputDirectory)
            {
                foreach (var subdir in Directory.GetDirectories(directory))
                {
                    foreach (var movieID in Directory.GetDirectories(subdir))
                    {
                        if (totalMovies % 100 == 0)
                            Console.WriteLine("Completed: " + totalMovies + "/" + Directory.GetDirectories(directory).Length);

                        //if (totalMovies / (double)Directory.GetDirectories(directory).Length > ExtractionAmount)
                        //    break;

                        totalMovies++;
                        try
                        {
                            string engLoc = movieID + "\\en.srt";
                            string hebLoc = movieID + "\\he.srt";

                            if (!File.Exists(hebLoc) || !File.Exists(engLoc))
                            {
                                if (!File.Exists(engLoc))
                                {
                                    Logger.WriteError(movieID + "\\en.srt", "Missing");
                                }

                                if (!File.Exists(hebLoc))
                                {
                                    Logger.WriteError(movieID + "\\he.srt", "Missing");
                                }
                                continue;
                            }

                            Subtitle subtitleOne = new Subtitle();
                            Subtitle subtitleTwo = new Subtitle();

                            bool subOneStatus = subtitleOne.ExtractAsSynchronized("eng", engLoc);
                            bool subTwoStatus = subtitleTwo.ExtractAsSynchronized("heb", hebLoc);

                            if (!subOneStatus || !subTwoStatus)
                            {
                                continue;
                            }

                            Synchronizer sync = new Synchronizer(subtitleOne, subtitleTwo, new SyncByLengthRatio());

                            double precision = sync.Align(500);
                            if (precision == 0)
                                continue;


                            Logger.WriteError(subtitleOne.Path, "Synced and extracted phrases");

                            TranSubtitle translations = sync.GetTranslations();

                            foreach (var item in translations.Translations)
                            {
                                string engSentence = FixEngSentence(item.Item1);
                                string hebSentence = FixHebSentence(item.Item2);

                                if (!inValidEnglishSen(item.Item1) || !inValidHebrewSen(item.Item2))
                                    continue;

                                savedEngSentences.Add(engSentence);
                                savedHebSentences.Add(hebSentence);


                                //string[] splitspace = { };
                                //string[] wordsEng = item.Item1.Split(splitby, StringSplitOptions.RemoveEmptyEntries);
                                //string[] wordsHeb = item.Item2.Split(splitby, StringSplitOptions.RemoveEmptyEntries);
                                //foreach (var word in wordsEng)
                                //{
                                //    if (!savedEngWords.ContainsKey(word)) savedEngWords.Add(word, 0);
                                //    savedEngWords[word] += 1;
                                //}
                                //foreach (var word in wordsHeb)
                                //{
                                //    if (!savedHebWords.ContainsKey(word)) savedHebWords.Add(word, 0);
                                //    savedHebWords[word] += 1;
                                //}
                            }

                            sumPrecision += precision;

                            if (translations == null)
                            {
                                continue;
                            }
                            syncedCount++;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                }
            }
            string text = totalMovies + "," + syncedCount + "," + (sumPrecision / (double)syncedCount);
            Logger.WriteLine(text);

            HashSet<string> memoryGood = new HashSet<string>();
            HashSet<string> memoryBad = new HashSet<string>();

            List<string> toWriteMemoryEng = new List<string>();
            List<string> toWriteMemoryHeb = new List<string>();

            //var pickedWordsHeb = (from entry in savedHebWords orderby entry.Value descending select entry.Key);//.Take(40000);
            //var pickedWordsEng = (from entry in savedEngWords orderby entry.Value descending select entry.Key);//.Take(40000);

            StreamWriter swEnTrain = new StreamWriter(OutputDirectory+"\\DownloadedFullTrain.en-he.en");
            StreamWriter swHeTrain = new StreamWriter(OutputDirectory + "\\DownloadedFullTrain.en-he.he");
            int limit = (int)(savedHebSentences.Count * 0.97);

            for (int i = 0; i < limit; i++)
            {
                swEnTrain.WriteLine(savedEngSentences[i]);
                swHeTrain.WriteLine(savedHebSentences[i]);
            }

            swEnTrain.Close();
            swHeTrain.Close();

            StreamWriter swEnTune = new StreamWriter(OutputDirectory + "\\DownloadedFullTune.en-he.en");
            StreamWriter swHeTune = new StreamWriter(OutputDirectory + "\\DownloadedFullTune.en-he.he");

            for (int i = (int)(savedHebSentences.Count * 0.97); i < savedHebSentences.Count * 0.99; i++)
            {
                swEnTune.WriteLine(savedEngSentences[i]);
                swHeTune.WriteLine(savedHebSentences[i]);
            }

            swEnTune.Close();
            swHeTune.Close();

            StreamWriter swEnTest = new StreamWriter(OutputDirectory + "\\DownloadedFullTest.en-he.en");
            StreamWriter swHeTest = new StreamWriter(OutputDirectory + "\\DownloadedFullTest.en-he.he");

            for (int i = (int)(savedHebSentences.Count * 0.99); i < savedHebSentences.Count * 1; i++)
            {
                swEnTest.WriteLine(savedEngSentences[i]);
                swHeTest.WriteLine(savedHebSentences[i]);
            }

            swEnTest.Close();
            swHeTest.Close();
            return;

            st.Start();




            // len 1
            StreamWriter Len1HeTest = new StreamWriter(OutputDirectory + "\\Len1\\Downloaded.he");
            StreamWriter Len1Ref0 = new StreamWriter(OutputDirectory + "\\Len1\\Downloaded.ref0");
            StreamWriter Len1Ref1 = new StreamWriter(OutputDirectory + "\\Len1\\Downloaded.ref1");
            StreamWriter Len1Ref2 = new StreamWriter(OutputDirectory + "\\Len1\\Downloaded.ref2");
            // len 2
            StreamWriter Len2HeTest = new StreamWriter(OutputDirectory + "\\Len2\\Downloaded.he");
            StreamWriter Len2Ref0 = new StreamWriter(OutputDirectory + "\\Len2\\Downloaded.ref0");
            StreamWriter Len2Ref1 = new StreamWriter(OutputDirectory+"\\Len2\\Downloaded.ref1");
            StreamWriter Len2Ref2 = new StreamWriter(OutputDirectory + "\\Len2\\Downloaded.ref2");
            // len 3
            StreamWriter Len3HeTest = new StreamWriter(OutputDirectory + "\\Len3\\Downloaded.he");
            StreamWriter Len3Ref0 = new StreamWriter(OutputDirectory+"\\Len3\\Downloaded.ref0");
            StreamWriter Len3Ref1 = new StreamWriter(OutputDirectory+"\\Len3\\Downloaded.ref1");
            StreamWriter Len3Ref2 = new StreamWriter(OutputDirectory + "\\Len3\\Downloaded.ref2");
            // len 4
            StreamWriter Len4HeTest = new StreamWriter(OutputDirectory + "\\Len4\\Downloaded.he");
            StreamWriter Len4Ref0 = new StreamWriter(OutputDirectory+"\\Len4\\Downloaded.ref0");
            StreamWriter Len4Ref1 = new StreamWriter(OutputDirectory+"\\Len4\\Downloaded.ref1");
            StreamWriter Len4Ref2 = new StreamWriter(OutputDirectory + "\\Len4\\Downloaded.ref2");
            // len 5-9
            StreamWriter Len5HeTest = new StreamWriter(OutputDirectory + "\\Len5\\Downloaded.he");
            StreamWriter Len5Ref0 = new StreamWriter(OutputDirectory+"\\Len5\\Downloaded.ref0");
            StreamWriter Len5Ref1 = new StreamWriter(OutputDirectory+"\\Len5\\Downloaded.ref1");
            StreamWriter Len5Ref2 = new StreamWriter(OutputDirectory + "\\Len5\\Downloaded.ref2");
            // len 10+
            StreamWriter Len10HeTest = new StreamWriter(OutputDirectory + "\\Len10\\Downloadeds.he");
            StreamWriter Len10Ref0 = new StreamWriter(OutputDirectory + "\\Len10\\Downloaded.ref0");
            StreamWriter Len10Ref1 = new StreamWriter(OutputDirectory+"\\Len10\\Downloaded.ref1");
            StreamWriter Len10Ref2 = new StreamWriter(OutputDirectory+"\\Len10\\Downloaded.ref2");






            Dictionary<string, Dictionary<string, int>> savedTranslations = new Dictionary<string, Dictionary<string, int>>();
            Dictionary<int, int> lengths = new Dictionary<int, int>();
            for (int i = limit; i < savedHebSentences.Count; i++)
            {

                int sentenceLength = savedHebSentences[i].Split(splitby, StringSplitOptions.RemoveEmptyEntries).Length;
                lengths.Add(i, sentenceLength);
            }

            var length1 = (from entry in lengths where entry.Value == 1 select entry.Key);
            var length2 = (from entry in lengths where entry.Value == 2 select entry.Key);
            var length3 = (from entry in lengths where entry.Value == 3 select entry.Key); 
            var length4 = (from entry in lengths where entry.Value == 4 select entry.Key);
            var length5 = (from entry in lengths where entry.Value == 5 select entry.Key);
            var length10 = (from entry in lengths where entry.Value ==10 select entry.Key);

             int minLength = Math.Min(Math.Min(Math.Min(length1.Count(), length2.Count()), Math.Min(length3.Count(),
                length4.Count())), Math.Min(length5.Count(), length10.Count()));

            if (minLength < 10000)
                throw new Exception();

            for (int i = 0; i < minLength; i++)
            {
                if (i%100==0)
                Console.WriteLine(i+"/"+minLength);
                string senLen1 = savedHebSentences[length1.ElementAt(i)];
                string senLen2 = savedHebSentences[length2.ElementAt(i)];
                string senLen3 = savedHebSentences[length3.ElementAt(i)];
                string senLen4 = savedHebSentences[length4.ElementAt(i)];
                string senLen5 = savedHebSentences[length5.ElementAt(i)];
                string senLen10 = savedHebSentences[length10.ElementAt(i)];

                // save the translation in dictionary for later reference generation.

                AddToDictionaryReference(length1, ref savedTranslations, ref savedHebSentences, ref savedEngSentences, i);
                AddToDictionaryReference(length2, ref savedTranslations, ref savedHebSentences, ref savedEngSentences, i);
                AddToDictionaryReference(length3, ref savedTranslations, ref savedHebSentences, ref savedEngSentences, i);
                AddToDictionaryReference(length4, ref savedTranslations, ref savedHebSentences, ref savedEngSentences, i);
                AddToDictionaryReference(length5, ref savedTranslations, ref savedHebSentences, ref savedEngSentences, i);
                AddToDictionaryReference(length10, ref savedTranslations, ref savedHebSentences, ref savedEngSentences, i);

                Len1HeTest.WriteLine(senLen1);
                Len2HeTest.WriteLine(senLen2);
                Len3HeTest.WriteLine(senLen3);
                Len4HeTest.WriteLine(senLen4);
                Len5HeTest.WriteLine(senLen5);
                Len10HeTest.WriteLine(senLen10);
            }

            for (int i = 0; i < minLength; i++)
            {
                SetRef(Len1Ref0, Len1Ref1, Len1Ref2, ref savedTranslations, savedHebSentences[length1.ElementAt(i)]);
                SetRef(Len2Ref0, Len2Ref1, Len2Ref2, ref savedTranslations, savedHebSentences[length2.ElementAt(i)]);
                SetRef(Len3Ref0, Len3Ref1, Len3Ref2, ref savedTranslations, savedHebSentences[length3.ElementAt(i)]);
                SetRef(Len4Ref0, Len4Ref1, Len4Ref2, ref savedTranslations, savedHebSentences[length4.ElementAt(i)]);
                SetRef(Len5Ref0, Len5Ref1, Len5Ref2, ref savedTranslations, savedHebSentences[length5.ElementAt(i)]);
                SetRef(Len10Ref0, Len10Ref1, Len10Ref2, ref savedTranslations, savedHebSentences[length10.ElementAt(i)]);
            }

            Len1HeTest.Close();
            Len2HeTest.Close();
            Len3HeTest.Close();
            Len4HeTest.Close();
            Len5HeTest.Close();
            Len10HeTest.Close();

            Len1Ref0.Close();
            Len1Ref1.Close();
            Len1Ref2.Close();
            Len2Ref0.Close();
            Len2Ref1.Close();
            Len2Ref2.Close();
            Len3Ref0.Close();
            Len3Ref1.Close();
            Len3Ref2.Close();
            Len4Ref0.Close();
            Len4Ref1.Close();
            Len4Ref2.Close();
            Len5Ref0.Close();
            Len5Ref1.Close();
            Len5Ref2.Close();
            Len10Ref0.Close();
            Len10Ref1.Close();
            Len10Ref2.Close();
            /*
            




            swEnTest.Close();
            swHeTest.Close();
            swEnTrain.Close();
            swHeTrain.Close();
            /*
            for (int i = 0; i < savedHebSentences.Count; i++)
            {
                if (i % 1000 == 0)
                {
                    Console.WriteLine("Dumping data");
                    using (StreamWriter swHe = File.AppendText("Downloaded.en-he.he"))
                    {
                        StreamWriter swEn = File.AppendText("Downloaded.en-he.en");
                        for (int j = 0; j < toWriteMemoryHeb.Count; j++)
                        {
                            swHe.WriteLine(toWriteMemoryHeb[j]);
                            swEn.WriteLine(toWriteMemoryEng[j]);
                        }
                        swEn.Close();
                        swHe.Close();
                    }

                    toWriteMemoryHeb = new List<string>();
                    toWriteMemoryEng = new List<string>();

                    Console.WriteLine("passed " + i + "/" + savedHebSentences.Count+", last run took: "+st.Elapsed+" seconds");
                    st.Restart();            
                }

                //string combined = savedHebSentences[i] + savedEngSentences[i];
                // new hebrew phrase?
                if (!memoryGood.Contains(savedHebSentences[i]) && !memoryBad.Contains(savedHebSentences[i]))
                {
                    string[] hebSen = savedHebSentences[i].Split(splitby, StringSplitOptions.RemoveEmptyEntries);
                    if (hebSen.Intersect(pickedWordsHeb).Count() == hebSen.Length)
                    {
                        memoryGood.Add(savedHebSentences[i]);
                    } else
                    {
                        memoryBad.Add(savedHebSentences[i]);
                    }
                }


                if (!memoryGood.Contains(savedEngSentences[i]) && !memoryBad.Contains(savedEngSentences[i]))
                {

                    string[] engSen = savedEngSentences[i].Split(splitby, StringSplitOptions.RemoveEmptyEntries);
                    if (engSen.Intersect(pickedWordsEng).Count() == engSen.Length)
                    {
                        memoryGood.Add(savedEngSentences[i]);
                    }
                    else
                    {
                        memoryBad.Add(savedEngSentences[i]);
                    }
                }

                if (memoryGood.Contains(savedEngSentences[i]) && memoryGood.Contains(savedHebSentences[i]))
                {
                    toWriteMemoryEng.Add(savedEngSentences[i]);
                    toWriteMemoryHeb.Add(savedHebSentences[i]);
                }
                
            }

            Console.WriteLine("Dumping data");
            using (StreamWriter swHe = File.AppendText("Downloaded.en-he.he"))
            {
                StreamWriter swEn = File.AppendText("Downloaded.en-he.en");
                for (int j = 0; j < toWriteMemoryHeb.Count; j++)
                {
                    swHe.WriteLine(toWriteMemoryHeb[j]);
                    swEn.WriteLine(toWriteMemoryEng[j]);
                }
                swEn.Close();
                swHe.Close();
            }

            StreamWriter sw = new StreamWriter("memoryGood.txt");
            foreach (var item in memoryGood)
            {
                sw.WriteLine(item);
            }
            sw.Close();
            sw = new StreamWriter("memoryBad.txt");
            foreach (var item in memoryBad)
            {
                sw.WriteLine(item);
            }
            sw.Close();*/


            //StreamWriter sw = new StreamWriter("outpuTest.txt");
            //Console.WriteLine("skipped:" + SubtitlesComparer.skipCount + "outof:" + d.GetDirectories().Count());

            //translator.WriteToHTMLfile("outpuTest.html");

            /*foreach (var item in translator.Translations)
            {
                foreach (var values in item.Value)
                {
                    if (values.Value > 0)
                        sw.WriteLine(item.Key + "  --> " + values.Key + "   Strength: (" + values.Value + ")");
                }
            }*/
            //sw.Close();
        }

        private string FixHebSentence(string item)
        {
            return item.Replace("’", "'");
        }

        private string FixEngSentence(string item)
        {
            return item.Replace("’", "'");
        }

        Regex rxBadEng = new Regex(@"[^a-zA-Z0-9\s':, ""\-.$\?]");
        Regex rxBadHeb = new Regex(@"[^0-9\s':, ""\-.$\?א-ת]");

        private bool inValidHebrewSen(string item2)
        {
            if (rxBadHeb.IsMatch(item2))
            {
                string tmp = rxBadHeb.Replace(item2,"");
                if (tmp == item2)
                    return true;
                return false;
            }
                
            return true;
        }

        private bool inValidEnglishSen(string item1)
        {
            if  (rxBadEng.IsMatch(item1))
                  return false;
            return true;   
        }

        private void SetRef(StreamWriter ref0, StreamWriter ref1, StreamWriter ref2, ref Dictionary<string, Dictionary<string, int>> savedTranslations, string v)
        {
            string line = v;
            
            if (savedTranslations[line].Count > 3)
            {
                var bestReferences = (from entry in savedTranslations[line] orderby entry.Value descending select entry.Key).Take(3);
                ref0.WriteLine(bestReferences.ElementAt(0));
                ref1.WriteLine(bestReferences.ElementAt(1));
                ref2.WriteLine(bestReferences.ElementAt(2));
            }
            else if (savedTranslations[line].Count == 3)
            {
                ref0.WriteLine(savedTranslations[line].ElementAt(0).Key);
                ref1.WriteLine(savedTranslations[line].ElementAt(1).Key);
                ref2.WriteLine(savedTranslations[line].ElementAt(2).Key);
            }
            else if (savedTranslations[line].Count == 2)
            {
                ref0.WriteLine(savedTranslations[line].ElementAt(0).Key);
                ref1.WriteLine(savedTranslations[line].ElementAt(1).Key);
                ref2.WriteLine(savedTranslations[line].ElementAt(1).Key);
            }
            else if (savedTranslations[line].Count == 1)
            {
                ref0.WriteLine(savedTranslations[line].ElementAt(0).Key);
                ref1.WriteLine(savedTranslations[line].ElementAt(0).Key);
                ref2.WriteLine(savedTranslations[line].ElementAt(0).Key);
            }
        }
    

       /* public void ExtractReferences()
        {
            translator = new Translation();

            Stopwatch st = new Stopwatch();

            //SubtitlesComparer sc;

            double sumPrecision = 0;
            int syncedCount = 0;
            int totalMovies = 0;


            List<string> savedHebSentences = new List<string>();
            List<string> savedEngSentences = new List<string>();
            Dictionary<string, int> savedHebWords = new Dictionary<string, int>();
            Dictionary<string, int> savedEngWords = new Dictionary<string, int>();

            Dictionary<string, Dictionary<string, int>> savedTranslations = new Dictionary<string, Dictionary<string, int>>();

            string[] splitby = { " ", ",", ".", "-", "!", "?" };

            foreach (var movieID in Directory.GetDirectories(InputDirectory))
            {
                if (totalMovies % 100 == 0)
                    Console.WriteLine("Completed: " + totalMovies + "/" + Directory.GetDirectories(InputDirectory).Length);

                if (totalMovies / (double)Directory.GetDirectories(InputDirectory).Length > ExtractionAmount)
                    break;

                totalMovies++;
                try
                {
                    string engLoc = movieID + "\\en.srt";
                    string hebLoc = movieID + "\\he.srt";

                    if (!File.Exists(hebLoc) || !File.Exists(engLoc))
                    {
                        if (!File.Exists(engLoc))
                        {
                            Logger.WriteError(movieID + "\\en.srt", "Missing");
                        }

                        if (!File.Exists(hebLoc))
                        {
                            Logger.WriteError(movieID + "\\he.srt", "Missing");
                        }
                        continue;
                    }

                    Subtitle subtitleOne = new Subtitle();
                    Subtitle subtitleTwo = new Subtitle();

                    bool subOneStatus = subtitleOne.ExtractAsSynchronized("eng", engLoc);
                    bool subTwoStatus = subtitleTwo.ExtractAsSynchronized("heb", hebLoc);

                    if (!subOneStatus || !subTwoStatus)
                    {
                        continue;
                    }

                    Synchronizer sync = new Synchronizer(subtitleOne, subtitleTwo, new SyncByLengthRatio());

                    double precision = sync.Align(500);
                    if (precision == 0)
                        continue;


                    Logger.WriteError(subtitleOne.Path, "Synced and extracted phrases");

                    TranSubtitle translations = sync.GetTranslations();

                    foreach (var item in translations.Translations)
                    {
                        
                        if (!savedTranslations.ContainsKey(item.Item2))
                        {
                            savedTranslations.Add(item.Item2, new Dictionary<string, int>());
                        } 
                        if (!savedTranslations[item.Item2].ContainsKey(item.Item1))
                        {
                            savedTranslations[item.Item2].Add(item.Item1, 0);
                        }
                        savedTranslations[item.Item2][item.Item1]++;
                         
                        savedEngSentences.Add(item.Item1);
                        savedHebSentences.Add(item.Item2);


                        //string[] splitspace = { };
                        string[] wordsEng = item.Item1.Split(splitby, StringSplitOptions.RemoveEmptyEntries);
                        string[] wordsHeb = item.Item2.Split(splitby, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var word in wordsEng)
                        {
                            if (!savedEngWords.ContainsKey(word)) savedEngWords.Add(word, 0);
                            savedEngWords[word] += 1;
                        }
                        foreach (var word in wordsHeb)
                        {
                            if (!savedHebWords.ContainsKey(word)) savedHebWords.Add(word, 0);
                            savedHebWords[word] += 1;
                        }
                    }

                    sumPrecision += precision;

                    if (translations == null)
                    {
                        continue;
                    }
                    syncedCount++;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            
            string text = totalMovies + "," + syncedCount + "," + (sumPrecision / (double)syncedCount);
            Logger.WriteLine(text);

            HashSet<string> memoryGood = new HashSet<string>();
            HashSet<string> memoryBad = new HashSet<string>();

            List<string> toWriteMemoryEng = new List<string>();
            List<string> toWriteMemoryHeb = new List<string>();

            var pickedWordsHeb = (from entry in savedHebWords orderby entry.Value descending select entry.Key).Take(40000);
            var pickedWordsEng = (from entry in savedEngWords orderby entry.Value descending select entry.Key).Take(40000);

            st.Start();
            StreamWriter swHe = new StreamWriter("Downloaded.en-he.he");
            StreamWriter swEn0 = new StreamWriter("Downloaded.en-he.ref0");
            StreamWriter swEn1 = new StreamWriter("Downloaded.en-he.ref1");
            StreamWriter swEn2 = new StreamWriter("Downloaded.en-he.ref2");

            StreamReader sr = new StreamReader(@"C:\Users\Sagi\Dropbox\JointProject\ExternalSystemsDB\5000words\DownloadedTest.en-he.he");
            StreamReader swback = new StreamReader(@"C:\Users\Sagi\Dropbox\JointProject\ExternalSystemsDB\5000words\DownloadedTest.en-he.en");
            while (!sr.EndOfStream)
            {
                string backupLine = swback.ReadLine();
                string line = sr.ReadLine();
                if (!savedTranslations.ContainsKey(line))
                {
                    swEn0.WriteLine(backupLine);
                    swEn1.WriteLine(backupLine);
                    swEn2.WriteLine(backupLine);
                } 
                else if (savedTranslations[line].Count > 3)
                {
                    var bestReferences = (from entry in savedTranslations[line] orderby entry.Value descending select entry.Key).Take(3);
                    swEn0.WriteLine(bestReferences.ElementAt(0));
                    swEn1.WriteLine(bestReferences.ElementAt(1));
                    swEn2.WriteLine(bestReferences.ElementAt(2));
                }
                else if (savedTranslations[line].Count == 3)
                {
                    swEn0.WriteLine(savedTranslations[line].ElementAt(0).Key);
                    swEn1.WriteLine(savedTranslations[line].ElementAt(1).Key);
                    swEn2.WriteLine(savedTranslations[line].ElementAt(2).Key);
                }
                else if (savedTranslations[line].Count == 2)
                {
                    swEn0.WriteLine(savedTranslations[line].ElementAt(0).Key);
                    swEn1.WriteLine(savedTranslations[line].ElementAt(1).Key);
                    swEn2.WriteLine(savedTranslations[line].ElementAt(1).Key);
                }
                else if (savedTranslations[line].Count == 1)
                {
                    swEn0.WriteLine(savedTranslations[line].ElementAt(0).Key);
                    swEn1.WriteLine(savedTranslations[line].ElementAt(0).Key);
                    swEn2.WriteLine(savedTranslations[line].ElementAt(0).Key);
                }
            }
            */
            /*

            for (int i = 0; i < savedHebSentences.Count; i++)
            {
                //string combined = savedHebSentences[i] + savedEngSentences[i];
                // new hebrew phrase?
                if (!memoryGood.Contains(savedHebSentences[i]) && !memoryBad.Contains(savedHebSentences[i]))
                {
                    string[] hebSen = savedHebSentences[i].Split(splitby, StringSplitOptions.RemoveEmptyEntries);
                    if (hebSen.Intersect(pickedWordsHeb).Count() == hebSen.Length)
                    {
                        memoryGood.Add(savedHebSentences[i]);
                    }
                    else
                    {
                        memoryBad.Add(savedHebSentences[i]);
                    }
                }


                if (!memoryGood.Contains(savedEngSentences[i]) && !memoryBad.Contains(savedEngSentences[i]))
                {

                    string[] engSen = savedEngSentences[i].Split(splitby, StringSplitOptions.RemoveEmptyEntries);
                    if (engSen.Intersect(pickedWordsEng).Count() == engSen.Length)
                    {
                        memoryGood.Add(savedEngSentences[i]);
                    }
                    else
                    {
                        memoryBad.Add(savedEngSentences[i]);
                    }
                }

                if (memoryGood.Contains(savedEngSentences[i]) && memoryGood.Contains(savedHebSentences[i]))
                {
                    //toWriteMemoryEng.Add(savedEngSentences[i]);
                    //toWriteMemoryHeb.Add(savedHebSentences[i]);

                    swHe.WriteLine(savedHebSentences[i]);
                    if (savedTranslations[savedHebSentences[i]].Count > 3)
                    {
                        var bestReferences = (from entry in savedTranslations[savedHebSentences[i]] orderby entry.Value descending select entry.Key).Take(3);
                        swEn0.WriteLine(bestReferences.ElementAt(0));
                        swEn1.WriteLine(bestReferences.ElementAt(1));
                        swEn2.WriteLine(bestReferences.ElementAt(2));
                    }
                    else if (savedTranslations[savedHebSentences[i]].Count == 3)
                    {
                        swEn0.WriteLine(savedTranslations[savedHebSentences[i]].ElementAt(0).Key);
                        swEn1.WriteLine(savedTranslations[savedHebSentences[i]].ElementAt(1).Key);
                        swEn2.WriteLine(savedTranslations[savedHebSentences[i]].ElementAt(2).Key);
                    }
                    else if (savedTranslations[savedHebSentences[i]].Count == 2)
                    {
                        swEn0.WriteLine(savedTranslations[savedHebSentences[i]].ElementAt(0).Key);
                        swEn1.WriteLine(savedTranslations[savedHebSentences[i]].ElementAt(1).Key);
                        swEn2.WriteLine(savedTranslations[savedHebSentences[i]].ElementAt(1).Key);
                    }
                    else if (savedTranslations[savedHebSentences[i]].Count == 1)
                    {
                        swEn0.WriteLine(savedTranslations[savedHebSentences[i]].ElementAt(0).Key);
                        swEn1.WriteLine(savedTranslations[savedHebSentences[i]].ElementAt(0).Key);
                        swEn2.WriteLine(savedTranslations[savedHebSentences[i]].ElementAt(0).Key);
                    }
                }

            }*//*
            swHe.Close();
            swEn0.Close();
            swEn1.Close();
            swEn2.Close();
        }*//*
        public void ExtractAsSrtOnlyEnglish()
        {
            Translation translator = new Translation();

            //SubtitlesComparer sc;

            double sumPrecision = 0;
            int syncedCount = 0;
            int totalMovies = 0;
            int counter = 0;
            foreach (var dir1 in Directory.GetDirectories(InputDirectory))
            {
                if (counter++ % 100 == 0)
                    Console.WriteLine("Done 1000 more:" + counter + "/" + Directory.GetDirectories(InputDirectory).Count());

                totalMovies++;
                try
                {
                    string engLoc = dir1 + "\\en0.srt";
                    string hebLoc = dir1 + "\\he0.srt";

                    if (!File.Exists(hebLoc) || !File.Exists(engLoc))
                    {
                        if (!File.Exists(engLoc))
                        {
                            Logger.WriteError(dir1 + "\\en0.srt", "Missing");
                        }

                        if (!File.Exists(hebLoc))
                        {
                            Logger.WriteError(dir1 + "\\he0.srt", "Missing");
                        }
                        continue;
                    }

                    Subtitle subtitleOne = new Subtitle();
                    Subtitle subtitleTwo = new Subtitle();

                    bool subOneStatus = subtitleOne.ExtractAsSrt("eng", engLoc);    
                    bool subTwoStatus = subtitleTwo.ExtractAsSrt("heb", hebLoc);

                    if (!subOneStatus || !subTwoStatus)
                    {
                        continue;
                    }


                    TranSubtitle translations = subtitleTwo.GetMediumSizeSentences();

                    translator.AddToDictionary(translations);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
            string text = totalMovies + "," + syncedCount + "," + (sumPrecision / (double)syncedCount);
            Logger.WriteLine(text);

            StreamWriter sw = new StreamWriter("outpuTest.txt");
            //Console.WriteLine("skipped:" + SubtitlesComparer.skipCount + "outof:" + d.GetDirectories().Count());
            foreach (var item in translator.Translations)
            {
                foreach (var values in item.Value)
                {
                    if (values.Value > 0)
                        sw.WriteLine(item.Key + "," + values.Value);
                }
            }
            sw.Close();


        }

        public void ExtractAsSrtOnlyHebrew()
        {
            Translation translator = new Translation();
            
            double sumPrecision = 0;
            int syncedCount = 0;
            int totalMovies = 0;
            int counter = 0;
            foreach (var dir1 in Directory.GetDirectories(InputDirectory))
            {
                if (counter++ % 100 == 0)
                    Console.WriteLine("Done 1000 more:" + counter + "/" + Directory.GetDirectories(InputDirectory).Count());

                totalMovies++;
                try
                {
                    string engLoc = dir1 + "\\en.srt";
                    string hebLoc = dir1 + "\\he.srt";

                    if (!File.Exists(hebLoc) || !File.Exists(engLoc))
                    {
                        if (!File.Exists(engLoc))
                        {
                            Logger.WriteError(dir1 + "\\en.srt", "Missing");
                        }

                        if (!File.Exists(hebLoc))
                        {
                            Logger.WriteError(dir1 + "\\he.srt", "Missing");
                        }
                        continue;
                    }

                    Subtitle subtitleOne = new Subtitle();
                    Subtitle subtitleTwo = new Subtitle();

                    bool subOneStatus = subtitleOne.ExtractAsSynchronized("eng", engLoc);
                    bool subTwoStatus = subtitleTwo.ExtractAsSynchronized("heb", hebLoc);
                    
                    if (!subOneStatus || !subTwoStatus)
                    {
                        continue;
                    }


                    TranSubtitle translations = subtitleTwo.GetMediumSizeSentences();

                    translator.AddToDictionary(translations);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
            string text = totalMovies + "," + syncedCount + "," + (sumPrecision / (double)syncedCount);
            Logger.WriteLine(text);

            StreamWriter sw = new StreamWriter("outpuTest.txt");
            //Console.WriteLine("skipped:" + SubtitlesComparer.skipCount + "outof:" + d.GetDirectories().Count());
            foreach (var item in translator.Translations)
            {
                foreach (var values in item.Value)
                {
                    if (values.Value > 0)
                        sw.WriteLine(item.Key + "@,@" + values.Value);
                }
            }
            sw.Close();


        }
        */
    }
}
