namespace DoenaSoft.DVDProfiler.GetCustomDVDFieldDefinitions
{
    partial class PluginFieldAccessForm
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
            this.PluginTreeView = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // PluginTreeView
            // 
            this.PluginTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PluginTreeView.Location = new System.Drawing.Point(12, 12);
            this.PluginTreeView.Name = "PluginTreeView";
            this.PluginTreeView.Size = new System.Drawing.Size(543, 331);
            this.PluginTreeView.TabIndex = 0;
            // 
            // PluginFieldAccessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 355);
            this.Controls.Add(this.PluginTreeView);
            this.Name = "PluginFieldAccessForm";
            this.Text = "Plugin Field Access";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView PluginTreeView;
    }
}