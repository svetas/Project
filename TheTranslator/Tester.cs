using BLEUevaluator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TheTranslator.Evaluators;
using TheTranslator.Extractors;
using TheTranslator.ImproveMethods;

namespace TheTranslator
{
    public class Tester
    {
        private Extractor m_extractor;
        private Statistics m_stats;

        /*public bool Init(string path)
        {
            try
            {
                m_stats = new PermutationsStatistics();
                m_extractor = new ExtractorNaive(path);
                m_extractor.build(ref m_stats);
                return true;
            }
            catch
            {
                return false;
            }
        }*/

        public bool InitWithDistance(string path)
        {
           // m_stats = new DistanceStatistics();
            /*m_extractor = new ExtractorDirect(path);
            m_extractor.build();
            m_extractor.Enhance();*/
            return true;
        }
        public string testSentenceDistanceAndPermutations(string item)
        {
            Combiner c = new CombinerNaive();
            Evaluator EV = new EvaluatorProbability((DistanceStatistics)m_stats);
            List<List<Sentence>> sentences = m_extractor.extractTransParts(item);
            List<TranslationOption> transOptions = c.combine(sentences, item);
            string trans = EV.GetBestTranslation(transOptions);
            return trans;
        }

        internal void CompareSystems(string reference, string system1, string system2)
        {
            Console.WriteLine("Comparing systems 1#: " + system1 + " vs 2#: " + system2);
            BLEU bleu = new BLEU();
            StreamReader srSource = new StreamReader(reference);
            StreamReader srSystemFirst = new StreamReader(system1);
            StreamReader srSystemSecond = new StreamReader(system2);
            StreamWriter signOutput = new StreamWriter(@"D:\tes\signTest.txt");
            double firstBetter = 0;
            double secondBetter = 0;
            int counter = 0;
            while (!srSource.EndOfStream)
            {
                string refe = srSource.ReadLine();
                double bleuFirst = BLEU.Evaluate(srSystemFirst.ReadLine(), new List<string> { refe });
                double bleuSecond = BLEU.Evaluate(srSystemSecond.ReadLine(), new List<string> { refe });
                counter++;


                if (bleuFirst > bleuSecond)
                {
                    firstBetter++;
                    signOutput.WriteLine(-1);
                } else if (bleuFirst < bleuSecond)
                {
                    secondBetter++;
                    signOutput.WriteLine(1);
                } else
                {
                    signOutput.WriteLine(0);
                    firstBetter += 0.5;
                    secondBetter += 0.5;
                }

            }
            
            Console.WriteLine("System "+ system1 + " is better then system "+ system2+ " with confidence of: " + SignTest.CalcConfidence((int)firstBetter, (int)firstBetter + (int)secondBetter));
            Console.WriteLine("System "+ system1 + " is better then system "+ system2+ " with confidence of: " + SignTest.CalcConfidence((int)secondBetter, (int)firstBetter + (int)secondBetter));
            srSource.Close();
            srSystemFirst.Close();
            srSystemSecond.Close();
            signOutput.Close();
        }


        public int LevenshteinDistance(string s, string t)
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
        }



