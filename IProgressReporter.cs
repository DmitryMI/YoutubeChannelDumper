using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeChannelDumper
{
    public interface IProgressReporter
    {
        void ReportProgress(YouHandler.FileDownloadingInfo video, double progress);
    }
}
