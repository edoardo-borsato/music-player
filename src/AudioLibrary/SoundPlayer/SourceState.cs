namespace AudioLibrary.SoundPlayer;

public enum SourceState
{
    /// <summary>Default State when loaded, can be manually set with AL.SourceRewind().</summary>
    Initial = 4113, // 0x00001011
    /// <summary>The source is currently playing.</summary>
    Playing = 4114, // 0x00001012
    /// <summary>The source has paused playback.</summary>
    Paused = 4115, // 0x00001013
    /// <summary>The source is not playing.</summary>
    Stopped = 4116, // 0x00001014
}
