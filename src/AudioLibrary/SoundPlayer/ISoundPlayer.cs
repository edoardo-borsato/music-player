using AudioLibrary.Wave;

namespace AudioLibrary.SoundPlayer;

public interface ISoundPlayer
{
    WaveFile? CurrentFile { get; }
    TimeSpan Position { get; }

    /// <summary>
    /// Open the given .wav file
    /// </summary>
    /// <param name="waveFile">The .wav file to reproduce</param>
    void Open(WaveFile waveFile);

    /// <summary>
    /// Open only the given channel of the given .wav file
    /// </summary>
    /// <param name="waveFile">The .wav file</param>
    /// <param name="channelSplitter">The channel splitter</param>
    /// <param name="channelToOpen">The channel to reproduce</param>
    void OpenChannel(WaveFile waveFile, IChannelSplitter channelSplitter, int channelToOpen);

    /// <summary>
    /// Close current .wav file
    /// </summary>
    void Close();

    /// <summary>
    /// Release all sound player resources
    /// </summary>
    void ShutDown();

    /// <summary>
    /// Get the current state of the audio source
    /// </summary>
    /// <returns></returns>
    SourceState GetSourceState();

    /// <summary>
    /// Play the audio
    /// </summary>
    void Play();

    /// <summary>
    /// Stop the audio
    /// </summary>
    void Stop();

    /// <summary>
    /// Pause the audio
    /// </summary>
    void Pause();

    /// <summary>
    /// Rewind the audio
    /// </summary>
    void Rewind();
}