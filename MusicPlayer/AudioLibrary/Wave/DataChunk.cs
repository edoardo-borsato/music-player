namespace AudioLibrary.Wave;

public abstract record DataChunk
{
    public required string Signature { get; init; }
    public required int ChunkSize { get; init; }
    public required byte[] Data { get; init; }
}