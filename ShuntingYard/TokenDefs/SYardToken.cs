namespace ShuntingYard.TokenDefs
{
    public enum Associativity
    {
        Unknown,
        Left,
        Right,
        Both
    }

    public record SYardToken(string OriginalValue)
    {
    }

    public record SYardWhiteSpace : SYardToken
    {
        public SYardWhiteSpace() : base("") { }
    }

    public record SYardNumber(string OriginalValue) : SYardToken(OriginalValue)
    {
        public double Value { get; } = double.Parse(OriginalValue);
    }


    public record SYardOperator(string OriginalValue, Associativity Associativity, int Precedence) : SYardToken(OriginalValue);

}
