using DoenaSoft.DVDProfiler.EnhancedTitles.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;

namespace DoenaSoft.DVDProfiler.EnhancedTitles
{
    public partial class SettingsForm : Form
    {
        private readonly Plugin Plugin;

        public SettingsForm(Plugin plugin)
        {
            Plugin = plugin;

            InitializeComponent();

            SetSettings();

            SetLabels();

            SetToolTips();

            SetComboBoxes();
        }

        private void SetToolTips()
        {
            ToolTip tt;

            tt = new ToolTip();
            tt.SetToolTip(ResetInternationalEnglishTitleButton, Texts.Reset);
            tt.SetToolTip(ResetAlternateOriginalTitleButton, Texts.Reset);
            tt.SetToolTip(ResetNonLatinLettersTitleButton, Texts.Reset);
            tt.SetToolTip(ResetAdditionalTitle1Button, Texts.Reset);
            tt.SetToolTip(ResetAdditionalTitle2Button, Texts.Reset);
        }

        private void SetSettings()
        {
            DefaultValues dv;

            dv = Plugin.Settings.DefaultValues;

            #region Labels

            InternationalEnglishTitleTextBox.Text = dv.InternationalEnglishTitleLabel;
            AlternateOriginalTitleTextBox.Text = dv.AlternateOriginalTitleLabel;
            NonLatinLettersTitleTextBox.Text = dv.NonLatinLettersTitleLabel;
            AdditionalTitle1TextBox.Text = dv.AdditionalTitle1Label;
            AdditionalTitle2TextBox.Text = dv.AdditionalTitle2Label;

            #endregion

            InternationalEnglishTitleCheckBox.Checked = dv.InternationalEnglishTitle;
            AlternateOriginalTitleCheckBox.Checked = dv.AlternateOriginalTitle;
            NonLatinLettersTitleCheckBox.Checked = dv.NonLatinLettersTitle;
            AdditionalTitle1CheckBox.Checked = dv.AdditionalTitle1;
            AdditionalTitle2CheckBox.Checked = dv.AdditionalTitle2;

            InternationalEnglishTitleFilterCheckBox.Checked = dv.InternationalEnglishTitleFilter;
            AlternateOriginalTitleFilterCheckBox.Checked = dv.AlternateOriginalTitleFilter;
            NonLatinLettersTitleFilterCheckBox.Checked = dv.NonLatinLettersTitleFilter;
            AdditionalTitle1FilterCheckBox.Checked = dv.AdditionalTitle1Filter;
            AdditionalTitle2FilterCheckBox.Checked = dv.AdditionalTitle2Filter;

            ExportToCollectionXmlCheckBox.Checked = dv.ExportToCollectionXml;
        }

        private void SetComboBoxes()
        {
            Dictionary<Int32, CultureInfo> uiLanguages;
            CultureInfo ci;

            uiLanguages = new Dictionary<Int32, CultureInfo>(2);
            ci = CultureInfo.GetCultureInfo("en");
            uiLanguages.Add(ci.LCID, ci);
            ci = CultureInfo.GetCultureInfo("de");
            uiLanguages.Add(ci.LCID, ci);
            UiLanguageComboBox.DataSource = new BindingSource(uiLanguages, null);
            UiLanguageComboBox.DisplayMember = "Value";
            UiLanguageComboBox.ValueMember = "Key";
            UiLanguageComboBox.Text = Plugin.Settings.DefaultValues.UiLanguage.DisplayName;
        }

        private void SetLabels()
        {
            #region Labels

            InternationalEnglishTitleLabel.Text = Texts.InternationalEnglishTitle;
            AlternateOriginalTitleLabel.Text = Texts.AlternateOriginalTitle;
            NonLatinLettersTitlelabel.Text = Texts.NonLatinLettersTitle;
            AdditionalTitle1Label.Text = Texts.AdditionalTitle1;
            AdditionalTitle2Label.Text = Texts.AdditionalTitle2;

            DefaultValues dv = Plugin.Settings.DefaultValues;

            InternationalEnglishTitleCheckBox.Text = dv.InternationalEnglishTitleLabel;
            AlternateOriginalTitleCheckBox.Text = dv.AlternateOriginalTitleLabel;
            NonLatinLettersTitleCheckBox.Text = dv.NonLatinLettersTitleLabel;
            AdditionalTitle1CheckBox.Text = dv.AdditionalTitle1Label;
            AdditionalTitle2CheckBox.Text = dv.AdditionalTitle2Label;

            #endregion

            #region Misc

            Text = Texts.Options;

            #region TabPages

            LabelTabPage.Text = Texts.Labels;
            ExcelColumnsTabPage.Text = Texts.ExcelColumns;
            MiscTabPage.Text = Texts.Misc;

            #endregion

            #region Labels

            UiLanguageLabel.Text = Texts.UiLanguage;

            ExportToCollectionXmlLabel.Text = Texts.ExportToCollectionXml;

            InternationalEnglishTitleFilterLabel.Text = String.Format(Texts.EnableFilter, dv.InternationalEnglishTitleLabel);

            AlternateOriginalTitleFilterLabel.Text = String.Format(Texts.EnableFilter, dv.AlternateOriginalTitleLabel);

            NonLatinLettersTitleFilterLabel.Text = String.Format(Texts.EnableFilter, dv.NonLatinLettersTitleLabel);

            AdditionalTitle1FilterLabel.Text = String.Format(Texts.EnableFilter, dv.AdditionalTitle1Label);

            AdditionalTitle2FilterLabel.Text = String.Format(Texts.EnableFilter, dv.AdditionalTitle2Label);

            #endregion

            #region Buttons

            ResetInternationalEnglishTitleButton.Text = Texts.ResetLabel;
            ResetAlternateOriginalTitleButton.Text = Texts.ResetLabel;
            ResetNonLatinLettersTitleButton.Text = Texts.ResetLabel;
            ResetAdditionalTitle1Button.Text = Texts.ResetLabel;
            ResetAdditionalTitle2Button.Text = Texts.ResetLabel;

            SaveButton.Text = Texts.Save;
            DiscardButton.Text = Texts.Cancel;

            #endregion

            #endregion
        }

