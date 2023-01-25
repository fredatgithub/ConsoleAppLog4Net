using System;
using System.IO;
using log4net;
using log4net.Config;

namespace ConsoleAppLog4Net
{
  internal class Program
  {
    static void Main(string[] arguments)
    {
      ILog logger = LogManager.GetLogger(typeof(Program));
      string Log4NetConfigFilePath = "log4net.config.xml";
      XmlConfigurator.ConfigureAndWatch(new FileInfo(Log4NetConfigFilePath));
      Action<string> Display = Console.WriteLine;
      var startOfApplication = "Start of the application";
      Display(startOfApplication);
      logger.Info(startOfApplication);
      Display("Press any key to exit:");
      Console.ReadKey();
    }
  }
}
