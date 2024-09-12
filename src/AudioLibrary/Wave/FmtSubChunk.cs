namespace AudioLibrary.Wave;

public record FmtSubChunk
{
    public required string Signature { get; init; }
    public required int ChunkSize { get; init; }
    public required WaveFormatEncoding AudioFormat { get; init; }
    public required int Channels { get; init; }
    public required int SampleRate { get; init; }
    public required int ByteRate { get; init; }
    public required int BlockAlign { get; init; }
    public required int BitsPerSample { get; init; }
}