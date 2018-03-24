using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NottCS.Models;
using NottCS.Services.JSONConverters;
using NottCS.Validations;
using Xunit;

namespace NottCSTest
{
    //TODO: Split eventadditionalparameter serialize and deserialize test
    public class JSONSerializeTest
    {
        [Fact]
        private void ValidatableObjectJSONSerializeTest()
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
        private void EventAdditionalParameterJSONTest()
        {
            var model1 = new EventAdditionalParameter()
            {
                Name = "Phone manufacturer", ValidationList =
                {
                    new StringNotEmptyRule(),
                    new StringAlphaNumericRule()
                }, Value = "Huawei"
            };

            JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            string model1JSON = JsonConvert.SerializeObject(model1, settings);
            var model1Deserialised = JsonConvert.DeserializeObject(model1JSON, settings);

            Assert.Equal(model1, model1Deserialised);
        }

        [Fact]
        private void EventAdditionalParameterListJSONTest()
        {
            var list = new List<EventAdditionalParameter>()
            {
                new EventAdditionalParameter()
                {
                    Name = "Phone manufacturer",
                    ValidationList =
                    {
                        new StringNotEmptyRule(),
                        new CharacterNumberRule(12),
                        new StringAlphaNumericRule()
                    },
                    Value = "Huawei"
                },
                new EventAdditionalParameter()
                {
                    Name = "Preferred name",
                    ValidationList =
                    {
                        new StringNotEmptyRule(),
                        new CharacterNumberRule(20),
                        new StringAlphaNumericRule()
                    }
                }
            };

            JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            string model1JSON = JsonConvert.SerializeObject(list, settings);
            var model1Deserialised = JsonConvert.DeserializeObject(model1JSON, settings);

            Assert.Equal(list, model1Deserialised);
        }




    }
}
