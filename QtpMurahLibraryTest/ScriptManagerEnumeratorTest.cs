using Microsoft.VisualStudio.TestTools.UnitTesting;
using QtpMurahLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QtpMurahLibraryTest
{
    [TestClass]
    public class ScriptManagerEnumeratorTest
    {
        private ScriptManagerEnumerator TestableScriptManagerEnumerator;
        private List<string> ListForTest;

        [TestInitialize]
        public void Initialize()
        {
            this.ListForTest = new List<string>()
            {
                "string 1",
                "string 2",
                "string 3",
                "string 4",
                "string 5",
            };
            this.TestableScriptManagerEnumerator = new ScriptManagerEnumerator(ListForTest);
        }

        [TestMethod]
        public void MoveNextTest()
        {
            foreach (string line in ListForTest)
            {
                var currentLine = this.TestableScriptManagerEnumerator.Current;
                Assert.AreEqual(line, currentLine);
                this.TestableScriptManagerEnumerator.MoveNext();
            }
            var canMoveNext = this.TestableScriptManagerEnumerator.MoveNext();
            Assert.IsFalse(canMoveNext);
        }

        [TestMethod]
        public void ResetTest()
        {
            this.MoveNextTest();
            this.TestableScriptManagerEnumerator.Reset();
            this.MoveNextTest();
        }

    }
}
