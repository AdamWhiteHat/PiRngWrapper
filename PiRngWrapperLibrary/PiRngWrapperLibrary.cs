using System;
using System.IO;
using System.Diagnostics;

/* For instructions on how to set up your raspberry pi for
 * true, hardware random number generation, please see:
 * 
 * http://www.csharpprogramming.tips/2016/07/true-hardware-random-number-generator.html
 */

namespace PiRngWrapperLibrary
{
	public static class HwrngDeviceWrapper
	{
		internal static string OutputDirectory = "/tmp/hwrng";

		internal static string GetTempFilename()
		{
			return string.Format("hwrng-output-{0}.bin", DateTime.Now.ToFileTimeUtc());
		}

		public static byte[] RandomBytes(uint sizeKilobytes)
		{
			string rngoutFilename = string.Format("{0}/{1}", OutputDirectory, GetTempFilename());

			HwrngDeviceWrapper.RunShellCommand("dd", string.Format("if=/dev/hwrng of={0} bs=1024 count={1}", rngoutFilename, sizeKilobytes));

			byte[] result = File.ReadAllBytes(rngoutFilename);

			HwrngDeviceWrapper.RunShellCommand("rm", rngoutFilename);

			return result;
		}

		internal static void RunShellCommand(string command, string arguments)
		{
			ProcessStartInfo procInfo = new ProcessStartInfo();
			procInfo.FileName = command;
			procInfo.Arguments = arguments;// string.Format("if=/dev/hwrng of={0} bs=1024 count={1}", rngoutFilename, sizeKilobytes);
			procInfo.UseShellExecute = false;
			procInfo.CreateNoWindow = true;
			procInfo.RedirectStandardOutput = true;
			procInfo.RedirectStandardError = true;

			Process proc = Process.Start(procInfo);
			proc.WaitForExit();
			proc.Dispose();

			proc = null;
			procInfo = null;
		}
	}
}
