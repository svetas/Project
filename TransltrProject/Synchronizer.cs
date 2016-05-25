using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TransltrProject.ExtractingAlgorithms;
using TransltrProject.SyncAlgorithms;

namespace TransltrProject
{
    
    public class Synchronizer
    {

        private Subtitle s1=null;
        private Subtitle s2=null;
        private Subtitle news2 = null;
        public int SyncThreshold { get; set; }
        SyncMethod syncer;

        public Synchronizer(Subtitle sub1, Subtitle sub2, SyncMethod syn)
        {
            syncer = syn;
            s1 = sub1;
            s2 = sub2;
            news2 = sub2;
        }

       
        
        public double Align(int minimumSpace)
        {
            
            SyncThreshold = minimumSpace;
            double precision;
            /* 
             * Step 1: Check if the subtitles are perfectly aligned 
             */
            precision = ExactMatchesRatio(s1,s2);
            if (precision>0.5)
                return precision;
            /*
             * Step 1.5: Check if the hebrew subtitle is not google translate 
             */
            if (ErrorSentenceRatio(s2) > 0.02)
            {
                Logger.WriteError(s1.Path, "Hebrew sub contains english words (probably google translate)");
                return 0;
            }
            /* 
             * Step 2: There is Offset and ratio deviation, 
             * Scan for them using time intervals.
             */
            double deviationGrade;



            AlignmentSetting settings = syncer.GetBestDeviation(s1, s2, out deviationGrade);
            
            int src1 = s1.Sentences[settings.FirstS].Start;
            int src2 = s1.Sentences[settings.FirstE].Start;
            int trg1 = s2.Sentences[settings.SecondS].Start;
            int trg2 = s2.Sentences[settings.SecondE].Start;


            if (s1.Path.Contains("2446040"))
            {
                int x = 5;
            }
            string locs = "1.1-->" + settings.FirstS + "   2.1-->" + settings.SecondS;
            string locs2 = "1.2-->" + settings.FirstE + "   2.2-->" + settings.SecondE;
            string text = s1.Path + '\n';
            string str1 = " 1: " + s1.Sentences[settings.FirstS].Text + " --> "  + s2.Sentences[settings.SecondS].Text;
            string str2 = " 2: " + s1.Sentences[settings.FirstE].Text + " --> " + s2.Sentences[settings.SecondE].Text;
            text = str1 + str2;
            //Console.WriteLine(text);
            Console.WriteLine("############");
            Console.WriteLine(str1);
            Console.WriteLine(str2);
            Logger.WriteDebug(text);

            double ratio = (trg1 - trg2) / (double)(src1 - src2);

            double offSet = trg2 - src2 * ratio;

            news2 = GenerateArtificialSubtitle(ratio, offSet);

            precision = ExactMatchesRatio(s1, news2);
            if (precision > 0.6)
                return precision;
            else
            {
                Logger.WriteError(s1.Path, "Movie couldn't be synced, Precision is "+precision);
            }
            return 0;
        }

        public double ErrorSentenceRatio(Subtitle sub)
        {
            Regex regex = new Regex("[a-zA-Z]+");
            Regex regexhe = new Regex("[א-ת]+");
            Regex exceptionsR = new Regex("(תורגם|צוות|קבוצת|תיקון|הגהה|QSUBS|אתר|TOREC|((w|W)(ww|WW))|(T|t)orec|(Q|q)subs)");
            HashSet<string> repetedwords = new HashSet<string>();    
            int errors = 0;
            foreach (var line in sub.Sentences)
            {
                MatchCollection m = regex.Matches(line.Text);
                if (m.Count > 0 && !exceptionsR.IsMatch(line.Text))
                {
                    errors++;
                }
            }
            double ratio = errors / (double)sub.Sentences.Count;
            return ratio;
        }


