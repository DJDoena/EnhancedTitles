using DoenaSoft.DVDProfiler.DVDProfilerHelper;
using DoenaSoft.DVDProfiler.EnhancedTitles.Resources;
using Invelos.DVDProfilerPlugin;
using Microsoft.WindowsAPICodePack.Taskbar;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DoenaSoft.DVDProfiler.EnhancedTitles
{
    internal sealed class XmlManager
    {
        private readonly Plugin Plugin;

        public XmlManager(Plugin plugin)
        {
            Plugin = plugin;
        }

        #region Export

        internal void Export(Boolean exportAll)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.AddExtension = true;
                sfd.DefaultExt = ".xml";
                sfd.Filter = "XML files|*.xml";
                sfd.OverwritePrompt = true;
                sfd.RestoreDirectory = true;
                sfd.Title = Texts.SaveXmlFile;
                sfd.FileName = "EnhancedTitles.xml";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    Object[] ids;
                    EnhancedTitlesList ets;

                    Cursor.Current = Cursors.WaitCursor;
                    using (ProgressWindow progressWindow = new ProgressWindow())
                    {
                        #region Progress

                        Int32 onePercent;

                        progressWindow.ProgressBar.Minimum = 0;
                        progressWindow.ProgressBar.Step = 1;
                        progressWindow.CanClose = false;

                        #endregion

                        ids = GetProfileIds(exportAll);

                        ets = new EnhancedTitlesList();
                        ets.Profiles = new Profile[ids.Length];

                        #region Progress

                        progressWindow.ProgressBar.Maximum = ids.Length;
                        progressWindow.Show();
                        if (TaskbarManager.IsPlatformSupported)
                        {
                            TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Normal);
                            TaskbarManager.Instance.SetProgressValue(0, progressWindow.ProgressBar.Maximum);
                        }
                        onePercent = progressWindow.ProgressBar.Maximum / 100;
                        if ((progressWindow.ProgressBar.Maximum % 100) != 0)
                        {
                            onePercent++;
                        }

                        #endregion

                        for (Int32 i = 0; i < ids.Length; i++)
                        {
                            String id;
                            IDVDInfo profile;

                            id = ids[i].ToString();
                            Plugin.Api.DVDByProfileID(out profile, id, PluginConstants.DATASEC_AllSections, 0);
                            ets.Profiles[i] = GetXmlProfile(profile);

                            #region Progress

                            progressWindow.ProgressBar.PerformStep();
                            if (TaskbarManager.IsPlatformSupported)
                            {
                                TaskbarManager.Instance.SetProgressValue(progressWindow.ProgressBar.Value, progressWindow.ProgressBar.Maximum);
                            }
                            if ((progressWindow.ProgressBar.Value % onePercent) == 0)
                            {
                                Application.DoEvents();
                            }

                            #endregion
                        }

                        try
                        {
                            ets.Serialize(sfd.FileName);

                            #region Progress

                            Application.DoEvents();
                            if (TaskbarManager.IsPlatformSupported)
                            {
                                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);
                            }
                            progressWindow.CanClose = true;
                            progressWindow.Close();

                            #endregion

                            MessageBox.Show(String.Format(MessageBoxTexts.DoneWithNumber, ids.Length, MessageBoxTexts.Exported)
                                , MessageBoxTexts.InformationHeader, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(String.Format(MessageBoxTexts.FileCantBeWritten, sfd.FileName, ex.Message)
                                , MessageBoxTexts.ErrorHeader, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            #region Progress

                            if (progressWindow.Visible)
                            {
                                if (TaskbarManager.IsPlatformSupported)
                                {
                                    TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);
                                }
                                progressWindow.CanClose = true;
                                progressWindow.Close();
                            }

                            #endregion
                        }
                    }

                    Cursor.Current = Cursors.Default;
                }
            }
        }

        private Profile GetXmlProfile(IDVDInfo profile)
        {
            TitleManager titleManager;
            Profile xmlProfile;
            EnhancedTitles et;
            DefaultValues dv;

            dv = Plugin.Settings.DefaultValues;

            titleManager = new TitleManager(profile);

            xmlProfile = new Profile();
            xmlProfile.Id = profile.GetProfileID();
            xmlProfile.Title = profile.GetTitle();
          
            et = new EnhancedTitles();
            xmlProfile.EnhancedTitles = et;

            et.InternationalEnglishTitle = GetText(titleManager.GetInternationalEnglishTitle, dv.InternationalEnglishTitleLabel);
            et.AlternateOriginalTitle = GetText(titleManager.GetAlternateOriginalTitle, dv.AlternateOriginalTitleLabel);
            et.NonLatinLettersTitle = GetText(titleManager.GetNonLatinLettersTitle, dv.NonLatinLettersTitleLabel);
            et.AdditionalTitle1 = GetText(titleManager.GetAdditionalTitle1, dv.AdditionalTitle1Label);
            et.AdditionalTitle2 = GetText(titleManager.GetAdditionalTitle2, dv.AdditionalTitle2Label);

            et.InvelosData = new InvelosData();
            et.InvelosData.Title = titleManager.GetTitle();
            et.InvelosData.SortTitle = titleManager.GetSortTitle();
            et.InvelosData.OriginalTitle = titleManager.GetOriginalTitle();

            return (xmlProfile);
        }

        private Text GetText(TitleManager.GetTextDelegate getText
            , String displayName)
        {
            Text text;
            String title;

            if (getText(out title))
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

        #endregion

        #region Import

        internal void Import(Boolean importAll)
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
                    EnhancedTitlesList ets;

                    Cursor.Current = Cursors.WaitCursor;

                    ets = null;

                    try
                    {
                        ets = EnhancedTitlesList.Deserialize(ofd.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(String.Format(MessageBoxTexts.FileCantBeRead, ofd.FileName, ex.Message)
                           , MessageBoxTexts.ErrorHeader, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    if (ets != null)
                    {
                        Int32 count;

                        count = 0;
                        if ((ets.Profiles != null) && (ets.Profiles.Length > 0))
                        {
                            using (ProgressWindow progressWindow = new ProgressWindow())
                            {
                                Dictionary<String, Boolean> profileIds;
                                Object[] ids;

                                #region Progress

                                Int32 onePercent;

                                progressWindow.ProgressBar.Minimum = 0;
                                progressWindow.ProgressBar.Step = 1;
                                progressWindow.CanClose = false;

                                #endregion

                                ids = GetProfileIds(importAll);

                                profileIds = new Dictionary<String, Boolean>(ids.Length);

                                #region Progress

                                progressWindow.ProgressBar.Maximum = ids.Length;
                                progressWindow.Show();
                                if (TaskbarManager.IsPlatformSupported)
                                {
                                    TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Normal);
                                    TaskbarManager.Instance.SetProgressValue(0, progressWindow.ProgressBar.Maximum);
                                }
                                onePercent = progressWindow.ProgressBar.Maximum / 100;
                                if ((progressWindow.ProgressBar.Maximum % 100) != 0)
                                {
                                    onePercent++;
                                }

                                #endregion

                                for (Int32 i = 0; i < ids.Length; i++)
                                {
                                    profileIds.Add(ids[i].ToString(), true);
                                }

                                foreach (Profile xmlProfile in ets.Profiles)
                                {
                                    if ((xmlProfile != null) && (xmlProfile.EnhancedTitles != null) && (profileIds.ContainsKey(xmlProfile.Id)))
                                    {
                                        IDVDInfo profile;
                                        EnhancedTitles et;
                                        TitleManager titleManager;

                                        profile = Plugin.Api.CreateDVD();
                                        profile.SetProfileID(xmlProfile.Id);

                                        titleManager = new TitleManager(profile);

                                        et = xmlProfile.EnhancedTitles;

                                        SetTitle(et.InternationalEnglishTitle, titleManager.SetInternationalEnglishTitle);
                                        SetTitle(et.AlternateOriginalTitle, titleManager.SetAlternateOriginalTitle);
                                        SetTitle(et.NonLatinLettersTitle, titleManager.SetNonLatinLettersTitle);
                                        SetTitle(et.AdditionalTitle1, titleManager.SetAdditionalTitle1);
                                        SetTitle(et.AdditionalTitle2, titleManager.SetAdditionalTitle2);

                                        count++;
                                    }

                                    #region Progress

                                    progressWindow.ProgressBar.PerformStep();
                                    if (TaskbarManager.IsPlatformSupported)
                                    {
                                        TaskbarManager.Instance.SetProgressValue(progressWindow.ProgressBar.Value, progressWindow.ProgressBar.Maximum);
                                    }
                                    if ((progressWindow.ProgressBar.Value % onePercent) == 0)
                                    {
                                        Application.DoEvents();
                                    }

                                    #endregion
                                }

                                #region Progress

                                Application.DoEvents();
                                if (TaskbarManager.IsPlatformSupported)
                                {
                                    TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);
                                }
                                progressWindow.CanClose = true;
                                progressWindow.Close();

                                #endregion
                            }
                        }

                        MessageBox.Show(String.Format(MessageBoxTexts.DoneWithNumber, count, MessageBoxTexts.Imported)
                                , MessageBoxTexts.InformationHeader, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    Cursor.Current = Cursors.Default;
                }
            }
        }

        private void SetTitle(Text text
            , Action<String> setText)
        {
            if ((text != null) && (text.Value != null))
            {
                setText(text.Value);
            }
            else
            {
                setText(String.Empty);
            }
        }

        #endregion

        private Object[] GetProfileIds(Boolean allIds)
        {
            Object[] ids;

            if (allIds)
            {
                ids = (Object[])(Plugin.Api.GetAllProfileIDs());
            }
            else
            {
                ids = (Object[])(Plugin.Api.GetFlaggedProfileIDs());
            }

            return (ids);
        }
    }
}