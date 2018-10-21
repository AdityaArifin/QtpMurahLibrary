using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using QtpMurahLibrary.Model;

namespace QtpMurahLibrary
{
    public class VbsTokenizer : IScriptTokenizer
    {
        public IList<string> Extensions => null;
        
        public TokenPair ExtractToken(int index, string line)
        {
            return null;
        }

        public bool IsLineConvertable(string line)
        {
            Regex regex = new Regex("^session\\.findById\\(\\\"\\S+\\/(\\S+)\\\"\\)\\.text\\s*=\\s*\\\"(.+)\\\"\\s*$");
            MatchCollection matches = regex.Matches(line);
            if (matches.Count == 0) return false;
            foreach (Match match in matches)
            {
                GroupCollection groups = match.Groups;
                if (groups.Count < 3) return false;
                if (groups[1].Value == "okcd") return false;
            }
            return true;
        }

        public bool IsLineUploadable(string line)
        {
            return false;
        }
    }
}
