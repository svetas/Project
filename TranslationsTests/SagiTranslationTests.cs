using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheTranslator;

namespace TranslationsTests
{
    [TestClass]
    public class SagiTranslationTests
    {
        static Tester test;

        [ClassInitialize]
        public static void Init(TestContext x)
        {
            test = new Tester();
            test.InitWithDistance(@"Z:");
        }

        [TestMethod]
        public void TestTranslation1()
        {
            string item = "זה לא בסדר";
            string trans = test.testSentenceDistanceAndPermutations(item);
            int score = ComputeLev("this is not right", trans);
            Assert.IsTrue(score < 3);
        }

        [TestMethod]
        public void TestTranslation2()
        {
            string item = "הוא ניסה להרוג אותי";
            string trans = test.testSentenceDistanceAndPermutations(item);
            int score = ComputeLev("he tried to kill me", trans);
            Assert.IsTrue(score < 3);
        }


        private int ComputeLev(string s, string t)
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
    }
}
