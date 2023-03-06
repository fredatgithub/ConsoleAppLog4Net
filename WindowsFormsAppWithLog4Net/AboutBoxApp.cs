using System;
using System.Reflection;
using System.Windows.Forms;
using log4net;

namespace WindowsFormsAppWithLog4Net
{
  partial class AboutBoxApp : Form
  {
    public AboutBoxApp()
    {
      InitializeComponent();
      Text = string.Format("À propos de {0}", AssemblyTitle);
      labelProductName.Text = AssemblyProduct;
      labelVersion.Text = String.Format("Version {0}", AssemblyVersion);
      labelCopyright.Text = AssemblyCopyright;
      labelCompanyName.Text = AssemblyCompany;
      textBoxDescription.Text = AssemblyDescription;
    }

    internal ILog logger = LogManager.GetLogger(typeof(FormMain));

    #region Accesseurs d'attribut de l'assembly

    public string AssemblyTitle
    {
      get
      {
        object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
        if (attributes.Length > 0)
        {
          AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
          if (titleAttribute.Title != "")
          {
            return titleAttribute.Title;
          }
        }
        return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
      }
    }

    public string AssemblyVersion
    {
      get
      {
        return Assembly.GetExecutingAssembly().GetName().Version.ToString();
      }
    }

    public string AssemblyDescription
    {
      get
      {
        object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
        if (attributes.Length == 0)
        {
          return "";
        }
        return ((AssemblyDescriptionAttribute)attributes[0]).Description;
      }
    }

    public string AssemblyProduct
    {
      get
      {
        object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
        if (attributes.Length == 0)
        {
          return "";
        }
        return ((AssemblyProductAttribute)attributes[0]).Product;
      }
    }

    public string AssemblyCopyright
    {
      get
      {
        object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
        if (attributes.Length == 0)
        {
          return "";
        }
        return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
      }
    }

    public string AssemblyCompany
    {
      get
      {
        object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
        if (attributes.Length == 0)
        {
          return "";
        }
        return ((AssemblyCompanyAttribute)attributes[0]).Company;
      }
    }
    #endregion

    private void OkButton_Click(object sender, EventArgs e)
    {
      logger.Info("about box window being closed");
      Close();
    }

    private void AboutBoxApp_Load(object sender, EventArgs e)
    {
      logger.Info("about box window being openned");
    }
  }
}
