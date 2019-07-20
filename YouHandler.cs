using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using VideoLibrary;
using YoutubeExplode;
using YoutubeExplode.Models;
using YoutubeExplode.Models.MediaStreams;
using Video = VideoLibrary.Video;

namespace YoutubeChannelDumper
{
    public class YouHandler
    {
        private YoutubeExplode.YoutubeClient _client;

        private IReadOnlyList<YoutubeExplode.Models.Video> _videos;

        private ILogger _logger;
        private IProgressReporter _progressReporter;

        private string _channelName;

        public enum DownloadQuality
        {
            Default,
            MuxedBest,      // Best available muxed stream
            SeparateBest    // Best available video stream, is better than Muxed video
        }

        public class FileDownloadingInfo
        {
            public string Name { get; set; }
            public MediaStreamInfo StreamInfo {get; set; }
        }

        public YouHandler()
        {
            _client = new YoutubeClient();
        }

        public void LoadChannelContentByName(string username)
        {
            LoadContents(username);
        }

        public void LoadChannelContentByLink(string channelId)
        {
            LoadContentsById(channelId);
            //LoadContents(username);
        }

        private void LoadContentsById(string channelId)
        {
            //string channelId = YoutubeClient.ParseChannelId(channelLink);
            try
            {
                //Task<string> channelIdTask = _client.GetChannelIdAsync(username);

                //string channelId = channelIdTask.Result;

                Task<Channel> channelTask = _client.GetChannelAsync(channelId);

                Channel channel = channelTask.Result;

                string playlistId = channel.GetChannelVideosPlaylistId();

                Task<Playlist> playlistTask = _client.GetPlaylistAsync(playlistId);

                Playlist playlist = playlistTask.Result;

                _videos = playlist.Videos;
            }
            catch (Exception e)
            {
                if (_logger != null)
                    _logger.Log(e.InnerException?.Message);
                else
                    throw;
            }
        }

        private void LoadContents(string username)
        {
            //string channelId = YoutubeClient.ParseChannelId(channelLink);
            try
            {
                Task<string> channelIdTask = _client.GetChannelIdAsync(username);

                string channelId = channelIdTask.Result;

                Task<Channel> channelTask = _client.GetChannelAsync(channelId);

                Channel channel = channelTask.Result;

                string playlistId = channel.GetChannelVideosPlaylistId();

                Task<Playlist> playlistTask = _client.GetPlaylistAsync(playlistId);

                Playlist playlist = playlistTask.Result;

                _videos = playlist.Videos;
            }
            catch (Exception e)
            {
                if(_logger != null)
                    _logger.Log(e.InnerException?.Message);
                else
                    throw;
            }
        }

        public IReadOnlyList<YoutubeExplode.Models.Video> GetVideos()
        {
            return _videos;
        }

        public List<FileDownloadingInfo> RequestVideoDownloading(IList<YoutubeExplode.Models.Video> videos, string directory, DownloadQuality quality, string audioExt)
        {
            return InitDownloading(videos, directory, quality, audioExt);
        }

        private static string RemoveProhibitedChars(string path)
        {
            
            string regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            path = r.Replace(path, "");

            return path;
        }

