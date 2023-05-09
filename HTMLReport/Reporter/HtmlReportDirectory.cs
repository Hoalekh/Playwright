using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HTMLReport.Utilities;



namespace HTMLReport.Reporter
{
    public class HtmlReportDirectory
    {
        public static string REPORT_ROOT { get; set; }
        public static string REPORT_FOLDER_PATH { get; set; }
        public static string REPORT_FILE_PATH { get; set; }
        public static string SCREENSHOT_PATH { get; set; }

        //other function
        public static string ACTUAL_SCREENSHOT_PATH { get; set; }
        public static string DIFFERENCE_SCREENSHOT_PATH { get; set; }
        public static string BASELINE_SCREENSHOT_PATH { get; set; }
        public static void InitReportDirection()
        {
            string projectPath = FilePath.GetCurrentDirectoryPath();
            REPORT_ROOT = projectPath + "\\Reports";
            REPORT_FOLDER_PATH = REPORT_ROOT + "\\Lastest Reports";
            REPORT_FILE_PATH = REPORT_FOLDER_PATH + "\\report.html";
            SCREENSHOT_PATH = REPORT_FOLDER_PATH + "\\Screenshot";

            //other function
            ACTUAL_SCREENSHOT_PATH = REPORT_FOLDER_PATH + "\\Actual";
            DIFFERENCE_SCREENSHOT_PATH = REPORT_FOLDER_PATH + "\\Difference";
            BASELINE_SCREENSHOT_PATH = FilePath.GetCurrentDirectoryPath() + "\\Resources\\Baseline";

            FilePath.CreateIfNotExists(REPORT_ROOT);
            checkExistReportAndRename(REPORT_FOLDER_PATH);
            FilePath.CreateIfNotExists(REPORT_FOLDER_PATH);
            FilePath.CreateIfNotExists(SCREENSHOT_PATH);
            FilePath.CreateIfNotExists(ACTUAL_SCREENSHOT_PATH);
            FilePath.CreateIfNotExists(DIFFERENCE_SCREENSHOT_PATH);
            FilePath.CreateIfNotExists(BASELINE_SCREENSHOT_PATH);
        }

        private static void checkExistReportAndRename(string reportFolder)
        {
            if (Directory.Exists(reportFolder))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(reportFolder);
                var newPath = REPORT_ROOT + "\\Report_" + dirInfo.CreationTime.ToString().Replace(":", ".").Replace("/", "-");
                Directory.Move(reportFolder, newPath);
            }
        }
    }
}
