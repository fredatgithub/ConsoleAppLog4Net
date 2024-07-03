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
    // don't forget to set always copy in properties of the file
    private string Log4NetConfigFilePath = "log4net.config.xml";

    private void Form1_Load(object sender, EventArgs e)
    {
      XmlConfigurator.ConfigureAndWatch(new FileInfo(Log4NetConfigFilePath));
      logger.Info("Démarrage de l'application");
      logger.Info($"Utilisateur connecté : {Environment.UserName}");
      logger.Info("Log info");
      logger.Debug("Log debug");
      logger.Error("Log error");
    }

    private void AProposdeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      logger.Info("Menu a propos de est appelé");
      AboutBoxApp about = new AboutBoxApp();
      about.Show();
    }

    private void QuitterToolStripMenuItem_Click(object sender, EventArgs e)
    {
      logger.Info("On ferme l'application");
      Application.Exit();
    }

    private void LogInformationToolStripMenuItemLogInfo_Click(object sender, EventArgs e)
    {
      // log info par défaut
      logInformationToolStripMenuItemLogInfo.Checked = true;
      logVerbeuxToolStripMenuItemLogVerbose.Checked = false;

    }

    private void LogVerbeuxToolStripMenuItemLogVerbose_Click(object sender, EventArgs e)
    {
      // log verbeux
      logInformationToolStripMenuItemLogInfo.Checked = false;
      logVerbeuxToolStripMenuItemLogVerbose.Checked = true;

    }
  }
}
