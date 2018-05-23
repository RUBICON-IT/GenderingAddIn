using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Word;
using System.Xml;
using System.IO;
using System.Reflection;
using Microsoft.Win32;

namespace GenderingAddIn
{
  public partial class ThisAddIn
  {
    static public Word.Application MyApplication = null;

    private void ThisAddIn_Startup(object sender, System.EventArgs e)
    {
      MyApplication = this.Application;
    }

    private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
    {
    }

    #region VSTO generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InternalStartup()
    {
      this.Startup += new System.EventHandler(ThisAddIn_Startup);
      this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
    }

    #endregion
  }
}
