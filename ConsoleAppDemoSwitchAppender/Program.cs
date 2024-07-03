using System;
using System.IO;
using System.Linq;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Repository.Hierarchy;


namespace ConsoleAppDemoSwitchAppender
{
  internal class Program
  {
    private static readonly ILog log = LogManager.GetLogger(typeof(Program));

    static void Main()
    {
      // Configuration de log4net
      XmlConfigurator.Configure(new FileInfo("log4net.config.xml"));

      // Liste les appenders disponibles pour debug
      ListAppenders();

      Console.WriteLine("Application started. Current logger is mainFileAppender.");
      log.Info("This is an info message using mainFileAppender.");

      Console.WriteLine("Press any key to switch to developerAppender...");

      SwitchAppender("developerAppender");

      Console.WriteLine("Logger switched to developerAppender.");
      log.Info("This is an info message using developerAppender.");

      Console.WriteLine("Press any key to set logging level to DEBUG...");

      
      SetLoggingLevel("DEBUG");
      Console.WriteLine("This is a debug message using developerAppender");
      log.Debug("This is a debug message using developerAppender.");

      Console.WriteLine("Press any key to set logging level to INFO...");

      SetLoggingLevel("INFO");
      Console.WriteLine("This is an info message using developerAppender");
      log.Info("This is an info message using developerAppender.");

      Console.WriteLine("Press any key to exit...");
      Console.ReadKey();
    }

    private static void ListAppenders()
    {
      var hierarchy = (Hierarchy)LogManager.GetRepository();
      var appenders = hierarchy.GetAppenders();

      Console.WriteLine("Available appenders:");
      foreach (var appender in appenders)
      {
        Console.WriteLine($"- {appender.Name}");
      }
    }

    private static void SwitchAppender(string newAppenderName)
    {
      var hierarchy = (Hierarchy)LogManager.GetRepository();
      var rootLogger = hierarchy.Root;

      // Liste les appenders disponibles pour debug
      var appenders = hierarchy.GetAppenders();
      Console.WriteLine("Available appenders:");
      foreach (var appender in appenders)
      {
        Console.WriteLine($"- {appender.Name}");
      }

      // Désactivez tous les appender actuels
      rootLogger.RemoveAllAppenders();

      // Récupère le nouvel appender et l'ajoute au root logger
      IAppender newAppender = appenders.FirstOrDefault(appender => appender.Name == newAppenderName);

      if (newAppender != null)
      {
        rootLogger.AddAppender(newAppender);
        log.Info($"Switched to appender: {newAppenderName}");
      }
      else
      {
        log.Error($"Appender {newAppenderName} not found.");
      }

      // Redémarrez les loggers pour appliquer les modifications
      hierarchy.Configured = true;
    }

    private static void SetLoggingLevel(string levelName)
    {
      var hierarchy = (Hierarchy)LogManager.GetRepository();
      var rootLogger = hierarchy.Root;

      Level level = hierarchy.LevelMap[levelName];
      if (level != null)
      {
        rootLogger.Level = level;
        log.Info($"Logging level set to: {levelName}");
      }
      else
      {
        log.Error($"Invalid logging level: {levelName}");
      }
    }
  }
}
