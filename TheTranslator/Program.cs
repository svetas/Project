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
            test2();
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
    }
}
