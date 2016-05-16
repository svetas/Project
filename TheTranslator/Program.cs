﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLEUevaluator;
using StackExchange.Redis;

namespace TheTranslator
{
    static class Program
    {
        static void Main()
        {
            test2();
            //TestBleu();
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
            t.testLargeDataBase(@"Z:\DownloadedFullTest.en-he.true.he", @"Z:\OurTranslated.en");
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

            List<string> hyp = "This is a hypothesis sentence .".Split(' ').ToList();
            List<List<string>> refs = new List<List<string>>();
            refs.Add("This is the first reference sentence .".Split(' ').ToList());
            refs.Add("This is the second reference sentence .".Split(' ').ToList());
            refs.Add("This is an hypothesis sentence .".Split(' ').ToList());
            bleu.stats(hyp, refs, result);

            Console.WriteLine(string.Join(",", result));
            Console.WriteLine(bleu.score(result));
            Console.ReadKey();
        }
    }
}
