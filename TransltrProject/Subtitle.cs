using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TransltrProject
{
    enum InputStatus { LineNumber,TimeValue,Text }
    public class Subtitle
    {
        public string Path { get; set; }
        public string Language { get; set; }
        public List<Sentence> Sentences { get; set; }
        
        public Subtitle(){}
        public Subtitle(Subtitle Other)
        {
            this.Path = Other.Path;
            this.Language = Other.Language;
            this.Sentences = new List<Sentence>();
            foreach (Sentence item in Other.Sentences)
            {
                this.Sentences.Add(new Sentence(item));
            }
        }

        public bool ScanAsXML(string language, string path)
        {
            Language = language;
            Path = path;

            Sentences = new List<Sentence>();
            int StartTime=0;
            XMLexctractor xmlExtractor = new XMLexctractor(path);

            string currentSentence = "";
            foreach (var item in xmlExtractor.data)
            {
                if (item.GetType() == typeof(documentSW)) {
                    string text = ((documentSW)item).Value;
                    if (text.StartsWith("'") || text.StartsWith(":") || text.StartsWith(",") || text.StartsWith(".") 
                        || text.StartsWith(")") || currentSentence.EndsWith("("))
                        currentSentence += text;
                    else
                        currentSentence += " " + text;
                }
                else if (item.GetType() == typeof(documentSTime)) {
                    if (((documentSTime)item).id.EndsWith("E"))
                        continue;
                    Sentence sentence = new Sentence();
                    Regex rx = new Regex(@"\((.*?)\)");
                    Regex rx2 = new Regex(@"\[(.*?)\]");
                    if (rx.IsMatch(currentSentence))
                    {
                        currentSentence = rx.Replace(currentSentence, string.Empty).TrimEnd(' ').TrimStart(' ');
                        currentSentence = rx2.Replace(currentSentence, string.Empty).TrimEnd(' ').TrimStart(' ');
                        if (currentSentence=="")
                            continue;
                    }

                    string time = ((documentSTime)item).value;
                    string[] splitedBegin = time.Split(':');
                    int BeginHour = int.Parse(splitedBegin[0]);
                    int BeginMin = int.Parse(splitedBegin[1]);
                    int BeginSec = int.Parse(splitedBegin[2].Split(',')[0]);
                    int BeginMilisec = int.Parse(splitedBegin[2].Split(',')[1]);

                    StartTime = BeginHour * 3600000 + BeginMin * 60000 +
                        BeginSec * 1000 + BeginMilisec;

                    sentence.Text = currentSentence;
                    sentence.Start = StartTime;

                    Sentences.Add(sentence);
                    if (Sentences.Count > 1)
                        Sentences[Sentences.Count - 2].TimeToNext = StartTime -
                            Sentences[Sentences.Count - 2].Start;

                    currentSentence = "";       
                }
            }
            return true;
        }

        public bool ExtractAsSynchronized(string language, string path)
        {
            Language = language;
            Path = path;
            Sentences = new List<Sentence>();
            string[] lines = File.ReadAllLines(path);
            foreach (string line in lines)
            {
                string[] parts = line.Split(new string[] { "@,@" },StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length==2)
                {
                    Sentence sentence = new Sentence();
                    int StartTime = int.Parse(parts[0]);
                    sentence.Index = Sentences.Count;
                    sentence.Start = StartTime;
                    if (Sentences.Count >= 1)
                        Sentences[Sentences.Count - 1].TimeToNext = StartTime -
                            Sentences[Sentences.Count - 1].Start;
                    sentence.Text = parts[1];
                    Sentences.Add(sentence);
                }
            }
            return true;
        }


        public bool ExtractAsSrt(string language, string path)
        {
            Language = language;
            Path = path;

            int StartTime = 0;
            int EndTime = 0;
            string[] lines = File.ReadAllLines(path, Encoding.GetEncoding("Windows-1255"));
            Sentences = new List<Sentence>();
            double numberOfSentence = 0;
            string time = null;
            string currentSentence = null;

            InputStatus status = InputStatus.LineNumber;

            for (int i = 0; i < lines.Length; i++)
            {
                switch (status)
                {
                    case InputStatus.LineNumber:
                        if (lines[i].Trim() == "")
                            continue;
                        if (double.TryParse(lines[i], out numberOfSentence))
                            status = InputStatus.TimeValue;
                        else
                        {
                            Logger.WriteError(path, "Structure Warning (Line Number is "+lines[i]+")");
                        }
                        break;
                    case InputStatus.TimeValue:
                        time = lines[i];
                        string[] times = time.Split(' ');
                        string[] splitedBegin = times[0].Split(':');
                        try
                        {
                            int BeginHour = int.Parse(splitedBegin[0]);
                            int BeginMin = int.Parse(splitedBegin[1]);
                            int BeginSec = int.Parse(splitedBegin[2].Split(',')[0]);
                            int BeginMilisec = int.Parse(splitedBegin[2].Split(',')[1]);
                            string[] splitedEnd = times[2].Split(':');
                            int EndHour = int.Parse(splitedEnd[0]);
                            int EndMin = int.Parse(splitedEnd[1]);
                            int EndSec = int.Parse(splitedEnd[2].Split(',')[0]);
                            int EndMilisec = int.Parse(splitedEnd[2].Split(',')[1]);
                            StartTime = BeginHour * 3600000 + BeginMin * 60000 +
                            BeginSec * 1000 + BeginMilisec;
                            EndTime = EndHour * 3600000 + EndMin * 60000
                                  + EndSec * 1000 + EndMilisec;
                        } catch(Exception) {
                            Logger.WriteError(path, "Structure Error (Time parsing)");
                            return false;
                        }

                        
                        status = InputStatus.Text;
                        currentSentence = "";
                        break;
                    case InputStatus.Text:
                        if (lines[i] == "" && currentSentence != "")
                        {
                            Sentence sentence = new Sentence();
                            sentence.Index = Sentences.Count;
                            sentence.Text = currentSentence;
                            sentence.Start = StartTime;
                            sentence.End = EndTime;
                            Sentences.Add(sentence);
                            if (Sentences.Count > 1)
                                Sentences[Sentences.Count - 2].TimeToNext = StartTime -
                                    Sentences[Sentences.Count - 2].Start;
                            status = InputStatus.LineNumber;
                        }
                        else {
                            if ((lines[i].StartsWith(".")|| lines[i].StartsWith("?")
                                || lines[i].StartsWith("!")) && language=="heb" && currentSentence!="")
                            {
                                string newline = lines[i];
                                newline = newline.TrimStart('.').TrimStart('?').TrimStart('!');
                                string secondTemp = currentSentence+" "+ newline;
                            }

                            currentSentence += " " + lines[i];
                        }
                        break;
                    default:
                        break;
                }
            }
            Clean();
            if (Sentences.Count < 50)
            {
                Logger.WriteError(path, "File has Insufficent Lines");
                return false;
            }
            return true;
        }

        internal TranSubtitle GetMediumSizeSentences()
        {
            TranSubtitle ans = new TranSubtitle();

            foreach (var item in Sentences)
            {
                string currentText = item.Text.TrimStart('"').TrimEnd('"');

                string[] splitChars = { "</b>", "<b>", "</i>", "<i>", ";", "?", "!", "-", "."};
                string[] cleanChars = { "\r", "\n" };

                foreach (string chr in cleanChars)
                {
                    currentText = currentText.Replace(chr, " ");
                }

                string[] srcSentences = currentText.Split(splitChars, StringSplitOptions.RemoveEmptyEntries);

                foreach (string phrase in srcSentences)
                {
                    ans.Add(phrase, phrase);
                }
            }
            return ans;
        }

        private void Clean()
        {
            int countClean=0;
            List<Sentence> cleanList = new List<Sentence>();
            for (int i = 0; i < Sentences.Count; i++)
            {
                Sentence sen=Sentences[i];
                if (!CleanSentense(sen.Text))
                {
                    if (countClean > 0)
                        cleanList[countClean - 1].TimeToNext += sen.TimeToNext;
                    continue;
                }
                else
                {
                    cleanList.Add(sen);
                    countClean++;
                }
            }
            this.Sentences = cleanList;
        }

        private bool CleanSentense(String s)
        {
            if (s.Contains("♪") || s.Contains("*"))
                return false;
            if (s.Contains("(") || s.Contains(")"))
                return false;
            if (s.Contains("[") || s.Contains("]"))
                return false;
            if (s.Contains("{") || s.Contains("}"))
                return false;
            if (s.Contains("<") || s.Contains(">"))
                return false;
            return true;

        }

        public string[] GetBrokenSentence(int i)
        {
            Sentence item = Sentences[i];
            string[] splitspace = {"!", ".", "-", ","};// { "?", "!", ".", "-" ,","};
            string[] chunks = item.Text.Split(splitspace, StringSplitOptions.RemoveEmptyEntries);
            List<string> subSplit = new List<string>();
            List<string> output = new List<string>();
            foreach (string str in chunks)
            {
                string str2 = str.TrimEnd(' ').TrimStart(' ');
                if (str2 != "")
                    subSplit.Add(str2);
            }

            string textMemory = "";
            foreach (var text in subSplit)
            {               
                if (text== "Mr" || text== "Mrs" || text== "Dr")
                {
                    textMemory = text;
                }
                else
                {
                    string str = text;
                    if (textMemory != "") {
                        str = textMemory + " " + text;
                        textMemory = "";
                    }
                    output.Add(str);
                }
            }

            return output.ToArray();
        }

       /* public TranSubtitle GetSmallSizeSentences()
        {
            TranSubtitle ans = new TranSubtitle();

            foreach (var item in Sentences)
            {
                string currentText = item.Text.TrimStart('"').TrimEnd('"');

                string[] splitChars = { "</b>", "<b>", "</i>", "<i>", ";", "?", "!", "-", ".", "," };
                string[] cleanChars = { "\r", "\n" };

                foreach (string chr in cleanChars)
                {
                    currentText = currentText.Replace(chr, " ");
                }

                string[] srcSentences = currentText.Split(splitChars, StringSplitOptions.RemoveEmptyEntries);

                foreach (string phrase in srcSentences)
                {
                    ans.Add(phrase, phrase);
                }
            }
            return ans;
        }*/

        /*
        public Subtitle(string language, string path)
        {
            Language = language;
            Path = path;
            int StartTime=0;
            int EndTime=0;
            string[] lines = File.ReadAllLines(path, Encoding.GetEncoding("Windows-1255"));
            Sentences = new List<Sentence>();
            int numberOfSentence=0;
            string time=null;
            string currentSentence = null;

            InputStatus status = InputStatus.Empty;

            for (int i = 0; i < lines.Length; i++)
            {
                switch (status)
                {
                    case InputStatus.Empty:
                        if (int.TryParse(lines[i],out numberOfSentence))
                            status = InputStatus.TimeValue;                           
                        break;
                    case InputStatus.TimeValue:
                        
                        time = lines[i];
                        string[] times = time.Split(' ');
                        string[] splitedBegin = times[0].Split(':');
                        int BeginHour = int.Parse(splitedBegin[0]);
                        int BeginMin = int.Parse(splitedBegin[1]);
                        int BeginSec = int.Parse(splitedBegin[2].Split(',')[0]);
                        int BeginMilisec = int.Parse(splitedBegin[2].Split(',')[1]);
                        string[] splitedEnd = times[2].Split(':');
                        int EndHour = int.Parse(splitedEnd[0]);
                        int EndMin = int.Parse(splitedEnd[1]);
                        int EndSec = int.Parse(splitedEnd[2].Split(',')[0]);
                        int EndMilisec = int.Parse(splitedEnd[2].Split(',')[1]);

                        StartTime = BeginHour * 3600000 + BeginMin * 60000 + 
                            BeginSec * 1000 + BeginMilisec;
                        EndTime = EndHour * 3600000 + EndMin * 60000 
                              + EndSec * 1000 + EndMilisec;

                        status = InputStatus.Text;
                        currentSentence="";
                        break;
                    case InputStatus.Text:
                        if (lines[i] == "")
                        {
                            status = InputStatus.Empty;

                            Sentence sentence = new Sentence();
                            sentence.m_Text = currentSentence;
                            sentence.m_Start = StartTime;
                            //sentence.End = EndTime;
                            Sentences.Add(sentence);
                            if (Sentences.Count>1)
                                Sentences[Sentences.Count - 2].m_TimeToNext = StartTime -
                                    Sentences[Sentences.Count - 2].m_Start;
                            
                        }
                        else 
                            currentSentence += " "+lines[i];                       
                        break;
                    default:
                        break;
                }
            }
            //printToFile(language);
        }*/
        public double Compare(Subtitle other,int offset,double ratio)
        {
            int countMatch = 0;
            int countTotal = 0;
            foreach (var thisSub in Sentences)
            {
                countTotal++;
                foreach (var otherSub in other.Sentences)
                {
                    if (Math.Abs(thisSub.Start - (otherSub.Start*ratio + offset)) < 300)
                    {
                        countMatch++;
                        break;
                    }
                }
            }
            return countMatch / (double)countTotal;
        }

        public void printToFile(string name) {
            StreamWriter sw = new StreamWriter(name);
            int i=0;
            foreach (var item in Sentences)
            {
               //if (item.m_TimeToNext>60000)
                sw.WriteLine(i++ + "," + item.TimeToNext);
            }
            sw.Close();
        }
        public void writeToFile(string path)
        {
            StreamWriter sw = new StreamWriter(path);
            foreach (var item in Sentences)
            {
                sw.WriteLine(item.Start + "@,@" + item.Text);
            }
            sw.Close();
        }
    }
}
