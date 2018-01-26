﻿using Xunit;
using NottCS.Validations;


namespace NottCSTest
{
    public class ValidationTest
    {
        [Fact]
        private void AlphaNumericRuleTest()
        {
            string nullString = null;
            string unicodeSymbols = "ταБЬℓσa";
            string symbols = "#asdn";
            string longString = "AISJCNAIOUCNUISCBYEFUIWUCBQWY8156a8wvef18q3ge1de8v71e1ava5d14e4v37g4q43rd4dsd7vwe" +
                                "g3434qviunq3vuinfu34fq3v51f34c3418v18q37g1fw56d1c843v183q1t78f1q4fv3q4v413q6g1v78qf5ad1sv5bm876ik17896o1i8ui8k1w8b1era61v17q11fwc";
            string validationMessage = "Some Message";

            var alphaNumericRule = new StringAlphaNumericRule() { ValidationMessage = validationMessage };
            Assert.Equal(validationMessage, alphaNumericRule.ValidationMessage);

            Assert.False(alphaNumericRule.Check(nullString));
            Assert.False(alphaNumericRule.Check(unicodeSymbols));
            Assert.False(alphaNumericRule.Check(symbols));

            Assert.True(alphaNumericRule.Check(longString));

        }

        [Fact]
        private void DigitsRuleTest()
        {
            string nullString = null;
            string unicodeSymbols = "Testing «ταБЬℓσ»: 1<2 & 4+1>3, now 20% off!";
            string alphabets = "SOMETHING";
            string digitString = "34734523123423142523534";
            string validationMessage = "Some Message";

            var digitsRule = new StringDigitsRule(){ ValidationMessage = validationMessage };
            Assert.Equal(validationMessage, digitsRule.ValidationMessage);

            Assert.False(digitsRule.Check(nullString));
            Assert.False(digitsRule.Check(unicodeSymbols));
            Assert.False(digitsRule.Check(alphabets));

            Assert.True(digitsRule.Check(digitString));
        }

        [Fact]
        private void NotEmptyRuleTest()
        {
            string nullString = null;
            string someString = "Testing «ταБЬℓσ»: 1<2 & 4+1>3, now 20% off!";
            string emptyString = "";
            string validationMessage = "Some Message";

            var stringNotEmptyRule = new StringNotEmptyRule() {ValidationMessage = validationMessage};
            Assert.Equal(validationMessage, stringNotEmptyRule.ValidationMessage);

            Assert.True(stringNotEmptyRule.Check(someString));

            Assert.False(stringNotEmptyRule.Check(emptyString));
            Assert.False(stringNotEmptyRule.Check(nullString));
        }
    }
}