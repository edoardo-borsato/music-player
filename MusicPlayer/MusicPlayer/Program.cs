using AudioLibrary.Device;
using AudioLibrary.Errors;
using AudioLibrary.SoundPlayer;
using AudioLibrary.Wave;

namespace MusicPlayer;

internal class Program
{
    static void Main(string[] args)
    {
        const string wavFilePath = @"D:\sample.wav";
        var fileInfo = new FileInfo(wavFilePath);

        var oadFinder = new OutputAudioDevicesFinder();
        var cadFinder = new CaptureDevicesFinder();
        var aadFinder = new AllAudioDevicesFinder();
        Console.WriteLine("Output audio devices");
        var oad = oadFinder.GetAll();
        foreach (var device in oad)
        {
            Console.WriteLine(device);
        }

        Console.WriteLine("Capture audio devices");
        var cad = cadFinder.GetAll();
        foreach (var device in cad)
        {
            Console.WriteLine(device);
        }

        Console.WriteLine("All audio devices");
        var aad = aadFinder.GetAll();
        foreach (var device in aad)
        {
            Console.WriteLine(device);
        }

        var waveAudio = new WaveFile(fileInfo.FullName);

        //var audioDevice = new AudioDevice(null);   // equivalent to DefaultAudioDevice
        //var audioDevice = new AudioDevice("");     // equivalent to DefaultAudioDevice
        var alcAudioErrorsManagerFactory = new AlcAudioErrorsManagerFactory();
        var audioDevice = new DefaultAudioDevice(alcAudioErrorsManagerFactory);
        var soundPlayer = new SoundPlayer(audioDevice, new AlAudioErrorsManager());
        soundPlayer.Open(waveAudio);
        Console.WriteLine($"Playing audio file on {audioDevice.DeviceName}: File name: {fileInfo.Name}. File duration : {waveAudio.Duration.Minutes}:{waveAudio.Duration.Seconds}.{waveAudio.Duration.Milliseconds}");
        soundPlayer.Play();

        Task.Run(() =>
        {
            while (soundPlayer.GetSourceState() == SourceState.Playing)
            {
                Console.WriteLine("Playing");
            }
        });

        Thread.Sleep(5000);
        Console.WriteLine("Position: " + soundPlayer.Position);
        soundPlayer.Pause();
        Thread.Sleep(5000);
        Console.WriteLine("Position: " + soundPlayer.Position);
        soundPlayer.Play();
        Console.WriteLine("Position: " + soundPlayer.Position);
        Thread.Sleep(5000);
        soundPlayer.Stop();
        Console.WriteLine("Position: " + soundPlayer.Position);
        soundPlayer.Close();
        Console.WriteLine("Position: " + soundPlayer.Position);
        soundPlayer.ShutDown();
    }
}