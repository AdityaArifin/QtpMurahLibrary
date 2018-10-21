using Microsoft.VisualStudio.TestTools.UnitTesting;
using QtpMurahLibrary;
using QtpMurahLibrary.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace QtpMurahLibraryTest
{
    [TestClass]
    public class ScriptManagerTest : IScriptTokenizer
    {
        private ScriptManager TestableScriptManager;
        public IList<string> Extensions => new List<string> { ".vbs" };

        [TestInitialize]
        public void Initialize()
        {
            string currentPath = System.AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(currentPath, "ReadTest1.vbs");
            this.TestableScriptManager = new ScriptManager(filePath, this);
        }

        [TestMethod]
        public void ReadTest()
        {
            this.TestableScriptManager.Read();
            var index = 0;
            foreach (string line in this.TestableScriptManager)
            {
                index++;
                var expectedLine = "This is line " + index;
                Assert.AreEqual(expectedLine, line);
            }

            foreach (TokenPair tokenPair in this.TestableScriptManager.tokenPairs)
            {
                var value = tokenPair.Value;
                var field = tokenPair.Field;

                Assert.AreNotEqual("This is line 3", value.value);
                Assert.AreNotEqual("This is line 4", value.value);
                Assert.AreNotEqual("This is line 3", field.value);
                Assert.AreNotEqual("This is line 4", field.value);

            }
        }

        public bool IsLineConvertable(string line)
        {
            return line != "This is line 3";
        }

        public bool IsLineUploadable(string line)
        {
            return line != "This is line 4";
        }

        public TokenPair ExtractToken(int index, string line)
        {
            var value = new Token()
            {
                lastCharacter = 10,
                startCharacter = 1,
                line = index,
                type = TokenType.FieldValue,
                value = line
            };

            var field = new Token()
            {
                lastCharacter = 10,
                startCharacter = 1,
                line = index,
                type = TokenType.FieldName,
                value = line
            };

            return new TokenPair(field, value);
        }
    }
}
