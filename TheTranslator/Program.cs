using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLEUevaluator;
using StackExchange.Redis;
using TheTranslator.GUI;

namespace TheTranslator
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }


           // test3();
            //TestBleu();
        

        private static double testBinom(int v1, int v2)
        {
            return SignTest.CalcConfidence(v1, v2);
        }

        /*public static void test()
        {
            Tester t = new Tester();
            //t.Init(@"C:\studies\project\DB\Big\17_new");
            //t.testLenX(@"C:\studies\project\DB\Big\17_new", 4);
            t.Init(@"Z:");
            t.testLenX(@"Z:", 4);
        }*/

        public static void test2()
        {
            Tester t = new Tester();
            t.InitWithDistance(@"Z:");
            //t.InitWithDistance(@"Z:\40000wordsNoPsik");
            t.testLargeDataBase(@"Z:\DownloadedFullTest.en-he.true.he", @"Z:\OurTranslatedafterEnhance.en");
        }

        public static void test3()
        {
            /*Tester t = new Tester();
            t.LoadFlatStats(@"Z:");           
            t.testMosesImprovment(@"Z:\DownloadedFullTest.en-he.true.en",@"Z:\DownloadedFullTest.en-he.true.he",@"Z:\MosesTranslated.en", @"Z:\MosesAfterImprovedByOur.en");
            t.CompareSystems(@"Z:\DownloadedFullTest.en-he.true.en", @"Z:\MosesTranslated.en", @"Z:\MosesAfterImprovedByOur.en");
            Console.ReadKey();*/
        }
        public static void testRedis()
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
            IDatabase db = redis.GetDatabase();
            string value = "abcdefg";
            db.StringSet("mykey", value);
            value = db.StringGet("mykey");
            Console.WriteLine(value); // writes: "abcdefg"
        }
        public static void TestBleu()
        {
            BLEU bleu = new BLEU();

            int[] result = new int[BLEU.getSuffStatCount()];

            string hyp = "This is a hypothesis sentence .";
            List<string> refs = new List<string>();
            refs.Add("This is the first reference sentence .");
            refs.Add("This is the second reference sentence .");
            refs.Add("This is an hypothesis sentence .");
            Console.WriteLine(BLEU.Evaluate(hyp, refs));
            Console.ReadKey();
        }
    }
}
