using System;
using System.Text;
using Invelos.DVDProfilerPlugin;

namespace DoenaSoft.DVDProfiler.EnhancedTitles
{
    internal sealed class TitleManager
    {
        private const String TextNotSet = null;

        private readonly IDVDInfo Profile;

        internal TitleManager(IDVDInfo profile)
        {
            Profile = profile;
        }

        #region Invelos Data

        #region Title

        internal void SetTitle(String title)
        {
            Profile.SetTitle(title);
        }

        internal String GetTitle()
        {
            String title;

            title = Profile.GetTitle();
            return (title);
        }

        #endregion

        #region SortTitle

        internal void SetSortTitle(String st)
        {
            Profile.SetSortTitle(st);
        }

        internal String GetSortTitle()
        {
            String st;

            st = Profile.GetSortTitle();
            return (st);
        }

        #endregion

        #region OriginalTitle

        internal void SetOriginalTitle(String ot)
        {
            Profile.SetOriginalTitle(ot);
        }

        internal String GetOriginalTitle()
        {
            String ot;

            ot = Profile.GetOriginalTitle();
            return (ot);
        }

        #endregion

        #endregion

        #region Plugin Data

        #region InternationalEnglishTitle
        internal String GetInternationalEnglishTitleWithFallback()
        {
            return (GetTextWithFallback(GetInternationalEnglishTitle));
        }

        internal Boolean GetInternationalEnglishTitle(out String iet)
        {
            return (GetText(Constants.InternationalEnglishTitle, out iet));
        }

        internal void SetInternationalEnglishTitle(String iet)
        {
            SetText(Constants.InternationalEnglishTitle, iet);
        }
        #endregion

        #region AlternateOriginalTitle
        internal String GetAlternateOriginalTitleWithFallback()
        {
            return (GetTextWithFallback(GetAlternateOriginalTitle));
        }

        internal Boolean GetAlternateOriginalTitle(out String aot)
        {
            return (GetText(Constants.AlternateOriginalTitle, out aot));
        }

        internal void SetAlternateOriginalTitle(String aot)
        {
            SetText(Constants.AlternateOriginalTitle, aot);
        }
        #endregion

        #region NonLatinLettersTitle
        internal String GetNonLatinLettersTitleWithFallback()
        {
            return (GetTextWithFallback(GetNonLatinLettersTitle));
        }

        internal Boolean GetNonLatinLettersTitle(out String nlt)
        {
            return (GetText(Constants.NonLatinLettersTitle, out nlt));
        }

        internal void SetNonLatinLettersTitle(String nlt)
        {
            SetText(Constants.NonLatinLettersTitle, nlt);
        }
        #endregion

        #region AdditionalTitle1
        internal String GetAdditionalTitle1WithFallback()
        {
            return (GetTextWithFallback(GetAdditionalTitle1));
        }

        internal Boolean GetAdditionalTitle1(out String at1)
        {
            return (GetText(Constants.AdditionalTitle1, out at1));
        }

        internal void SetAdditionalTitle1(String at1)
        {
            SetText(Constants.AdditionalTitle1, at1);
        }
        #endregion

        #region AdditionalTitle2
        internal String GetAdditionalTitle2WithFallback()
        {
            return (GetTextWithFallback(GetAdditionalTitle2));
        }

        internal Boolean GetAdditionalTitle2(out String at2)
        {
            return (GetText(Constants.AdditionalTitle2, out at2));
        }

        internal void SetAdditionalTitle2(String at2)
        {
            SetText(Constants.AdditionalTitle2, at2);
        }
        #endregion

        #endregion

        #region Text
        internal delegate Boolean GetTextDelegate(out String text);

        internal Boolean GetText(String fieldName, out String text)
        {
            String encoded;
            String decoded;

            decoded = TextNotSet;
            encoded = Profile.GetCustomString(Constants.FieldDomain, fieldName, Constants.ReadKey, TextNotSet);
            if (encoded != TextNotSet)
            {
                decoded = Encoding.Unicode.GetString(Convert.FromBase64String(encoded));
            }
            text = decoded;
            return (text != TextNotSet);
        }

        private String GetTextWithFallback(GetTextDelegate getText)
        {
            String text;

            if (getText(out text) == false)
            {
                text = String.Empty;
            }
            return (text);
        }

        private void SetText(String fieldName, String decoded)
        {
            String encoded;

            encoded = Convert.ToBase64String(Encoding.Unicode.GetBytes(decoded));
            Profile.SetCustomString(Constants.FieldDomain, fieldName, InternalConstants.WriteKey, encoded);
        }
        #endregion 
    }
}