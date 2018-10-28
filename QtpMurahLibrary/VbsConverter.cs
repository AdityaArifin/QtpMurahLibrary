using QtpMurahLibrary.Interface;
using QtpMurahLibrary.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace QtpMurahLibrary
{
    public class VbsConverter : IScriptConverter
    {
        public IList<string> Convert(IList<TokenPair> tokenPairs, IList<string> lines)
        {
            IList<string> convertedLines = new List<string>();
            IList<string> convertedValues = new List<string>();
            bool isForEachPrinted = false;

            for (var i = 0; i < lines.Count; i++)
            {
                var line = lines[i];
                var token = tokenPairs[i];

                if (token == null)
                {
                    isForEachPrinted = ConvertNull(convertedLines, isForEachPrinted, line);
                    continue;
                }
                string convertedValue = token.Field.Value;
                convertedValue = RenameIfDuplicated(convertedValues, token, convertedValue);
                line = line.Remove(token.Value.StartCharacter - 1, token.Value.Length + 2);
                convertedValues.Add(convertedValue);
                token.Field.Value = convertedValue;
                convertedValue = "tableRow.Cells(1, Evaluate(\"UploadData\" & \"[" + convertedValue + "]\").Column)";
                line = line + convertedValue;
                convertedLines.Add(line);
            }
            convertedLines.Add("Next tableRow");
            return convertedLines;
        }

        private static string RenameIfDuplicated(IList<string> convertedValues, TokenPair token, string convertedValue)
        {
            int index = 1;
            while (convertedValues.Contains(convertedValue))
            {
                index++;
                convertedValue = token.Field.Value + index;
            }

            return convertedValue;
        }

        private static bool ConvertNull(IList<string> convertedLines, bool isForEachPrinted, string line)
        {
            Regex regex = new Regex("^session");
            MatchCollection matches = regex.Matches(line);
            if (matches.Count > 0 && !isForEachPrinted)
            {
                isForEachPrinted = true;
                convertedLines.Add("For Each tableRow In Range(\"UploadData\").Rows");
            }
            convertedLines.Add(line);
            return isForEachPrinted;
        }
    }
}
