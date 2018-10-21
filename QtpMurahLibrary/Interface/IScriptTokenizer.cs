using QtpMurahLibrary.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace QtpMurahLibrary
{
    public interface IScriptTokenizer
    {
        IList<string> Extensions { get; }
        bool IsLineConvertable(string line);
        bool IsLineUploadable(string line); //ganti nama should i tokenize diz line?
        TokenPair ExtractToken(int index, string line);
    }
}
