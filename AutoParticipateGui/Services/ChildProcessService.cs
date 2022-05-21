using System.Diagnostics;

namespace AutoParticipateGui.Services
{
    public class ChildProcessService
    {
        static ChildProcessService()
        {
            
        }

        public static Process CreateProcess(string workingDirectory, string arguments)
        {
            return new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = RegistryService.PythonExecutablePath,
                    WorkingDirectory = workingDirectory,
                    Arguments = arguments,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                },
                EnableRaisingEvents = true
            };
        }
    }
}