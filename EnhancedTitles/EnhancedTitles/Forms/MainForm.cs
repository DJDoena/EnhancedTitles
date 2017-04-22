using DoenaSoft.DVDProfiler.DVDProfilerHelper;
using DoenaSoft.DVDProfiler.EnhancedTitles.Resources;
using Invelos.DVDProfilerPlugin;
using System;
using System.IO;
using System.Windows.Forms;

namespace DoenaSoft.DVDProfiler.EnhancedTitles
{
    internal sealed partial class MainForm : Form
    {
        private readonly Plugin Plugin;

        private readonly IDVDInfo Profile;

        private readonly TitleManager TitleManager;

        private readonly Boolean FullEdit;

        private Boolean DataChanged;

        internal MainForm(Plugin plugin
            , IDVDInfo profile
            , Boolean fullEdit)
        {
            Plugin = plugin;
            Profile = profile;
            FullEdit = fullEdit;

            InitializeComponent();

            TitleManager = new TitleManager(Profile);

            SetTextBoxes();
            SetLabels();
            SetReadOnlies();

            DataChanged = false;
        }

        private void SetReadOnlies()
        {
            TitleTextBox.Enabled = FullEdit;
            SortTitleTextBox.Enabled = FullEdit;
            OriginalTitleTextBox.Enabled = FullEdit;

            if (Plugin.IsRemoteAccess)
            {
                ImportFromXMLToolStripMenuItem.Enabled = false;
                PasteAllToolStripMenuItem.Enabled = false;
                SaveButton.Enabled = false;

                SetControlsReadonly(Controls);
            }
        }

        private void SetControlsReadonly(Control.ControlCollection controls)
        {
            if (controls != null)
            {
                foreach (Control control in controls)
                {
                    if (control is TextBox)
                    {
                        ((TextBox)control).ReadOnly = true;
                    }
                    else
                    {
                        SetControlsReadonly(control.Controls);
                    }
                }
            }
        }

        private void SetTextBoxes()
        {
            #region Invelos Data

            TitleTextBox.Text = TitleManager.GetTitle();
            SortTitleTextBox.Text = TitleManager.GetSortTitle();
            OriginalTitleTextBox.Text = TitleManager.GetOriginalTitle();

            #endregion

            #region Plugin Data

            InternationalEnglishTitleTextBox.Text = TitleManager.GetInternationalEnglishTitleWithFallback();
            AlternateOriginalTitleTextBox.Text = TitleManager.GetAlternateOriginalTitleWithFallback();
            NonLatinLettersTitleTextBox.Text = TitleManager.GetNonLatinLettersTitleWithFallback();
            AdditionalTitle1TextBox.Text = TitleManager.GetAdditionalTitle1WithFallback();
            AdditionalTitle2TextBox.Text = TitleManager.GetAdditionalTitle2WithFallback();

            #endregion
        }

        private void SetLabels()
        {
            DefaultValues dv;

            dv = Plugin.Settings.DefaultValues;

            #region Invelos Data

            TitleLabel.Text = Texts.Title;
            SortTitleLabel.Text = Texts.SortTitle;
            OriginalTitleLabel.Text = Texts.OriginalTitle;

            #endregion

            #region Plugin Data

            InternationalEnglishTitleLabel.Text = dv.InternationalEnglishTitleLabel;
            AlternateOriginalTitleLabel.Text = dv.AlternateOriginalTitleLabel;
            NonLatinLettersTitleLabel.Text = dv.NonLatinLettersTitleLabel;
            AdditionalTitle1Label.Text = dv.AdditionalTitle1Label;
            AdditionalTitle2Label.Text = dv.AdditionalTitle2Label;

            #endregion

            #region Misc

            #region GroupBoxes

            InvelosDataGroupBox.Text = Texts.InvelosData;
            PluginDataGroupBox.Text = Texts.PluginData;

            #endregion

            #region Menu

            EditToolStripMenuItem.Text = Texts.Edit;
            CopyAllToolStripMenuItem.Text = Texts.CopyAllToClipboard;
            PasteAllToolStripMenuItem.Text = Texts.PasteAllFromClipboard;

            ToolsToolStripMenuItem.Text = Texts.Tools;
            OptionsToolStripMenuItem.Text = Texts.Options;
            ExportToXMLToolStripMenuItem.Text = Texts.ExportToXml;
            ImportFromXMLToolStripMenuItem.Text = Texts.ImportFromXml;
            ExportOptionsToolStripMenuItem.Text = Texts.ExportOptions;
            ImportOptionsToolStripMenuItem.Text = Texts.ImportOptions;

            HelpToolStripMenuItem.Text = Texts.Help;
            CheckForUpdatesToolStripMenuItem.Text = Texts.CheckForUpdates;
            AboutToolStripMenuItem.Text = Texts.About;


            #endregion

            #region Buttons

            SaveButton.Text = Texts.Save;
            DiscardButton.Text = Texts.Cancel;

            #endregion

            #endregion
        }

