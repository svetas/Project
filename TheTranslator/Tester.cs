using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheTranslator.Evaluators;
using TheTranslator.Extractors;

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
            m_extractor = new ExtractorDirect(path);
            m_extractor.build();
            m_extractor.Enhance();
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

                trans = m_extractor.ExtractExactTranslation(item);
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
