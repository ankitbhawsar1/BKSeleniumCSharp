using System;
using Newtonsoft.Json.Linq;

namespace SeleniumCSharpFramework.Utilities
{
	public class JsonReader
	{
		public string getData(string tokenName)
		{
			var myJsonStr = File.ReadAllText("data/TestData.json");
			var jsonObj = JToken.Parse(myJsonStr);
			return jsonObj.SelectToken(tokenName).Value<string>();
		}

        public string[] getArrayData(string tokenName)
        {
            var myJsonStr = File.ReadAllText("data/TestData.json");
            var jsonObj = JToken.Parse(myJsonStr);
			//return jsonObj.SelectToken(tokenName).ToObject<List<string>>();
			return jsonObj.SelectTokens(tokenName).Values<string>().ToList<string>().ToArray();
        }
    }
}

