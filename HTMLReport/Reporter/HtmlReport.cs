using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;


namespace HTMLReport.Reporter
{
    public class HtmlReport
    {
        private static int testCaseIndex;

        private static ExtentReports _report;
        private static ExtentTest extentTestSuite;
        private static ExtentTest extentTestCase;

        public static ExtentReports createReport()
        {
            if (_report == null)
            {
                _report = createInstance();
            }
            return _report;
        }

        public static ExtentReports createInstance()
        {
            HtmlReportDirectory.InitReportDirection();
            ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(HtmlReportDirectory.REPORT_FILE_PATH);
            htmlReporter.Config.DocumentTitle = "Automation Test Report";
            htmlReporter.Config.ReportName = "Automation Test Report";
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;
            htmlReporter.Config.Encoding = "UTF-8";

            ExtentReports report = new ExtentReports();
            report.AttachReporter(htmlReporter);
            return report;
        }
        public static void flush()
        {
            if (_report != null)
            {
                _report.Flush();
            }
        }

        public static ExtentTest createTest(string className, string classDescription = "")
        {
            if (_report == null)
            {
                _report = createInstance();
            }
            extentTestSuite = _report.CreateTest(className, classDescription);
            return extentTestSuite;
        }

        public static ExtentTest createNode(string className, string testcase, string description = "")
        {
            if (extentTestSuite == null)
            {
                extentTestSuite = createTest(className);
            }
            extentTestCase = extentTestSuite.CreateNode(testcase, description);
            return extentTestCase;
        }

        public static ExtentTest GetParent()
        {
            return extentTestSuite;
        }

        public static ExtentTest GetNode()
        {
            return extentTestCase;
        }

        public static ExtentTest GetTest()
        {
            if (GetNode() == null)
            {
                return GetParent();
            }
            return GetNode();
        }

        public static void Pass(string des)
        {
            GetTest().Pass(des);
            TestContext.WriteLine(des);
        }

        public static void Fail(string des)
        {
            GetTest().Fail(des);
            TestContext.WriteLine(des);
        }

        public static void Fail(string des, string path)
        {
            GetTest().Fail(des).AddScreenCaptureFromPath(path);
            TestContext.WriteLine(des);
        }

        public static void Fail(string des, string ex, string path)
        {
            GetTest().Fail(des).Fail(ex).AddScreenCaptureFromPath(path);
            TestContext.WriteLine(des);
        }

        /*
        public static void Info(HttpWebRequest request, HttpWebResponse response)
        {
            GetTest().Info(MarkupHelperPlus.CreateRequest(request, response);
        }        */

        public static void Warning(string des)
        {
            GetTest().Warning(des);
            TestContext.WriteLine(des);
        }

        public static void Skip(string des)
        {
            GetTest().Skip(des);
            TestContext.WriteLine(des);
        }

        /*----------------------MARKUP EXTENT REPORT------------------------*/

        /*public static void MarkUpHtml()
        {
            var htmlMarkup = HtmlInjector.CreateHtml();
            var m = MarkupHelper.CreateTable(htmlMarkup, ExtentColor.Transparent);
            GetTest().Info(m);
        }*/
        public static void MarkupPassJson()
        {
            var json = "{'foo':'bar':'foos':['b','a','r'], " +
                "'bar':{'foo':'bar', 'bar':false, 'foobar':1234}}";
            GetTest().Info(MarkupHelper.CreateCodeBlock(json, CodeLanguage.Json));
        }
        public static void MarkupTable()
        {
            string[][] someInts = new string[][] {
                new string[] {
                    "<label> HAHAHA </label>"}
            };
            var m = MarkupHelper.CreateTable(someInts);
            GetTest().Info(m);
        }

        public static void MarkupPassLabel()
        {
            var text = "Passed";
            var m = MarkupHelper.CreateLabel(text, ExtentColor.Green);
            GetTest().Pass(m);
        }
        public static void MarkupFailLabel()
        {
            var text = "Failed";
            var m = MarkupHelper.CreateLabel(text, ExtentColor.Red);
            GetTest().Fail(m);
        }
        public static void MarkupWarningLabel()
        {
            var text = "Warning";
            var m = MarkupHelper.CreateLabel(text, ExtentColor.Orange);
            GetTest().Warning(m);
        }
        public static void MarkupSkipLabel()
        {
            var text = "Skipped";
            var m = MarkupHelper.CreateLabel(text, ExtentColor.Blue);
            GetTest().Skip(m);
        }

    }
}
