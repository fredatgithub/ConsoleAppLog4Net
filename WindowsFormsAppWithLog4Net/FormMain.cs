using System;
using System.IO;
using System.Windows.Forms;
using log4net;
using log4net.Config;

namespace WindowsFormsAppWithLog4Net
{
  public partial class FormMain : Form
  {
    public FormMain()
    {
      InitializeComponent();
    }

    internal ILog logger = LogManager.GetLogger(typeof(FormMain));
    private string Log4NetConfigFilePath = "log4net.config.xml";

    private void Form1_Load(object sender, EventArgs e)
    {
      XmlConfigurator.ConfigureAndWatch(new FileInfo(Log4NetConfigFilePath));
      logger.Info("Démarrage de l'application");
      logger.Info($"Utilisateur connecté : {Environment.UserName}");
    }
  }
}
