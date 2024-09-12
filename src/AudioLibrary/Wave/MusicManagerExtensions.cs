using OpenTK.Audio.OpenAL;

namespace AudioLibrary.Wave;

public static class MusicManagerExtensions
{
    public static ALFormat ToAlFormat(this SoundFormat soundFormat)
    {
        return (ALFormat)soundFormat;
    }

    public static IEnumerable<short> To16BpsSamples(this IEnumerable<byte> data)
    {
        var samples = new List<short>();
        var byteArray = data.ToArray();
        for (var byteIndex = 0; byteIndex < byteArray.Length; byteIndex += 2)
        {
            var sample = BitConverter.ToInt16(byteArray, byteIndex);
            samples.Add(sample);
        }

        return samples;
    }

    public static IEnumerable<float> To32BpsSamples(this IEnumerable<byte> data)
    {
        var samples = new List<float>();
        var byteArray = data.ToArray();
        for (var byteIndex = 0; byteIndex < byteArray.Length; byteIndex += 4)
        {
            var sample = BitConverter.ToSingle(byteArray, byteIndex);
            samples.Add(sample);
        }

        return samples;
    }
}