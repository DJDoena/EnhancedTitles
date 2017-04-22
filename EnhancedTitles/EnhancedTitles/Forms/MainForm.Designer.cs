namespace DoenaSoft.DVDProfiler.EnhancedTitles
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.SaveButton = new System.Windows.Forms.Button();
            this.DiscardButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExportToXMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImportFromXMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExportOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImportOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PasteAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CheckForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InvelosDataGroupBox = new System.Windows.Forms.GroupBox();
            this.OriginalTitleLabel = new System.Windows.Forms.Label();
            this.OriginalTitleTextBox = new System.Windows.Forms.TextBox();
            this.SortTitleLabel = new System.Windows.Forms.Label();
            this.SortTitleTextBox = new System.Windows.Forms.TextBox();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.TitleTextBox = new System.Windows.Forms.TextBox();
            this.PluginDataGroupBox = new System.Windows.Forms.GroupBox();
            this.AdditionalTitle2Label = new System.Windows.Forms.Label();
            this.AdditionalTitle2TextBox = new System.Windows.Forms.TextBox();
            this.AdditionalTitle1Label = new System.Windows.Forms.Label();
            this.AdditionalTitle1TextBox = new System.Windows.Forms.TextBox();
            this.NonLatinLettersTitleLabel = new System.Windows.Forms.Label();
            this.NonLatinLettersTitleTextBox = new System.Windows.Forms.TextBox();
            this.AlternateOriginalTitleLabel = new System.Windows.Forms.Label();
            this.AlternateOriginalTitleTextBox = new System.Windows.Forms.TextBox();
            this.InternationalEnglishTitleLabel = new System.Windows.Forms.Label();
            this.InternationalEnglishTitleTextBox = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.InvelosDataGroupBox.SuspendLayout();
            this.PluginDataGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(416, 298);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 1;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.OnSaveButtonClick);
            // 
            // DiscardButton
            // 
            this.DiscardButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.DiscardButton.Location = new System.Drawing.Point(497, 298);
            this.DiscardButton.Name = "DiscardButton";
            this.DiscardButton.Size = new System.Drawing.Size(75, 23);
            this.DiscardButton.TabIndex = 2;
            this.DiscardButton.Text = "Cancel";
            this.DiscardButton.UseVisualStyleBackColor = true;
            this.DiscardButton.Click += new System.EventHandler(this.OnDiscardButtonClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EditToolStripMenuItem,
            this.ToolsToolStripMenuItem,
            this.HelpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(584, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ToolsToolStripMenuItem
            // 
            this.ToolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OptionsToolStripMenuItem,
            this.ExportToXMLToolStripMenuItem,
            this.ImportFromXMLToolStripMenuItem,
            this.ExportOptionsToolStripMenuItem,
            this.ImportOptionsToolStripMenuItem});
            this.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem";
            this.ToolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.ToolsToolStripMenuItem.Text = "&Tools";
            // 
            // OptionsToolStripMenuItem
            // 
            this.OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem";
            this.OptionsToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.OptionsToolStripMenuItem.Text = "&Options";
            this.OptionsToolStripMenuItem.Click += new System.EventHandler(this.OnOptionsToolStripMenuItemClick);
            // 
            // ExportToXMLToolStripMenuItem
            // 
            this.ExportToXMLToolStripMenuItem.Name = "ExportToXMLToolStripMenuItem";
            this.ExportToXMLToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.ExportToXMLToolStripMenuItem.Text = "&Export to XML";
            this.ExportToXMLToolStripMenuItem.Click += new System.EventHandler(this.OnExportToXMLToolStripMenuItemClick);
            // 
            // ImportFromXMLToolStripMenuItem
            // 
            this.ImportFromXMLToolStripMenuItem.Name = "ImportFromXMLToolStripMenuItem";
            this.ImportFromXMLToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.ImportFromXMLToolStripMenuItem.Text = "&Import from XML";
            this.ImportFromXMLToolStripMenuItem.Click += new System.EventHandler(this.OnImportFromXMLToolStripMenuItemClick);
            // 
            // ExportOptionsToolStripMenuItem
            // 
            this.ExportOptionsToolStripMenuItem.Name = "ExportOptionsToolStripMenuItem";
            this.ExportOptionsToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.ExportOptionsToolStripMenuItem.Text = "Export Options";
            this.ExportOptionsToolStripMenuItem.Click += new System.EventHandler(this.OnExportOptionsToolStripMenuItemClick);
            // 
            // ImportOptionsToolStripMenuItem
            // 
            this.ImportOptionsToolStripMenuItem.Name = "ImportOptionsToolStripMenuItem";
            this.ImportOptionsToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.ImportOptionsToolStripMenuItem.Text = "Import Options";
            this.ImportOptionsToolStripMenuItem.Click += new System.EventHandler(this.OnImportOptionsToolStripMenuItemClick);
            // 
            // EditToolStripMenuItem
            // 
            this.EditToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CopyAllToolStripMenuItem,
            this.PasteAllToolStripMenuItem});
            this.EditToolStripMenuItem.Name = "EditToolStripMenuItem";
            this.EditToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.EditToolStripMenuItem.Text = "Edit";
            // 
            // CopyAllToolStripMenuItem
            // 
            this.CopyAllToolStripMenuItem.Name = "CopyAllToolStripMenuItem";
            this.CopyAllToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.CopyAllToolStripMenuItem.Text = "Copy all";
            this.CopyAllToolStripMenuItem.Click += new System.EventHandler(this.OnCopyAllToolStripMenuItemClick);
            // 
            // PasteAllToolStripMenuItem
            // 
            this.PasteAllToolStripMenuItem.Name = "PasteAllToolStripMenuItem";
            this.PasteAllToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.PasteAllToolStripMenuItem.Text = "Paste all";
            this.PasteAllToolStripMenuItem.Click += new System.EventHandler(this.OnPasteAllToolStripMenuItemClick);
            // 
            // HelpToolStripMenuItem
            // 
            this.HelpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CheckForUpdatesToolStripMenuItem,
            this.AboutToolStripMenuItem});
            this.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
            this.HelpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.HelpToolStripMenuItem.Text = "Help";
            // 
            // CheckForUpdatesToolStripMenuItem
            // 
            this.CheckForUpdatesToolStripMenuItem.Name = "CheckForUpdatesToolStripMenuItem";
            this.CheckForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.CheckForUpdatesToolStripMenuItem.Text = "&Check for Updates";
            this.CheckForUpdatesToolStripMenuItem.Click += new System.EventHandler(this.OnCheckForUpdatesToolStripMenuItemClick);
            // 
            // AboutToolStripMenuItem
            // 
            this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            this.AboutToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.AboutToolStripMenuItem.Text = "&About";
            this.AboutToolStripMenuItem.Click += new System.EventHandler(this.OnAboutToolStripMenuItemClick);
            // 
            // InvelosDataGroupBox
            // 
            this.InvelosDataGroupBox.Controls.Add(this.OriginalTitleLabel);
            this.InvelosDataGroupBox.Controls.Add(this.OriginalTitleTextBox);
            this.InvelosDataGroupBox.Controls.Add(this.SortTitleLabel);
            this.InvelosDataGroupBox.Controls.Add(this.SortTitleTextBox);
            this.InvelosDataGroupBox.Controls.Add(this.TitleLabel);
            this.InvelosDataGroupBox.Controls.Add(this.TitleTextBox);
            this.InvelosDataGroupBox.Location = new System.Drawing.Point(12, 27);
            this.InvelosDataGroupBox.Name = "InvelosDataGroupBox";
            this.InvelosDataGroupBox.Size = new System.Drawing.Size(560, 105);
            this.InvelosDataGroupBox.TabIndex = 4;
            this.InvelosDataGroupBox.TabStop = false;
            this.InvelosDataGroupBox.Text = "Invelos Data";
            // 
            // OriginalTitleLabel
            // 
            this.OriginalTitleLabel.AutoSize = true;
            this.OriginalTitleLabel.Location = new System.Drawing.Point(6, 74);
            this.OriginalTitleLabel.Name = "OriginalTitleLabel";
            this.OriginalTitleLabel.Size = new System.Drawing.Size(68, 13);
            this.OriginalTitleLabel.TabIndex = 4;
            this.OriginalTitleLabel.Text = "Original Title:";
            // 
            // OriginalTitleTextBox
            // 
            this.OriginalTitleTextBox.Location = new System.Drawing.Point(210, 71);
            this.OriginalTitleTextBox.MaxLength = 500;
            this.OriginalTitleTextBox.Name = "OriginalTitleTextBox";
            this.OriginalTitleTextBox.Size = new System.Drawing.Size(344, 20);
            this.OriginalTitleTextBox.TabIndex = 5;
            // 
            // SortTitleLabel
            // 
            this.SortTitleLabel.AutoSize = true;
            this.SortTitleLabel.Location = new System.Drawing.Point(6, 48);
            this.SortTitleLabel.Name = "SortTitleLabel";
            this.SortTitleLabel.Size = new System.Drawing.Size(52, 13);
            this.SortTitleLabel.TabIndex = 2;
            this.SortTitleLabel.Text = "Sort Title:";
            // 
            // SortTitleTextBox
            // 
            this.SortTitleTextBox.Location = new System.Drawing.Point(210, 45);
            this.SortTitleTextBox.MaxLength = 500;
            this.SortTitleTextBox.Name = "SortTitleTextBox";
            this.SortTitleTextBox.Size = new System.Drawing.Size(344, 20);
            this.SortTitleTextBox.TabIndex = 3;
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Location = new System.Drawing.Point(6, 22);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(30, 13);
            this.TitleLabel.TabIndex = 0;
            this.TitleLabel.Text = "Title:";
            // 
            // TitleTextBox
            // 
            this.TitleTextBox.Location = new System.Drawing.Point(210, 19);
            this.TitleTextBox.MaxLength = 500;
            this.TitleTextBox.Name = "TitleTextBox";
            this.TitleTextBox.Size = new System.Drawing.Size(344, 20);
            this.TitleTextBox.TabIndex = 1;
            // 
            // PluginDataGroupBox
            // 
            this.PluginDataGroupBox.Controls.Add(this.AdditionalTitle2Label);
            this.PluginDataGroupBox.Controls.Add(this.AdditionalTitle2TextBox);
            this.PluginDataGroupBox.Controls.Add(this.AdditionalTitle1Label);
            this.PluginDataGroupBox.Controls.Add(this.AdditionalTitle1TextBox);
            this.PluginDataGroupBox.Controls.Add(this.NonLatinLettersTitleLabel);
            this.PluginDataGroupBox.Controls.Add(this.NonLatinLettersTitleTextBox);
            this.PluginDataGroupBox.Controls.Add(this.AlternateOriginalTitleLabel);
            this.PluginDataGroupBox.Controls.Add(this.AlternateOriginalTitleTextBox);
            this.PluginDataGroupBox.Controls.Add(this.InternationalEnglishTitleLabel);
            this.PluginDataGroupBox.Controls.Add(this.InternationalEnglishTitleTextBox);
            this.PluginDataGroupBox.Location = new System.Drawing.Point(12, 138);
            this.PluginDataGroupBox.Name = "PluginDataGroupBox";
            this.PluginDataGroupBox.Size = new System.Drawing.Size(560, 154);
            this.PluginDataGroupBox.TabIndex = 0;
            this.PluginDataGroupBox.TabStop = false;
            this.PluginDataGroupBox.Text = "Plugin Data";
            // 
            // AdditionalTitle2Label
            // 
            this.AdditionalTitle2Label.AutoSize = true;
            this.AdditionalTitle2Label.Location = new System.Drawing.Point(6, 126);
            this.AdditionalTitle2Label.Name = "AdditionalTitle2Label";
            this.AdditionalTitle2Label.Size = new System.Drawing.Size(91, 13);
            this.AdditionalTitle2Label.TabIndex = 8;
            this.AdditionalTitle2Label.Text = "Alternate Title #2:";
            // 
            // AdditionalTitle2TextBox
            // 
            this.AdditionalTitle2TextBox.Location = new System.Drawing.Point(210, 123);
            this.AdditionalTitle2TextBox.MaxLength = 500;
            this.AdditionalTitle2TextBox.Name = "AdditionalTitle2TextBox";
            this.AdditionalTitle2TextBox.Size = new System.Drawing.Size(344, 20);
            this.AdditionalTitle2TextBox.TabIndex = 9;
            // 
            // AdditionalTitle1Label
            // 
            this.AdditionalTitle1Label.AutoSize = true;
            this.AdditionalTitle1Label.Location = new System.Drawing.Point(6, 100);
            this.AdditionalTitle1Label.Name = "AdditionalTitle1Label";
            this.AdditionalTitle1Label.Size = new System.Drawing.Size(91, 13);
            this.AdditionalTitle1Label.TabIndex = 6;
            this.AdditionalTitle1Label.Text = "Alternate Title #1:";
            // 
            // AdditionalTitle1TextBox
            // 
            this.AdditionalTitle1TextBox.Location = new System.Drawing.Point(210, 97);
            this.AdditionalTitle1TextBox.MaxLength = 500;
            this.AdditionalTitle1TextBox.Name = "AdditionalTitle1TextBox";
            this.AdditionalTitle1TextBox.Size = new System.Drawing.Size(344, 20);
            this.AdditionalTitle1TextBox.TabIndex = 7;
            // 
            // NonLatinLettersTitleLabel
            // 
            this.NonLatinLettersTitleLabel.AutoSize = true;
            this.NonLatinLettersTitleLabel.Location = new System.Drawing.Point(6, 74);
            this.NonLatinLettersTitleLabel.Name = "NonLatinLettersTitleLabel";
            this.NonLatinLettersTitleLabel.Size = new System.Drawing.Size(114, 13);
            this.NonLatinLettersTitleLabel.TabIndex = 4;
            this.NonLatinLettersTitleLabel.Text = "Non-Latin Letters Title:";
            // 
            // NonLatinLettersTitleTextBox
            // 
            this.NonLatinLettersTitleTextBox.Location = new System.Drawing.Point(210, 71);
            this.NonLatinLettersTitleTextBox.MaxLength = 500;
            this.NonLatinLettersTitleTextBox.Name = "NonLatinLettersTitleTextBox";
            this.NonLatinLettersTitleTextBox.Size = new System.Drawing.Size(344, 20);
            this.NonLatinLettersTitleTextBox.TabIndex = 5;
            // 
            // AlternateOriginalTitleLabel
            // 
            this.AlternateOriginalTitleLabel.AutoSize = true;
            this.AlternateOriginalTitleLabel.Location = new System.Drawing.Point(6, 48);
            this.AlternateOriginalTitleLabel.Name = "AlternateOriginalTitleLabel";
            this.AlternateOriginalTitleLabel.Size = new System.Drawing.Size(113, 13);
            this.AlternateOriginalTitleLabel.TabIndex = 2;
            this.AlternateOriginalTitleLabel.Text = "Alternate Original Title:";
            // 
            // AlternateOriginalTitleTextBox
            // 
            this.AlternateOriginalTitleTextBox.Location = new System.Drawing.Point(210, 45);
            this.AlternateOriginalTitleTextBox.MaxLength = 500;
            this.AlternateOriginalTitleTextBox.Name = "AlternateOriginalTitleTextBox";
            this.AlternateOriginalTitleTextBox.Size = new System.Drawing.Size(344, 20);
            this.AlternateOriginalTitleTextBox.TabIndex = 3;
            // 
            // InternationalEnglishTitleLabel
            // 
            this.InternationalEnglishTitleLabel.AutoSize = true;
            this.InternationalEnglishTitleLabel.Location = new System.Drawing.Point(6, 22);
            this.InternationalEnglishTitleLabel.Name = "InternationalEnglishTitleLabel";
            this.InternationalEnglishTitleLabel.Size = new System.Drawing.Size(128, 13);
            this.InternationalEnglishTitleLabel.TabIndex = 0;
            this.InternationalEnglishTitleLabel.Text = "International English Title:";
            // 
            // InternationalEnglishTitleTextBox
            // 
            this.InternationalEnglishTitleTextBox.Location = new System.Drawing.Point(210, 19);
            this.InternationalEnglishTitleTextBox.MaxLength = 500;
            this.InternationalEnglishTitleTextBox.Name = "InternationalEnglishTitleTextBox";
            this.InternationalEnglishTitleTextBox.Size = new System.Drawing.Size(344, 20);
            this.InternationalEnglishTitleTextBox.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AcceptButton = this.SaveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.DiscardButton;
            this.ClientSize = new System.Drawing.Size(584, 331);
            this.Controls.Add(this.PluginDataGroupBox);
            this.Controls.Add(this.InvelosDataGroupBox);
            this.Controls.Add(this.DiscardButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(600, 370);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(600, 370);
            this.Name = "MainForm";
            this.Text = "Enhanced Titles";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.InvelosDataGroupBox.ResumeLayout(false);
            this.InvelosDataGroupBox.PerformLayout();
            this.PluginDataGroupBox.ResumeLayout(false);
            this.PluginDataGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button DiscardButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.GroupBox InvelosDataGroupBox;
        private System.Windows.Forms.GroupBox PluginDataGroupBox;
        private System.Windows.Forms.Label AdditionalTitle1Label;
        private System.Windows.Forms.Label AdditionalTitle2Label;
        private System.Windows.Forms.Label AlternateOriginalTitleLabel;
        private System.Windows.Forms.Label InternationalEnglishTitleLabel;
        private System.Windows.Forms.Label NonLatinLettersTitleLabel;
        private System.Windows.Forms.Label OriginalTitleLabel;
        private System.Windows.Forms.Label SortTitleLabel;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.TextBox AdditionalTitle1TextBox;
        private System.Windows.Forms.TextBox AdditionalTitle2TextBox;
        private System.Windows.Forms.TextBox AlternateOriginalTitleTextBox;
        private System.Windows.Forms.TextBox InternationalEnglishTitleTextBox;
        private System.Windows.Forms.TextBox NonLatinLettersTitleTextBox;
        private System.Windows.Forms.TextBox OriginalTitleTextBox;
        private System.Windows.Forms.TextBox SortTitleTextBox;
        private System.Windows.Forms.TextBox TitleTextBox;
        private System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CheckForUpdatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CopyAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExportOptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExportToXMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ImportFromXMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ImportOptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PasteAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolsToolStripMenuItem;

    }
}