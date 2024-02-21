

using ShuntingYard.TokenDefs;

namespace ShuntingYard
{
    public class ShuntingYardProcesor
    {
        private Dictionary<string, SYardOperator> _operatorAssociativity = new();

        private Stack<SYardToken> Ouput = new();
        private Stack<SYardOperator> Operator = new();
        public List<SYardToken> Tokens = new();

        public ShuntingYardProcesor()
        {
            BuildAssociativityDictionary();
        }

        private void BuildAssociativityDictionary()
        {
            _operatorAssociativity.Add("(", new SYardOperator("(", Associativity.Unknown, 9));
            _operatorAssociativity.Add(")", new SYardOperator(")", Associativity.Unknown, 9));
            _operatorAssociativity.Add("^", new SYardOperator("^", Associativity.Right, 3));
            _operatorAssociativity.Add("*", new SYardOperator("*", Associativity.Left, 3));
            _operatorAssociativity.Add("/", new SYardOperator("/", Associativity.Left, 3));
            _operatorAssociativity.Add("+", new SYardOperator("+", Associativity.Left, 2));
            _operatorAssociativity.Add("-", new SYardOperator("-", Associativity.Left, 2));
            _operatorAssociativity.Add("min", new SYardOperator("min", Associativity.Right, 2));
            _operatorAssociativity.Add("max", new SYardOperator("max", Associativity.Right, 2));
        }

        int _currentIdx = 0;
        string _input = "";
        string _output = "";
        public string Parse(string input)
        {
            Tokens.Clear();
            if (input.Length == 0) { return ""; }
            this._input = input;
            _currentIdx = 0;
            BuildTokens();
            ProcessTokens();


            return "";
        }


        private void BuildTokens()
        {
            while (_currentIdx < _input.Length)
            {
                SYardToken token = _input[_currentIdx] switch
                {
                    var a when Char.IsDigit(a) => CreateNumber(),
                    var a when Char.IsLetter(a) => CreateFunctionName(),
                    var a when _operatorAssociativity.ContainsKey(a.ToString()) => CreateOperator(),
                    var a when Char.IsWhiteSpace(a) => CreateWhiteSpace(),
                    _ => throw new NotImplementedException()
                }; ;
                this.Tokens.Add(token);
                _currentIdx++;
            }
        }
        private char Peek()
        {
            if (_currentIdx < _input.Length-1)
            {
                return _input[_currentIdx + 1];
            }
            else
            { return '\0'; }
        }

        private SYardToken CreateWhiteSpace()
        {
            while (Char.IsWhiteSpace(Peek()))
            {
                _currentIdx++;
            }
            return new SYardWhiteSpace();
        }



        // Has to be an existing operator.
        private SYardToken CreateOperator()
        {
            var currentChar = _input[_currentIdx];
            var selectedOperator = _operatorAssociativity[currentChar.ToString()];
            if (selectedOperator != null)
            {
                return selectedOperator;
            }
            throw new MissingMemberException($"Cannot find operator, : {currentChar}");
        }

        private SYardToken CreateFunctionName()
        {
            var startPos = _currentIdx;
            while (Char.IsLetter(Peek()))
            {
                _currentIdx++;
            }
            return new SYardOperator(_input.Substring(startPos, (_currentIdx - startPos) + 1), Associativity.Right, 000);

        }

        private SYardToken CreateNumber()
        {
            var startPos = _currentIdx;
            while (Char.IsDigit(Peek()))
            {
                _currentIdx++;
            }
            return new SYardNumber(_input.Substring(startPos, (_currentIdx - startPos) + 1));
        }

        /// <summary>
        ///  this is the actual algorithim in action
        /// </summary>
        void ProcessTokens()
        {
            //if (selectedOperator != null)
            //{
            //    // Peek throws if you don't have a value
            //    if (this.Operator.Count > 0)
            //    {
            //        var peeked = this.Operator.Peek();
            //        if (selectedOperator.Precedence < peeked.Precedence ||
            //            (selectedOperator.Associativity == Associativity.Left && selectedOperator.Precedence == peeked.Precedence))
            //        {
            //            this.Ouput.Push(selectedOperator);
            //        }
            //        else
            //        {
            //            this.Operator.Push(selectedOperator);
            //        }
            //    }
            //    else
            //    {
            //        this.Operator.Push(selectedOperator);
            //    }

            //}
        }

        private bool IsOperator(char a)
        {
            throw new NotImplementedException();
        }


    }
}
