using QtpMurahLibrary.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace QtpMurahLibrary.Interface
{
    public interface IScriptConverter
    {
        IList<string> Convert(IList<TokenPair> tokenPairs, IList<string> lines);
    }
}
