using System;
using Newtonsoft.Json.Linq;

namespace SalesforceProject.Utilities
{
	public class JsonReader
	{
		public string getStringData(string tokenName)
		{
			var jsonStr = File.ReadAllText("Data/userData.json");
			var jsonObj = JToken.Parse(jsonStr);
			return jsonObj.SelectToken(tokenName).Value<string>();
		}
	}
}

