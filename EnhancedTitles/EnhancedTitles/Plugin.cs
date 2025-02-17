using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
using System.Windows.Forms;
using DoenaSoft.DVDProfiler.DVDProfilerHelper;
using DoenaSoft.DVDProfiler.EnhancedTitles.Resources;
using DoenaSoft.DVDProfiler.GetCustomDVDFieldDefinitions;
using DoenaSoft.ToolBox.Generics;
using Invelos.DVDProfilerPlugin;

namespace DoenaSoft.DVDProfiler.EnhancedTitles
{
    public partial class Plugin : IDVDProfilerPlugin, IDVDProfilerDataAwarePlugin
    {
        private readonly String SettingsFile;

        private readonly String ErrorFile;

        private readonly String ApplicationPath;

        internal IDVDProfilerAPI Api { get; private set; }

        internal Settings Settings { get; private set; }

        private String CurrentProfileId;

        private Boolean DatabaseRestoreRunning = false;

        internal Boolean IsRemoteAccess { get; private set; }

        #region MenuTokens

        private String DvdMenuToken = "";

        private const Int32 DvdMenuId = 1;

        //private String PluginInfoToolsMenuToken = "";

        //private const Int32 PluginInfoToolsMenuId = 100;

        private String PersonalizeScreenToken = "";

        private const Int32 PersonalizeScreenId = 11;

        private String CollectionExportMenuToken = "";

        private const Int32 CollectionExportMenuId = 21;

        private String CollectionImportMenuToken = "";

        private const Int32 CollectionImportMenuId = 22;

        private String CollectionExportToCsvMenuToken = "";

        private const Int32 CollectionExportToCsvMenuId = 23;

        private String CollectionFlaggedExportMenuToken = "";

        private const Int32 CollectionFlaggedExportMenuId = 31;

        private String CollectionFlaggedImportMenuToken = "";

        private const Int32 CollectionFlaggedImportMenuId = 32;

        private String CollectionFlaggedExportToCsvMenuToken = "";

        private const Int32 CollectionFlaggedExportToCsvMenuId = 33;

        private String ToolsOptionsMenuToken = "";

        private const Int32 ToolsOptionsMenuId = 41;

        private String ToolsExportOptionsMenuToken = "";

        private const Int32 ToolsExportOptionsMenuId = 42;

        private String ToolsImportOptionsMenuToken = "";

        private const Int32 ToolsImportOptionsMenuId = 43;

        #endregion

        private readonly Dictionary<String, String> FilterTokens;

        static Plugin()
        {
            DVDProfilerHelperAssemblyLoader.Load();
        }

        public Plugin()
        {
            FilterTokens = new Dictionary<String, String>();
            ApplicationPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Doena Soft\EnhancedTitles\";
            SettingsFile = ApplicationPath + "EnhancedTitles.xml";
            ErrorFile = Environment.GetEnvironmentVariable("TEMP") + @"\EnhancedTitlesCrash.xml";
        }

        #region I.. Members

        #region IDVDProfilerPlugin Members

