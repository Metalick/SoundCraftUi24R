//using System.Threading;
using System.Threading.Tasks;
using UIsocket.enums;
using WebSocketSharp;

namespace UIsocket
{
    public class DataClass : IDataClass
    {
        readonly WebSocket ws = ConnectionClass.MixConnection;

        public DataClass()
        {
            ws.OnError += EventOnError;
            ws.OnClose += EventOnClose;
        }

        private void EventOnError(object Sender, ErrorEventArgs e)
        {
            ConnectionClass.CloseMixer();
        }

        private void EventOnClose(object Sender, CloseEventArgs e)
        {
            ws.OnError -= EventOnError;
            ws.OnClose -= EventOnClose;
        }

        public void MuteChannel(Channeltype channeltype, int channel, bool on)
        {
            if (ConnectionClass.isOpen)
            {
                byte onoff = 0;
                if (on) { onoff = 1; }
                ws.SendAsync($"3:::SETD^{ channeltype}.{ channel }.mute^{ onoff }", Completed);
                if (ConnectionClass.Channels[(byte)channeltype][channel].Settings.Stereo && channel % 2 == 0)
                {
                    Task.Delay(10).Wait();
                    ws.SendAsync($"3:::SETD^{ channeltype}.{ channel + 1 }.mute^{ onoff }", Completed);
                }
            }
        }

        public void SoloChannel(Channeltype channeltype, int channel, bool on)
        {
            if (ConnectionClass.isOpen)
            {
                byte onoff = 0;
                if (on) { onoff = 1; }
                ws.SendAsync($"3:::SETD^{ channeltype }.{ channel }.solo^{ onoff }", Completed);
                if (ConnectionClass.Channels[(byte)channeltype][channel].Settings.Stereo && channel % 2 == 0)
                {
                    Task.Delay(10).Wait();
                    ws.SendAsync($"3:::SETD^{ channeltype }.{ channel + 1 }.solo^{ onoff }", Completed);
                }
            }
        }

        public void ChangeSnapshot(string Show, string Snapshot)
        {
            if (ConnectionClass.isOpen)
            {
                ws.SendAsync($"3:::LOADSNAPSHOT^{ Show }^{ Snapshot }", Completed);
            }
        }

        private void Completed(bool obj)
        {
            if (obj)
            {
                //Console.WriteLine("Messeage sent.");
            }
        }

        public void ChangeCue(string Show, string Cue)
        {
            if (ConnectionClass.isOpen)
            {
                ws.SendAsync($"3:::LOADCUE^{ Show }^{ Cue  }", Completed);
            }
        }

        public void ChangeMuteGroup(uint mgroup)
        {
            if (ConnectionClass.isOpen)
            {
                ws.SendAsync($"3:::SETD^mgmask^{ mgroup }", Completed);
                ConnectionClass.MuteGroupVal = mgroup;
            }
        }
        public void ClearSolo(dataclasses.Channel[][] Channels)
        {
            for (byte o = 0; o < Channels.GetLength(0); o++)
            {
                for (byte i = 0; i < Channels[o].GetLength(0); i++)
                {
                    if (Channels[o][i].Settings.Solo)
                    {
                        ws.SendAsync($"3:::SETD^{ (Channeltype)o }.{ i }.solo^{ 0 }", Completed);
                        Channels[o][i].Settings.Solo = false;
                    }
                    if (Channels[o][i].Settings.Stereo && i % 2 == 0)
                    {
                        ws.SendAsync($"3:::SETD^{ (Channeltype)o }.{ i + 1 }.solo^{ 0 }", Completed);
                        Channels[o][i + 1].Settings.Solo = false;
                    }

                }
            }
        }
    }
}