        private List<FileDownloadingInfo> InitDownloading(IList<YoutubeExplode.Models.Video> videos, string directory, DownloadQuality quality, string audioExtForced)
        {
            List < FileDownloadingInfo > result = new List<FileDownloadingInfo>();

            //_client.DownloadClosedCaptionTrackAsync();

            //ThreadPool.SetMaxThreads(Environment.ProcessorCount, Environment.ProcessorCount);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            foreach (var vid in videos)
            {
                try
                {
                    Task<MediaStreamInfoSet> streamInfoTask = _client.GetVideoMediaStreamInfosAsync(vid.Id);

                    MediaStreamInfoSet infoSet = streamInfoTask.Result;

                    string vidTitle = RemoveProhibitedChars(vid.Title);

                    switch (quality)
                    {
                        case DownloadQuality.Default:
                        case DownloadQuality.MuxedBest:

                            IReadOnlyList<MuxedStreamInfo> muxedStreams = infoSet.Muxed;
                            MuxedStreamInfo muxedHighestQuality = muxedStreams.WithHighestVideoQuality();
                            string ext = muxedHighestQuality.Container.GetFileExtension();
                            string path = directory + "/" + vidTitle + "." + ext;

                            var file = new FileDownloadingInfo() {Name = vidTitle, StreamInfo = muxedHighestQuality};
                            InitStreamDownloading(file, path, vid);
                            result.Add(file);
                            break;

                        case DownloadQuality.SeparateBest:

                            IReadOnlyList<VideoStreamInfo> videoStreams = infoSet.Video;
                            IReadOnlyList<AudioStreamInfo> audioStreams = infoSet.Audio;
                            AudioStreamInfo highestBitRate = audioStreams.WithHighestBitrate();
                            VideoStreamInfo videoHighestQuality = videoStreams.WithHighestVideoQuality();

                            string extVideo = videoHighestQuality.Container.GetFileExtension();

                            string pathVideo = directory + "/" + vidTitle + "." + extVideo;

                            string extAudio = highestBitRate.Container.GetFileExtension();
                            string pathAudio = directory + "/" + vidTitle + "." + extAudio;

                            if (audioExtForced == String.Empty)
                            {
                                if (pathAudio.Equals(pathVideo))
                                    pathAudio += ".audio." + extAudio;
                            }
                            else
                            {
                                pathAudio += "." + audioExtForced;
                            }

                            FileDownloadingInfo audio = new FileDownloadingInfo()
                                {Name = vidTitle + "(audio)", StreamInfo = highestBitRate};
                            FileDownloadingInfo video = new FileDownloadingInfo()
                            {
                                Name = vidTitle,
                                StreamInfo = videoHighestQuality
                            };

                            if (File.Exists(pathAudio))
                            {
                                _logger.Log("File " + pathAudio + "already exists!. Consider removing or renaming.");
                            }
                            else
                            {
                                InitStreamDownloading(audio, pathAudio, vid);
                                result.Add(audio);
                            }

                            if (File.Exists(pathVideo))
                            {
                                _logger.Log("File " + pathVideo + "already exists!. Consider removing or renaming.");
                            }
                            else
                            {
                                InitStreamDownloading(video, pathVideo, vid);
                                result.Add(video);
                            }

                            break;

                        default:
                            throw new ArgumentOutOfRangeException(nameof(quality), quality, null);
                    }
                }
                catch (Exception ex)
                {
                    _logger.Log(ex.InnerException?.Message);
                }
            }

            return result;
        }

        public void SetLogger(ILogger logger)
        {
            _logger = logger;
        }

        public void SetProgressReporter(IProgressReporter reporter)
        {
            _progressReporter = reporter;
        }

        private void InitStreamDownloading(FileDownloadingInfo streamDownloadingInfoInfo, string path, YoutubeExplode.Models.Video video)
        {
            IProgress<double> progress;
            if(_progressReporter == null)
                progress = new Progress(_logger, video.Title);
            else
            {
                progress = new BaredProgress(_progressReporter, streamDownloadingInfoInfo);
            }

            var downloadingTask = _client.DownloadMediaStreamAsync(streamDownloadingInfoInfo.StreamInfo, path, progress);
        }

        class Progress : IProgress<double>
        {
            private ILogger _logger;
            private string _videoName;

            private int _prevReport = 0;
            public Progress(ILogger logger, string videoName)
            {
                _logger = logger;
                _videoName = videoName;
            }

            public void Report(double value)
            {
                int reportValue = (int) (value * 100);
                if (reportValue == _prevReport)
                    return;

                if (reportValue % 10 == 0)
                {
                    _logger?.Log( _videoName + ": " + reportValue);
                    _prevReport = reportValue;
                }

                if (reportValue >= 100)
                {
                    _logger?.Log(_videoName + " FINISHED");
                }
            }
        }

        class BaredProgress : IProgress<double>
        {
            private IProgressReporter _logger;
            private FileDownloadingInfo _video;

            private int _prevReport = 0;
            public BaredProgress(IProgressReporter logger, FileDownloadingInfo video)
            {
                _logger = logger;
                _video = video;
            }

            public void Report(double value)
            {
                int reportValue = (int)(value * 100);
                if (reportValue == _prevReport)
                    return;

                if (reportValue % 10 == 0)
                {
                    //_logger?.Log(_videoName + ": " + reportValue);
                    _logger?.ReportProgress(_video, value);
                    _prevReport = reportValue;
                }
            }
        }
    }
}
