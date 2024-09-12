namespace AudioLibrary.SoundPlayer;

public class ChannelSplitter : IChannelSplitter
{
    public IEnumerable<IEnumerable<byte>> Split(byte[] data, int channels)
    {
        var outputs = InitOutputs(channels);

        var count = 0;
        for (var i = 0; i < data.Length; i += 2)
        {
            outputs[count].Add(data[i]);
            outputs[count].Add(data[i + 1]);
            count++;
            if (count < channels)
            {
                continue;
            }
            count = 0;
        }

        return outputs;
    }

    #region Utility Methods

    private static List<List<byte>> InitOutputs(int channels)
    {
        var outputs = new List<List<byte>>();
        for (var i = 0; i < channels; i++)
        {
            outputs.Add([]);
        }

        return outputs;
    }

    #endregion
}