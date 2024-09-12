using AudioLibrary.Device;
using AudioLibrary.Errors;
using AudioLibrary.Wave;
using OpenTK.Audio.OpenAL;

namespace AudioLibrary.SoundPlayer;

public class SoundPlayer : ISoundPlayer
{
    #region Private fields

    private int _bufferId;
    private int _sourceId;
    private bool _isWaveFileOpened;
    private bool _isOnlyOneChannelOpened;

    private readonly IAudioErrorsManager _audioErrorsManager;
    private readonly IAudioDevice _audioDevice;

    #endregion

    #region Properties

    public WaveFile? CurrentFile { get; private set; }

    public TimeSpan Position => GetPosition();

    #endregion

    public SoundPlayer(IAudioDevice audioDevice, IAlAudioErrorsManager alAudioErrorsManager)
    {
        _audioErrorsManager = alAudioErrorsManager;
        _audioDevice = audioDevice ?? throw new ArgumentNullException($"{nameof(audioDevice)} cannot be null");
        _audioDevice.Open();
        _isWaveFileOpened = false;
        _isOnlyOneChannelOpened = false;
    }

    public void Open(WaveFile waveFile)
    {
        if (!_isWaveFileOpened)
        {
            GenerateAudioDataBuffer();
            GenerateSource();
            CurrentFile = waveFile;
            FillAudioDataBuffer(waveFile);
            BindSourceToAudioDataBuffer();
            _isWaveFileOpened = true;
            _isOnlyOneChannelOpened = false;
        }
        else
        {
            throw new InvalidOperationException("A wave file is already opened. Use Close before opening a new wave file");
        }
    }

    public void OpenChannel(WaveFile waveFile, IChannelSplitter channelSplitter, int channelToOpen)
    {
        if (!_isWaveFileOpened)
        {
            GenerateAudioDataBuffer();
            GenerateSource();
            CurrentFile = waveFile;
            FillAudioDataBufferFromChannel(waveFile, channelSplitter, channelToOpen);
            BindSourceToAudioDataBuffer();
            _isWaveFileOpened = true;
            _isOnlyOneChannelOpened = true;
        }
        else
        {
            throw new InvalidOperationException("A wave file is already opened. Use Close before opening a new wave file");
        }
    }

    public void Close()
    {
        if (_isWaveFileOpened)
        {
            Stop();
            AL.DeleteSource(_sourceId);
            _audioErrorsManager.ManageLastError();
            AL.DeleteBuffer(_bufferId);
            _audioErrorsManager.ManageLastError();
            CurrentFile = null;
            _isWaveFileOpened = false;
            _isOnlyOneChannelOpened = false;
        }
    }

    public void ShutDown()
    {
        TryClose();
        _audioDevice.Close();
    }

    public SourceState GetSourceState()
    {
        AL.GetSource(_sourceId, ALGetSourcei.SourceState, out var state);
        _audioErrorsManager.ManageLastError();
        return (SourceState)state;
    }

    public void Play()
    {
        if (_isWaveFileOpened)
        {
            AL.SourcePlay(_sourceId);
            _audioErrorsManager.ManageLastError();
        }
    }

    public void Stop()
    {
        if (_isWaveFileOpened)
        {
            AL.SourceStop(_sourceId);
            _audioErrorsManager.ManageLastError();
        }
    }

    public void Pause()
    {
        if (_isWaveFileOpened)
        {
            AL.SourcePause(_sourceId);
            _audioErrorsManager.ManageLastError();
        }
    }

    public void Rewind()
    {
        if (_isWaveFileOpened)
        {
            AL.SourceRewind(_sourceId);
            _audioErrorsManager.ManageLastError();
        }
    }

    #region Utility Methods

    private void GenerateSource()
    {
        _sourceId = AL.GenSource();
        _audioErrorsManager.ManageLastError();
    }

    private void GenerateAudioDataBuffer()
    {
        _bufferId = AL.GenBuffer();
        _audioErrorsManager.ManageLastError();
    }

    private void BindSourceToAudioDataBuffer()
    {
        AL.Source(_sourceId, ALSourcei.Buffer, _bufferId);
        _audioErrorsManager.ManageLastError();
    }

    private void FillAudioDataBuffer(WaveFile waveFile)
    {
        var soundData = waveFile.DataChunk.Data;
        var alFormat = waveFile.SoundFormat.ToAlFormat();
        var frequency = waveFile.FormatChunk.SampleRate;
        var normalizedBufferSize = MakeBufferSizeAMultipleOf4Floor(soundData);
        var lastDataToSkip = soundData.Length - normalizedBufferSize;
        var normalizedSoundData = soundData.SkipLast(lastDataToSkip);
        AL.BufferData(_bufferId, alFormat, normalizedSoundData.ToArray(), frequency);
        _audioErrorsManager.ManageLastError();
    }

    private void FillAudioDataBufferFromChannel(WaveFile waveFile, IChannelSplitter channelSplitter, int channelToOpen)
    {
        var channels = waveFile.FormatChunk.Channels;
        if (channelToOpen >= channels)
        {
            throw new InvalidOperationException($"The channel to open must be a 0 based index less than {channels}");
        }

        var soundData = waveFile.DataChunk.Data;
        var channelsData = channelSplitter.Split(soundData, channels).ToList();
        var channelToOpenData = channelsData[channelToOpen].ToArray();
        var soundFormat = waveFile.FormatChunk.BitsPerSample == 8 ? SoundFormat.Mono8 : SoundFormat.Mono16;
        var alFormat = soundFormat.ToAlFormat();
        var frequency = waveFile.FormatChunk.SampleRate;
        var normalizedBufferSize = MakeBufferSizeAMultipleOf4Floor(channelToOpenData);
        var lastDataToSkip = channelToOpenData.Length - normalizedBufferSize;
        var normalizedSoundData = channelToOpenData.SkipLast(lastDataToSkip);
        AL.BufferData(_bufferId, alFormat, normalizedSoundData.ToArray(), frequency);
        _audioErrorsManager.ManageLastError();
    }

    private void TryClose()
    {
        try
        {
            Close();
        }
        catch
        {
            // ignored
        }
    }

    private TimeSpan GetPosition()
    {
        var position = TimeSpan.FromSeconds(-1);
        if (CurrentFile is not null)
        {
            try
            {
                AL.GetSource(_sourceId, ALGetSourcei.ByteOffset, out var byteOffset);
                _audioErrorsManager.ManageLastError();
                var completeDataLength = CurrentFile.DataChunk.Data.Length;
                var channels = CurrentFile.FormatChunk.Channels;
                var bytesLength = _isOnlyOneChannelOpened ? completeDataLength / channels : completeDataLength;
                position = byteOffset * CurrentFile.Duration / bytesLength;
            }
            catch
            {
                position = TimeSpan.FromSeconds(-1);
            }
        }

        return position;
    }

    /// <summary>
    /// Some sound format like Stereo16, in order to work properly must have a buffer size multiple of 4, or a AlcError.InvalidValue will be received
    /// </summary>
    /// <param name="soundData">The buffer containing the sound data</param>
    /// <returns></returns>
    private static int MakeBufferSizeAMultipleOf4Floor(byte[] soundData)
    {
        return soundData.Length - soundData.Length % 4;
    }

    #endregion
}