using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Xml;
using Microsoft.Win32;
using System.IO;
using System.Reflection;
using System.Net;

namespace GenderingAddIn
{
  public class GenderingEngine
  {
    public static XmlDocument GetGenderingTable()
    {
      string fullPath = GetWordTableFullPath();
      Stream stream = null;
      XmlDocument genderingTable = new XmlDocument();

      try
      {
        if (fullPath.ToLowerInvariant().StartsWith("res:"))
        {
          stream = Assembly.GetAssembly(typeof(ThisAddIn)).GetManifestResourceStream(fullPath.Substring(4));
        }
        else if (fullPath.ToLowerInvariant().StartsWith("file://"))
        {
          stream = File.Open(fullPath.Substring(7), FileMode.Open, FileAccess.Read);
        }
        else
        {
          WebRequest request = HttpWebRequest.Create(fullPath);
          request.Credentials = CredentialCache.DefaultCredentials;
          WebResponse response = request.GetResponse();
          stream = response.GetResponseStream();
        }

        using (StreamReader reader = new StreamReader(stream))
        {
          genderingTable.LoadXml(reader.ReadToEnd());
          reader.Close();
        }
      }
      catch (Exception)
      {
        throw;
      }
      finally
      {
        if (stream != null)
          stream.Close();
      }

      return genderingTable;
    }

    private static string GetWordTableFullPath()
    {
      string path = @"res:GenderingAddIn.Worttabelle.xml";
      object pathValue = GetPathValue(Registry.CurrentUser);
      if (pathValue == null)
      {
        pathValue = GetPathValue(Registry.LocalMachine);
      }
      if (pathValue != null)
      {
        path = pathValue.ToString();
      }

      return path;
    }

    private static object GetPathValue(RegistryKey registryKey)
    {
      object pathValue = null;
      try
      {
        using (RegistryKey softwareKey = registryKey.OpenSubKey(@"Software"))
        {
          using (RegistryKey settingsKey = softwareKey.OpenSubKey(@"Rubicon\GenderingAddIn\Settings"))
          {
            if (settingsKey != null)
            {
              pathValue = settingsKey.GetValue("GenderingTableUrl");
            }
          }
        }
      }
      catch (SecurityException)
      {
      }
      return pathValue;
    }
  }
}
