using AudioLibrary.Errors;
using OpenTK.Audio.OpenAL;

namespace AudioLibrary.Device;

public class AudioDevice : IAudioDevice
{
    #region Private fields

    private readonly IAlcAudioErrorsManager _audioErrorsManager;
    private ALDevice _device;
    private ALContext _context;

    #endregion

    #region Properties

    public string DeviceName { get; }
    public bool IsOpen { get; private set; }

    #endregion

    public AudioDevice(string deviceName, IAlcAudioErrorsManagerFactory alcAudioErrorsManagerFactory)
    {
        DeviceName = deviceName;
        _device = ALC.OpenDevice(DeviceName);
        _audioErrorsManager = alcAudioErrorsManagerFactory.Create(_device);
        _audioErrorsManager.ManageLastError();
        IsOpen = false;
    }

    public void Open()
    {
        if (!IsOpen)
        {
            _context = ALC.CreateContext(_device, new ALContextAttributes());
            _audioErrorsManager.ManageLastError();
            ALC.MakeContextCurrent(_context);
            _audioErrorsManager.ManageLastError();
            IsOpen = true;
        }
    }

    public void Close()
    {
        if (IsOpen)
        {
            _device = ALC.GetContextsDevice(_context);
            _audioErrorsManager.ManageLastError();
            ALC.MakeContextCurrent(ALContext.Null);
            _audioErrorsManager.ManageLastError();
            ALC.DestroyContext(_context);
            _audioErrorsManager.ManageLastError();
            ALC.CloseDevice(_device);
            IsOpen = false;
        }
    }
}