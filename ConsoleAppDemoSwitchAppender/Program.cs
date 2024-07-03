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
      SetLoggingLevel("INFO");

      Console.WriteLine("Application started. Current logger is mainFileAppender.");
      log.Info("Application started. Current logger is mainFileAppender.");
      log.Info("This is an info message using mainFileAppender.");
      log.Debug("******************log debug in mainfile appender, you should not be able to see it in log file with info mode");
      SetInternalDebugging(true);
      SetLoggingLevel("DEBUG");
      log.Info("This is an info message using mainFileAppender in debug mode.");
      log.Debug("log debug in mainfile appender, you should be able to see it in log file with debug mode");
      Console.WriteLine("Now we switch to developerAppender...");

      SwitchAppenderV2("developerAppender");
      ListAppenders();
      SetInternalDebugging(false);
      SetLoggingLevel("INFO");
      Console.WriteLine("Logger switched to developerAppender.");
      log.Info("This is an info message using developerAppender with info mode.");
      log.Debug("******************log debug in developer appender, you should not be able to see it in log file with info mode");
      Console.WriteLine("Now switching to logging level to DEBUG...");

      SetInternalDebugging(true);
      SetLoggingLevel("DEBUG");
      Console.WriteLine("This is a debug message using developerAppender");
      log.Info("This is info message using developerAppender.");
      log.Debug("This is a debug message using developerAppender.");

      Console.WriteLine("Press any key to set logging level to INFO...");
      SetInternalDebugging(false);
      SetLoggingLevel("INFO");
      Console.WriteLine("This is an info message using developerAppender");
      log.Info("This is an info message using developerAppender.");
      log.Debug("back to info, no debug message should be in log file with info mode");
      Console.WriteLine("Press any key to exit...");
      Console.ReadKey();
    }

    private static void SetInternalDebugging(bool value = true)
    {
      // Activer les logs de débogage pour log4net
      log4net.Util.LogLog.InternalDebugging = value;
    }

    private static void ListAppenders()
    {
      var hierarchy = (Hierarchy)LogManager.GetRepository();
      var appenders = hierarchy.GetAppenders();

      //log.Info("Available appenders:");
      Console.WriteLine("Available appenders:");
      foreach (var appender in appenders)
      {
        //log.Info($"- {appender.Name}");
        Console.WriteLine($"- {appender.Name}");
      }

      Console.WriteLine("");
    }

    private static void SwitchAppenderV1(string newAppenderName)
    {
      var hierarchy = (Hierarchy)LogManager.GetRepository();
      var rootLogger = hierarchy.Root;

      // Liste les appenders disponibles pour debug
      var appenders = hierarchy.GetAppenders();
      //Console.WriteLine("Available appenders:");
      //foreach (var appender in appenders)
      //{
      //  Console.WriteLine($"- {appender.Name}");
      //}

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

    private static void SwitchAppenderV2(string newAppenderName)
    {
      var hierarchy = (Hierarchy)LogManager.GetRepository();
      var rootLogger = hierarchy.Root;

      // Désactivez tous les appender actuels
      rootLogger.RemoveAllAppenders();

      // Récupère le nouvel appender et l'ajoute au root logger
      IAppender newAppender = hierarchy.GetAppenders().FirstOrDefault(appender => appender.Name == newAppenderName);

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

    private static void SwitchAppenderV3(string newAppenderName)
    {
      var hierarchy = (Hierarchy)LogManager.GetRepository();

      // Clear the existing configuration
      hierarchy.ResetConfiguration();

      // Reconfigure log4net using the original configuration file
      XmlConfigurator.Configure(new FileInfo("log4net.config.xml"));

      var newAppender = hierarchy.GetAppenders().FirstOrDefault(appender => appender.Name == newAppenderName);

      if (newAppender != null)
      {
        var rootLogger = hierarchy.Root;
        rootLogger.RemoveAllAppenders();
        rootLogger.AddAppender(newAppender);
        log.Info($"Switched to appender: {newAppenderName}");
      }
      else
      {
        log.Error($"Appender {newAppenderName} not found.");
      }

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