        private void OnSaveButtonClick(Object sender, EventArgs e)
        {
            #region Invelos Data

            if (FullEdit)
            {
                TitleManager.SetTitle(TitleTextBox.Text);
                TitleManager.SetSortTitle(SortTitleTextBox.Text);
                TitleManager.SetOriginalTitle(OriginalTitleTextBox.Text);
            }

            #endregion

            #region Plugin Data

            TitleManager.SetInternationalEnglishTitle(InternationalEnglishTitleTextBox.Text);
            TitleManager.SetAlternateOriginalTitle(AlternateOriginalTitleTextBox.Text);
            TitleManager.SetNonLatinLettersTitle(NonLatinLettersTitleTextBox.Text);
            TitleManager.SetAdditionalTitle1(AdditionalTitle1TextBox.Text);
            TitleManager.SetAdditionalTitle2(AdditionalTitle2TextBox.Text);

            #endregion

            if (FullEdit)
            {
                Plugin.Api.SaveDVDToCollection(Profile);
            }

            Plugin.Api.ReloadCurrentDVD();

            DataChanged = false;

            Close();
        }

        private void OnDiscardButtonClick(Object sender, EventArgs e)
        {
            Close();
        }

        private void OnOptionsToolStripMenuItemClick(Object sender, EventArgs e)
        {
            using (SettingsForm form = new SettingsForm(Plugin))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    SetLabels();
                    Plugin.RegisterCustomFields();
                }
            }
        }

        private void OnExportToXMLToolStripMenuItemClick(Object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.AddExtension = true;
                sfd.DefaultExt = ".xml";
                sfd.Filter = "XML files|*.xml";
                sfd.OverwritePrompt = true;
                sfd.RestoreDirectory = true;
                sfd.Title = Texts.SaveXmlFile;
                sfd.FileName = "EnhancedTitles." + Profile.GetProfileID() + ".xml";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    EnhancedTitles et;

                    et = GetEnhancedTitlesForXmlStructure();

                    try
                    {
                        et.Serialize(sfd.FileName);
                        MessageBox.Show(MessageBoxTexts.Done, MessageBoxTexts.InformationHeader, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(String.Format(MessageBoxTexts.FileCantBeWritten, sfd.FileName, ex.Message)
                            , MessageBoxTexts.ErrorHeader, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private EnhancedTitles GetEnhancedTitlesForXmlStructure()
        {
            EnhancedTitles et;
            DefaultValues dv;

            dv = Plugin.Settings.DefaultValues;
            et = new EnhancedTitles();

            et.InternationalEnglishTitle = GetText(InternationalEnglishTitleTextBox, dv.InternationalEnglishTitleLabel);
            et.AlternateOriginalTitle = GetText(AlternateOriginalTitleTextBox, dv.AlternateOriginalTitleLabel);
            et.NonLatinLettersTitle = GetText(NonLatinLettersTitleTextBox, dv.NonLatinLettersTitleLabel);
            et.AdditionalTitle1 = GetText(AdditionalTitle1TextBox, dv.AdditionalTitle1Label);
            et.AdditionalTitle2 = GetText(AdditionalTitle2TextBox, dv.AdditionalTitle2Label);

            et.InvelosData = new InvelosData();
            et.InvelosData.Title = TitleTextBox.Text;
            et.InvelosData.SortTitle = SortTitleTextBox.Text;
            et.InvelosData.OriginalTitle = OriginalTitleTextBox.Text;

            return (et);
        }

        private Text GetText(TextBox textBox
            , String displayName)
        {
            Text text;
            String title;

            title = textBox.Text;
            if (String.IsNullOrEmpty(title) == false)
            {
                text = new Text();
                text.Value = title;
                text.DisplayName = displayName;
            }
            else
            {
                text = null;
            }
            return (text);
        }

        private void OnImportFromXMLToolStripMenuItemClick(Object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.CheckFileExists = true;
                ofd.Filter = "XML files|*.xml";
                ofd.Multiselect = false;
                ofd.RestoreDirectory = true;
                ofd.Title = Texts.LoadXmlFile;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    EnhancedTitles et;

                    try
                    {
                        et = EnhancedTitles.Deserialize(ofd.FileName);
                    }
                    catch (Exception ex)
                    {
                        et = null;
                        MessageBox.Show(String.Format(MessageBoxTexts.FileCantBeRead, ofd.FileName, ex.Message)
                           , MessageBoxTexts.ErrorHeader, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    if (et != null)
                    {
                        SetEnhancedTitlesFromXmlStructure(et);
                    }
                }
            }
        }

        private void SetEnhancedTitlesFromXmlStructure(EnhancedTitles et)
        {
            SetText(et.InternationalEnglishTitle, InternationalEnglishTitleTextBox);
            SetText(et.AlternateOriginalTitle, AlternateOriginalTitleTextBox);
            SetText(et.NonLatinLettersTitle, NonLatinLettersTitleTextBox);
            SetText(et.AdditionalTitle1, AdditionalTitle1TextBox);
            SetText(et.AdditionalTitle2, AdditionalTitle2TextBox);
        }

        private void SetText(Text text
            , TextBox textBox)
        {
            if ((text != null) && (text.Value != null))
            {
                textBox.Text = text.Value;
            }
            else
            {
                textBox.Text = String.Empty;
            }
        }

        private void OnCheckForUpdatesToolStripMenuItemClick(Object sender, EventArgs e)
        {
            OnlineAccess.Init("Doena Soft.", "EnhancedTitles");
            OnlineAccess.CheckForNewVersion("http://doena-soft.de/dvdprofiler/3.9.0/versions.xml", this, "EnhancedTitles", GetType().Assembly);
        }

        private void OnAboutToolStripMenuItemClick(Object sender, EventArgs e)
        {
            using (AboutBox aboutBox = new AboutBox(GetType().Assembly))
            {
                aboutBox.ShowDialog();
            }
        }

        private void OnTextBoxTextChanged(Object sender, EventArgs e)
        {
            DataChanged = true;
        }

        private void OnFormClosing(Object sender, FormClosingEventArgs e)
        {
            if (DataChanged)
            {
                if (MessageBox.Show(MessageBoxTexts.AbandonChangesText, MessageBoxTexts.AbandonChangesHeader, MessageBoxButtons.YesNo
                    , MessageBoxIcon.Question) == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void OnImportOptionsToolStripMenuItemClick(Object sender, EventArgs e)
        {
            Plugin.ImportOptions();
            SetLabels();
        }

        private void OnExportOptionsToolStripMenuItemClick(Object sender, EventArgs e)
        {
            Plugin.ExportOptions();
        }

        private void OnCopyAllToolStripMenuItemClick(Object sender, EventArgs e)
        {
            String xml;

            EnhancedTitles et;

            et = GetEnhancedTitlesForXmlStructure();

            using (Utf8StringWriter sw = new Utf8StringWriter())
            {
                EnhancedTitles.XmlSerializer.Serialize(sw, et);

                xml = sw.ToString();
            }

            try
            {
                Clipboard.SetDataObject(xml, true, 4, 250);
            }
            catch
            {
                MessageBox.Show(MessageBoxTexts.CopyToClipboardFailed, MessageBoxTexts.ErrorHeader
                 , MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void OnPasteAllToolStripMenuItemClick(Object sender, EventArgs e)
        {
            EnhancedTitles et;

            try
            {
                String xml;

                xml = Clipboard.GetText();

                using (StringReader sr = new StringReader(xml))
                {
                    et = (EnhancedTitles)(EnhancedTitles.XmlSerializer.Deserialize(sr));
                }
            }
            catch
            {
                et = null;
                MessageBox.Show(MessageBoxTexts.PasteFromClipboardFailed, MessageBoxTexts.ErrorHeader
                    , MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            if (et != null)
            {
                SetEnhancedTitlesFromXmlStructure(et);

                SetStandardTitles(et);
            }
        }

        private void SetStandardTitles(EnhancedTitles et)
        {
            if (FullEdit == false)
            {
                return;
            }

            if (et.InvelosData != null)
            {
                SetStandardTitle(et.InvelosData.Title, TitleTextBox);
                SetStandardTitle(et.InvelosData.SortTitle, SortTitleTextBox);
                SetStandardTitle(et.InvelosData.OriginalTitle, OriginalTitleTextBox);
            }
            else
            {
                SetStandardTitle(String.Empty, TitleTextBox);
                SetStandardTitle(String.Empty, SortTitleTextBox);
                SetStandardTitle(String.Empty, OriginalTitleTextBox);
            }
        }

        private void SetStandardTitle(String text, TextBox textBox)
        {
            if (String.IsNullOrEmpty(text) == false)
            {
                textBox.Text = text;
            }
        }
    }
}