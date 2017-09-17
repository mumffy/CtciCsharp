using CtciCsharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CtciCsharpTest
{
    [TestClass]
    public class Q01_IsUnique_Tests
    {
        Ch01 _c;

        public Q01_IsUnique_Tests()
        {
            _c = new Ch01();
        }

        [TestMethod]
        public void Basics()
        {
            Assert.IsTrue(_c.Q01_IsUnique("abc"));
            Assert.IsFalse(_c.Q01_IsUnique("aaa"));
        }

        [TestMethod]
        public void StrangeCases() // need confirmation from IVer
        {
            Assert.IsTrue(_c.Q01_IsUnique(""));
        }
    }

    [TestClass]
    public class Q01X_IsUnique_NoAddDSes_Tests
    {
        [TestMethod]
        public void Basics()
        {
            Assert.IsTrue(true);
        }
    }

    [TestClass]
    public class Q02_CheckPermutation_Tests
    {
        Ch01 _c = new Ch01();

        [TestMethod]
        public void Basics()
        {
            Assert.IsTrue(_c.Q02_CheckPermutation("abc", "cba"));
            Assert.IsTrue(_c.Q02_CheckPermutation("abc", "abc"));
            Assert.IsTrue(_c.Q02_CheckPermutation("aaa", "aaa"));

            Assert.IsFalse(_c.Q02_CheckPermutation("aaa", "aba"));
            Assert.IsFalse(_c.Q02_CheckPermutation("abc", "abd"));
            Assert.IsFalse(_c.Q02_CheckPermutation("abc", "xyz"));
        }

        [TestMethod]
        public void DifferentLengths()
        {
            Assert.IsFalse(_c.Q02_CheckPermutation("", "a"));
            Assert.IsFalse(_c.Q02_CheckPermutation("a", "aa"));
            Assert.IsFalse(_c.Q02_CheckPermutation("ab", "abb"));
        }

        [TestMethod]
        public void SomewhatStrange()
        {
            Assert.IsTrue(_c.Q02_CheckPermutation("aa ", "a a"));
            Assert.IsFalse(_c.Q02_CheckPermutation("aaa", "aAa"));
        }
    }

    [TestClass]
    public class Q03_URLify_Tests
    {
        Ch01 _c = new Ch01();

        [TestMethod]
        public void Sample()
        {
            Assert.Equals(@"Mr%20John%20Smith", _c.Q03_URLify("Mr John Smith".ToCharArray(), 13));
        }
    }
}
