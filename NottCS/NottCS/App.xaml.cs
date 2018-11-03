using System;
using System.Diagnostics;
using System.IO;
using NLog;
using NLog.Common;
using NLog.Config;
using NLog.Targets;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NottCS.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace NottCS
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();


            MainPage = new MainPage();
            SetupNLog();

            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string filename = Path.Combine(path, "log123.txt");
            Debug.WriteLine(System.Environment.SpecialFolder.Personal);
            Debug.WriteLine($"Log file exists: {File.Exists(filename)}");
        }

        private void SetupNLog()
        {
            NLog.LogManager.ThrowExceptions = true;
            NLog.LogManager.ThrowConfigExceptions = true;

            NLog.Config.LoggingConfiguration cfg = new NLog.Config.LoggingConfiguration();
            
//            FileTarget ft = new FileTarget("f");
//            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
//            string filename = Path.Combine(path, "log123.txt");
//            ft.FileName = filename;
//            ft.FileNameKind = FilePathKind.Absolute;
//            ft.AutoFlush = true;
//            cfg.AddTarget("f", ft);


            var debugTarget = new OutputDebugStringTarget("target1")
            {
                Layout = @"[${level}] ${message} ${date:format=HH\:mm\:ss} ${callsite}"
            };
            cfg.AddTarget(debugTarget);

//            cfg.AddRuleForOneLevel(LogLevel.Error, ft); // only errors to file
            cfg.AddRuleForAllLevels(debugTarget); // all to console
            NLog.LogManager.Configuration = cfg;
            NLog.LogManager.ReconfigExistingLoggers();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
