using System;
using SalesforceProject.Utilities;

namespace SalesforceProject.Tests
{
	public class SignUpTest: BaseClass
	{

		[SetUp]
		public void SetupSignup()
		{
			getDriver().Url = getURl();
		}

		[Test]
		public void SingupTest()
		{

		}
	}
}

