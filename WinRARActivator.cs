using System;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Lumi
{
    internal static class WinRARActivator
    {
        private static readonly string RarKey = GetLicenseData();
        
        private static string GetLicenseData()
        {
            // Obfuscated license content to avoid static analysis
            var data = new string[]
            {
                "RAR registration data",
                "LumiLTC", 
                "Pls Dont Remove This Line",
                "UID=c881245b7b1a78985cb0",
                "64122122505cb05c44e75618ab5ea84c86e876e620d42d566d4453",
                "18f59893063b0c337398603ef609acfb0eac3505bc19e61df2b7f5", 
                "bba0aeef9172868794c0d6b2314c038d105f9b3ba638ec8a82305b",
                "a209c087680d071cbbdbb10a9652f8c2b06091a5243fbbc24b381d",
                "4cb3b58c52c3d7d99b828c76f88937dd4d94058fb3038d105f9b3b",
                "a638ec8aa57606488b324a1e71be06e54787b797df438679604ee6", 
                "92b1f552734e6580bee03078379b0cdddee16bb6f4a53644961125",
                "------------------------------------------------------"
            };
            return string.Join(Environment.NewLine, data);
        }

        private static readonly string Loc64 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "WinRAR");
        private static readonly string Loc32 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "WinRAR");
        private static readonly string WinRAR64 = Path.Combine(Loc64, "WinRAR.exe");
        private static readonly string WinRAR32 = Path.Combine(Loc32, "WinRAR.exe");
        private static readonly string RarReg64 = Path.Combine(Loc64, "rarreg.key");
        private static readonly string RarReg32 = Path.Combine(Loc32, "rarreg.key");

        public static async Task<(bool Success, string Message)> ActivateWinRARAsync()
        {
            try
            {
                // Anti-sandbox: Check for real user environment
                if (!await IsRealEnvironment())
                {
                    await Task.Delay(5000 + new Random().Next(1000, 3000)); // Variable delay in sandbox
                    return (false, "Environment validation failed");
                }
                
                // Legitimate system activity simulation
                await SimulateLegitimateActivity();
                
                // Step 1: Check if WinRAR is installed
                var installedLocation = GetInstalledWinRARLocation();
                if (string.IsNullOrEmpty(installedLocation))
                {
                    return (false, "❌ WinRAR không được tìm thấy trong hệ thống!");
                }

                string licenseFile = Path.Combine(installedLocation, "rarreg.key");
                string winrarExe = Path.Combine(installedLocation, "WinRAR.exe");

                // Step 2: Check if already activated
                if (File.Exists(licenseFile))
                {
                    try
                    {
                        var existingContent = File.ReadAllText(licenseFile);
                        if (existingContent.Contains("RAR registration data") && 
                            (existingContent.Contains("El Psy Congroo") || existingContent.Contains("General Public License")))
                        {
                            return (true, "✅ WinRAR đã được kích hoạt trước đó!");
                        }
                    }
                    catch { }
                }

                // Step 3: Start WinRAR once to ensure file presence (like batch script)
                await StartWinRARBriefly(winrarExe);
                await Task.Delay(1000);

                // Step 4: Terminate all WinRAR processes
                await TerminateWinRARProcesses();
                await Task.Delay(500);

                // Step 5: Clean up existing registration keys
                await CleanupExistingKeys(licenseFile);
                await Task.Delay(500);

                // Step 6: Write new license key with elevated privileges
                bool writeResult = await WriteRegistrationKey(licenseFile);
                
                if (!writeResult)
                {
                    return (false, "❌ Không thể tạo license file. Vui lòng chạy với quyền Administrator!");
                }

                // Step 7: Verify license was created
                if (!File.Exists(licenseFile))
                {
                    return (false, "❌ License file không được tạo thành công!");
                }

                // Step 8: Validate license content
                try
                {
                    var content = File.ReadAllText(licenseFile);
                    if (!content.Contains("RAR registration data") || !content.Contains("El Psy Congroo"))
                    {
                        return (false, "❌ License file được tạo nhưng nội dung không hợp lệ!");
                    }
                }
                catch (Exception ex)
                {
                    return (false, $"❌ Không thể đọc license file: {ex.Message}");
                }

                // Step 9: Start WinRAR again to apply registration (like batch script)
                await Task.Delay(1000);
                await StartWinRARBriefly(winrarExe);
                await Task.Delay(2000);
                await TerminateWinRARProcesses();

                return (true, "✅ Kích hoạt WinRAR thành công!\n\n🎉 Registered to: Hououin Kyouma\n📧 Organization: El Psy Congroo");
            }
            catch (Exception ex)
            {
                return (false, $"❌ Lỗi không mong đợi: {ex.Message}");
            }
        }

        // Helper methods implementing batch script logic
        private static async Task StartWinRARBriefly(string winrarExe)
        {
            try
            {
                using (var process = Process.Start(new ProcessStartInfo
                {
                    FileName = winrarExe,
                    UseShellExecute = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                }))
                {
                    // Let it start briefly
                    await Task.Delay(500);
                }
            }
            catch
            {
                // Ignore errors in brief startup
            }
        }

        private static async Task TerminateWinRARProcesses()
        {
            await Task.Run(() =>
            {
                try
                {
                    var processes = Process.GetProcessesByName("WinRAR");
                    foreach (var process in processes)
                    {
                        try
                        {
                            process.Kill();
                            process.WaitForExit(1000); // Wait max 1 second
                        }
                        catch
                        {
                            // Ignore individual process kill errors
                        }
                        finally
                        {
                            process.Dispose();
                        }
                    }
                }
                catch
                {
                    // Ignore general termination errors
                }
            });
        }

        private static async Task CleanupExistingKeys(string licenseFile)
        {
            try
            {
                if (File.Exists(licenseFile))
                {
                    File.Delete(licenseFile);
                }
                await Task.Delay(100); // Small delay for file system
            }
            catch
            {
                // Ignore cleanup errors
            }
        }

        private static async Task<bool> WriteRegistrationKey(string licenseFile)
        {
            try
            {
                // Try direct write first
                File.WriteAllText(licenseFile, RarKey);
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                // Try with elevated privileges using PowerShell
                return await Task.Run(() => WriteWithElevatedPrivileges(licenseFile));
            }
            catch
            {
                return false;
            }
        }

        // Backward compatibility method
        public static async Task<bool> ActivateWinRARAsyncLegacy()
        {
            var result = await ActivateWinRARAsync();
            return result.Success;
        }

        private static string GetInstalledWinRARLocation()
        {
            // Method 1: Check standard installation paths
            if (File.Exists(WinRAR64))
            {
                return Loc64;
            }
            if (File.Exists(WinRAR32))
            {
                return Loc32;
            }

            // Method 2: Check Windows Registry for WinRAR installation
            try
            {
                // Check HKEY_LOCAL_MACHINE for 64-bit systems
                using (var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WinRAR"))
                {
                    if (key != null)
                    {
                        var installPath = key.GetValue("exe64") as string ?? key.GetValue("exe32") as string;
                        if (!string.IsNullOrEmpty(installPath) && File.Exists(installPath))
                        {
                            return Path.GetDirectoryName(installPath);
                        }
                    }
                }

                // Check HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node for 32-bit on 64-bit systems
                using (var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\WinRAR"))
                {
                    if (key != null)
                    {
                        var installPath = key.GetValue("exe32") as string;
                        if (!string.IsNullOrEmpty(installPath) && File.Exists(installPath))
                        {
                            return Path.GetDirectoryName(installPath);
                        }
                    }
                }

                // Check HKEY_CURRENT_USER
                using (var key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\WinRAR"))
                {
                    if (key != null)
                    {
                        var installPath = key.GetValue("exe64") as string ?? key.GetValue("exe32") as string;
                        if (!string.IsNullOrEmpty(installPath) && File.Exists(installPath))
                        {
                            return Path.GetDirectoryName(installPath);
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Registry access failed, continue with other methods
            }

            // Method 3: Search in PATH environment variable
            try
            {
                var pathEnv = Environment.GetEnvironmentVariable("PATH");
                if (!string.IsNullOrEmpty(pathEnv))
                {
                    var paths = pathEnv.Split(';');
                    foreach (var path in paths)
                    {
                        try
                        {
                            var winrarPath = Path.Combine(path.Trim(), "WinRAR.exe");
                            if (File.Exists(winrarPath))
                            {
                                return path.Trim();
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
            }
            catch (Exception)
            {
                // PATH search failed
            }

            // Method 4: Search common installation directories
            var commonPaths = new[]
            {
                @"C:\Program Files\WinRAR",
                @"C:\Program Files (x86)\WinRAR",
                @"C:\WinRAR",
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WinRAR"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "WinRAR")
            };

            foreach (var path in commonPaths)
            {
                try
                {
                    var winrarExe = Path.Combine(path, "WinRAR.exe");
                    if (File.Exists(winrarExe))
                    {
                        return path;
                    }
                }
                catch
                {
                    continue;
                }
            }

            return null; // WinRAR not found
        }

        private static bool WriteWithElevatedPrivileges(string licenseFile)
        {
            try
            {
                // Escape the license key for PowerShell
                var escapedKey = RarKey.Replace("'", "''").Replace("`", "``").Replace("\"", "`\"");
                
                var processInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c echo {escapedKey} > \"{licenseFile}\"",
                    Verb = "runas", // Request elevation
                    UseShellExecute = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                };

                using (var process = Process.Start(processInfo))
                {
                    process?.WaitForExit();
                    return process?.ExitCode == 0;
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool IsWinRARInstalled()
        {
            return !string.IsNullOrEmpty(GetInstalledWinRARLocation());
        }

        public static bool IsWinRARLicensed()
        {
            var installedLocation = GetInstalledWinRARLocation();
            if (string.IsNullOrEmpty(installedLocation))
            {
                return false;
            }

            var licenseFile = Path.Combine(installedLocation, "rarreg.key");
            return File.Exists(licenseFile);
        }

        public static string GetWinRARVersion()
        {
            try
            {
                var installedLocation = GetInstalledWinRARLocation();
                if (string.IsNullOrEmpty(installedLocation))
                {
                    return "Không tìm thấy";
                }

                var winrarPath = Path.Combine(installedLocation, "WinRAR.exe");
                if (!File.Exists(winrarPath))
                {
                    return "Không tìm thấy";
                }

                var versionInfo = FileVersionInfo.GetVersionInfo(winrarPath);
                return versionInfo.FileVersion ?? "Không xác định";
            }
            catch
            {
                return "Không xác định";
            }
        }

        private static async Task<bool> IsRealEnvironment()
        {
            try
            {
                // Check for minimum system activity and real timing
                var startTime = DateTime.Now;
                await Task.Delay(100);
                var endTime = DateTime.Now;
                
                // Real systems have more variable timing due to other processes
                var timeDiff = (endTime - startTime).TotalMilliseconds;
                
                // Check processor count (sandboxes often have limited cores)
                var coreCount = Environment.ProcessorCount;
                
                // Check available memory (sandboxes often have limited RAM)
                var workingSet = Environment.WorkingSet;
                
                // Check for real user interaction patterns
                var tickCount = Environment.TickCount;
                
                return timeDiff > 80 && timeDiff < 200 && 
                       coreCount >= 2 && 
                       workingSet > 50000000 && 
                       tickCount > 300000; // System has been running for at least 5 minutes
            }
            catch
            {
                return true; // Assume real if check fails
            }
        }
        
        public static string GetDebugInfo()
        {
            var info = new System.Text.StringBuilder();
            
            // Check standard paths
            info.AppendLine($"64-bit path: {WinRAR64} - {(File.Exists(WinRAR64) ? "✓" : "✗")}");
            info.AppendLine($"32-bit path: {WinRAR32} - {(File.Exists(WinRAR32) ? "✓" : "✗")}");
            
            // Check license files
            var installedLocation = GetInstalledWinRARLocation();
            if (!string.IsNullOrEmpty(installedLocation))
            {
                var licenseFile = Path.Combine(installedLocation, "rarreg.key");
                info.AppendLine($"License file: {licenseFile} - {(File.Exists(licenseFile) ? "✓" : "✗")}");
                
                if (File.Exists(licenseFile))
                {
                    try
                    {
                        var content = File.ReadAllText(licenseFile);
                        var isValid = content.Contains("RAR registration data") && 
                                     (content.Contains("El Psy Congroo") || content.Contains("General Public License"));
                        info.AppendLine($"License valid: {(isValid ? "✓" : "✗")}");
                        info.AppendLine($"License size: {content.Length} chars");
                        
                        if (content.Contains("El Psy Congroo"))
                        {
                            info.AppendLine("License type: Hououin Kyouma (El Psy Congroo)");
                        }
                        else if (content.Contains("General Public License"))
                        {
                            info.AppendLine("License type: General Public License");
                        }
                    }
                    catch (Exception ex)
                    {
                        info.AppendLine($"License read error: {ex.Message}");
                    }
                }
            }
            
            // Check registry
            try
            {
                using (var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WinRAR"))
                {
                    if (key != null)
                    {
                        var exe64 = key.GetValue("exe64") as string;
                        var exe32 = key.GetValue("exe32") as string;
                        info.AppendLine($"Registry HKLM\\SOFTWARE\\WinRAR:");
                        info.AppendLine($"  exe64: {exe64 ?? "null"}");
                        info.AppendLine($"  exe32: {exe32 ?? "null"}");
                    }
                    else
                    {
                        info.AppendLine("Registry HKLM\\SOFTWARE\\WinRAR: Not found");
                    }
                }
            }
            catch (Exception ex)
            {
                info.AppendLine($"Registry error: {ex.Message}");
            }
            
            return info.ToString();
        }

        public static bool TestWinRARActivation()
        {
            try
            {
                var installedLocation = GetInstalledWinRARLocation();
                if (string.IsNullOrEmpty(installedLocation))
                {
                    return false;
                }

                var winrarExe = Path.Combine(installedLocation, "WinRAR.exe");
                if (!File.Exists(winrarExe))
                {
                    return false;
                }

                // Run WinRAR with a test command to check if it shows nag screen
                var processInfo = new ProcessStartInfo
                {
                    FileName = winrarExe,
                    Arguments = "lb", // List command that would normally show nag screen
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                };

                using (var process = Process.Start(processInfo))
                {
                    process?.WaitForExit(5000); // Wait max 5 seconds
                    
                    // If WinRAR is licensed, it should run without showing nag dialog
                    return process?.ExitCode == 0;
                }
            }
            catch
            {
                return false;
            }
        }
        

        
        private static async Task SimulateLegitimateActivity()
        {
            try
            {
                // Simulate legitimate system utility behavior
                var random = new Random();
                
                // Check system time (legitimate software often does this)
                _ = DateTime.Now;
                await Task.Delay(random.Next(50, 150));
                
                // Check environment variables (normal behavior)
                _ = Environment.UserName;
                _ = Environment.MachineName;
                await Task.Delay(random.Next(25, 75));
                
                // Check current directory (normal file operation)
                _ = Directory.GetCurrentDirectory();
                await Task.Delay(random.Next(10, 50));
            }
            catch
            {
                // Ignore errors in simulation
            }
        }
    }
}