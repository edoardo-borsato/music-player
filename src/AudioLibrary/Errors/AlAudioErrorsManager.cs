using OpenTK.Audio.OpenAL;

namespace AudioLibrary.Errors;

public class AlAudioErrorsManager : IAlAudioErrorsManager
{
    public void ManageLastError()
    {
        var lastError = AL.GetError();
        switch (lastError)
        {
            case ALError.NoError:
                return;
            case ALError.InvalidName:
                throw new ArgumentException($"{AL.GetErrorString(ALError.InvalidName)}: Invalid Name parameter passed to OpenAL call");
            case ALError.IllegalEnum:
                throw new ArgumentException($"{AL.GetErrorString(ALError.IllegalEnum)}: Invalid parameter passed to OpenAL call");
            case ALError.InvalidValue:
                throw new ArgumentException($"{AL.GetErrorString(ALError.InvalidValue)}: Invalid OpenAL enum parameter value");
            case ALError.IllegalCommand:
                throw new InvalidOperationException($"{AL.GetErrorString(ALError.IllegalCommand)}: Illegal OpenAL call");
            case ALError.OutOfMemory:
                throw new OutOfMemoryException($"{AL.GetErrorString(ALError.OutOfMemory)}: No OpenAL memory left");
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}