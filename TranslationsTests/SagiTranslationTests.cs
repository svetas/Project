using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheTranslator;

namespace TranslationsTests
{
    [TestClass]
    public class SagiTranslationTests
    {
        Tester test;

        [TestInitialize]
        public void Init()
        {
            test = new Tester();
            test.InitWithDistance(@"Z:");
        }

        [TestMethod]
        public void TestSetLen10()
        {
            Assert.IsTrue(test.testLargeDataBase(@"Z:\Len10\Downloaded.he"));
        }
        [TestMethod]
        public void TestTranslation()
        {
            string item = "בפעם הבאה, אל תזמיני רק מנה אחת";
            Assert.IsTrue(test.testSentenceDistanceAndPermutations(item));
        }
        [TestMethod]
        public void SingleSimpleWords()
        {
            Assert.IsTrue(test.testSentence("משעמם כאן"));
        }
    }
}
