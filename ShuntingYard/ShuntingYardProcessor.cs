

using ShuntingYard.TokenDefs;

using static ShuntingYard.TokenDefs.Associativity;
namespace ShuntingYard
{
    public class ShuntingYardProcessor
    {
        private Dictionary<string, SYardOperator> _operatorList = new();

        private Stack<SYardToken> Ouput = new();
        private Stack<SYardOperator> Operator = new();
        public List<SYardToken> Tokens = new();

        public ShuntingYardProcessor()
        {
            BuildAssociativityDictionary();
        }

        private void BuildAssociativityDictionary()
        {
            _operatorList.Add("(", new("(", Unknown, 9));
            _operatorList.Add(")", new(")", Unknown, 9));
            _operatorList.Add("^", new("^", Right, 3));
            _operatorList.Add("*", new("*", Left, 3));
            _operatorList.Add("/", new("/", Left, 3));
            _operatorList.Add("+", new("+", Left, 2));
            _operatorList.Add("-", new("-", Left, 2));
            _operatorList.Add("min", new("min", Right, 2));
            _operatorList.Add("max", new("max", Right, 2));
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
            return ProcessOutput(); ;
        }

        /// <summary>
        /// Converst output to string
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private string ProcessOutput()
        {
            return String.Join(" ", this.Ouput);
        }

        private void BuildTokens()
        {
            while (_currentIdx < _input.Length)
            {
                SYardToken token = _input[_currentIdx] switch
                {
                    var a when Char.IsDigit(a) => CreateNumber(),
                    var a when Char.IsLetter(a) => CreateFunctionName(),
                    var a when _operatorList.ContainsKey(a.ToString()) => CreateOperator(),
                    var a when Char.IsWhiteSpace(a) => CreateWhiteSpace(),
                    _ => throw new NotImplementedException()
                }; ;
                this.Tokens.Add(token);
                _currentIdx++;
            }
        }
        private char Peek()
        {
            if (_currentIdx < _input.Length - 1)
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
            var selectedOperator = _operatorList[currentChar.ToString()];
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
            return new SYardOperator(_input.Substring(startPos, (_currentIdx - startPos) + 1), Right, 000);

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
            foreach (var token in Tokens)
            {
                // Just say no to white space
                if (token is SYardWhiteSpace) continue;
                // NUmbers are always output.
                if (token is SYardNumber) this.Ouput.Push(token);
                if (token is SYardOperator)
                {
                    var selectedOperator = (SYardOperator)token;
                    // Stack will throw on Peek if there is no content.
                    if (this.Operator.Count > 0)
                    {
                        var peeked = this.Operator.Peek();
                        // Right parens means the end fo a function call, or expression
                        // we keep popping elements until we find the "("
                        if (selectedOperator.OriginalValue == ")")
                        {
                            var topOperator = new SYardToken("");
                            do
                            {
                                topOperator = this.Operator.Pop();
                                if (topOperator.OriginalValue != "(")
                                    this.Ouput.Push(topOperator);

                            } while (topOperator.OriginalValue != "(");
                        }
                        else
                        {
                            if (selectedOperator.Precedence < peeked.Precedence ||
                                (selectedOperator.Associativity == Left && selectedOperator.Precedence == peeked.Precedence))
                            {
                                this.Ouput.Push(selectedOperator);
                            }
                            else
                            {
                                this.Operator.Push(selectedOperator);
                            }
                        }
                    }
                    else
                    {
                        this.Operator.Push(selectedOperator);
                    }
                }
            }
            // Any remaining operators 
            // put on the output
            while (Operator.Count > 0)
            {
                this.Ouput.Push(Operator.Pop());
            }
        }


    }
}
