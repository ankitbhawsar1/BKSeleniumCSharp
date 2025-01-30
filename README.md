
Allure commands

- npm install -g allure-commandline
- allure serve allure-results

Jenkins
- java -jar jenkins.war   
Jenkins initial setup is required. An admin user has been created and a password generated.
Please use the following password to proceed to installation:

fafcf9f178eb4b89af0f17a8de7f5dbd

This may also be found at: /Users/ankit/.jenkins/secrets/initialAdminPassword



Run all test from all files/
 - dotnet test name.csproj  


Run category wise test
 - dotnet test SeleniumCSharpFramework.csproj  --filter TestCategory=Smoke

Pass parameter from commond line
- dotnet test SeleniumCSharpFramework.csproj --filter TestCategory=Regression -- TestRunParameters.Parameter\(name=\"browser\",value=\"Firefox\"\)


- dotnet test SeleniumCSharpFramework.csproj --filter "TestCategory=Regression" -- TestRunParameters.Parameter\(name=\"browser\",value=\"Firefox\"\)

