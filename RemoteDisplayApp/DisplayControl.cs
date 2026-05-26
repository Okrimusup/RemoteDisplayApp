using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Vortice.DXCore;
using Vortice.Win32;
using static RemoteDisplayApp.Logic;



namespace RemoteDisplayApp
{
    public static class DisplayControl
    {
        private const string HardwareId = @"Root\MttVDD";


        public static void InstallDriver(string infPath, string devconPath)
        {
            if (!isDriverInstalled)
            {
                try
                {
                    string arguments = $"install \"{infPath}\" {HardwareId}";
                    ProcessStartInfo startInfo = startDevCon(arguments, devconPath);
                    using (Process process = Process.Start(startInfo))
                    {

                        process.WaitForExit();

                        if (process.ExitCode == 0)
                        {
                            Log("Драйвер монитора успешно установлен!");
                            isDriverInstalled = true;
                        }
                        else
                        {
                            Log($"Ошибка при установке. Код возврата: {process.ExitCode}", LogLevel.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log($"Ошибка при установке драйвера: {ex}", LogLevel.Error);
                }
            }
            else
            {
                Log("Драйвер монитора уже установлен", LogLevel.Warning);
            }
        }

        public static int UninstalDevice(string devconPath)
        {
            try
            {
                int exitCode;
                string arguments = $"remove {HardwareId}";
                ProcessStartInfo startInfo = startDevCon(arguments, devconPath);
                using (Process process = Process.Start(startInfo))
                {

                    process.WaitForExit();

                    if (process.ExitCode == 0)
                    {
                        Log("Драйвер монитора успешно удален!");
                        isDriverInstalled = false;
                        return process.ExitCode;
                    }
                    else
                    {
                        Log($"Ошибка при удалении. Код возврата: {process.ExitCode}", LogLevel.Error);
                        exitCode = process.ExitCode;
                        return process.ExitCode;
                    }
                }
            }
            catch (Exception ex)
            {
                Log($"Ошибка при удалении драйвера: {ex}", LogLevel.Error);
                return -1;
            }
        }
        public static int UninstallDriver(string devconPath, string originalName)
        {
            try
            {
                int deviceCode = UninstalDevice(devconPath);
                string oemInf = FindDriverOem(originalName);
                if (oemInf != null)
                {
                    string arguments = $"/delete-driver {oemInf} /uninstall /force";
                    int exitCode = StartAdminProcess(arguments, "pnputil.exe");

                    if (exitCode == 0)
                    {
                        Log("Драйвер монитора успешно удален из системы!");
                        return deviceCode;
                    }
                    else
                    {
                        Log($"Ошибка при удалении драйвера из системы. Код возврата: {exitCode}", LogLevel.Error);
                        return deviceCode;
                    }
                }
                else
                {
                    Log("Не удалось найти OEM INF для драйвера монитора.", LogLevel.Warning);
                    return deviceCode;
                }
            }
            catch (Exception ex)
            {
                Log($"Ошибка при удалении драйвера: {ex}", LogLevel.Error);
                return -1;
            }
        }
        private static int StartAdminProcess(string arguments, string File)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = File,
                    Arguments = arguments,
                    UseShellExecute = true,
                    Verb = "runas",
                    CreateNoWindow = true
                };
                using Process process = Process.Start(startInfo);
                process.WaitForExit();
                return process.ExitCode;
            }
            catch (Exception ex)
            {
                Log($"Ошибка при инициализации devcon: {ex}", LogLevel.Error);
                return -1;
            }
        }
        private static ProcessStartInfo startDevCon(string arguments, string devconPath)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = devconPath,
                    Arguments = arguments,
                    UseShellExecute = true,
                    Verb = "runas",
                    CreateNoWindow = true

                };
                return startInfo;
            }
            catch (Exception ex)
            {
                Log($"Ошибка при инициализации devcon: {ex}", LogLevel.Error);
                return null;
            }
        }
        private static ProcessStartInfo startPnpUtil(string arguments)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "pnputil.exe",
                    Arguments = arguments,
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                };
                return startInfo;
            }
            catch (Exception ex)
            {
                Log($"Ошибка при инициализации pnputil: {ex}", LogLevel.Error);
                return null;
            }
        }
        public static string FindDriverOem(string originalName)
        {
            try
            {
                ProcessStartInfo psi = startPnpUtil("/enum-drivers");
                using Process process = Process.Start(psi);

                string output = process.StandardOutput.ReadToEnd();

                process.WaitForExit();

                // Разбиваем на блоки пустыми строками
                string[] blocks = output.Split(
                    new[] { Environment.NewLine + Environment.NewLine },
                    StringSplitOptions.RemoveEmptyEntries);

                foreach (string block in blocks)
                {
                    if (block.Contains(originalName,
                            StringComparison.OrdinalIgnoreCase))
                    {
                        // Ищем oemXX.inf
                        Match match = Regex.Match(
                            block,
                            @"oem\d+\.inf",
                            RegexOptions.IgnoreCase);

                        if (match.Success)
                        {
                            Log(match.Value);
                            return match.Value;
                        }
                    }
                }

                return null;

                return null;
            }
            catch (Exception ex)
            {
                Log($"Ошибка при поиске драйвера: {ex}", LogLevel.Error);
                return null;
            }
        }

    }

}

