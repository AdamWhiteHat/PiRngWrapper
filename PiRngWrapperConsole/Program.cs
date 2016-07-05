using System;
using PiRngWrapperLibrary;

/* For instructions on how to set up your raspberry pi for
 * true, hardware random number generation, please see:
 * 
 * http://www.csharpprogramming.tips/2016/07/true-hardware-random-number-generator.html
 */

namespace PiRngWrapper
{
	internal class Program
	{
		static void Main(string[] args)
		{
			byte[] randomBytes = HwrngDeviceWrapper.RandomBytes(1);

			Console.WriteLine("Length: {0}", randomBytes.Length);
			Console.WriteLine("{0:X}-{1:X}-{2:X}-{3:X}", randomBytes[0], randomBytes[1], randomBytes[2], randomBytes[3]);
			Console.WriteLine();
			Console.WriteLine("Hit any key to continue...");

			Console.ReadLine();
		}
	}
}
