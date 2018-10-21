using System;
using System.Collections.Generic;
using System.Text;

namespace QtpMurahLibrary.Model
{
    public enum TokenType
    {
        FieldName, FieldValue
    }

    public class Token
    {
        public int startCharacter;
        public int lastCharacter;
        public int line;
        public string value;
        public TokenType type;
    }

    public class TokenPair
    {
        public Token Field { get; private set; }
        public Token Value { get; private set; }

        public TokenPair(Token field, Token value)
        {
            if (field.type != TokenType.FieldName) throw new Exception("Token type is not field");
            if (value.type != TokenType.FieldValue) throw new Exception("Token type is not value");
            this.Field = field;
            this.Value = value;
        }
    }
}
