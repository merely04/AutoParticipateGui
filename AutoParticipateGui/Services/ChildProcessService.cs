using System.Diagnostics;
using AutoParticipateGui.Controllers;

namespace AutoParticipateGui.Services
{
    public class ChildProcessService
    {
        public static Process CreatePythonProcess(string workingDirectory, string arguments)
        {
            return new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = SettingsController.PythonExecutablePath,
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
        
        public static Process CreateCmdProcess(string workingDirectory)
        {
            return new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd",
                    WorkingDirectory = workingDirectory,
                    // Arguments = arguments,
                    UseShellExecute = false,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                },
                EnableRaisingEvents = true
            };
        }
    }
}