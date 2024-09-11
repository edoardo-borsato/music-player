namespace AudioLibrary.Wave;

public record ListSubChunk : DataChunk
{
    public required string TypeId { get; init; }
}