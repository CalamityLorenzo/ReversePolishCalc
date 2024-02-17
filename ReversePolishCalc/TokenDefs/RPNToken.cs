
namespace ReversePolishCalc.TokenDefs
{
    public class RPNToken(string originalVal)
    {
        public String OriginalValue { get; } = originalVal;
    }

    public class NumberRPNToken : RPNToken
    {
        public NumberRPNToken(string originalDigits) : base(originalDigits)
        {
            processInput(originalDigits);
        }

        private void processInput(string originalVal)
        {
            this.NumberValue = double.Parse(originalVal);
        }

        public double NumberValue { get; private set; }
    }

    public class OperatorRPNToken(string operatorVal) : RPNToken(operatorVal);

    public class WhiteSpaceToken(): RPNToken("");
}
