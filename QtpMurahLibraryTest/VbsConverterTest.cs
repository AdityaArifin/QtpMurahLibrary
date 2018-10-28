using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QtpMurahLibrary;
using QtpMurahLibrary.Model;

namespace QtpMurahLibraryTest
{
    [TestClass]
    public class VbsConverterTest
    {
        List<string> originalLines;
        List<string> convertedOriginalLines;
        IList<TokenPair> originalTokenPairs;
        IList<TokenPair> convertedTokenPairs;
        VbsConverter vbsConverter;

        [TestInitialize]
        public void Initialize()
        {
            PrepareLines();
            this.originalTokenPairs = PrepareTokenPairs();
            this.convertedTokenPairs = PrepareConvertedTokenPairs();
            this.vbsConverter = new VbsConverter();
        }

        private static List<TokenPair> PrepareTokenPairs()
        {
            return new List<TokenPair>()
            {
                null,
                null,
                null,
                null,
                null,
                new TokenPair(
                    new Token
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
                    }),
                new TokenPair(
                    new Token
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
                    }),
                null,
                new TokenPair(
                    new Token
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
                    }),
                new TokenPair(
                    new Token
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
                    })
            };
        }
        private static List<TokenPair> PrepareConvertedTokenPairs()
        {
            return new List<TokenPair>()
            {
                null,
                null,
                null,
                null,
                null,
                new TokenPair(
                    new Token
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
                    }),
                new TokenPair(
                    new Token
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
                    }),
                null,
                new TokenPair(
                    new Token
                    {
                        StartCharacter = 29,
                        Length = 14,
                        Line = 2,
                        Type = TokenType.FieldName,
                        Value = "ctxtVBAK-VBELN2"
                    },
                    new Token()
                    {
                        StartCharacter = 54,
                        Length = 10,
                        Line = 2,
                        Type = TokenType.FieldValue,
                        Value = "4000000472"
                    }),
                new TokenPair(
                    new Token
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
                    })
            };
        }

        private void PrepareLines()
        {
            this.originalLines = new List<string>
            {
                "If Not IsObject(application) Then",
                "Set SapGuiAuto  = GetObject(\"SAPGUISERVER\")",
                "Set application = SapGuiAuto.GetScriptingEngine",
                "End If",
                "session.findById(\"wnd[0]/tbar[0]/okcd\").text = \"/nvf11\"",
                "session.findById(\"wnd[0]/usr/tblSAPMV60ATCTRL_ERF_FAKT/ctxtKOMFK-VBELN[0,0]\").text = \"/9000001116\"",
                "session.findById(\"wnd[0]/usr/ctxtVBAK-VBELN\").text = \"4000000472\"",
                "session.findById(\"wnd[0]\").sendVKey 11",
                "session.findById(\"wnd[0]/usr/ctxtVBAK-VBELN\").text = \"4000000475\"",
                "session.findById(\"wnd[0]/usr/tabsTAXI_TABSTRIP_OVERVIEW/tabpT\\01/ssubSUBSCREEN_BODY:SAPMV45A:4400/subSUBSCREEN_TC:SAPMV45A:4900/tblSAPMV45ATCTRL_U_ERF_AUFTRAG/ctxtVBAP-PRCTR[53,0]\").text = \"70151A1\""
            };

            this.convertedOriginalLines = new List<string>
            {
                "If Not IsObject(application) Then",
                "Set SapGuiAuto  = GetObject(\"SAPGUISERVER\")",
                "Set application = SapGuiAuto.GetScriptingEngine",
                "End If",
                "For Each tableRow In Range(\"UploadData\").Rows",
                "session.findById(\"wnd[0]/tbar[0]/okcd\").text = \"/nvf11\"",
                "session.findById(\"wnd[0]/usr/tblSAPMV60ATCTRL_ERF_FAKT/ctxtKOMFK-VBELN[0,0]\").text = tableRow.Cells(1, Evaluate(\"UploadData\" & \"[ctxtKOMFK-VBELN[0,0]]\").Column)",
                "session.findById(\"wnd[0]/usr/ctxtVBAK-VBELN\").text = tableRow.Cells(1, Evaluate(\"UploadData\" & \"[ctxtVBAK-VBELN]\").Column)",
                "session.findById(\"wnd[0]\").sendVKey 11",
                "session.findById(\"wnd[0]/usr/ctxtVBAK-VBELN\").text = tableRow.Cells(1, Evaluate(\"UploadData\" & \"[ctxtVBAK-VBELN2]\").Column)",
                "session.findById(\"wnd[0]/usr/tabsTAXI_TABSTRIP_OVERVIEW/tabpT\\01/ssubSUBSCREEN_BODY:SAPMV45A:4400/subSUBSCREEN_TC:SAPMV45A:4900/tblSAPMV45ATCTRL_U_ERF_AUFTRAG/ctxtVBAP-PRCTR[53,0]\").text = tableRow.Cells(1, Evaluate(\"UploadData\" & \"[ctxtVBAP-PRCTR[53,0]]\").Column)",
                "Next tableRow"
            };
        }

        [TestMethod]
        public void ConvertVbs()
        {
            IList<string> convertedLines = this.vbsConverter.Convert(this.originalTokenPairs, this.originalLines);
            for(int i = 0; i < this.convertedOriginalLines.Count; i++)
            {
                Assert.AreEqual(this.convertedOriginalLines[i], convertedLines[i], "Error on test data: " + i);
            }

            for(int i = 0; i < this.convertedTokenPairs.Count; i++)
            {
                Assert.AreEqual(this.convertedTokenPairs[i], this.originalTokenPairs[i], "Error on test data: " + i);
            }
        }
    }
}
