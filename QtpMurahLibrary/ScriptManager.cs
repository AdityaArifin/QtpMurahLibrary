using QtpMurahLibrary.Interface;
using QtpMurahLibrary.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace QtpMurahLibrary
{
    public class ScriptManager: IEnumerable<string>
    {
        private string filePath;
        private IScriptTokenizer scriptTokenizer;
        public IList<TokenPair> tokenPairs;
        public IList<string> Lines { get; private set; }

        public ScriptManager(string filePath, IScriptTokenizer scriptTokenizer)
        {
            this.filePath = filePath;
            this.scriptTokenizer = scriptTokenizer;
        }

        public IEnumerator<string> GetEnumerator()
        {
            return (this.Lines.GetEnumerator());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Read()
        {
            if(!File.Exists(this.filePath))
            {
                throw new FileNotFoundException("File " + filePath + " does not exist.");
            }
            var extensions = scriptTokenizer.Extensions;
            var fileExtension = Path.GetExtension(filePath);
            if (!extensions.Contains(fileExtension)) throw new NotSupportedException("File " + fileExtension + " is not supported.");

            using (FileStream stream = new FileStream(this.filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                ReadProcedure(stream);
            }
        }

        public IList<string> Convert(IScriptConverter scriptConverter)
        {
            return scriptConverter.Convert(tokenPairs, Lines);
        }

        private void ReadProcedure(FileStream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            using (StreamReader reader = new StreamReader(stream))
            {
                List<string> result = new List<string>();
                List<TokenPair> tokenPairs = new List<TokenPair>();
                int index = -1;
                while (!reader.EndOfStream)
                {
                    index++;
                    var line = reader.ReadLine();
                    result.Add(line);
                    if (!this.scriptTokenizer.IsLineConvertable(line)) continue;
                    if (!this.scriptTokenizer.IsLineUploadable(line)) continue;
                    var tokenPair = scriptTokenizer.ExtractToken(index, line);
                    tokenPairs.Add(tokenPair);
                }
                reader.Dispose();
                this.Lines = result;
                this.tokenPairs = tokenPairs;
            }
            stream.Close();
        }
    }

    public class ScriptManagerEnumerator : IEnumerator<string>
    {
        public string Current => this.List[index];

        object IEnumerator.Current => Current;
        private IList<string> List;
        private int index;

        public ScriptManagerEnumerator(IList<string> list)
        {
            this.List = list;
            this.index = 0;
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            this.index++;
            return index < this.List.Count;
        }

        public void Reset()
        {
            this.index = 0;
        }
    }
}
