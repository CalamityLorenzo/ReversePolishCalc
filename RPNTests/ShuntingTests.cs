using ReversePolishCalc.TokenDefs;
using ReversePolishCalc;
using ShuntingYard;
using ShuntingYard.TokenDefs;
using System.Diagnostics;
using Newtonsoft.Json.Linq;

namespace Tests
{
    public class ShuntingTests
    {

        [Fact]
        public void TokenOutput()
        {
            ShuntingYardProcesor syr = new();


            syr.Parse("1+2+3");

            Assert.True(syr.Tokens.Count == 5);
            Assert.True(syr.Tokens[0] is SYardNumber);
            Assert.True(syr.Tokens[1] is SYardOperator);
            Assert.True(syr.Tokens[3] is SYardOperator);
        }

        [Fact]
        public void TokenValueOutput()
        {
            ShuntingYardProcesor syr = new();


            syr.Parse("1+2+3");
            
            Trace.WriteLine((syr.Tokens[0] as SYardNumber).Value);
            Trace.WriteLine((syr.Tokens[1] as SYardOperator).OriginalValue);
            Trace.WriteLine((syr.Tokens[2] as SYardNumber).Value);
            Trace.WriteLine((syr.Tokens[4] as SYardNumber).Value);

            Assert.True(syr.Tokens.Count == 5);
            Assert.True((syr.Tokens[0] as SYardNumber).Value == 1);
            Assert.True((syr.Tokens[1] as SYardOperator).OriginalValue == "+");
            Assert.True((syr.Tokens[2] as SYardNumber).Value == 2);

        }

        [Fact]
        public void TokenValueWhitespaceAndOutput()
        {
            ShuntingYardProcesor syr = new();


            syr.Parse("1*2*3/       max(pot(4))");


            Assert.True(syr.Tokens.Count == 14);
            Assert.True((syr.Tokens[5] as SYardOperator).OriginalValue == "/");
            Assert.True((syr.Tokens[6] as SYardWhiteSpace).OriginalValue=="");
            Assert.True((syr.Tokens[7] as SYardOperator).OriginalValue == "max");
            Assert.True((syr.Tokens[13] as SYardOperator).OriginalValue == ")");

        }
    }
}
