using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using NottCS.Validations;


namespace NottCSTest
{
    public class ValidationTest
    {
        [Fact]
        private void AlphaNumericRuleTest()
        {
            string nullString = null;
            string emptyString = "";
            string unicodeSymbols = "ταБЬℓσa";
            string symbols = "#asdn";
            string longString = "AISJCNAIOUCNUISCBYEFUIWUCBQWY8156a8wvef18q3ge1de8v71e1ava5d14e4v37g4q43rd4dsd7vwe" +
                                "g3434qviunq3vuinfu34fq3v51f34c3418v18q37g1fw56d1c843v183q1t78f1q4fv3q4v413q6g1v78qf5ad1sv5bm876ik17896o1i8ui8k1w8b1era61v17q11fwc"
            var alphaNumericRule = new AlphaNumericRule(){ValidationMessage = "Some Message"};
            Assert.False(alphaNumericRule.Check(nullString));
            Assert.False(alphaNumericRule.Check(emptyString));
            Assert.False(alphaNumericRule.Check(unicodeSymbols));
            Assert.False(alphaNumericRule.Check(symbols));

            Assert.True(alphaNumericRule.Check(longString));

        }
    }
}
