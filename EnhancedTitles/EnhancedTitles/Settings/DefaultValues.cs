using DoenaSoft.DVDProfiler.EnhancedTitles.Resources;
using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;

namespace DoenaSoft.DVDProfiler.EnhancedTitles
{
    [ComVisible(false)]
    [Serializable]
    public sealed class DefaultValues
    {
        #region Labels

        public String InternationalEnglishTitleLabel = Texts.InternationalEnglishTitle;

        public String AlternateOriginalTitleLabel = Texts.AlternateOriginalTitle;

        public String NonLatinLettersTitleLabel = Texts.NonLatinLettersTitle;

        public String AdditionalTitle1Label = Texts.AdditionalTitle1;

        public String AdditionalTitle2Label = Texts.AdditionalTitle2;

        #endregion

        #region Excel

        public Boolean InternationalEnglishTitle = true;

        public Boolean AlternateOriginalTitle = true;

        public Boolean NonLatinLettersTitle = false;

        public Boolean AdditionalTitle1 = false;

        public Boolean AdditionalTitle2 = false;

        #endregion

        #region Filters

        public Boolean InternationalEnglishTitleFilter = false;

        public Boolean AlternateOriginalTitleFilter = false;

        public Boolean NonLatinLettersTitleFilter = false;

        public Boolean AdditionalTitle1Filter = false;

        public Boolean AdditionalTitle2Filter = false;

        #endregion

        #region Misc

        public Int32 UiLcid
        {
            get
            {
                return (UiLanguage.LCID);
            }
            set
            {
                UiLanguage = CultureInfo.GetCultureInfo(value);
            }
        }

        [XmlIgnore]
        internal CultureInfo UiLanguage;

        public Boolean ExportToCollectionXml = false;

        #endregion

        public DefaultValues()
        {
            UiLanguage = GetUILanguage();
        }

        internal static CultureInfo GetUILanguage()
            => ((Thread.CurrentThread.CurrentUICulture.Name.StartsWith("de")) ? (CultureInfo.GetCultureInfo("de")) : (CultureInfo.GetCultureInfo("en")));
    }
}