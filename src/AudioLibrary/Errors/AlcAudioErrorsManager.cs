using OpenTK.Audio.OpenAL;

namespace AudioLibrary.Errors;

public class AlcAudioErrorsManager : IAlcAudioErrorsManager
{
    private readonly ALDevice _device;

    public AlcAudioErrorsManager(ALDevice audioDevice)
    {
        _device = audioDevice;
    }

    public void ManageLastError()
    {
        var lastError = ALC.GetError(_device);
        switch (lastError)
        {
            case AlcError.NoError:
                return;
            case AlcError.InvalidDevice:
                throw new IOException("Invalid Device: No Device. The device handle or specifier names are inaccessible driver/server");
            case AlcError.InvalidContext:
                throw new ArgumentException("Invalid Context: No Device. Invalid context ID. The Context argument does not name a valid context");
            case AlcError.InvalidEnum:
                throw new ArgumentException("Invalid Enum: Bad enum. A token used is not valid, or not applicable");
            case AlcError.InvalidValue:
                throw new ArgumentException("Invalid Value: A value (e.g. Attribute) is not valid, or not applicable");
            case AlcError.OutOfMemory:
                throw new OutOfMemoryException("Out Of Memory: Out of memory. Unable to allocate memory");
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}