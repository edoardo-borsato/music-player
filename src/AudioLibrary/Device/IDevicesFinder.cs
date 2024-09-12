namespace AudioLibrary.Device
{
    public interface IDevicesFinder
    {
        IEnumerable<string> GetAll();
    }
}