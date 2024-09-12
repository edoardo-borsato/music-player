namespace AudioLibrary.Wave;

public class WaveFile
{
    public TimeSpan Duration { get; private set; }
    public RiffHeader RiffHeader { get; private set; }
    public FmtSubChunk FmtSubChunk { get; private set; }
    public DataChunk DataChunk { get; private set; }
    public SoundFormat SoundFormat { get; private set; }

    public WaveFile(Stream stream)
    {
        ArgumentNullException.ThrowIfNull(stream);

        ParseStream(stream);
    }

    public WaveFile(string filePath) : this(File.Open(filePath, FileMode.Open))
    {
    }

    public WaveFile(FileInfo file) : this(file.FullName)
    {
    }

    #region Utility Methods

    private void ParseStream(Stream stream)
    {
        using var reader = new BinaryReader(stream);
        var riffHeader = GetRiffHeader(reader);
        var (fmtSubChunk, soundFormat) = GetFmtSubChunk(reader);
        var dataChunk = GetDataChunk(reader);

        RiffHeader = riffHeader;
        FmtSubChunk = fmtSubChunk;
        DataChunk = dataChunk;
        SoundFormat = soundFormat;
        Duration = TimeSpan.FromSeconds(DataChunk.Data.Length / (float)FmtSubChunk.ByteRate);
    }

    private static RiffHeader GetRiffHeader(BinaryReader reader)
    {
        var signature = new string(reader.ReadChars(4));
        if (signature != "RIFF")
        {
            throw new NotSupportedException("Specified stream is not a wave file.");
        }

        var riffChunkSize = reader.ReadInt32();

        var format = new string(reader.ReadChars(4));
        if (format != "WAVE")
        {
            throw new NotSupportedException("Specified stream is not a wave file.");
        }

        return new RiffHeader
        {
            Signature = signature,
            ChunkSize = riffChunkSize,
            Format = format
        };
    }

    private static (FmtSubChunk fmtSubChunk, SoundFormat soundFormat) GetFmtSubChunk(BinaryReader reader)
    {
        var formatSignature = new string(reader.ReadChars(4));
        if (formatSignature != "fmt ")
            throw new NotSupportedException("Specified wave file is not supported.");

        var formatChunkSize = reader.ReadInt32();
        var audioFormat = reader.ReadInt16();
        var channels = reader.ReadInt16();
        var sampleRate = reader.ReadInt32();
        var byteRate = reader.ReadInt32();
        var blockAlign = reader.ReadInt16();
        var bitsPerSample = reader.ReadInt16();

        var soundFormat = GetSoundFormat(channels, bitsPerSample);

        var fmtSubChunk = new FmtSubChunk
        {
            Signature = formatSignature,
            ChunkSize = formatChunkSize,
            AudioFormat = ToWaveFormatEncoding(audioFormat),
            Channels = channels,
            SampleRate = sampleRate,
            ByteRate = byteRate,
            BlockAlign = blockAlign,
            BitsPerSample = bitsPerSample
        };

        return (fmtSubChunk, soundFormat);
    }

    private static DataChunk GetDataChunk(BinaryReader reader)
    {
        var dataSignature = new string(reader.ReadChars(4));
        switch (dataSignature)
        {
            case "data":
            {
                var dataChunkSize = reader.ReadInt32();
                var data = reader.ReadBytes((int)reader.BaseStream.Length);
                return new DataSubChunk
                {
                    Signature = dataSignature,
                    ChunkSize = dataChunkSize,
                    Data = data
                };
            }
            case "LIST":
            {
                var listChunkSize = reader.ReadInt32();
                var listTypeId = new string(reader.ReadChars(4));
                var data = reader.ReadBytes((int)reader.BaseStream.Length);
                return new ListSubChunk
                {
                    Signature = dataSignature,
                    ChunkSize = listChunkSize,
                    TypeId = listTypeId,
                    Data = data
                };
            }
            default:
                throw new NotSupportedException($"Specified wave file is not supported: {dataSignature}");
        }
    }

    private static WaveFormatEncoding ToWaveFormatEncoding(int audioFormat)
    {
        if (Enum.IsDefined(typeof(WaveFormatEncoding), (ushort)audioFormat))
        {
            return (WaveFormatEncoding)audioFormat;
        }

        throw new IndexOutOfRangeException($"{nameof(audioFormat)}: Value {audioFormat} is not a valid value for audio format");
    }

    private static SoundFormat GetSoundFormat(int channels, int bitsPerSample)
    {
        return channels switch
        {
            1 => bitsPerSample == 8 ? SoundFormat.Mono8 : SoundFormat.Mono16,
            2 => bitsPerSample == 8 ? SoundFormat.Stereo8 : SoundFormat.Stereo16,
            _ => throw new NotSupportedException("The specified sound format is not supported.")
        };
    }

    #endregion
}