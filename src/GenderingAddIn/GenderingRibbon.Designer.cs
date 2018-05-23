namespace GenderingAddIn
{
  partial class GenderingRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public GenderingRibbon()
      : base(Globals.Factory.GetRibbonFactory())
    {
      InitializeComponent();
    }

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GenderingRibbon));
      this.genderingTab = this.Factory.CreateRibbonTab();
      this.GenderingGroup = this.Factory.CreateRibbonGroup();
      this.vollformBox = this.Factory.CreateRibbonCheckBox();
      this.binnenIBox = this.Factory.CreateRibbonCheckBox();
      this.separator1 = this.Factory.CreateRibbonSeparator();
      this.separator2 = this.Factory.CreateRibbonSeparator();
      this.partCheckBox = this.Factory.CreateRibbonCheckBox();
      this.CheckGenderButton = this.Factory.CreateRibbonButton();
      this.RemoveHighlightButton = this.Factory.CreateRibbonButton();
      this.genderingTab.SuspendLayout();
      this.GenderingGroup.SuspendLayout();
      // 
      // genderingTab
      // 
      this.genderingTab.Groups.Add(this.GenderingGroup);
      this.genderingTab.Label = "Gendering";
      this.genderingTab.Name = "genderingTab";
      // 
      // GenderingGroup
      // 
      this.GenderingGroup.Items.Add(this.vollformBox);
      this.GenderingGroup.Items.Add(this.binnenIBox);
      this.GenderingGroup.Items.Add(this.separator1);
      this.GenderingGroup.Items.Add(this.CheckGenderButton);
      this.GenderingGroup.Items.Add(this.partCheckBox);
      this.GenderingGroup.Items.Add(this.separator2);
      this.GenderingGroup.Items.Add(this.RemoveHighlightButton);
      this.GenderingGroup.Label = "Gendering";
      this.GenderingGroup.Name = "GenderingGroup";
      // 
      // vollformBox
      // 
      this.vollformBox.Checked = true;
      this.vollformBox.Label = "Vollform";
      this.vollformBox.Name = "vollformBox";
      this.vollformBox.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.vollformBox_Click);
      // 
      // binnenIBox
      // 
      this.binnenIBox.Label = "Binnen-I";
      this.binnenIBox.Name = "binnenIBox";
      this.binnenIBox.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.binnenIBox_Click);
      // 
      // separator1
      // 
      this.separator1.Name = "separator1";
      // 
      // separator2
      // 
      this.separator2.Name = "separator2";
      // 
      // partCheckBox
      // 
      this.partCheckBox.Label = "Wortteile finden";
      this.partCheckBox.Name = "partCheckBox";
      // 
      // CheckGenderButton
      // 
      this.CheckGenderButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
      this.CheckGenderButton.Image = ((System.Drawing.Image)(resources.GetObject("CheckGenderButton.Image")));
      this.CheckGenderButton.Label = "Text überprüfen";
      this.CheckGenderButton.Name = "CheckGenderButton";
      this.CheckGenderButton.ShowImage = true;
      this.CheckGenderButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.CheckGenderButton_Click);
      // 
      // RemoveHighlightButton
      // 
      this.RemoveHighlightButton.Image = ((System.Drawing.Image)(resources.GetObject("RemoveHighlightButton.Image")));
      this.RemoveHighlightButton.Label = "Markierungen entfernen";
      this.RemoveHighlightButton.Name = "RemoveHighlightButton";
      this.RemoveHighlightButton.ShowImage = true;
      this.RemoveHighlightButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.RemoveHighlightButton_Click);
      // 
      // GenderingRibbon
      // 
      this.Name = "GenderingRibbon";
      this.RibbonType = "Microsoft.Word.Document";
      this.Tabs.Add(this.genderingTab);
      this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.GenderingRibbon_Load);
      this.genderingTab.ResumeLayout(false);
      this.genderingTab.PerformLayout();
      this.GenderingGroup.ResumeLayout(false);
      this.GenderingGroup.PerformLayout();

    }

    #endregion

    internal Microsoft.Office.Tools.Ribbon.RibbonGroup GenderingGroup;
    internal Microsoft.Office.Tools.Ribbon.RibbonTab genderingTab;
    internal Microsoft.Office.Tools.Ribbon.RibbonButton CheckGenderButton;
    internal Microsoft.Office.Tools.Ribbon.RibbonSeparator separator1;
    internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox binnenIBox;
    internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox vollformBox;
    internal Microsoft.Office.Tools.Ribbon.RibbonSeparator separator2;
    internal Microsoft.Office.Tools.Ribbon.RibbonButton RemoveHighlightButton;
    internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox partCheckBox;
  }

  partial class ThisRibbonCollection
  {
    internal GenderingRibbon GenderingRibbon
    {
      get { return this.GetRibbon<GenderingRibbon>(); }
    }
  }
}
