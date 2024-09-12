namespace AudioLibrary.Device
{
    public interface IAudioDevice
    {
        public string DeviceName { get; }

        public bool IsOpen { get; }

        /// <summary>
        /// Open an audio device
        /// </summary>
        /// <exception cref="IOException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="OutOfMemoryException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void Open();

        /// <summary>
        /// Close an audio device
        /// </summary>
        /// <exception cref="IOException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="OutOfMemoryException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void Close();
    }
}