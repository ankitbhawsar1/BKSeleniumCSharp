using System;
using Newtonsoft.Json.Linq;

namespace SeleniumCSharpFramework.Utilities
{
	public class JsonReader
	{
        string projectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        public string getData(string tokenName)
		{
            string testDataPath = projectPath + "/Data/" + "TestData.json";
            var myJsonStr = File.ReadAllText(testDataPath);
			var jsonObj = JToken.Parse(myJsonStr);
			return jsonObj.SelectToken(tokenName).Value<string>();
		}

        public string[] getArrayData(string tokenName)
        {
            string testDataPath = projectPath + "/Data/" + "TestData.json";
            var myJsonStr = File.ReadAllText(testDataPath);
            var jsonObj = JToken.Parse(myJsonStr);
			//return jsonObj.SelectToken(tokenName).ToObject<List<string>>();
			return jsonObj.SelectTokens(tokenName).Values<string>().ToList<string>().ToArray();
        }
    }
}

