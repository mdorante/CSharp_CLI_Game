using System;
using System.IO;

namespace Game_Project.Lib
{
    public class History
    {
        private static string basePath = "./logs";
        private static string logPath = $"{basePath}/history.log";

        public static void Initialize()
        {
            if (!Directory.Exists(basePath))
                Directory.CreateDirectory(basePath);

            if (!File.Exists(logPath))
                File.WriteAllText(logPath, string.Empty);
        }

        public static void LogEvent(string gameEvent)
        {
            File.AppendAllText(logPath, DateTime.Now + " " + gameEvent + '\n');
        }

        public static void ReadLog()
        {
            string[] contents = File.ReadAllLines(logPath);

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            for (int i = 0; i < contents.Length; i++)
            {
                Console.WriteLine(contents[i]);
            }
            Console.ResetColor();

            Console.WriteLine("\nPress Enter to return to main menu.");
            Console.ReadLine();
        }
    }
}
