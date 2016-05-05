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
            test.Init(@"C:\studies\project\DB\Big\17_new");
        }

        [TestMethod]
        public void TestSetLen10()
        {
            Assert.IsTrue(test.testLargeDataBase(@"C:\studies\project\DB\Big\17_new\Len10\Downloaded.he"));
        }
        [TestMethod]
        public void TestTranslation()
        {
            Assert.IsTrue(test.testSentence("משעמם כאן"));
        }
        [TestMethod]
        public void SingleSimpleWords()
        {
            Assert.IsTrue(test.testSentence("משעמם כאן"));
        }
    }
}
