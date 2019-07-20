using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using YoutubeExplode.Models;

namespace YoutubeChannelDumper
{
    public partial class YoutubeBrowser : Form, ILogger, IProgressReporter
    {
        private YouHandler _youHandler;
        private List<YouHandler.FileDownloadingInfo> _videosInProgress = new List<YouHandler.FileDownloadingInfo>();
        private List<int> _progressbarStartList = new List<int>();

        private const int ReportMaxNameLen = 70;
        private const string ProgressBarString = "[____________________]";
        private const char ProgressBarFiller = '#';

        public YoutubeBrowser()
        {
            InitializeComponent();
        }

        private void ProcessChannel_Click(object sender, EventArgs e)
        {
            string link = ChannelLinkBox.Text;
            _youHandler = new YouHandler();
            _youHandler.SetLogger(this);
            _youHandler.SetProgressReporter(this);

            WriteLog("Loading channel contents...");
            try
            {
                _youHandler.LoadChannelContentByName(link);
                var list = _youHandler.GetVideos();

                foreach (Video video in list)
                {
                    VideoListBox.Items.Add(new VideoListItem(video));
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
            }
        }

        private void SelectAllButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < VideoListBox.Items.Count; i++)
            {
                VideoListBox.SelectedIndices.Add(i);
            }
        }

        private void DeselectButton_Click(object sender, EventArgs e)
        {
            VideoListBox.BeginUpdate();

            VideoListBox.SelectedIndices.Clear();

            VideoListBox.EndUpdate();
        }

        private void YoutubeBrowser_Load(object sender, EventArgs e)
        {
            DownloadPathBox.Text = GetDownloadFolderPath();
        }

        static string GetDownloadFolderPath()
        {
            return Registry
                .GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders",
                    "{374DE290-123F-4565-9164-39C4925E467B}", String.Empty).ToString();
        }

        private void DownloadButton_Click(object sender, EventArgs e)
        {
            SendDownloadRequest();
        }

        private void SendDownloadRequest()
        {
            List<Video> selectedVideos = GetSelectedVideos();

            if (selectedVideos.Count == 0)
            {
                MessageBox.Show("Select at least one video!");
                return;
            }


            WriteLog("Initializing downloading...");

            string folder = DownloadPathBox.Text;

            YouHandler.DownloadQuality quality = YouHandler.DownloadQuality.Default;

            if (MuxedBestButton.Checked)
            {
                WriteLog("Quality: " + MuxedBestButton.Text);
                quality = YouHandler.DownloadQuality.MuxedBest;
            }

            if (SeparateBestButt.Checked)
            {
                WriteLog("Quality: " + SeparateBestButt.Text);
                quality = YouHandler.DownloadQuality.SeparateBest;
            }


            string ext = "";
            if (ChangeExtBox.Checked)
                ext = "m4a";

            var result = _youHandler.RequestVideoDownloading(selectedVideos, folder, quality, ext);

            CreateProgressReportHandler(result);
        }

        private void CreateProgressReportHandler(List<YouHandler.FileDownloadingInfo> videos)
        {
            _videosInProgress.AddRange(videos);

            for (int i = 0; i < videos.Count; i++)
            {
                string name;
                if (_videosInProgress[i].Name.Length > ReportMaxNameLen)
                {
                    name = _videosInProgress[i].Name.Substring(0, ReportMaxNameLen);
                }
                else
                {
                    name = _videosInProgress[i].Name.PadLeft(ReportMaxNameLen);
                }
                
                LogBox.Text += name + " ";
                _progressbarStartList.Add(LogBox.Text.Length);
                LogBox.Text += ProgressBarString + "\n";
            }
        }

        private void SetProgressBarValue(int leftBraceIndex, int rightBraceIndex, double value)
        {
            int part = (int) (value * (rightBraceIndex - leftBraceIndex));

            char[] text = LogBox.Text.ToCharArray();

            int count = 0;

            for (int i = leftBraceIndex + 1; i < rightBraceIndex && count < part; i++)
            {
                text[i] = ProgressBarFiller;
                count++;
            }

            LogBox.Text = new string(text);
        }

        private List<Video> GetSelectedVideos()
        {
            List<Video> videos = new List<Video>();

            foreach (VideoListItem item in VideoListBox.SelectedItems)
            {
                videos.Add(item.Video);
            }

            return videos;
        }

        private void WriteLog(string line)
        {
            LogBox.Text += line + "\n";

            ScrollDownLog();
        }

        private void ScrollDownLog()
        {
            LogBox.SelectionStart = LogBox.Text.Length;

            LogBox.ScrollToCaret();
        }

        public void Log(string line)
        {
            //WriteLog(line);
            if (InvokeRequired)
            {
                Invoke(new Action<string>(WriteLog), new object[] {line});
            }

            else WriteLog(line);
        }

        private void ReportProgressInternal(YouHandler.FileDownloadingInfo video, double progress)
        {
            int index = _videosInProgress.IndexOf(video);

            if (index == -1)
            {
                CreateProgressReportHandler(new List<YouHandler.FileDownloadingInfo>() { video});

                index = _videosInProgress.Count - 1;
            }
            

            int barLeftBrace = _progressbarStartList[index];
            SetProgressBarValue(barLeftBrace, barLeftBrace + ProgressBarString.Length - 1, progress);

            if (progress >= 1.0f)
            {
                _videosInProgress.RemoveAt(index);
                _progressbarStartList.RemoveAt(index);

                if (_videosInProgress.Count == 0)
                {
                    MessageBox.Show("All tasks finished!");
                }
            }
        }

        public void ReportProgress(YouHandler.FileDownloadingInfo video, double progress)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<YouHandler.FileDownloadingInfo, double>(ReportProgressInternal), new object[] { video, progress });
            }
            else
            {
                ReportProgressInternal(video, progress);
            }
        }

        private void ClearLogButton_Click(object sender, EventArgs e)
        {
            if (_videosInProgress.Count != 0)
            {
                MessageBox.Show("Please wait until downloading finishes");
                return;
            }

            LogBox.Text = String.Empty;
        }

        private void AltProceedButton_Click(object sender, EventArgs e)
        {
            string link = ChannelLinkBox.Text;
            _youHandler = new YouHandler();
            _youHandler.SetLogger(this);
            _youHandler.SetProgressReporter(this);

            WriteLog("Loading channel contents...");
            try
            {
                _youHandler.LoadChannelContentByLink(link);
                var list = _youHandler.GetVideos();

                foreach (Video video in list)
                {
                    VideoListBox.Items.Add(new VideoListItem(video));
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
            }
        }
    }
}
