using QtpMurahLibrary.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace QtpMurahLibrary
{
    public interface IScriptTokenizer
    {
        IList<string> Extensions { get; }
        TokenPair ExtractToken(int index, string line);
    }
}
