namespace ShuntingYard.TokenDefs
{
    public enum Associativity
    {
        Unknown,
        Left,
        Right,
        Both
    }

    internal record SYardToken(string OriginalValue)
    {
    }

    internal record SYardWhiteSpace : SYardToken
    {
        public SYardWhiteSpace() : base("") { }
    }

    internal record SYardNumber(string OriginalValue) : SYardToken(OriginalValue)
    {
        public double Value { get; } = double.Parse(OriginalValue);
    }


    internal record SYardOperator(string OriginalValue, Associativity Associativity, int Order, Func<Stack<SYardToken> ) : SYardToken(OriginalValue);

}
