using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Word;
using Microsoft.Office.Tools.Ribbon;
using Word = Microsoft.Office.Interop.Word;
using System.Xml;
using System.Diagnostics;
using System.Windows.Forms;

namespace GenderingAddIn
{
  public partial class GenderingRibbon
  {
    private const string GenderingBoxTitle = "Gendering";
    private const string GenderingFieldName = "GenderingAddinField";

    private void GenderingRibbon_Load(object sender, RibbonUIEventArgs e)
    {
    }

    private void CheckGenderButton_Click(object sender, RibbonControlEventArgs e)
    {
      SearchWordsForm statusForm = null;

      try
      {
        ThisAddIn.MyApplication.DisplayAlerts = Word.WdAlertLevel.wdAlertsNone;

        Word.Document document = ThisAddIn.MyApplication.ActiveDocument;

        if (!RemoveGenderingFields())
          return;

        statusForm = new SearchWordsForm();
        statusForm.Show();

        XmlDocument genderingTable = GenderingEngine.GetGenderingTable();
        List<string> words = GetWords(genderingTable);

        List<Word.Range> foundRanges = GetFoundRanges(statusForm, document, words);

        bool switchToFormFillIn = false;

        foreach (Word.Range foundRange in foundRanges)
        {
          List<string> alternatives = GetAlternatives(genderingTable, foundRange.Text.ToLowerInvariant());

          Trace.WriteLine(foundRange.Start);
          Trace.WriteLine(foundRange.End);

          bool alreadyGendered = false;
          foreach (string alternative in alternatives)
          {
            if (IsAlreadyGendered(document, foundRange, alternative))
            {
              alreadyGendered = true;
              break;
            }
          }

          if (alternatives.Count != 0 && !alreadyGendered)
          {
            CreateDropDown(document, foundRange, alternatives);
            switchToFormFillIn = true;
          }
        }

        if (switchToFormFillIn && Is97(document))
        {
          document.Protect(WdProtectionType.wdAllowOnlyFormFields); 
        }
      }
      catch (Exception exception)
      {
        MessageBox.Show(exception.Message, "Fehler", MessageBoxButtons.OK);
      }
      finally
      {
        if (statusForm != null)
          statusForm.Close();

        ThisAddIn.MyApplication.DisplayAlerts = Word.WdAlertLevel.wdAlertsMessageBox;
      }
    }

    private List<Range> GetFoundRanges(SearchWordsForm statusForm, Document document, List<string> words)
    {
      List<Range> foundRanges = new List<Word.Range>();
      Range range;
      int i = 0;

      foreach (string word in words)
      {
        i++;
        statusForm.PercentageLabel.Text = string.Format("{0}%", (int)(100.0 * ((double)i) / words.Count));
        statusForm.SearchForLabel.Text = word;
        statusForm.Update();

        range = document.Content;
        range.Find.Forward = true;
        range.Find.Wrap = Word.WdFindWrap.wdFindStop;
        range.Find.Text = word;
        range.Find.MatchWholeWord = !partCheckBox.Checked;

        while (range.Find.Execute())
        {
          foundRanges.Add(range.Duplicate);
        }
      }

      return foundRanges;
    }

    private bool IsAlreadyGendered(Document document, Range foundRange, string alternative)
    {
      object start = foundRange.Start;
      object end = foundRange.Start + alternative.Length;
      Word.Range checkRange = null;
      try
      {
        checkRange = document.Range(ref start, ref end);
        if (checkRange.Text.ToLowerInvariant() == alternative.ToLowerInvariant())
          return true;
      }
      catch { };
              
      end = foundRange.End;
      start = foundRange.End - alternative.Length;
      try
      {
        checkRange = document.Range(ref start, ref end);
        if (checkRange.Text.ToLowerInvariant() == alternative.ToLowerInvariant())
          return true;
      }
      catch { };
      return false;
    }

    private bool Is97(Document document)
    {
      return document.SaveFormat.Equals((int) WdSaveFormat.wdFormatDocument97);
    }

    private void CreateDropDown(Document document, Range foundRange, List<string> alternatives)
    {

      if (Is97(document))
      {
        Word.Range fieldRange = foundRange.Duplicate;
        string text = foundRange.Text;
        FormField alternativesField = foundRange.FormFields.Add(fieldRange, WdFieldType.wdFieldFormDropDown);
        //alternativesField.StatusText = GenderingBoxTitle;
        DropDown dropDown = alternativesField.DropDown;
        dropDown.ListEntries.Add(text);
        foreach (string alternative in alternatives)
          dropDown.ListEntries.Add(alternative);
        alternativesField.Name = GenderingFieldName;
      }
      else
      {
        foundRange.HighlightColorIndex = Word.WdColorIndex.wdGray25;

        object wordref = foundRange;
        string rangeText = foundRange.Text;

        try
        {
          if (foundRange.ContentControls.Count == 0)
          {
            Word.ContentControl combobox = document.ContentControls.Add(Word.WdContentControlType.wdContentControlComboBox, ref wordref);
            combobox.DropdownListEntries.Add(rangeText, Guid.NewGuid().ToString());
            combobox.Tag = GenderingFieldName;
            combobox.Title = GenderingBoxTitle;
            foreach (string alternative in alternatives)
            {
              combobox.DropdownListEntries.Add(alternative, Guid.NewGuid().ToString());
            }
          }
        }
        catch
        {
        }

      }
    }

