using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TransltrProject.SyncAlgorithms;

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

        public void ExtractFromSyncronized()
        {
            translator = new Translation();
          
            double sumPrecision = 0;
            int syncedCount = 0;
            int totalMovies = 0;         

            List<string> savedHebSentences = new List<string>();
            List<string> savedEngSentences = new List<string>();
            Dictionary<string,int> savedHebWords = new Dictionary<string, int>();
            Dictionary<string, int> savedEngWords = new Dictionary<string, int>();


            string[] splitby = { " ", ",", ".", "-", "!" , "?" };
            foreach (string directory in InputDirectory)
            {
                foreach (var subdir in Directory.GetDirectories(directory))
                {
                    foreach (var movieID in Directory.GetDirectories(subdir))
                    {
                        if (totalMovies % 100 == 0)
                            Console.WriteLine("Completed: " + totalMovies + "/" + Directory.GetDirectories(directory).Length);

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
                                if (item.Item1.Contains("?") || item.Item2.Contains("?"))
                                    continue;
                                string engSentence = FixEngSentence(item.Item1);
                                string hebSentence = FixHebSentence(item.Item2);

                                if (!inValidEnglishSen(item.Item1) || !inValidHebrewSen(item.Item2))
                                    continue;


                                savedEngSentences.Add(engSentence);
                                savedHebSentences.Add(hebSentence);
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
            int limit = (int)(savedHebSentences.Count * 1);

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
    }
}
