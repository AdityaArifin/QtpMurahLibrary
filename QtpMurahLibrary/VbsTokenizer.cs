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
            Regex regex = new Regex("^session\\.findById\\(\\\"\\S+\\/(\\S+)\\\"\\)\\.text\\s*=\\s*\\\"(.+)\\\"\\s*$");
            MatchCollection matches = regex.Matches(line);
            if (matches.Count == 0) return null;
            foreach (Match match in matches)
            {
                GroupCollection groups = match.Groups;
                if (groups.Count < 3) return null;
                if (groups[1].Value == "okcd") return null;

                Token field = ExtractRegex(index, groups[1], TokenType.FieldName);
                Token value = ExtractRegex(index, groups[2], TokenType.FieldValue);
                return new TokenPair(field, value);

            }
            return null;
        }

        private Token ExtractRegex(int index, Group group, TokenType tokenType)
        {
            Token token = new Token
            {
                StartCharacter = group.Index,
                Length = group.Length,
                Line = index,
                Type = tokenType,
                Value = group.Value
            };
            return token;
        }
    }
}
