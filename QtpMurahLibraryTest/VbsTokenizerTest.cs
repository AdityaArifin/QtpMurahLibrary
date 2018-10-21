using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QtpMurahLibrary;

namespace QtpMurahLibraryTest
{
    [TestClass]
    public class VbsTokenizerTest
    {
        private VbsTokenizer vbsTokenizer;
        private List<string> convertableLines;
        private List<string> unconvertableLines;
        private List<string> uploadableLines;
        private List<string> unUploadableLines;

        [TestInitialize]
        public void Initialize()
        {
            this.convertableLines = new List<string>
            {
                "session.findById(\"wnd[0]/usr/tblSAPMV60ATCTRL_ERF_FAKT/ctxtKOMFK-VBELN[0,0]\").text = \"/9000001116\"",
                "session.findById(\"wnd[0]/usr/ctxtVBAK-VBELN\").text = \"4000000472\"",
                "session.findById(\"wnd[0]/usr/tabsTAXI_TABSTRIP_OVERVIEW/tabpT\\01/ssubSUBSCREEN_BODY:SAPMV45A:4400/subSUBSCREEN_TC:SAPMV45A:4900/tblSAPMV45ATCTRL_U_ERF_AUFTRAG/ctxtVBAP-PRCTR[53,0]\").text = \"70151A1\""
            };

            this.unconvertableLines = new List<string>
            {
                "session.findById(\"wnd[0]\").maximize",
                "If Not IsObject(session) Then",
                "session.findById(\"wnd[0]/tbar[0]/okcd\").text = \"/nvf01\"",
                "aushdlasuhdlaisudhlasiudhasludhasludh",
                "session.findById(\"wnd[0]/usr/ctxtVBAK-VBELN\").caretPosition = 10"
            };
            this.vbsTokenizer = new VbsTokenizer();
        }

        [TestMethod]
        public void IsLineConvertable()
        {
            foreach(string line in this.convertableLines)
            {
                Assert.IsTrue(this.vbsTokenizer.IsLineConvertable(line), "Error on test data: " + line);
            }

            foreach(string line in this.unconvertableLines)
            {
                Assert.IsFalse(this.vbsTokenizer.IsLineConvertable(line), "Error on test data: " + line);
            }

        }

        [TestMethod]
        public void IsLineUploadable()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void ExtractToken()
        {
            Assert.Fail();
        }
    }
}
