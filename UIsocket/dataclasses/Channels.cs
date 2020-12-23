using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;

namespace UIsocket.dataclasses
{
    public class Channel
    {
        public byte ChannelNumbner { get; private set; }
        public ChannelSettings Settings { get; set; }
        public enums.Channeltype Channeltype { get; private set; }
        public Channel(byte channelNumber, enums.Channeltype channeltype)
        {
            Settings = new ChannelSettings(){
                Mute = false,
                Solo = false,
                Stereo = false
            };
            ChannelNumbner = channelNumber;
            Channeltype = channeltype;
        }


        public class ChannelSettings
        {
            public bool Mute { get; set; }
            public bool Solo { get; set; }
            public bool Stereo { get; set; }
        }

    }
}