        public double ExactMatchesRatio(Subtitle s1In, Subtitle s2In)
        {
            Sentence[] src = s1In.Sentences.ToArray();
            Sentence[] dst = s2In.Sentences.ToArray();

            int countSync = 0;
            int countTotal = 0;
            int subMatchLoc = 0;
            for (int i = 0; i < src.Length; i++)
            {
                countTotal++;
                // Get Closest Starting Subtitle
                int startPoint = Math.Max(0, subMatchLoc - 4);
                for (int j = startPoint; j < dst.Length; j++)
                {
                    if (Math.Abs(src[i].Start - dst[j].Start) < SyncThreshold)
                    {
                        subMatchLoc = j;
                        countSync++;
                        break;
                    }
                }
            }
            return countSync / (double)countTotal;
        }

        private Subtitle GenerateArtificialSubtitle(double ratio,double offSet)
        {
 	        Subtitle ans = new Subtitle();
            ans.Sentences = new List<Sentence>();
            foreach (var item in s2.Sentences)
	        {
		        Sentence newSen = new Sentence();      
                newSen.Start = (int)((item.Start - offSet) / (ratio));
                newSen.Text = item.Text;
                if (ans.Sentences.Count>0)
                    newSen.TimeToNext = newSen.Start - ans.Sentences.Last().Start;
                ans.Sentences.Add(newSen);
	        }
            return ans;
        }

        
        /*  public TranSubtitle Align(int minimumSpace, out double precision)
          {
              List<int> bigSpaces_list1;
              List<int> bigSpaces_list2;
            
              List<Sentence> shortList1;
              List<Sentence> shortList2;

              Console.WriteLine("new file");
              do {
              bigSpaces_list1 = new List<int>();
              bigSpaces_list2 = new List<int>();

              shortList1 = new List<Sentence>();
              shortList2 = new List<Sentence>();

              foreach (var item in list1)
              {
                  if (item.m_TimeToNext > minimumSpace)
                  {
                      bigSpaces_list1.Add(item.m_TimeToNext);
                      shortList1.Add(item);
                  }
              }
              foreach (var item in list2)
              {
                  if (item.m_TimeToNext > minimumSpace)
                  {
                      bigSpaces_list2.Add(item.m_TimeToNext);
                      shortList2.Add(item);
                  }
              }

              minimumSpace = minimumSpace + 100;

              } while (bigSpaces_list1.Count>15 || bigSpaces_list2.Count>15);


              Tuple<int, int, int, int> settings = GetBestDeviation(bigSpaces_list1, bigSpaces_list2);
              int src1 = shortList1[settings.Item1].m_Start;
              int src2 = shortList1[settings.Item2].m_Start;
              int trg1 = shortList2[settings.Item3].m_Start;
              int trg2 = shortList2[settings.Item4].m_Start;
              //Logger.WriteLine(shortList1[settings.Item1].m_Text + "--->" + shortList2[settings.Item3].m_Text);
              //Logger.WriteLine(shortList1[settings.Item2].m_Text + "--->" + shortList2[settings.Item4].m_Text);
              double ratio = (trg1-trg2)/(double)(src1-src2);

              double offSet = trg2-src2* ratio;
              Console.WriteLine("size1= " + shortList1.Count + " size2= " + shortList2.Count);

              Subtitle newSubtitle =  GenerateArtificialSubtitles(ratio,offSet);

              TranSubtitle tr =  ExtractAlinedSentences(s1, newSubtitle, out precision);

              return tr;
          }
          */


        public Subtitle GetFirstSyncedSub()
        {
            return s1;
        }

        public Subtitle GetSecondSyncedSub()
        {
            return news2;
        }

        /*internal TranSubtitle GetTranslations()
        {
            TranSubtitle tr = ExtractAlinedSentences(s1, newSubtitle, out precision);

            return tr;
        }*/

        public TranSubtitle GetTranslations()
        {
            ExtractorMethod exMethod = new FlowExtractor(s1, news2, SyncThreshold);
            //ExtractorMethod exMethod = new ExectAlignmentExtractor(s1, news2, SyncThreshold);
            return exMethod.Extract();
        }
    }
}
