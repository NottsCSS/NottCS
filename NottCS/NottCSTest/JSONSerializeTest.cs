using Newtonsoft.Json;
using NottCS.Validations;
using Xunit;

namespace NottCSTest
{
    public class JSONSerializeTest
    {
        [Fact]
        void ValidatableObjectJSONSerializeTest()
        {
            string standardString = "STANDARD STRING";
            string unicodeString = "Testing «ταБЬℓσ»: 1<2 & 4+1>3, now 20% off!";
            string emptyString = "";
            string nullString = null;

            void StringValidatableObjectTest(string inputString)
            {
                var validatableObject = new ValidatableObject<string>() {Value = inputString};
                //Serialization test
                var inputJSON = JsonConvert.SerializeObject(inputString);
                var validatableObjectJSON = JsonConvert.SerializeObject(validatableObject);

                Assert.Equal(inputJSON, validatableObjectJSON);
            }

            StringValidatableObjectTest(standardString);
            StringValidatableObjectTest(unicodeString);
            StringValidatableObjectTest(emptyString);
            StringValidatableObjectTest(nullString);

        }

    }
}
