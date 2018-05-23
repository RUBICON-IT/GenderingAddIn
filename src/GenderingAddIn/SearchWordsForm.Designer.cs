namespace GenderingAddIn
{
  partial class SearchWordsForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

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

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchWordsForm));
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.SearchForLabel = new System.Windows.Forms.Label();
      this.PercentageLabel = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.SuspendLayout();
      // 
      // pictureBox1
      // 
      this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
      this.pictureBox1.Location = new System.Drawing.Point(49, 12);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(16, 16);
      this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pictureBox1.TabIndex = 0;
      this.pictureBox1.TabStop = false;
      // 
      // SearchForLabel
      // 
      this.SearchForLabel.AutoSize = true;
      this.SearchForLabel.Location = new System.Drawing.Point(72, 14);
      this.SearchForLabel.Name = "SearchForLabel";
      this.SearchForLabel.Size = new System.Drawing.Size(77, 13);
      this.SearchForLabel.TabIndex = 1;
      this.SearchForLabel.Text = "SearchForText";
      this.SearchForLabel.UseWaitCursor = true;
      // 
      // PercentageLabel
      // 
      this.PercentageLabel.AutoSize = true;
      this.PercentageLabel.Location = new System.Drawing.Point(13, 13);
      this.PercentageLabel.Name = "PercentageLabel";
      this.PercentageLabel.Size = new System.Drawing.Size(21, 13);
      this.PercentageLabel.TabIndex = 2;
      this.PercentageLabel.Text = "0%";
      // 
      // SearchWordsForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(284, 46);
      this.ControlBox = false;
      this.Controls.Add(this.PercentageLabel);
      this.Controls.Add(this.SearchForLabel);
      this.Controls.Add(this.pictureBox1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Name = "SearchWordsForm";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Gendering";
      this.TopMost = true;
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.PictureBox pictureBox1;
    public System.Windows.Forms.Label SearchForLabel;
    public System.Windows.Forms.Label PercentageLabel;
  }
}