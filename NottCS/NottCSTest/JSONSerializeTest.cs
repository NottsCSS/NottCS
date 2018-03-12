using System;
using System.Diagnostics;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NottCS.Models;
using NottCS.Services.JSONSerializer;
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

        [Fact]
        void IValidationRuleJSONTest()
        {
            IValidationRule<string> rule1 = new StringNumericRule();
            var rule1Expected = new StringNumericRule();

            var rule1JSON = JsonConvert.SerializeObject(rule1);
            var rule1JSONExpected = JsonConvert.SerializeObject(rule1Expected);

            //Serialization test
            Assert.Equal(typeof(StringNumericRule), rule1.GetType()); //same underlying type
            Debug.WriteLine(rule1JSON);
            Assert.Equal(rule1JSONExpected, rule1JSON); //same json

            //Deserialization test
            var rule1Deserialized = JsonConvert.DeserializeObject<IValidationRule<string>>(rule1JSON, new IValidationRuleConverter());
            Assert.Equal(rule1.ValidationMessage, rule1Deserialized.ValidationMessage); //same validation message
            Assert.Equal(rule1.GetType(), rule1Deserialized.GetType()); //same underlying type
            
        }
        [Fact]
        void EventAdditionalParameterJSONTest()
        {
            var model1 = new EventAdditionalParameter()
            {
                Name = "Phone manufacturer", ValidationList =
                {
                    new StringNotEmptyRule(),
                    new StringAlphaNumericRule()
                }, Value = "Huawei"
            };

            string model1JSON = JsonConvert.SerializeObject(model1);

            var model1Deserialized = JsonConvert.DeserializeObject(model1JSON);
            JObject something = model1Deserialized as JObject;
            EventAdditionalParameter model1Deserialized2 = something?.ToObject<EventAdditionalParameter>();
            Assert.Equal(model1, model1Deserialized2);
        }

    }
}
