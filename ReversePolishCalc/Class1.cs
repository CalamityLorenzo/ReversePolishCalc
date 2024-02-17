using ReversePolishCalc.TokenDefs;
using System.Windows.Markup;

namespace ReversePolishCalc
{
    public class ReversePolishCalculator
    {
        private int _currentIdx;

        Stack<RPNToken> Ouput = new Stack<RPNToken>();
        Stack<RPNToken> Operators = new Stack<RPNToken>();

        public double Parse(String calculation)
        {
            if (calculation.Length == 0) return 0;
            // WE gotta parse
            BuildTokens(calculation);
            _currentIdx = 0;


            return 0f;
        }

        private void BuildTokens(string calculation)
        {
            while (_currentIdx < calculation.Length - 1)
            {
               RPNToken token = calculation[_currentIdx] switch { 
               var a  Char.IsAsciiDigit (a) => CreateNumber(calculation);
               case '+' => new OperaetorRPNToken("+");
                case _ => throw new Exception();
                }
            }
        }
    }
}