        public void Load(IDVDProfilerAPI api)
        {
            //System.Diagnostics.Debugger.Launch();

            this.Api = api;

            if (Directory.Exists(ApplicationPath) == false)
            {
                Directory.CreateDirectory(ApplicationPath);
            }
            if (File.Exists(SettingsFile))
            {
                try
                {
                    this.Settings = Settings.Deserialize(SettingsFile);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(String.Format(MessageBoxTexts.FileCantBeRead, SettingsFile, ex.Message)
                        , MessageBoxTexts.ErrorHeader, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            this.EnsureSettingsAndSetUILanguage();

            this.SetIsRemoteAccess();

            this.Api.RegisterForEvent(PluginConstants.EVENTID_FormCreated);
            this.Api.RegisterForEvent(PluginConstants.EVENTID_FormDestroyed);

            this.Api.RegisterForEvent(PluginConstants.EVENTID_DatabaseOpened);

            this.Api.RegisterForEvent(PluginConstants.EVENTID_DVDPersonalizeShown);

            this.Api.RegisterForEvent(PluginConstants.EVENTID_RestoreStarting);
            this.Api.RegisterForEvent(PluginConstants.EVENTID_RestoreFinished);
            this.Api.RegisterForEvent(PluginConstants.EVENTID_RestoreCancelled);

            DvdMenuToken = this.Api.RegisterMenuItemA(PluginConstants.FORMID_Main, PluginConstants.MENUID_Form
                , "DVD", Texts.ET, DvdMenuId, "", PluginConstants.SHORTCUT_KEY_A + 19, PluginConstants.SHORTCUT_MOD_Ctrl + PluginConstants.SHORTCUT_MOD_Shift, false);

            CollectionExportMenuToken = api.RegisterMenuItem(PluginConstants.FORMID_Main, PluginConstants.MENUID_Form
                , @"Collection\" + Texts.ET, Texts.ExportToXml, CollectionExportMenuId);

            if (this.IsRemoteAccess == false)
            {
                CollectionImportMenuToken = api.RegisterMenuItem(PluginConstants.FORMID_Main, PluginConstants.MENUID_Form
                    , @"Collection\" + Texts.ET, Texts.ImportFromXml, CollectionImportMenuId);
            }

            CollectionExportToCsvMenuToken = api.RegisterMenuItem(PluginConstants.FORMID_Main, PluginConstants.MENUID_Form
                , @"Collection\" + Texts.ET, Texts.ExportToExcel, CollectionExportToCsvMenuId);

            CollectionFlaggedExportMenuToken = api.RegisterMenuItem(PluginConstants.FORMID_Main, PluginConstants.MENUID_Form
                , @"Collection\Flagged\" + Texts.ET, Texts.ExportToXml, CollectionFlaggedExportMenuId);

            if (this.IsRemoteAccess == false)
            {
                CollectionFlaggedImportMenuToken = api.RegisterMenuItem(PluginConstants.FORMID_Main, PluginConstants.MENUID_Form
                   , @"Collection\Flagged\" + Texts.ET, Texts.ImportFromXml, CollectionFlaggedImportMenuId);
            }

            CollectionFlaggedExportToCsvMenuToken = api.RegisterMenuItem(PluginConstants.FORMID_Main, PluginConstants.MENUID_Form
                , @"Collection\Flagged\" + Texts.ET, Texts.ExportToExcel, CollectionFlaggedExportToCsvMenuId);

            ToolsOptionsMenuToken = api.RegisterMenuItem(PluginConstants.FORMID_Main, PluginConstants.MENUID_Form
               , @"Tools\" + Texts.ET, Texts.Options, ToolsOptionsMenuId);
            ToolsExportOptionsMenuToken = api.RegisterMenuItem(PluginConstants.FORMID_Main, PluginConstants.MENUID_Form
                , @"Tools\" + Texts.ET, Texts.ExportOptions, ToolsExportOptionsMenuId);
            ToolsImportOptionsMenuToken = api.RegisterMenuItem(PluginConstants.FORMID_Main, PluginConstants.MENUID_Form
                , @"Tools\" + Texts.ET, Texts.ImportOptions, ToolsImportOptionsMenuId);

            //PluginInfoToolsMenuToken = Api.RegisterMenuItem(PluginConstants.FORMID_Main, PluginConstants.MENUID_Form
            //    , "Tools", "Show Plugin FieldAccess", PluginInfoToolsMenuId);

            this.RegisterCustomFields();
        }

        public void Unload()
        {
            this.Api.UnregisterMenuItem(DvdMenuToken);

            this.Api.UnregisterMenuItem(CollectionExportMenuToken);
            this.Api.UnregisterMenuItem(CollectionImportMenuToken);
            this.Api.UnregisterMenuItem(CollectionExportToCsvMenuToken);

            this.Api.UnregisterMenuItem(CollectionFlaggedExportMenuToken);
            this.Api.UnregisterMenuItem(CollectionFlaggedImportMenuToken);
            this.Api.UnregisterMenuItem(CollectionFlaggedExportToCsvMenuToken);

            //Api.UnregisterMenuItem(PluginInfoToolsMenuToken);
            this.Api.UnregisterMenuItem(ToolsOptionsMenuToken);
            this.Api.UnregisterMenuItem(ToolsExportOptionsMenuToken);
            this.Api.UnregisterMenuItem(ToolsImportOptionsMenuToken);

            try
            {
                this.Settings.Serialize(SettingsFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format(MessageBoxTexts.FileCantBeWritten, SettingsFile, ex.Message)
                    , MessageBoxTexts.ErrorHeader, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.Api = null;
        }

        public void HandleEvent(Int32 EventType, Object EventData)
        {
            try
            {
                switch (EventType)
                {
                    case (PluginConstants.EVENTID_CustomMenuClick):
                        {
                            this.HandleMenuClick((Int32)EventData);
                            break;
                        }
                    case (PluginConstants.EVENTID_FormCreated):
                        {
                            if ((Int32)EventData == PluginConstants.FORMID_Personalize)
                            {
                                PersonalizeScreenToken = this.Api.RegisterMenuItemA(PluginConstants.FORMID_Personalize, PluginConstants.MENUID_Form
                                    , Texts.ET, Texts.ET, PersonalizeScreenId, "", PluginConstants.SHORTCUT_KEY_A + 19, PluginConstants.SHORTCUT_MOD_Ctrl + PluginConstants.SHORTCUT_MOD_Shift, false);
                            }
                            break;
                        }
                    case (PluginConstants.EVENTID_FormDestroyed):
                        {
                            if ((Int32)EventData == PluginConstants.FORMID_Personalize)
                            {
                                if (String.IsNullOrEmpty(PersonalizeScreenToken) == false)
                                {
                                    this.Api.UnregisterMenuItem(PersonalizeScreenToken);
                                }
                                CurrentProfileId = null;
                            }
                            break;
                        }
                    case (PluginConstants.EVENTID_RestoreStarting):
                        {
                            DatabaseRestoreRunning = true;
                            this.RegisterCustomFields(false);
                            break;
                        }
                    case (PluginConstants.EVENTID_DatabaseOpened):
                    case (PluginConstants.EVENTID_RestoreFinished):
                    case (PluginConstants.EVENTID_RestoreCancelled):
                        {
                            DatabaseRestoreRunning = false;
                            this.RegisterCustomFields();
                            break;
                        }
                    case (PluginConstants.EVENTID_DVDPersonalizeShown):
                        {
                            //System.Diagnostics.Debugger.Launch();
                            CurrentProfileId = (String)EventData;
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                try
                {
                    MessageBox.Show(String.Format(MessageBoxTexts.CriticalError, ex.Message, ErrorFile)
                        , MessageBoxTexts.CriticalErrorHeader, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    if (File.Exists(ErrorFile))
                    {
                        File.Delete(ErrorFile);
                    }

                    this.LogException(ex);
                }
                catch (Exception inEx)
                {
                    MessageBox.Show(String.Format(MessageBoxTexts.FileCantBeWritten, ErrorFile, inEx.Message)
                        , MessageBoxTexts.ErrorHeader, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #region IDVDProfilerDataAwarePlugin

        public Boolean ExportsCustomDataXML()
        {
            Boolean exportsXml;

            exportsXml = ((this.Settings.DefaultValues.ExportToCollectionXml)
                && (DatabaseRestoreRunning == false));
            return (exportsXml);
        }

        public String GetCustomDataXMLForDVD(IDVDInfo SourceDVD)
        {
            String xml;
            String iet;
            Boolean hasInternationalEnglishTitle;
            String aot;
            Boolean hasAlternateOriginalTitle;
            String nlt;
            Boolean hasNonLatinLettersTitle;
            String at1;
            Boolean hasAdditionalTitle1;
            String at2;
            Boolean hasAdditionalTitle2;
            TitleManager tm;

            if (this.Settings.DefaultValues.ExportToCollectionXml == false)
            {
                return (String.Empty);
            }
            else if (DatabaseRestoreRunning)
            {
                return (String.Empty);
            }

            tm = new TitleManager(SourceDVD);

            hasInternationalEnglishTitle = tm.GetInternationalEnglishTitle(out iet);
            hasAlternateOriginalTitle = tm.GetAlternateOriginalTitle(out aot);
            hasNonLatinLettersTitle = tm.GetNonLatinLettersTitle(out nlt);
            hasAdditionalTitle1 = tm.GetAdditionalTitle1(out at1);
            hasAdditionalTitle2 = tm.GetAdditionalTitle2(out at2);

            if (hasInternationalEnglishTitle || hasAlternateOriginalTitle || hasNonLatinLettersTitle || hasAdditionalTitle1 || hasAdditionalTitle2)
            {
                StringBuilder sb;
                DefaultValues dv;

                dv = this.Settings.DefaultValues;
                sb = new StringBuilder("<EnhancedTitles>");
                if (hasInternationalEnglishTitle)
                {
                    AddTag(sb, Constants.InternationalEnglishTitle, dv.InternationalEnglishTitleLabel, iet);
                }
                if (hasAlternateOriginalTitle)
                {
                    AddTag(sb, Constants.AlternateOriginalTitle, dv.AlternateOriginalTitleLabel, aot);
                }
                if (hasNonLatinLettersTitle)
                {
                    AddTag(sb, Constants.NonLatinLettersTitle, dv.NonLatinLettersTitleLabel, nlt);
                }
                if (hasAdditionalTitle1)
                {
                    AddTag(sb, Constants.AdditionalTitle1, dv.AdditionalTitle1Label, at1);
                }
                if (hasAdditionalTitle2)
                {
                    AddTag(sb, Constants.AdditionalTitle2, dv.AdditionalTitle2Label, at2);
                }
                sb.Append("</EnhancedTitles>");
                xml = sb.ToString();
            }
            else
            {
                xml = String.Empty;
            }

            return (xml);
        }

        public String GetHTMLForDPVarsFunctionSection()
        {
            return (String.Empty);
        }

        public String GetHTMLForDPVarsDataSection(IDVDInfo SourceDVD, IDVDInfo CompareDVD)
        {
            return (String.Empty);
        }

        public String GetHTMLForTag(String TagName
            , String FullTag
            , IDVDInfo SourceDVD
            , IDVDInfo CompareDVD
            , out Boolean Handled)
        {
            TitleManager titleManager;
            String text;
            DefaultValues dv;

            if (String.IsNullOrEmpty(TagName))
            {
                Handled = false;

                return (null);
            }
            else if (TagName.StartsWith(Constants.HtmlPrefix + ".") == false)
            {
                Handled = false;

                return (null);
            }

            titleManager = new TitleManager(SourceDVD);
            Handled = true;
            dv = this.Settings.DefaultValues;
            switch (TagName)
            {
                #region Titles
                case (Constants.HtmlPrefix + "." + Constants.InternationalEnglishTitle):
                    {
                        text = HtmlEncode(titleManager.GetInternationalEnglishTitleWithFallback());
                        break;
                    }
                case (Constants.HtmlPrefix + "." + Constants.AlternateOriginalTitle):
                    {
                        text = HtmlEncode(titleManager.GetAlternateOriginalTitleWithFallback());
                        break;
                    }
                case (Constants.HtmlPrefix + "." + Constants.NonLatinLettersTitle):
                    {
                        text = HtmlEncode(titleManager.GetNonLatinLettersTitleWithFallback());
                        break;
                    }
                case (Constants.HtmlPrefix + "." + Constants.AdditionalTitle1):
                    {
                        text = HtmlEncode(titleManager.GetAdditionalTitle1WithFallback());
                        break;
                    }
                case (Constants.HtmlPrefix + "." + Constants.AdditionalTitle2):
                    {
                        text = HtmlEncode(titleManager.GetAdditionalTitle2WithFallback());
                        break;
                    }
                #endregion
                #region Labels
                case (Constants.HtmlPrefix + "." + Constants.InternationalEnglishTitle + Constants.LabelSuffix):
                    {
                        text = HtmlEncode(dv.InternationalEnglishTitleLabel);
                        break;
                    }
                case (Constants.HtmlPrefix + "." + Constants.AlternateOriginalTitle + Constants.LabelSuffix):
                    {
                        text = HtmlEncode(dv.AlternateOriginalTitleLabel);
                        break;
                    }
                case (Constants.HtmlPrefix + "." + Constants.NonLatinLettersTitle + Constants.LabelSuffix):
                    {
                        text = HtmlEncode(dv.NonLatinLettersTitleLabel);
                        break;
                    }
                case (Constants.HtmlPrefix + "." + Constants.AdditionalTitle1 + Constants.LabelSuffix):
                    {
                        text = HtmlEncode(dv.AdditionalTitle1Label);
                        break;
                    }
                case (Constants.HtmlPrefix + "." + Constants.AdditionalTitle2 + Constants.LabelSuffix):
                    {
                        text = HtmlEncode(dv.AdditionalTitle2Label);
                        break;
                    }
                #endregion
                default:
                    {
                        Handled = false;
                        text = null;
                        break;
                    }
            }
            return (text);
        }

        public Object GetCustomHTMLTagNames()
        {
            String[] tags;

            tags = new String[] { Constants.HtmlPrefix + "." + Constants.InternationalEnglishTitle
                    , Constants.HtmlPrefix + "." + Constants.AlternateOriginalTitle
                    , Constants.HtmlPrefix + "." + Constants.NonLatinLettersTitle
                    , Constants.HtmlPrefix + "." + Constants.AdditionalTitle1
                    , Constants.HtmlPrefix + "." + Constants.AdditionalTitle2
                    , Constants.HtmlPrefix + "." + Constants.InternationalEnglishTitle + Constants.LabelSuffix
                    , Constants.HtmlPrefix + "." + Constants.AlternateOriginalTitle + Constants.LabelSuffix
                    , Constants.HtmlPrefix + "." + Constants.NonLatinLettersTitle + Constants.LabelSuffix
                    , Constants.HtmlPrefix + "." + Constants.AdditionalTitle1 + Constants.LabelSuffix
                    , Constants.HtmlPrefix + "." + Constants.AdditionalTitle2 + Constants.LabelSuffix };
            return (tags);
        }

        public Object GetCustomHTMLParamsForTag(String TagName)
        {
            return (null);
        }

        public bool FilterFieldMatch(string FieldFilterToken
            , int ComparisonTypeIndex
            , object ComparisonValue
            , IDVDInfo TestDVD)
        {
            var comparisonText = ((ComparisonValue as string ?? ComparisonValue?.ToString()) ?? string.Empty).Trim();

            if (string.IsNullOrEmpty(comparisonText))
            {
                return true;
            }

            if (FilterTokens.TryGetValue(FieldFilterToken, out _))
            {
                var fields = new[] { Constants.InternationalEnglishTitle
                    , Constants.AlternateOriginalTitle
                    , Constants.NonLatinLettersTitle
                    , Constants.AdditionalTitle1
                    , Constants.AdditionalTitle2 };

                int index = 0;
                bool contains;
                do
                {
                    contains = ContainsFilter(ComparisonTypeIndex, comparisonText, TestDVD, fields[index++]);
                } while (!contains && index < fields.Length);

                if (contains)
                {
                    return contains;
                }

                if (this.Settings.DefaultValues.StandardFilter)
                {
                    this.Api.DVDByProfileID(out var profile, TestDVD.GetProfileID(), 0, 0);

                    contains = ContainsFilter(ComparisonTypeIndex, comparisonText, profile.GetTitle());

                    if (contains)
                    {
                        return contains;
                    }

                    contains = ContainsFilter(ComparisonTypeIndex, comparisonText, profile.GetOriginalTitle());

                    if (contains)
                    {
                        return contains;
                    }

                    contains = ContainsFilter(ComparisonTypeIndex, comparisonText, profile.GetSortTitle());
                }

                return contains;
            }

            return false;
        }

        #endregion

        #endregion

        #region RegisterCustomFields

        internal void RegisterCustomFields(Boolean rebuildFilters = true)
        {
            try
            {
                DefaultValues dv;

                this.UnregisterCustomFilterField(rebuildFilters);

                #region Schema

                using (Stream stream
                    = typeof(EnhancedTitles).Assembly.GetManifestResourceStream("DoenaSoft.DVDProfiler.EnhancedTitles.EnhancedTitles.xsd"))
                {
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        String xsd;

                        xsd = sr.ReadToEnd();
                        this.Api.SetGlobalSetting(Constants.FieldDomain, "EnhancedTitlesSchema", xsd, Constants.ReadKey, InternalConstants.WriteKey);
                    }
                }

                #endregion

                dv = this.Settings.DefaultValues;

                //RegisterCustomField(Constants.InternationalEnglishTitle, dv.InternationalEnglishTitleLabel, rebuildFilters, dv.InternationalEnglishTitleFilter);
                //RegisterCustomField(Constants.AlternateOriginalTitle, dv.AlternateOriginalTitleLabel, rebuildFilters, dv.AlternateOriginalTitleFilter);
                //RegisterCustomField(Constants.NonLatinLettersTitle, dv.NonLatinLettersTitleLabel, rebuildFilters, dv.NonLatinLettersTitleFilter);
                //RegisterCustomField(Constants.AdditionalTitle1, dv.AdditionalTitle1Label, rebuildFilters, dv.AdditionalTitle1Filter);
                //RegisterCustomField(Constants.AdditionalTitle2, dv.AdditionalTitle2Label, rebuildFilters, dv.AdditionalTitle2Filter);

                this.RegisterCustomField(Constants.InternationalEnglishTitle, dv.InternationalEnglishTitleLabel, rebuildFilters);
                this.RegisterCustomField(Constants.AlternateOriginalTitle, dv.AlternateOriginalTitleLabel, rebuildFilters);
                this.RegisterCustomField(Constants.NonLatinLettersTitle, dv.NonLatinLettersTitleLabel, rebuildFilters);
                this.RegisterCustomField(Constants.AdditionalTitle1, dv.AdditionalTitle1Label, rebuildFilters);
                this.RegisterCustomField(Constants.AdditionalTitle2, dv.AdditionalTitle2Label, rebuildFilters);

                this.RegisterCustomFilterField("TitleSearchField", Texts.ET, rebuildFilters, true);
            }
            catch (Exception ex)
            {
                try
                {
                    MessageBox.Show(String.Format(MessageBoxTexts.CriticalError, ex.Message, ErrorFile)
                        , MessageBoxTexts.CriticalErrorHeader, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    if (File.Exists(ErrorFile))
                    {
                        File.Delete(ErrorFile);
                    }

                    this.LogException(ex);
                }
                catch (Exception inEx)
                {
                    MessageBox.Show(String.Format(MessageBoxTexts.FileCantBeWritten, ErrorFile, inEx.Message)
                        , MessageBoxTexts.ErrorHeader, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void UnregisterCustomFilterField(Boolean rebuildFilters)
        {
            //System.Diagnostics.Debugger.Launch();

            if (rebuildFilters)
            {
                foreach (String token in FilterTokens.Keys)
                {
                    try
                    {
                        this.Api.RemoveCustomFilterField(token);
                    }
                    catch (COMException)
                    { }
                }
                FilterTokens.Clear();
            }
        }

        private void RegisterCustomField(String fieldName
            , String displayName
            , Boolean rebuildFilters)
        //, Boolean filterIsEnabled)
        {
            this.Api.CreateCustomDVDField(Constants.FieldDomain, fieldName, PluginConstants.FIELD_TYPE_STRING, Constants.ReadKey, InternalConstants.WriteKey);
            this.Api.SetCustomDVDFieldStorage(Constants.FieldDomain, fieldName, InternalConstants.WriteKey, true, false);
        }

        private void RegisterCustomFilterField(String fieldName
            , String displayName
            , Boolean rebuildFilters
            , Boolean filterIsEnabled)
        {
            if ((rebuildFilters) && (filterIsEnabled))
            {
                String token;

                if (displayName == null)
                {
                    ResourceManager rm = Texts.ResourceManager;

                    displayName = rm.GetString(fieldName);
                }

                //System.Diagnostics.Debugger.Launch();

                token = this.Api.SetCustomFieldFilterableA(displayName, PluginConstants.FILTER_INPUT_TEXT
                    , new String[] { Texts.FilterStartsWith, Texts.FilterContains }, null);

                if (token != null)
                {
                    FilterTokens.Add(token, fieldName);
                }
                else
                {
                    System.Diagnostics.Debug.Fail("No valid token for search!");
                }
            }
        }

        #endregion

        private void SetIsRemoteAccess()
        {
            String name;
            Boolean isRemote;
            String localPath;

            this.Api.GetCurrentDatabaseInformation(out name, out isRemote, out localPath);

            //System.Diagnostics.Debugger.Launch();

            this.IsRemoteAccess = isRemote;
        }

        private static void AddTag(StringBuilder sb
            , String tagName
            , String displayName
            , String title)
        {
            String base64;

            sb.Append("<");
            sb.Append(tagName);
            displayName = XmlConvertHelper.GetWindows1252Text(displayName, out base64);
            sb.Append(" DisplayName=\"");
            sb.Append(displayName);
            sb.Append("\"");
            if (base64 != null)
            {
                sb.Append(" Base64DisplayName=\"");
                sb.Append(base64);
                sb.Append("\"");
            }
            title = XmlConvertHelper.GetWindows1252Text(title, out base64);
            if (base64 != null)
            {
                sb.Append(" Base64Title=\"");
                sb.Append(base64);
                sb.Append("\"");
            }
            sb.Append(">");
            sb.Append(title);
            sb.Append("</");
            sb.Append(tagName);
            sb.Append(">");
        }

        private void EnsureSettingsAndSetUILanguage()
        {
            Texts.Culture = DefaultValues.GetUILanguage();

            CultureInfo uiLanguage = this.EnsureSettings();

            Texts.Culture = uiLanguage;

            MessageBoxTexts.Culture = uiLanguage;
        }

        private CultureInfo EnsureSettings()
        {
            if (this.Settings == null)
            {
                this.Settings = new Settings();
            }

            if (this.Settings.DefaultValues == null)
            {
                this.Settings.DefaultValues = new DefaultValues();
            }

            return (this.Settings.DefaultValues.UiLanguage);
        }

        private static String HtmlEncode(String decoded)
        {
            String encoded;

            encoded = String.Join("", decoded.ToCharArray().Select(c =>
                {
                    Int32 number;
                    String newChar;

                    number = c;
                    if (number > 127)
                    {
                        newChar = "&#" + number.ToString() + ";";
                    }
                    else
                    {
                        newChar = HttpUtility.HtmlEncode(c.ToString());
                    }
                    return (newChar);
                }).ToArray());
            return (encoded);
        }

        private void HandleMenuClick(Int32 MenuEventID)
        {
            try
            {
                switch (MenuEventID)
                {
                    case (DvdMenuId):
                        {
                            this.OpenEditor(true);

                            break;
                        }
                    case (PersonalizeScreenId):
                        {
                            this.OpenEditor(false);

                            break;
                        }
                    //case (PluginInfoToolsMenuId):
                    //    {
                    //        ShowPluginFieldAccess();
                    //        break;
                    //    }
                    case (CollectionExportMenuId):
                        {
                            XmlManager xmlManager = new XmlManager(this);

                            xmlManager.Export(true);

                            break;
                        }
                    case (CollectionImportMenuId):
                        {
                            XmlManager xmlManager = new XmlManager(this);

                            xmlManager.Import(true);

                            break;
                        }
                    case (CollectionExportToCsvMenuId):
                        {
                            CsvManager csvManager = new CsvManager(this);

                            csvManager.Export(true);

                            break;
                        }
                    case (CollectionFlaggedExportMenuId):
                        {
                            XmlManager xmlManager = new XmlManager(this);

                            xmlManager.Export(false);

                            break;
                        }
                    case (CollectionFlaggedImportMenuId):
                        {
                            XmlManager xmlManager = new XmlManager(this);

                            xmlManager.Import(false);

                            break;
                        }
                    case (CollectionFlaggedExportToCsvMenuId):
                        {
                            CsvManager csvManager = new CsvManager(this);

                            csvManager.Export(false);

                            break;
                        }
                    case (ToolsOptionsMenuId):
                        {
                            this.OpenSettings();

                            break;
                        }
                    case (ToolsExportOptionsMenuId):
                        {
                            this.ExportOptions();

                            break;
                        }
                    case (ToolsImportOptionsMenuId):
                        {
                            this.ImportOptions();

                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                try
                {
                    MessageBox.Show(String.Format(MessageBoxTexts.CriticalError, ex.Message, ErrorFile)
                        , MessageBoxTexts.CriticalErrorHeader, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    if (File.Exists(ErrorFile))
                    {
                        File.Delete(ErrorFile);
                    }

                    this.LogException(ex);
                }
                catch (Exception inEx)
                {
                    MessageBox.Show(String.Format(MessageBoxTexts.FileCantBeWritten, ErrorFile, inEx.Message), MessageBoxTexts.ErrorHeader
                        , MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        internal void ImportOptions()
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
                    DefaultValues dv = null;

                    try
                    {
                        dv = XmlSerializer<DefaultValues>.Deserialize(ofd.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(String.Format(MessageBoxTexts.FileCantBeRead, ofd.FileName, ex.Message)
                           , MessageBoxTexts.ErrorHeader, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    if (dv != null)
                    {
                        this.Settings.DefaultValues = dv;
                        Texts.Culture = dv.UiLanguage;
                        MessageBoxTexts.Culture = dv.UiLanguage;
                        MessageBox.Show(MessageBoxTexts.Done, MessageBoxTexts.InformationHeader, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        internal void ExportOptions()
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.AddExtension = true;
                sfd.DefaultExt = ".xml";
                sfd.Filter = "XML files|*.xml";
                sfd.OverwritePrompt = true;
                sfd.RestoreDirectory = true;
                sfd.Title = Texts.SaveXmlFile;
                sfd.FileName = "EnhancedTitlesOptions.xml";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    DefaultValues dv = this.Settings.DefaultValues;
                    try
                    {
                        XmlSerializer<DefaultValues>.Serialize(sfd.FileName, dv);

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

        private void OpenSettings()
        {
            using (SettingsForm form = new SettingsForm(this))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    this.RegisterCustomFields();
                }
            }
        }

        private void ShowPluginFieldAccess()
        {
            String xml;
            CustomPluginDataDefinitions customPluginDataDefinitions;

            xml = this.Api.GetCustomDVDFieldDefinitions();
            customPluginDataDefinitions = CustomPluginDataDefinitions.Deserialize(xml);
            using (PluginFieldAccessForm form = new PluginFieldAccessForm(customPluginDataDefinitions))
            {
                form.ShowDialog();
            }
        }

        private void OpenEditor(Boolean fullEdit)
        {
            IDVDInfo profile;
            String profileId = CurrentProfileId;

            if (String.IsNullOrEmpty(profileId))
            {
                profile = this.Api.GetDisplayedDVD();
                profileId = profile.GetProfileID();
            }
            if (String.IsNullOrEmpty(profileId) == false)
            {
                this.Api.DVDByProfileID(out profile, profileId, PluginConstants.DATASEC_AllSections, -1);
                if (profile.GetProfileID() == null)
                {
                    profile = this.Api.CreateDVD();
                    profile.SetProfileID(profileId);
                }
                using (MainForm form = new MainForm(this, profile, fullEdit))
                {
                    form.ShowDialog();
                }
            }
        }

        private void LogException(Exception ex)
        {
            ex = this.WrapCOMException(ex);

            ExceptionXml exceptionXml = new ExceptionXml(ex);

            XmlSerializer<ExceptionXml>.Serialize(ErrorFile, exceptionXml);
        }

        private Exception WrapCOMException(Exception ex)
        {
            Exception returnEx = ex;

            if (ex is COMException comEx)
            {
                String lastApiError = this.Api.GetLastError();

                EnhancedCOMException newEx = new EnhancedCOMException(lastApiError, comEx);

                returnEx = newEx;
            }

            return (returnEx);
        }

        private static bool ContainsFilter(int comparisonTypeIndex
            , string comparisonText
            , IDVDInfo testDVD
            , string fieldName)
        {
            if ((new TitleManager(testDVD)).GetText(fieldName, out var text))
            {
                var contains = ContainsFilter(comparisonTypeIndex, comparisonText, text);

                return contains;
            }

            return false;
        }

        private static bool ContainsFilter(int comparisonTypeIndex, string comparisonText, string text)
        {
            text = text ?? string.Empty;

            var contains = (comparisonTypeIndex == 0)
                ? (text.IndexOf(comparisonText, StringComparison.OrdinalIgnoreCase) == 0)
                : (text.IndexOf(comparisonText, StringComparison.OrdinalIgnoreCase) >= 0);

            return contains;
        }
    }
}