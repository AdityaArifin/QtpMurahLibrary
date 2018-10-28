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
                if (tokenPair == null) continue;
                var value = tokenPair.Value;
                var field = tokenPair.Field;

                Assert.AreNotEqual("This is line 3", value.Value);
                Assert.AreNotEqual("This is line 4", value.Value);
                Assert.AreNotEqual("This is line 3", field.Value);
                Assert.AreNotEqual("This is line 4", field.Value);

            }
        }

        public TokenPair ExtractToken(int index, string line)
        {
            if( line == "This is line 3") return null;
            if (line == "This is line 4") return null;
            var value = new Token()
            {
                Length = 10,
                StartCharacter = 1,
                Line = index,
                Type = TokenType.FieldValue,
                Value = line
            };

            var field = new Token()
            {
                Length = 10,
                StartCharacter = 1,
                Line = index,
                Type = TokenType.FieldName,
                Value = line
            };

            return new TokenPair(field, value);
        }
    }
}