        internal void testMosesImprovment(string sourceEn, string sourceHe, string moses, string output,ImprovementMethod improveMethod)
        {
            //while (true)
            //{
            StreamReader srEN = new StreamReader(sourceEn);
            StreamReader srHe = new StreamReader(sourceHe);
            StreamReader srM = new StreamReader(moses);
            StreamWriter sw = new StreamWriter(output);
            StreamWriter stats = new StreamWriter(@"D:\tes\stats.txt");
            stats.WriteLine("bleuBetter,jaccardMosesOurs,levinstain,timeRepeated,lengthDifference");
            Regex rxHeb = new Regex(@"[א-ת]");
            string trans;
            int counter = 0, total = 0;

            while (!srHe.EndOfStream)
            {
                if (total % 100 == 0)
                {
                    Console.WriteLine(total + " updated: " + counter);
                }
                string lineM = srM.ReadLine();
                string item = srHe.ReadLine();
                string itemEnglish = srEN.ReadLine();

                total++;

                trans = m_extractor.ExtractExactTranslation(item, 0);
                int x;
                string chosen = improveMethod.ChooseBetter(trans, lineM,"",out x);

                if (chosen != lineM)
                    counter++;

                sw.WriteLine(chosen);


                // sveta algorithm 

                /*bool mosesB = true;
                trans = m_extractor.ExtractExactTranslation(item, 0);
                if (trans.Length > 0)
                {
                    //if (trans.Split(' ').Length < 5 && m_extractor.GetTimesRepeated(item, trans) > 50)
                    //{
                    //    mosesB = false;
                    //}
                    if (trans.Split(' ').Length >= 5 && Math.Abs(trans.Split(' ').Length - source.Split(' ').Length) <=2)
                    {
                        mosesB = false;
                    }
                    if (trans.Split(' ').Length >= 5 && LevenshteinDistance(trans,lineM)<lineM.Length/4)
                    {
                        mosesB = false;
                    }
                    if (trans.Split(' ').Length > 3 && m_extractor.GetTimesRepeated(item, trans) > 1)
                    {
                        mosesB = false;
                    }
                    if (trans == lineM.TrimEnd(' '))
                    {
                        mosesB = true;
                    }
                }
                if (mosesB)
                    sw.WriteLine(lineM);
                else {
                    sw.WriteLine(trans);
                    counter++;
                }
                */
                /*
                //**********************************************
                // score: 22
                trans = m_extractor.ExtractExactTranslation(item, 0);
                double levinstain = LevenshteinDistance(trans, lineM);

                if (trans == null || trans.Length == 0)
                {
                    if (levinstain <=1.5)
                    {
                        sw.WriteLine(trans);
                        counter++;
                    } else
                    {
                        sw.WriteLine(lineM);
                    }
                } else
                {
                    sw.WriteLine(lineM);
                }
                //**********************************************
                */
                /*
                trans = m_extractor.ExtractExactTranslation(item, 0);

                double bleuOur = BLEU.Evaluate(trans, new List<string>() { itemEnglish });
                double bleuMoses = BLEU.Evaluate(lineM, new List<string>() { itemEnglish });
                int bleuBetter = 0;
                if (bleuOur > bleuMoses)
                    bleuBetter = 1;
                else if (bleuMoses > bleuOur)
                    bleuBetter = -1;

                double jaccardMosesOurs = CalcJaccard(trans, lineM);

                double levinstain = LevenshteinDistance(trans, lineM);

                int timeRepeated = m_extractor.GetTimesRepeated(item, trans);

                int lengthDifference = Math.Abs(trans.Split(' ').Length - item.Split(' ').Length);


                

                if (trans == null || trans.Length == 0)
                    sw.WriteLine(lineM);
                else {
                    sw.WriteLine(trans);
                    stats.WriteLine(bleuBetter + "," + jaccardMosesOurs + "," + levinstain + "," + timeRepeated + "," + lengthDifference);
                    counter++;
                }
                */


                /*
                //mine, 66

                trans = m_extractor.ExtractExactTranslation(item, 1);

                if (trans == null || trans.Length == 0)
                {
                    sw.WriteLine(lineM);
                }
                else if (rxHeb.IsMatch(lineM))
                {
                    sw.WriteLine(trans);
                    counter++;
                }
                else if (trans.Split(' ').Length > 3)
                {
                    sw.WriteLine(trans);
                    counter++;
                }
                else
                {
                    sw.WriteLine(lineM);
                }*/



                /*trans = m_extractor.ExtractExactTranslation(item, 0);
                if (trans.Length > 0)
                    if (trans.Split(' ').Length < 5)
                    {
                        if (m_extractor.GetTimesRepeated(item,trans) > 15)
                        {
                            sw.WriteLine(trans);
                            counter++;
                        } else
                        {
                            sw.WriteLine(lineM);
                        }
                    }
                    else if (Math.Abs(trans.Split(' ').Length - source.Split(' ').Length)<3)
                    {
                        sw.WriteLine(trans);
                        counter++;
                    } else
                    {
                        sw.WriteLine(lineM);
                    }
                else
                    sw.WriteLine(lineM);

            */


                /* To be tested! transMany = m_extractor.ExtractExactTranslation(item, 1);
                transExists = m_extractor.ExtractExactTranslation(item, 0);

                if (transExists.Length > 0 && transExists.Split(' ').Length > 5)
                {
                    sw.WriteLine(transExists);
                } else if (transMany.Length > 0 && transMany.Split(' ').Length > 3)
                {
                    sw.WriteLine(transMany);
                } else
                    sw.WriteLine(lineM);*/

                /*trans = m_extractor.ExtractExactTranslation(item,1);

                if (trans == null || trans.Length == 0)
                {
                    sw.WriteLine(lineM);
                }
                else if (rxHeb.IsMatch(lineM))
                {
                    sw.WriteLine(trans);
                    counter++;
                }
               //else if (lineM.Split(' ').Length > trans.Split(' ').Length)
               // {
               //     sw.WriteLine(lineM);
               // } else
               // {
               //     sw.WriteLine(trans);
               //     counter++;
               // }
                    else if (trans.Split(' ').Length>3) {
                        sw.WriteLine(trans);
                        counter++;
                    } else
                    {
                        sw.WriteLine(lineM);
                    }
                }*/

            }


            Console.WriteLine("We improved " + counter + "/" + total + ", thats " + counter / (double)total + "!");
            sw.Close();
            srHe.Close();
            srM.Close();
            stats.Close();
            //}
            //Console.ReadKey();
        }

