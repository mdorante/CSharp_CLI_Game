using System;
using System.IO;

namespace Game_Project.Lib
{
    public class History
    {
        public static void LogEvent(string gameEvent)
        {
            // set the log path at the project root
            string logPath = "../../../history.log";

            File.AppendAllText(logPath, DateTime.Now + " " + gameEvent + '\n');
        }

        public static void ReadLog()
        {
            string path = "../../../history.log";

            string[] contents = File.ReadAllLines(path);

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
