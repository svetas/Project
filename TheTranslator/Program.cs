using BLEUevaluator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheTranslator
{
    static class Program
    {
        static void Main()
        {
            //test2();
            TestBleu();
        }

        public static void test()
        {
            Tester t = new Tester();
            //t.Init(@"C:\studies\project\DB\Big\17_new");
            //t.testLenX(@"C:\studies\project\DB\Big\17_new", 4);
            t.Init(@"Z:");
            t.testLenX(@"Z:", 4);
        }
        public static void test2()
        {
            Tester t = new Tester();
            t.InitWithDistance(@"Z:\40000wordsNoPsik");
            t.testLargeDataBase(@"Z:\40000wordsNoPsik\Len4\Downloaded.low.he", @"Z:\40000wordsNoPsik\Len4\translated.low.he");
        }

        public static void TestBleu()
        {
            BLEU bleu = new BLEU();

            int[] result = new int[BLEU.getSuffStatCount()];

            List<string> hyp = "This is a hypothesis sentence .".Split(' ').ToList();
            List<List<string>> refs = new List<List<string>>();
            refs.Add("This is the first reference sentence .".Split(' ').ToList());
            refs.Add("This is the second reference sentence .".Split(' ').ToList());

            bleu.stats(hyp, refs, result);

            Console.WriteLine(string.Join(",", result));
            Console.WriteLine(bleu.score(result));
        }
    }
}