        private double CalcJaccard(string trans, string lineM)
        {
            List<string> l1 = trans.Split(' ').ToList();
            List<string> l2 = lineM.Split(' ').ToList();

            int mone = l1.Intersect(l2).Count();
            int mechane = l1.Union(l2).Count();

            return mone / (double)mechane;


        }

        public void testLenX(string testFilesPath, int x)
        {
            Combiner c = new CombinerNaive();
            Evaluator EV = new EvaluatorConnectivity((PermutationsStatistics)m_stats);
            string path = testFilesPath + "\\Len" + x;
            StreamReader soReader = new StreamReader(path + @"\Downloaded.he");
            StreamWriter soWrite = new StreamWriter(path + @"\ans.txt");
            string[] allLine = soReader.ReadToEnd().Split(new string[] { "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in allLine)
            {
                //soWrite.WriteLine(item);
                List<List<Sentence>> sentences = m_extractor.extractTransParts(item);
                List<TranslationOption> transOptions = c.combine(sentences, item);
                string trans = EV.GetBestTranslation(transOptions);
                if (trans == null || trans.Length == 0)
                    trans = "fail:(";
                soWrite.WriteLine(trans);
            }

            soWrite.Close();
            soReader.Close();


        }
        /*public static bool testWordData()
        {
            //WordList s = new WordList("short");
            //WordList l = new WordList("long");
            Sentence s2 = new Sentence("two", 2);
            Sentence s10 = new Sentence("ten", 10);
            Sentence s1 = new Sentence("one", 1);
            Sentence s3 = new Sentence("3", 3);
            Sentence s4 = new Sentence("4", 4);
            Sentence s12 = new Sentence("10+two", 12);
            Sentence s15 = new Sentence("10+5", 15);

            //s.addSentence(s2);
            //s.addSentence(s10);
            //
            //l.addSentence(s1);
            //l.addSentence(s2);
            //l.addSentence(s3);
            //l.addSentence(s4);
            //
            //List<Sentence> ans = l.getItemsInCommon(s);
            ////long list ends first
            //if (ans.Count != 1 || ans[0].m_id != (2))
            //    return false;
            //
            //l.addSentence(s12);
            //ans = s.getItemsInCommon(l);
            ////short list ends first
            //if (ans.Count != 1 || ans[0].m_id != (2))
            //    return false;
            //
            //s.addSentence(s15);
            //l.addSentence(s15);
            //ans = s.getItemsInCommon(l);
            ////lists end at the same time
            //if (ans.Count != 2 || ans[0].m_id != (2) || ans[1].m_id != (15))
            //    return false;
            //
            //return true;
        }*/


       /* public bool testExtractor()
        {
            Extractor e = new ExtractorNaive(@"C:\studies\project\DB");
            return e.build(ref m_stats);

        }*/
        public bool testLargeDataBase(string testPath,string outputPath)
        {
            // using sveta's implementations
            StreamWriter sw = new StreamWriter(outputPath);
            Combiner c = new CombinerNaive();
            SeperatingCombiner sc = new SeperatingCombiner(m_extractor);
            Evaluator EV = new EvaluatorNaive((DistanceStatistics)m_stats);
            int total = 0;
            int counter = 0;
            StreamReader sr = new StreamReader(testPath);
            string trans;
            while (!sr.EndOfStream)
            {
                string item = sr.ReadLine();
                counter = 0;
                total++;

                //List<List<Sentence>> sentences = m_extractor.extractTransParts(item);
                //List<TranslationOption> transOptions = c.combine(sentences, item);       
                //trans = EV.GetBestTranslation(transOptions);

                trans = m_extractor.ExtractExactTranslation(item,0);
                if (trans=="")
                {
                    trans = sc.SeperateTranslate(item, 2);
                }
                 if (trans == "")
                {
                    trans = sc.SeperateTranslate(item, 3);
                }
                 if (trans == "")
                {
                    trans = sc.SeperateTranslate(item, 4);
                }
                 if (trans == null || trans.Length == 0)
                {
                    sw.WriteLine("UNK UNK UNK UNK UNK");
                }else 
                {
                    
                    sw.WriteLine(trans.TrimStart(' '));
                }
            }
            Console.WriteLine(counter + "/" + total);
            sw.Close();
            return true;
        }

       /* public void testExtractor2()
        {
            Extractor e = new ExtractorNaive(@"C:\studies\project\DB");
            e.build(ref m_stats);
            e.printTrans(e.extractTransParts("לא תודה"));
            e.printTrans(e.extractTransParts("שלום מה נשמע"));
        }*/

        /*public void testCombiner()
        {
            Extractor e = new ExtractorNaive(@"C:\studies\project\DB");
            e.build(ref m_stats);
            e.printTrans(e.extractTransParts("לא תודה"));
            e.printTrans(e.extractTransParts("שלום מה נשמע"));
            Combiner c = new CombinerNaive();
            c.combine(e.extractTransParts("לא תודה"), "לא תודה");
            c.combine(e.extractTransParts("שלום מה נשמע"), "שלום מה נשמע");


        }*/

        
        /*public void teastAll2()
        {
            string path = @"C:\studies\project\DB";
            Extractor e = new ExtractorNaive(path);
            e.build(ref m_stats);

            Combiner c = new CombinerNaive();
            Evaluator EV = new EvaluatorNaive(m_stats);

            //שמשיה
            string item = "שמשיה";
            Console.WriteLine(item);
            string trans = EV.GetBestTranslation(c.combine(e.extractTransParts(item), item));
            if (trans == null || trans.Length == 0)
                trans = "fail:(";
            Console.WriteLine(trans);


            //string item = "לא";
            item = "לא";
            Console.WriteLine(item);
            trans = EV.GetBestTranslation(c.combine(e.extractTransParts(item), item));
            if (trans == null || trans.Length == 0)
                trans = "fail:(";
            Console.WriteLine(trans);

            //string item = "ליסטיקאקספיאלידושס";
            item = "סופרקליפראגליסטיקאקספיאלידושס";
            Console.WriteLine(item);
            trans = EV.GetBestTranslation(c.combine(e.extractTransParts(item), item));
            if (trans == null || trans.Length == 0)
                trans = "fail:(";
            Console.WriteLine(trans);

            //string item = "יקר מידי";
            item = "יקר מידי";
            Console.WriteLine(item);
            //Console.WriteLine((e.getWordTranslation(item.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)[0]).m_target));
            trans = EV.GetBestTranslation(c.combine(e.extractTransParts(item), item));
            if (trans == null || trans.Length == 0)
                trans = "fail:(";
            Console.WriteLine(trans);

            //string item = "האם נצליח לתרגם";
            item = "  האם נצליח   לתרגם ";
            Console.WriteLine(item);
            trans = EV.GetBestTranslation(c.combine(e.extractTransParts(item), item));
            if (trans == null || trans.Length == 0)
                trans = "fail:(";
            Console.WriteLine(trans);
        }*/



            /*
        public void teastAll3()
        {
            string path = @"C:\studies\project\DB";
            Extractor e = new ExtractorNaive(path);
            e.build(ref m_stats);

            Combiner c = new CombinerNaive();
            Evaluator EV = new EvaluatorNaive(m_stats);

            //שמשיה
            string item = "היכן מצאת יופי כזה";
            Console.WriteLine(item);
            string trans = EV.GetBestTranslation(c.combine(e.extractTransParts(item), item));
            if (trans == null || trans.Length == 0)
                trans = "fail:(";
            Console.WriteLine(trans);
        }*/

        public bool testSentence(string item)
        {
            try
            {
                Combiner c = new CombinerNaive();
                Evaluator EV = new EvaluatorNaive(m_stats);
                List<List<Sentence>> sentences = m_extractor.extractTransParts(item);
                List<TranslationOption> transOptions = c.combine(sentences, item);
                string trans = EV.GetBestTranslation(transOptions);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
