using System;
using System.Collections;
using System.IO;

namespace AddressHistoryDAL
{
    public class InputOutput
    {
        private static string _appFolder;
        private readonly string _logFile = "log.txt";

        private static InputOutput _instance;
        private static readonly object _syncLock = new object();

        private InputOutput() { }

        public static InputOutput GetInstance()
        {
            if (_instance == null)
            {
                lock (_syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new InputOutput();
                    }
                }
                SetupFileIO();
            }

            return _instance;
        }

        public static void SetupFileIO()
        {
            if (Environment.GetEnvironmentVariable("home") != null)
            {
                _appFolder = Environment.GetEnvironmentVariable("home") + @"\Documents\AddressHistory";
            }
            else if (Environment.GetEnvironmentVariable("userprofile") != null)
            {
                _appFolder = Environment.GetEnvironmentVariable("userprofile") + @"\Documents\AddressHistory";
            }
            else
            {
                throw new Exception("ERROR: cannot create data directory.");
            }

            if (Directory.Exists(_appFolder))
            {
                Environment.CurrentDirectory = _appFolder;
            }
            else
            {
                Directory.CreateDirectory(_appFolder);
                Environment.CurrentDirectory = _appFolder;
            }
        }

        public void DisplayException(Exception ex)
        {

            Console.WriteLine("***** Exception *****");
            Console.WriteLine($"{ex.Message}");

            if (ex.Source.Equals("AddressHistory"))
            {
                Console.WriteLine($"Source:\t\t{ex.Source}");
                Console.WriteLine($"Class:\t\t{ex.TargetSite.DeclaringType}");
                Console.WriteLine($"Member name:\t{ex.TargetSite}");
                Console.WriteLine($"Member type:\t{ex.TargetSite.MemberType}");

                foreach (DictionaryEntry item in ex.Data)
                {
                    Console.WriteLine($"\t\t{item.Key} {item.Value} ");
                }
            }
            else
            {
                Console.WriteLine($"Source: {ex.Source}");
            }
            Console.WriteLine($"\nStackTrace:\n{ex.StackTrace}\n");
        }

        public void WriteLogFile(string text)
        {

            using (StreamWriter logFile = File.AppendText(_logFile))
            {
                logFile.WriteLine($"{DateTime.Now} {text}");
            }
        }

        public void WriteLogFile(Exception ex)
        {

            using (StreamWriter logFile = File.AppendText(_logFile))
            {
                logFile.WriteLine("***** Exception *****");
                logFile.WriteLine($"{DateTime.Now} {ex.Message}");

                if (ex.Source.Equals("AddressHistory"))
                {
                    logFile.WriteLine($"Source:\t\t{ex.Source}");
                    logFile.WriteLine($"Class:\t\t{ex.TargetSite.DeclaringType}");
                    logFile.WriteLine($"Member name:\t{ex.TargetSite}");
                    logFile.WriteLine($"Member type:\t{ex.TargetSite.MemberType}");

                    foreach (DictionaryEntry item in ex.Data)
                    {
                        logFile.WriteLine($"\t\t{item.Key} {item.Value} ");
                    }
                }
                else
                {
                    logFile.WriteLine($"Source: {ex.Source}");
                }
                logFile.WriteLine($"\nStackTrace:\n{ex.StackTrace}\n");
            }
        }
    }
}
