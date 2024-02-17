using ReversePolishCalc;
using ReversePolishCalc.TokenDefs;

namespace RPNTests
{
    public class UnitTest1
    {
        [Fact]
        public void TokenOutput()
        {
            ReversePolishCalculator rpn = new();

            rpn.Parse("1 2 +");

            Assert.True(rpn.Tokens.Count== 5);
            Assert.True(rpn.Tokens[0] is NumberRPNToken);
            Assert.True(rpn.Tokens[2] is NumberRPNToken);
            Assert.True(rpn.Tokens[4] is OperatorRPNToken);
        }

        [Fact]
        public void TokenValues()
        {
            ReversePolishCalculator rpn = new();
            rpn.Parse("1 2 +");


            NumberRPNToken n = rpn.Tokens[0] as NumberRPNToken;
            
            Assert.True((rpn.Tokens[0] as NumberRPNToken).NumberValue == 1d);
            Assert.True((rpn.Tokens[4] as OperatorRPNToken).OriginalValue == "+");
            Assert.True((rpn.Tokens[2] as NumberRPNToken).NumberValue == 2d);
        }

        [Fact]
        public void SimpleAddValues()
        {
            ReversePolishCalculator rpn = new();
            var RESULT = rpn.Parse("1 2 +");
            Assert.True(RESULT == 3);
        }

        [Fact]
        public void MultiAddSubValues()
        {
            ReversePolishCalculator rpn = new();
            var RESULT = rpn.Parse("1 2 3 + -");
            Assert.True(RESULT == -4);
        }

        [Fact]
        public void MultiAddSubValuesA()
        {
            ReversePolishCalculator rpn = new();
            var RESULT = rpn.Parse("1 2 + 2 -");
            Assert.True(RESULT == 1);
        }
    }
}