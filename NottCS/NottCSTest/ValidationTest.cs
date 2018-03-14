using Newtonsoft.Json;
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
            string unicodeSymbols = "ταБЬℓσa";
            string symbols = "#asdn";
            string longString = "AISJCNAIOUCNUISCBYEFUIWUCBQWY8156a8wvef18q3ge1de8v71e1ava5d14e4v37g4q43rd4dsd7vwe" +
                                "g3434qviunq3vuinfu34fq3v51f34c3418v18q37g1fw56d1c843v183q1t78f1q4fv3q4v413q6g1v78qf5ad1sv5bm876ik17896o1i8ui8k1w8b1era61v17q11fwc";
            var alphaNumericRule = new StringAlphaNumericRule() {};

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

            var digitsRule = new StringNumericRule();

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

            var stringNotEmptyRule = new StringNotEmptyRule() {};

            Assert.True(stringNotEmptyRule.Check(someString));

            Assert.False(stringNotEmptyRule.Check(emptyString));
            Assert.False(stringNotEmptyRule.Check(nullString));
        }

        [Fact]
        private void CharacterNumberRuleJSONTest()
        {
            var setting = new JsonSerializerSettings() {TypeNameHandling = TypeNameHandling.All};
            IValidationRule<string> rule1 = new CharacterNumberRule(10);
            string rule1JSON = JsonConvert.SerializeObject(rule1, setting);
            var rule1Deserialised = JsonConvert.DeserializeObject(rule1JSON, setting);
            Assert.Equal(rule1, rule1Deserialised);


        }
    }
}
