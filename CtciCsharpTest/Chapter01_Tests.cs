using System;
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
        Ch01 C = new Ch01();

        private string _input;
        private string _expected;

        private void Verify()
        {
            if (_input == null || _expected == null)
            {
                throw new ArgumentNullException();
            }
            if (_input.Length < _expected.Length)
            {
                throw new ArgumentOutOfRangeException();
            }

            char[] inputArray = _input.ToCharArray();
            char[] expectedArray = _expected.ToCharArray();

            int trueLength = inputArray.Length;
            while (trueLength > 0 && Char.IsWhiteSpace(inputArray[trueLength-1]))
                trueLength--;

            C.Q03_URLify(inputArray, trueLength);

            Assert.AreEqual(expectedArray.Length, inputArray.Length);
            for (int i = 0; i < expectedArray.Length; i++)
            {
                Assert.AreEqual(expectedArray[i], inputArray[i]);
            }
        }

        [TestInitialize]
        public void Init()
        {
            _input = null;
            _expected = null;
        }

        [TestMethod]
        public void Sample()
        {
            _input    = "Mr John Smith    ";
            _expected = "Mr%20John%20Smith";
            Verify();
        }

        [TestMethod]
        public void Basic1()
        {
            _input    = " a  ";
            _expected = "%20a";
            Verify();
        }

        [TestMethod]
        public void Basic2()
        {
            _input    = "  a    ";
            _expected = "%20%20a";
            Verify();
        }

        [TestMethod]
        public void AllWhiteSpace()
        {
            _input    = "     ";
            _expected = "     ";
            Verify();
        }

        [TestMethod]
        public void NoWhiteSpace()
        {
            _input    = "abcde";
            _expected = "abcde";
            Verify();
        }

        [TestMethod]
        public void ConsecutiveSpaces()
        {
            _input    = "a  b   c          ";
            _expected = "a%20%20b%20%20%20c";
            Verify();
        }
    }

    //[TestClass]
    //public class Q04_PalindromePermutation_Tests
    //{
        
                    
    //    [TestMethod]
    //    public void Sample()
    //    {

    //    }
    //}
}
