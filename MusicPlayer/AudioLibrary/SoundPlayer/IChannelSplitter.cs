namespace AudioLibrary.SoundPlayer;

public interface IChannelSplitter
{
    IEnumerable<IEnumerable<byte>> Split(byte[] data, int channels);
}