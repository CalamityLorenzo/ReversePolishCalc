

using ShuntingYard.TokenDefs;

namespace ShuntingYard
{
    public class ShuntingYardProcesor
    {
        int _currentIdx = 0;
        string _input = "";
        string _output = "";
        public string Parse(string input)
        {
            if (input.Length == 0) { return ""; }
            this._input = input;
            _currentIdx = 0;
            BuildTokens();
            ProcessInput();


            return "";
        }

        private void ProcessInput()
        {
            throw new NotImplementedException();
        }

        private void BuildTokens()
        {
            while (_currentIdx < _input.Length)
            {
                SYardToken token = _input[_currentIdx] switch
                {
                    var a when Char.IsDigit(a) => CreateNumber(),
                    var a when Char.IsLetter(a) => CreateFunctionName(),
                    var a when IsOperator(a) => CreateOperator(),
                    var a when Char.IsWhiteSpace(a) => CreateWhiteSpace(),
                    _ => throw new NotImplementedException()
                }; ;
                _currentIdx++;
            }
        }
        private char Peek()
        {
            if (_currentIdx < _input.Length)
            { return _input[_currentIdx += 1]; }
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

  

        private SYardToken CreateOperator()
        {
            var currentChar = _input[_currentIdx];
            if(currentChar == '(') { }
        }

        private bool IsOperator(char a)
        {
            throw new NotImplementedException();
        }

        private SYardToken CreateFunctionName()
        {
            throw new NotImplementedException();
        }

        private SYardToken CreateNumber()
        {
            throw new NotImplementedException();
        }
    }
}
