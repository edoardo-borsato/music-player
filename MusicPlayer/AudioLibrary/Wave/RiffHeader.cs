namespace AudioLibrary.Wave;

public record RiffHeader
{
    public required string Signature { get; init; }
    public required int ChunkSize { get; init; }
    public required string Format { get; init; }
}