        private void OnDiscardButtonClick(Object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void OnSaveButtonClick(Object sender, EventArgs e)
        {
            CultureInfo uiLanguage;
            DefaultValues dv;

            dv = Plugin.Settings.DefaultValues;

            #region Labels

            dv.InternationalEnglishTitleLabel = InternationalEnglishTitleTextBox.Text;
            dv.AlternateOriginalTitleLabel = AlternateOriginalTitleTextBox.Text;
            dv.NonLatinLettersTitleLabel = NonLatinLettersTitleTextBox.Text;
            dv.AdditionalTitle1Label = AdditionalTitle1TextBox.Text;
            dv.AdditionalTitle2Label = AdditionalTitle2TextBox.Text;

            #endregion

            dv.InternationalEnglishTitle = InternationalEnglishTitleCheckBox.Checked;
            dv.AlternateOriginalTitle = AlternateOriginalTitleCheckBox.Checked;
            dv.NonLatinLettersTitle = NonLatinLettersTitleCheckBox.Checked;
            dv.AdditionalTitle1 = AdditionalTitle1CheckBox.Checked;
            dv.AdditionalTitle2 = AdditionalTitle2CheckBox.Checked;

            dv.InternationalEnglishTitleFilter = InternationalEnglishTitleFilterCheckBox.Checked;
            dv.AlternateOriginalTitleFilter = AlternateOriginalTitleFilterCheckBox.Checked;
            dv.NonLatinLettersTitleFilter = NonLatinLettersTitleFilterCheckBox.Checked;
            dv.AdditionalTitle1Filter = AdditionalTitle1FilterCheckBox.Checked;
            dv.AdditionalTitle2Filter = AdditionalTitle2FilterCheckBox.Checked;

            dv.ExportToCollectionXml = ExportToCollectionXmlCheckBox.Checked;

            uiLanguage = GetUiLanguage();
            dv.UiLanguage = uiLanguage;
            Texts.Culture = uiLanguage;
            MessageBoxTexts.Culture = uiLanguage;

            DialogResult = DialogResult.OK;

            Close();
        }

        private CultureInfo GetUiLanguage()
        {
            CultureInfo ci;
            KeyValuePair<Int32, CultureInfo> kvp;

            kvp = (KeyValuePair<Int32, CultureInfo>)(UiLanguageComboBox.SelectedItem);
            ci = kvp.Value;
            return (ci);
        }

        #region OnResetButtonClick

        private void OnResetInternationalEnglishTitleButtonClick(Object sender, EventArgs e)
        {
            CultureInfo ci;

            ci = SetTempLanguage();
            InternationalEnglishTitleTextBox.Text = Texts.InternationalEnglishTitle;
            UnsetTempLanguage(ci);
        }

        private void OnResetAlternateOriginalTitleButtonClick(Object sender, EventArgs e)
        {
            CultureInfo ci;

            ci = SetTempLanguage();
            AlternateOriginalTitleTextBox.Text = Texts.AlternateOriginalTitle;
            UnsetTempLanguage(ci);
        }

        private void OnResetNonLatinLettersTitleButtonClick(Object sender, EventArgs e)
        {
            CultureInfo ci;

            ci = SetTempLanguage();
            NonLatinLettersTitleTextBox.Text = Texts.NonLatinLettersTitle;
            UnsetTempLanguage(ci);
        }

        private void OnResetAdditionalPrice1ButtonClick(Object sender, EventArgs e)
        {
            CultureInfo ci;

            ci = SetTempLanguage();
            AdditionalTitle1TextBox.Text = Texts.AdditionalTitle1;
            UnsetTempLanguage(ci);
        }

        private void OnResetAdditionalPrice2ButtonClick(Object sender, EventArgs e)
        {
            CultureInfo ci;

            ci = SetTempLanguage();
            AdditionalTitle2TextBox.Text = Texts.AdditionalTitle2;
            UnsetTempLanguage(ci);
        }

        private CultureInfo SetTempLanguage()
        {
            CultureInfo previousCI;
            CultureInfo currentCI;

            previousCI = Texts.Culture;
            currentCI = GetUiLanguage();
            Texts.Culture = currentCI;
            return (previousCI);
        }

        private void UnsetTempLanguage(CultureInfo ci)
        {
            Texts.Culture = ci;
        }

        #endregion
    }
}