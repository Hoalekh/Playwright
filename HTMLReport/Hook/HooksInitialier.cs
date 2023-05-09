using HTMLReport.Drivers;
using HTMLReport.Reporter;
using NUnit.Framework.Interfaces;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace HTMLReport.Hook
{
    [Binding]
    public sealed class HooksInitialier
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks

        [BeforeScenario]
        public void BeforeScenario()
        {
            HtmlReport.createReport();
            HtmlReport.createTest(TestContext.CurrentContext.Test.ClassName);
            HtmlReport.createNode(TestContext.CurrentContext.Test.ClassName, TestContext.CurrentContext.Test.Name);
        }


        [AfterScenario]
        public void AfterScenario()
        {
            //TODO: implement logic that has to run after executing each scenario
            TestStatus testStatus = TestContext.CurrentContext.Result.Outcome.Status;
            if (testStatus.Equals(TestStatus.Passed))
            {

            }
            else if (testStatus.Equals(TestStatus.Failed))
            {

            }

            HtmlReport.flush();
        }

    }
}