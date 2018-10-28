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
        public int StartCharacter { get; set; }
        public int Length { get; set; }
        public int Line { get; set; }
        public string Value { get; set; }
        public TokenType Type { get; set; }

        public override bool Equals(object obj)
        {
            var token = obj as Token;
            return token != null &&
                   StartCharacter == token.StartCharacter &&
                   Length == token.Length &&
                   Line == token.Line &&
                   Value == token.Value &&
                   Type == token.Type;
        }

        public override int GetHashCode()
        {
            var hashCode = 1699532497;
            hashCode = hashCode * -1521134295 + StartCharacter.GetHashCode();
            hashCode = hashCode * -1521134295 + Length.GetHashCode();
            hashCode = hashCode * -1521134295 + Line.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Value);
            hashCode = hashCode * -1521134295 + Type.GetHashCode();
            return hashCode;
        }
    }

    public class TokenPair
    {
        public Token Field { get; private set; }
        public Token Value { get; private set; }

        public TokenPair(Token field, Token value)
        {
            if (field.Type != TokenType.FieldName) throw new Exception("Token type is not field");
            if (value.Type != TokenType.FieldValue) throw new Exception("Token type is not value");
            this.Field = field;
            this.Value = value;
        }

        public override bool Equals(object obj)
        {
            var pair = obj as TokenPair;
            return pair != null &&
                   EqualityComparer<Token>.Default.Equals(Field, pair.Field) &&
                   EqualityComparer<Token>.Default.Equals(Value, pair.Value);
        }

        public override int GetHashCode()
        {
            var hashCode = 1214255753;
            hashCode = hashCode * -1521134295 + EqualityComparer<Token>.Default.GetHashCode(Field);
            hashCode = hashCode * -1521134295 + EqualityComparer<Token>.Default.GetHashCode(Value);
            return hashCode;
        }
    }
}
