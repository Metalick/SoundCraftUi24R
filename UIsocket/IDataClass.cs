//using System.Threading;
using UIsocket.dataclasses;
using UIsocket.enums;

namespace UIsocket
{
    public interface IDataClass
    {
        void ChangeCue(string Show, string Cue);
        void ChangeMuteGroup(uint mgroup);
        void ChangeSnapshot(string Show, string Snapshot);
        void ClearSolo(Channel[][] Channels);
        void MuteChannel(Channeltype channeltype, int channel, bool on);
        void SoloChannel(Channeltype channeltype, int channel, bool on);
    }
}