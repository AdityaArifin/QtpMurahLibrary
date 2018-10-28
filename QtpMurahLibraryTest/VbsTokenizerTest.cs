using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QtpMurahLibrary;
using QtpMurahLibrary.Model;

namespace QtpMurahLibraryTest
{
    [TestClass]
    public class VbsTokenizerTest
    {
        private VbsTokenizer vbsTokenizer;
        private List<string> convertableLines;
        private List<string> unConvertableLines;
        private List<string> tokenizableLines;
        private List<TokenPair> lineTokens;
        private List<string> unTokenizableLines;

        [TestInitialize]
        public void Initialize()
        {
            this.convertableLines = new List<string>
            {
                "session.findById(\"wnd[0]/usr/tblSAPMV60ATCTRL_ERF_FAKT/ctxtKOMFK-VBELN[0,0]\").text = \"/9000001116\"",
                "session.findById(\"wnd[0]/usr/ctxtVBAK-VBELN\").text = \"4000000472\"",
                "session.findById(\"wnd[0]/usr/tabsTAXI_TABSTRIP_OVERVIEW/tabpT\\01/ssubSUBSCREEN_BODY:SAPMV45A:4400/subSUBSCREEN_TC:SAPMV45A:4900/tblSAPMV45ATCTRL_U_ERF_AUFTRAG/ctxtVBAP-PRCTR[53,0]\").text = \"70151A1\""
            };

            this.unConvertableLines = new List<string>
            {
                "session.findById(\"wnd[0]\").maximize",
                "If Not IsObject(session) Then",
                "session.findById(\"wnd[0]/tbar[0]/okcd\").text = \"/nvf01\"",
                "aushdlasuhdlaisudhlasiudhasludhasludh",
                "session.findById(\"wnd[0]/usr/ctxtVBAK-VBELN\").caretPosition = 10",
                "If Not IsObject(application) Then",
                "Set SapGuiAuto  = GetObject(\"SAPGUISERVER\")",
                "Set application = SapGuiAuto.GetScriptingEngine",
                "End If",
                "If Not IsObject(connection) Then",
                "Set connection = application.Children(0)",
                ""
            };

            this.tokenizableLines = this.convertableLines;
            this.lineTokens = new List<TokenPair>()
            {
                new TokenPair(
                    new Token()
                    {
                        StartCharacter = 55,
                        Length = 20,
                        Line = 1,
                        Type = TokenType.FieldName,
                        Value = "ctxtKOMFK-VBELN[0,0]"
                    },
                    new Token()
                    {
                        StartCharacter = 86,
                        Length = 11,
                        Line = 1,
                        Type = TokenType.FieldValue,
                        Value = "/9000001116"
                    }
                    ),
                 new TokenPair(
                    new Token()
                    {
                        StartCharacter = 29,
                        Length = 14,
                        Line = 2,
                        Type = TokenType.FieldName,
                        Value = "ctxtVBAK-VBELN"
                    },
                    new Token()
                    {
                        StartCharacter = 54,
                        Length = 10,
                        Line = 2,
                        Type = TokenType.FieldValue,
                        Value = "4000000472"
                    }
                    ),
                 new TokenPair(
                    new Token()
                    {
                        StartCharacter = 159,
                        Length = 20,
                        Line = 3,
                        Type = TokenType.FieldName,
                        Value = "ctxtVBAP-PRCTR[53,0]"
                    },
                    new Token()
                    {
                        StartCharacter = 190,
                        Length = 7,
                        Line = 3,
                        Type = TokenType.FieldValue,
                        Value = "70151A1"
                    }
                    )
            };

            this.unTokenizableLines = this.unConvertableLines;
            this.vbsTokenizer = new VbsTokenizer();
        }

        [TestMethod]
        public void ExtractToken()
        {
            for (int i = 0; i < convertableLines.Count(); i++)
            {
                TokenPair tokenPair = this.vbsTokenizer.ExtractToken(i + 1, this.convertableLines[i]);
                Assert.AreEqual(this.lineTokens[i].Field, tokenPair.Field, "Error on test data: Field " + i);
                Assert.AreEqual(this.lineTokens[i].Value, tokenPair.Value, "Error on test data: Value " + i);
            };

            for (int i = 0; i < unConvertableLines.Count(); i++)
            {
                TokenPair tokenPair = this.vbsTokenizer.ExtractToken(i + 1, this.unConvertableLines[i]);
                Assert.IsNull(tokenPair, "Error on test data: Field " + i);
            };
        }
    }
}
