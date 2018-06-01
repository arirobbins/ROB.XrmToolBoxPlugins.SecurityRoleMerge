using System;

namespace ROB.XrmToolBoxPlugins.SecurityRoleMerge
{
    public class ConsoleLogger : ILogger
    {
        public void LogError(string message)
        {
            Console.WriteLine(message);
        }

        public void LogInfo(string message)
        {
            Console.WriteLine(message);
        }
    }
}