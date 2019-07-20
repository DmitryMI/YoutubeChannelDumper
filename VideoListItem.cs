using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YoutubeExplode.Models;

namespace YoutubeChannelDumper
{
    class VideoListItem : ListViewItem
    {
        public string Name { get; private set; }
        public Video Video { get; private set; }
        public VideoListItem(Video video)
        {
            Name = video.Title;
            Video = video;
            Text = Name;
        }
    }
}
