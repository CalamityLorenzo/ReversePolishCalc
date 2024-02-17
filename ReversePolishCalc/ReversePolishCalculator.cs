using ReversePolishCalc.TokenDefs;

namespace ReversePolishCalc
{
    public class ReversePolishCalculator
    {
        private int _currentIdx;

        Stack<RPNToken> Output = new Stack<RPNToken>();
        Stack<RPNToken> Operators = new Stack<RPNToken>();
        public List<RPNToken> Tokens = new();
        private string calculationString;

        public double Parse(String calculation)
        {
            this.calculationString = calculation;
            if (calculationString.Length == 0) return 0;
            // WE gotta parse
            BuildTokens(calculationString);
            _currentIdx = 0;
            return ProcessCalculation();
        }

        private double ProcessCalculation()
        {
            // Take our tokens and convert into an actual fucking equation.
            foreach (var token in Tokens)
            {
                if (token is WhiteSpaceToken) continue;

                if (token is NumberRPNToken)
                {
                    Output.Push(token as NumberRPNToken);
                }
                else
                {
                    // We assume all these are currently binary operators
                    var right = Output.Pop() as NumberRPNToken;
                    var left = Output!.Pop()! as NumberRPNToken;
                    var expressionOutput = (token as OperatorRPNToken).OriginalValue switch
                    {
                        "+" => BinaryExpression(left!, right!, (a, b) => (a + b)),
                        "-" => BinaryExpression(left!, right!, (a, b) => (a - b)),
                        "/" => BinaryExpression(left!, right!, (a, b) => (a / b)),
                        "*" => BinaryExpression(left!, right!, (a, b) => (a * b)),
                        _ => throw new NotImplementedException((token as OperatorRPNToken).OriginalValue)
                    };

                    Output.Push(expressionOutput);
                }
            }
            return (Output.Pop() as NumberRPNToken).NumberValue;
        }


        private NumberRPNToken BinaryExpression(NumberRPNToken left, NumberRPNToken right, Func<double, double, double> operation)
        {
            var result = operation(left.NumberValue, right.NumberValue);
            return new(result.ToString());
        }

        private void BuildTokens(string calculation)
        {
            while (_currentIdx < calculation.Length)
            {
                RPNToken token = calculation[_currentIdx] switch
                {
                    var a when Char.IsAsciiDigit(a) => CreateNumber(calculation),
                    '+' => new OperatorRPNToken("+"),
                    '-' => new OperatorRPNToken("-"),
                    '*' => new OperatorRPNToken("*"),
                    '/' => new OperatorRPNToken("/"),
                    var b when char.IsWhiteSpace(b) || b == '\n' => new WhiteSpaceToken(),
                    _ => throw new Exception(),
                };

                this.Tokens.Add(token);
                this._currentIdx += 1;
            }
        }

        private NumberRPNToken CreateNumber(string calculation)
        {
            // Iterate the length of the string until not a number
            var counter = _currentIdx;
            while (Char.IsAsciiDigit(Peek()))
            {
                _currentIdx += 1;
            }
            return new NumberRPNToken(calculation.Substring(counter, _currentIdx - counter + 1));
        }

        char Peek()
        {
            if (_currentIdx < calculationString.Length - 1)
                return calculationString[_currentIdx + 1];
            else
                return '\0';
        }
    }
}