    private List<string> GetAllAlternatives(XmlDocument genderingTable)
    {
      List<string> alternatives = new List<string>();
      XmlNodeList nodes = genderingTable.SelectNodes("//Hauptwort");
      foreach (XmlNode node in nodes)
      {
        XmlNode alternateTextNode = node.NextSibling;
        XmlNode nodeTypeNode = alternateTextNode.NextSibling;
        if (alternateTextNode != null)
        {
          if (nodeTypeNode != null)
          {
            bool binnenI = nodeTypeNode.InnerText.ToLowerInvariant() == "y";

            if (binnenIBox.Checked && binnenI || vollformBox.Checked && !binnenI)
            {
              if (!alternatives.Contains(alternateTextNode.InnerText)) 
                alternatives.Add(alternateTextNode.InnerText);
            }
          }
        }
      }
      return alternatives;
    }
    
    private List<string> GetAlternatives(XmlDocument genderingTable, string word)
    {
      List<string> alternatives = new List<string>();

      XmlNodeList nodes = genderingTable.SelectNodes(string.Format("//Hauptwort[translate(text(),'ABCDEFGHIJKLMNOPQRSTUVWXYZÄÖÜß','abcdefghijklmnopqrstuvwxyzäöüß')='{0}']", word));
      foreach (XmlNode node in nodes)
      {
        XmlNode alternateTextNode = node.NextSibling;
        XmlNode nodeTypeNode = alternateTextNode.NextSibling;
        if (alternateTextNode != null)
        {
          if (nodeTypeNode != null)
          {
            bool binnenI = nodeTypeNode.InnerText.ToLowerInvariant() == "y";

            if (binnenIBox.Checked && binnenI || vollformBox.Checked && !binnenI)
            {
              alternatives.Add(alternateTextNode.InnerText);
            }
          }
        }
      }

      return alternatives;
    }

    private List<string> GetWords(XmlDocument genderingTable)
    {
      List<string> words = new List<string>();

      XmlNodeList nodes = genderingTable.SelectNodes("//Hauptwort");
      foreach (XmlNode node in nodes)
      {
        if (!words.Contains(node.InnerText))
          words.Add(node.InnerText);
      }

      return words;
    }

    private void vollformBox_Click(object sender, RibbonControlEventArgs e)
    {
    }

    private void binnenIBox_Click(object sender, RibbonControlEventArgs e)
    {
    }

    private void RemoveHighlightButton_Click(object sender, RibbonControlEventArgs e)
    {
      RemoveGenderingFields();
    }

    private bool RemoveGenderingFields()
    {
      Word.Document document = ThisAddIn.MyApplication.ActiveDocument;

      if (Is97(document))
      {
        if (! document.ProtectionType.Equals((object) WdProtectionType.wdNoProtection))
        {
          try
          {
            document.Unprotect();
          }
          catch (COMException e)
          {
            if (e.ErrorCode == -2146822803)
            {
              MessageBox.Show("Zum Anbieten von Gendering-Vorschlägen wird der Dokumentschutz-Modus 'Felder ausfüllen' ohne Kennwort verwendet." 
                + " Der bestehende Dokumentschutz konnte nicht aufgehoben werden, möglicherweise wurde ein Kennwort vergeben. Es liegt folgendes Problem vor: " 
                + e.Message, "Fehler", MessageBoxButtons.OK);

              return false;
            }
            else
            {
              throw;
            }
          }
        }

        foreach (FormField field in document.FormFields)
        {
          if (field.Type == WdFieldType.wdFieldFormDropDown && field.Name == GenderingFieldName)
          {
            string text = field.Result;
            Range range = field.Range.Duplicate;
            range.InsertAfter(text);
            field.Delete();
          }
        }
      }
      else
      {
        foreach (Word.ContentControl contcontrol in document.ContentControls)
        {
          if (contcontrol.Tag == GenderingFieldName)
          {
            contcontrol.Range.HighlightColorIndex = Word.WdColorIndex.wdNoHighlight;
            contcontrol.Delete();
          }
        }
      }
      
      return true;
    }
  }
}
